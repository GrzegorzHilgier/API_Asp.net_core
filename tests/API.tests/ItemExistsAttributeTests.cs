using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Requests.Item;
using Domain.Responses.Item;
using Domain.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using REST_API.Filters;
using Xunit;

namespace API.Tests
{
    public class ItemExistsAttributeTests
    {
        [Fact]
        public async Task Should_continue_pipeline_when_id_is_present()
        {
            var id = Guid.NewGuid();
            var itemService = new Mock<IItemService>();
            itemService
                .Setup(x => x.GetItemAsync(It.IsAny<GetItemRequest>()))
                .ReturnsAsync(new ItemResponse {Id = id});
            var filter = new ItemExistsAttribute.ItemExistsFilterImpl(itemService.Object);
            var actionContext = new ActionContext(new DefaultHttpContext(),new Microsoft.AspNetCore.Routing.RouteData(), new ActionDescriptor() );
            var actionExecuteContext = new ActionExecutingContext(
                actionContext,  
                new List<IFilterMetadata>(),
                new Dictionary<string, object>
                {
                    {"id", id}
                }, new object());
            var nextCallback = new Mock<ActionExecutionDelegate>();
            await filter.OnActionExecutionAsync(actionExecuteContext, nextCallback.Object);
            nextCallback.Verify(executionDelegate => executionDelegate(), Times.Once);
        }
    }
}
