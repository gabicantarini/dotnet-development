#pragma checksum "C:\Users\Gabriela\Desktop\Cantarini\Gabriela\2022TI\AlsComprasNew\AlsCompras\Views\Home\VehicleTireDamage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e66819372ee0a331f0dd8543e8c37a23181ad5cc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_VehicleTireDamage), @"mvc.1.0.view", @"/Views/Home/VehicleTireDamage.cshtml")]
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
#line 1 "C:\Users\Gabriela\Desktop\Cantarini\Gabriela\2022TI\AlsComprasNew\AlsCompras\Views\_ViewImports.cshtml"
using AlsCompras;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Gabriela\Desktop\Cantarini\Gabriela\2022TI\AlsComprasNew\AlsCompras\Views\_ViewImports.cshtml"
using AlsCompras.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e66819372ee0a331f0dd8543e8c37a23181ad5cc", @"/Views/Home/VehicleTireDamage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"93cd62a69e740fb06e37f811f78ba10632cbe513", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_VehicleTireDamage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "VehicleDamage", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn advnc_search_form_btn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("background-color: #F1D792"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("formFirstOwner"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("/Home/vehicleFirstOwner"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/scripts/vehicleTireDamage.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Gabriela\Desktop\Cantarini\Gabriela\2022TI\AlsComprasNew\AlsCompras\Views\Home\VehicleTireDamage.cshtml"
  
    ViewData["Title"] = "Desgaste Pneus";


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<section class=\"home-one\">\r\n    <div class=\"container\">\r\n        <div class=\"col-lg-10 m-auto\">\r\n            <div class=\"home_content home1_style\">\r\n                <textarea id=\"fileImgRigth\" hidden>");
#nullable restore
#line 10 "C:\Users\Gabriela\Desktop\Cantarini\Gabriela\2022TI\AlsComprasNew\AlsCompras\Views\Home\VehicleTireDamage.cshtml"
                                              Write(ViewBag.FileImgRigth);

#line default
#line hidden
#nullable disable
            WriteLiteral("</textarea>\r\n                <textarea id=\"fileImgLeft\" hidden>");
#nullable restore
#line 11 "C:\Users\Gabriela\Desktop\Cantarini\Gabriela\2022TI\AlsComprasNew\AlsCompras\Views\Home\VehicleTireDamage.cshtml"
                                             Write(ViewBag.FileImgLeft);

#line default
#line hidden
#nullable disable
            WriteLiteral("</textarea>\r\n                <textarea id=\"fileImgFront\" hidden>");
#nullable restore
#line 12 "C:\Users\Gabriela\Desktop\Cantarini\Gabriela\2022TI\AlsComprasNew\AlsCompras\Views\Home\VehicleTireDamage.cshtml"
                                              Write(ViewBag.FileImgFront);

#line default
#line hidden
#nullable disable
            WriteLiteral("</textarea>\r\n                <textarea id=\"fileImgBack\" hidden>");
#nullable restore
#line 13 "C:\Users\Gabriela\Desktop\Cantarini\Gabriela\2022TI\AlsComprasNew\AlsCompras\Views\Home\VehicleTireDamage.cshtml"
                                             Write(ViewBag.FileImgBack);

#line default
#line hidden
#nullable disable
            WriteLiteral("</textarea>\r\n                <textarea id=\"fileImgAbove\" hidden>");
#nullable restore
#line 14 "C:\Users\Gabriela\Desktop\Cantarini\Gabriela\2022TI\AlsComprasNew\AlsCompras\Views\Home\VehicleTireDamage.cshtml"
                                              Write(ViewBag.FileImgAbove);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</textarea>
                <div class=""home-text text-center mb30 d-none d-sm-block"">
                    <h2 class=""title"" style=""color: #5F6973; font-size: 30px""><span class=""aminated-object1""></span>OFERTA DE AVALIAÇÃO</h2>
                </div>
                <!-- Linha do Tempo -->
                <nav class=""navbar justify-content-center d-block mb-5"">
                    <div");
            BeginWriteAttribute("class", " class=\"", 1005, "\"", 1013, 0);
            EndWriteAttribute();
            WriteLiteral(@" id=""conteudoNavbarSuportado"">
                        <ul class=""nav nav-pills nav-fill"">
                            <li class=""nav-item d-none d-sm-block d-md-block"">
                                1. INFORMAÇÕES SOBRE O VEÍCULO
                            </li>
                            <li class=""nav-item"">
                                2. ESTADO DO VEÍCULO
                            </li>
                            <li class=""nav-item d-none d-sm-block d-md-block"">
                                3. PERFIL
                            </li>
                            <li class=""nav-item d-none d-sm-block d-md-block"">
                                4. A TUA OFERTA
                            </li>
                        </ul>
                    </div>
                </nav>
                <div class=""container col-10"">
                    <div class=""col-12"">
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e66819372ee0a331f0dd8543e8c37a23181ad5cc9826", async() => {
                WriteLiteral(@"
                            <div class=""mb-3"">
                                <h4>Qual é o estado dos pneus dianteiros?</h4>
                            </div>
                            <div class=""row mb-3"">
                                <div class=""col-sm-4 btn-group-toggle"" data-toggle=""buttons"">
                                    <input type=""radio"" class=""btn-check"" name=""vehicleOption1"" id=""option1"" value=""Bom estado"" autocomplete=""off"" onclick=""javascript: $('#divMsgError1').addClass('d-none');"">
                                    <label class=""btn btn-outline-secondary btn-lg w-100 h-100 pt-3"" for=""option1"">Bom estado</label>
                                </div>
                                <div class=""col-sm-4 btn-group-toggle"" data-toggle=""buttons"">
                                    <input type=""radio"" class=""btn-check"" name=""vehicleOption1"" id=""option2"" value=""Desgaste a 50%"" autocomplete=""off"" onclick=""javascript: $('#divMsgError1').addClass('d-none');"">
                 ");
                WriteLiteral(@"                   <label class=""btn btn-outline-secondary btn-lg w-100 h-100 pt-3"" for=""option2"">Desgaste a 50%</label>
                                </div>
                                <div class=""col-sm-4 btn-group-toggle"" data-toggle=""buttons"">
                                    <input type=""radio"" class=""btn-check"" name=""vehicleOption1"" id=""option3"" value=""A substituir"" autocomplete=""off"" onclick=""javascript: $('#divMsgError1').addClass('d-none');"">
                                    <label class=""btn btn-outline-secondary btn-lg w-100 h-100 pt-3"" for=""option3"">A substituir</label>
                                </div>
                            </div>
                            <div id=""divMsgError1"" class=""row mb-3 d-none"">
                                <span class=""text-danger"">
                                    Erro por favor selecione uma das opções!
                                </span>
                            </div>
                            <div class=""mb-3"">
  ");
                WriteLiteral(@"                              <h4>Qual o estado de desgaste dos pneus traseiros?</h4>
                            </div>
                            <div class=""row mb-3"">
                                <div class=""col-sm-4 btn-group-toggle"" data-toggle=""buttons"">
                                    <input type=""radio"" class=""btn-check"" name=""vehicleOption2"" id=""option4"" value=""Bom estado"" autocomplete=""off"" onclick=""javascript: $('#divMsgError2').addClass('d-none');"">
                                    <label class=""btn btn-outline-secondary btn-lg w-100 h-100 pt-3"" for=""option4"">Bom estado</label>
                                </div>
                                <div class=""col-sm-4 btn-group-toggle"" data-toggle=""buttons"">
                                    <input type=""radio"" class=""btn-check"" name=""vehicleOption2"" id=""option5"" value=""Desgaste a 50%"" autocomplete=""off"" onclick=""javascript: $('#divMsgError2').addClass('d-none');"">
                                    <label class=""btn btn-ou");
                WriteLiteral(@"tline-secondary btn-lg w-100 h-100 pt-3"" for=""option5"">Desgaste a 50%</label>
                                </div>
                                <div class=""col-sm-4 btn-group-toggle"" data-toggle=""buttons"">
                                    <input type=""radio"" class=""btn-check"" name=""vehicleOption2"" id=""option6"" value=""A substituir"" autocomplete=""off"" onclick=""javascript: $('#divMsgError2').addClass('d-none');"">
                                    <label class=""btn btn-outline-secondary btn-lg w-100 h-100 pt-3"" for=""option6"">A substituir</label>
                                </div>
                            </div>
                            <div id=""divMsgError2"" class=""row mb-3 d-none"">
                                <span class=""text-danger"">
                                    Erro por favor selecione uma das opções!
                                </span>
                            </div>
                            <div class=""row mt-5 mb-3"">
                                <div");
                WriteLiteral(" class=\"col-sm-12 d-flex justify-content-between\">\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e66819372ee0a331f0dd8543e8c37a23181ad5cc14566", async() => {
                    WriteLiteral("<span class=\"flaticon-left-arrow\"></span>PÁGINA ANTERIOR");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                                    <button type=""button"" onclick=""javascript: verifyForm2();"" class=""btn advnc_search_form_btn""><span class=""flaticon-right-arrow""></span>PÁGINA SEGUINTE</button>
                                </div>
                            </div>
                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e66819372ee0a331f0dd8543e8c37a23181ad5cc17906", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n");
            }
            );
            WriteLiteral("\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
