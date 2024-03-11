﻿using Microsoft.EntityFrameworkCore;
using Skynet.Core.Entities;
using Skynet.Core.Specifications;

namespace Skynet.Infrastructure.Data;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
	public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
	{
		var query = inputQuery;

		if(spec.Criteria is not null)
			query = query.Where(spec.Criteria);

		query = spec.Includes.Aggregate(query, (current, include) =>  current.Include(include));

		return query;
	}
}