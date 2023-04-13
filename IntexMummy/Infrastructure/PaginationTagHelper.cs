using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using IntexMummy.Models.ViewModels;
using Microsoft.AspNetCore.Http;
namespace IntexMummy.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-blah")]
    public class PaginationTagHelper : TagHelper
    {
        //Dymamically create the page links for us

        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }
        public PageInfo PageBlah { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            int startPage = 1;
            int endPage = PageBlah.TotalPages;
            string gender = PageBlah.GenderSearchString;
            long id = PageBlah.idSearchString;
            string preservation = PageBlah.PreservationSearchString;
            string headdirection = PageBlah.HeadDirectionSearchString;
            string age = PageBlah.AgeSearchString;
            if (PageBlah.TotalPages > 5)
            {
                if (PageBlah.CurrentPage <= 3)
                {
                    endPage = 5;
                }
                else if (PageBlah.CurrentPage >= PageBlah.TotalPages - 2)
                {
                    startPage = PageBlah.TotalPages - 4;
                }
                else
                {
                    startPage = PageBlah.CurrentPage - 2;
                    endPage = PageBlah.CurrentPage + 2;
                }
            }
            if (PageBlah.CurrentPage > 1)
            {
                
                
                TagBuilder next = new TagBuilder("a");
                next.Attributes["href"] = uh.Action(PageAction, new { idSearchString = id, GenderSearchString = gender, PreservationSearchString = preservation, HeadDirectionSearchString = headdirection, AgeSearchString = age, pageNum = PageBlah.CurrentPage - 1 });
                next.InnerHtml.Append("<   ");

                final.InnerHtml.AppendHtml(next);
            }
            
            for (int i = startPage; i <= endPage; i++)
            {
                
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes["href"] = uh.Action(PageAction, new {idSearchString = id, GenderSearchString = gender, PreservationSearchString = preservation, HeadDirectionSearchString = headdirection, AgeSearchString = age, pageNum = i }); 
                
                if (PageClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageBlah.CurrentPage
                        ? PageClassSelected : PageClassNormal);
                }

                tb.InnerHtml.Append(i.ToString());

                final.InnerHtml.AppendHtml(tb);
            }
            
            

            if (PageBlah.CurrentPage < PageBlah.TotalPages)
            {
                TagBuilder next = new TagBuilder("a");
                next.Attributes["href"] = uh.Action(PageAction, new { idSearchString = id, GenderSearchString = gender, PreservationSearchString = preservation, HeadDirectionSearchString = headdirection, AgeSearchString = age, pageNum = PageBlah.CurrentPage + 1 });
                next.InnerHtml.Append(">");
                 // add a class to the anchor tag

                final.InnerHtml.AppendHtml(next);
            }

            tho.Content.AppendHtml(final.InnerHtml);
        }

    }
}
