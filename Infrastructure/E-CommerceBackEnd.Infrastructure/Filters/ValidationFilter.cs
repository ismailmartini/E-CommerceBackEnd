using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        //actiona gelen isteklerde devreye giren filter
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.ModelState.IsValid)
            {
              var errors=  context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage))//model state'den bütün erorları alıoruz
                    .ToArray();
                context.Result=new BadRequestObjectResult(errors);
                return;
            }
            await next();
        }
    }
}
