using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Responses.Item;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace REST_API.Conventions
{
    public static class CartAPIConvention
    {
        [ProducesResponseType(typeof(PaginatedEntity<Guid>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Exact)]
        public static void Get(params object[] paramObjects)
        {
        }
    }
}
