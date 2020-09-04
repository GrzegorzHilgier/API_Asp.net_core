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
    public static class ItemAPIConvention
    {

        [ProducesResponseType(typeof(PaginatedEntity<ItemResponse>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Exact)]
        public static void Get(params object[] paramObjects)
        {
        }

        [ProducesResponseType(typeof(ItemResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        public static void GetById(params object[] paramObjects)
        {
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Exact)]
        public static void Delete(params object[] paramObjects)
        {
        }

    }
}
