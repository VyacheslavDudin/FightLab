#pragma checksum "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ee3ff4405a560568705bed7f39adffad5b6516d6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Fight_Details), @"mvc.1.0.view", @"/Views/Fight/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\_ViewImports.cshtml"
using WebApplication2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\_ViewImports.cshtml"
using WebApplication2.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ee3ff4405a560568705bed7f39adffad5b6516d6", @"/Views/Fight/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b36aee4455a440795f240a74431c307640c545e", @"/Views/_ViewImports.cshtml")]
    public class Views_Fight_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebApplication2.Fight>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
  
    ViewData["Title"] = "Детально про бій";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Детальна інформація про бій</h1>\r\n\r\n<div>\r\n    <hr />\r\n    <dl class=\"row\">\r\n         <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 13 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Fighter1));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 16 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
       Write(Html.DisplayFor(model => model.Fighter1.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 19 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Fighter2));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 22 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
       Write(Html.DisplayFor(model => model.Fighter2.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n");
#nullable restore
#line 25 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
             if(Model.Winner=="Fighter1"||Model.Winner=="Fighter2")
            {
                

#line default
#line hidden
#nullable disable
            WriteLiteral("Переможець");
#nullable restore
#line 27 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
                                       
            }
            else{

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
            Write(Html.DisplayNameFor(model => model.Winner));

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
                                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </dt>\r\n        <dd class = \"col-sm-10\">\r\n");
#nullable restore
#line 34 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
                 if(Model.Winner=="Fighter1")
                {
                     

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
                Write(Html.DisplayFor(model => model.Fighter1.Name));

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
                                                                   
                }
                else if(Model.Winner=="Fighter2")
                {
                     

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
                Write(Html.DisplayFor(model => model.Fighter2.Name));

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
                                                                   
                }
                else{

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
                Write(Html.DisplayFor(model => model.Winner));

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
                                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            \r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 47 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.TypeOfWin));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 50 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
       Write(Html.DisplayFor(model => model.TypeOfWin));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 53 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Date));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n");
#nullable restore
#line 57 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
             if(Model.Date==null )
                {
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("-");
#nullable restore
#line 59 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
                                  
                }
                else
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 63 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
               Write(Html.DisplayFor(model => model.Date));

#line default
#line hidden
#nullable disable
#nullable restore
#line 63 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
                                                         
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </dd>  \r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ee3ff4405a560568705bed7f39adffad5b6516d611034", async() => {
                WriteLiteral("Редагувати");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 70 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
                                                   WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    <a class=\"btn btn-outline-secondary ml-2 back\"");
            BeginWriteAttribute("href", " href=", 2131, "", 2149, 1);
#nullable restore
#line 71 "D:\Програмування C++\KNU_Studying\2Course_2Semestr\IStaTP\FinalLab\FightLab\Views\Fight\Details.cshtml"
WriteAttributeValue("", 2137, ViewBag.url, 2137, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Назад</a>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <script>document.querySelector(\".back\").href=sessionStorage.getItem(\'url\');sessionStorage.removeItem(\'url\')</script>\r\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApplication2.Fight> Html { get; private set; }
    }
}
#pragma warning restore 1591
