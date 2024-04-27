using HtmxProject.Application.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmxProject.Application.Categories
{
	public interface ICategoryService
	{
		Task<IReadOnlyCollection<NameValueDto<Guid>>> GetAsNameValueAsync();
	}
}