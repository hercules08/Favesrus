﻿using AutoMapper;
using PagedList;
using System.Linq;

namespace Favesrus.Core.Common
{
    public class PagedListConverter<T1,T2> : ITypeConverter<IPagedList<T1>, IPagedList<T2>>
    {
        public IPagedList<T2> Convert(ResolutionContext context)
        {
            var models = (StaticPagedList<T1>)context.SourceValue;
            var viewModels = models.Select(Mapper.Map<T1, T2>);
            return new StaticPagedList<T2>(viewModels, models.PageNumber, models.PageSize, models.TotalItemCount);
        }
    }
}