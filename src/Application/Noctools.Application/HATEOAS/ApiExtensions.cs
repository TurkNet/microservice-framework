using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noctools.Application;
using Noctools.Domain;
using Noctools.Utils.Extensions;
using Newtonsoft.Json;

namespace Noctools.Application.HATEOAS
{
    public static class ApiExtensions
    {
        public static List<LinkItem> CreateLinksForCollection(this IUrlHelper urlHelper, string methodName,
            Criterion criterion, int totalCount)
        {
            var links = new List<LinkItem>
            {
                new LinkItem(urlHelper.Link(methodName, new
                {
                    pagecount = criterion.PageSize,
                    page = (int) (double) criterion.CurrentPage,
                    orderby = criterion.SortBy
                }), "self", "GET"),
                new LinkItem(urlHelper.Link(methodName, new
                {
                    pagecount = criterion.PageSize,
                    page = 1,
                    orderby = criterion.SortBy
                }), "first", "GET"),
                new LinkItem(urlHelper.Link(methodName, new
                {
                    pagecount = criterion.PageSize,
                    page = criterion.GetTotalPages(totalCount),
                    orderby = criterion.SortBy
                }), "last", "GET")
            };

            // self 
            if (criterion.HasNext(totalCount))
                links.Add(new LinkItem(urlHelper.Link(methodName, new
                {
                    pagecount = criterion.PageSize,
                    page = criterion.CurrentPage + 1,
                    orderby = criterion.SortBy
                }), "next", "GET"));

            if (criterion.HasPrevious())
                links.Add(new LinkItem(urlHelper.Link(methodName, new
                {
                    pagecount = criterion.PageSize,
                    page = criterion.CurrentPage - 1,
                    orderby = criterion.SortBy
                }), "previous", "GET"));

            return links;
        }

        public static dynamic ExpandSingleItem(this IUrlHelper urlHelper, string methodName, IdModelBase item)
        {
            var links = GetLinks(urlHelper, methodName, item.Id);
            var resource = item.ToDynamic() as IDictionary<string, object>;
            resource?.Add("links", links);

            return resource;
        }

        public static void AddPaginateInfo(this HttpResponse httpResponse, Criterion criterion, int numberOfItems)
        {
            var paginationMetadata = new
            {
                totalCount = numberOfItems,
                pageSize = criterion.PageSize,
                currentPage = criterion.CurrentPage,
                totalPages = criterion.GetTotalPages(numberOfItems)
            };

            httpResponse.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));
        }

        private static IEnumerable<LinkItem> GetLinks(IUrlHelper urlHelper, string methodName, Guid id)
        {
            var links = new List<LinkItem>
            {
                new LinkItem(
                    urlHelper.Link(methodName, new {id}),
                    "self",
                    "GET")
            };

            return links;
        }
    }
}