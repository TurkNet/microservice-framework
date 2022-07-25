using System;
using System.Linq;
using Noctools.Domain;

namespace Noctools.Application.HATEOAS
{
    public static class CriterionExtensions
    {
        public static bool HasPrevious(this Criterion criterion)
        {
            return criterion.CurrentPage > 1;
        }

        public static bool HasNext(this Criterion criterion, int totalCount)
        {
            return criterion.CurrentPage < (int)GetTotalPages(criterion, totalCount);
        }

        public static double GetTotalPages(this Criterion criterion, int totalCount)
        {
            return Math.Ceiling(totalCount / (double)criterion.PageSize);
        }

        public static bool HasQuery(this Criterion criterion)
        {
            return !string.IsNullOrEmpty(criterion.SortBy);
        }

        public static bool IsDescending(this Criterion criterion)
        {
            if (!string.IsNullOrEmpty(criterion.SortOrder))
                return criterion.SortOrder.Split(' ').Last().ToLowerInvariant().StartsWith("desc");
            return false;
        }
    }
}
