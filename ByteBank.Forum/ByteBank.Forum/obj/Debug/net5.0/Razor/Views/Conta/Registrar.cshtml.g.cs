#pragma checksum "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Registrar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e5a0a8a2eb306969dfb8b553dd51e57f0cd704ea"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Conta_Registrar), @"mvc.1.0.view", @"/Views/Conta/Registrar.cshtml")]
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
#line 1 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\_ViewImports.cshtml"
using ByteBank.Forum;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\_ViewImports.cshtml"
using ByteBank.Forum.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Registrar.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e5a0a8a2eb306969dfb8b553dd51e57f0cd704ea", @"/Views/Conta/Registrar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f6d0493028697888a33ccbd549fb1fe1480fcf56", @"/Views/_ViewImports.cshtml")]
    public class Views_Conta_Registrar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ByteBank.Forum.ViewModels.ContaRegistrarViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RegistrarPorAutenticacaoExterna", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("provider"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 7 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Registrar.cshtml"
  
    ViewData["Title"] = "Registrar";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Registrar</h1>\r\n\r\n");
#nullable restore
#line 14 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Registrar.cshtml"
 using (Html.BeginForm())
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h4>Complete o cadastro de sua conta!</h4>\r\n    <br />\r\n");
#nullable restore
#line 18 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Registrar.cshtml"

    //O primeiro parametro é a mensagem e o segundo é a classe do bootstrap
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Registrar.cshtml"
Write(Html.ValidationSummary("", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Registrar.cshtml"
                                                               

    //Com esse código o asp.net vai criar todos os campos definidos na model automaticamente
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Registrar.cshtml"
Write(Html.EditorForModel());

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-group\">\r\n        <div class=\"offset-2 col-md-10\">\r\n            <input type=\"submit\" value=\"Registrar\" class=\"btn\" />\r\n        </div>\r\n\r\n");
#nullable restore
#line 30 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Registrar.cshtml"
          
            var esquemaAutenticacao = await SignInManager.GetExternalAuthenticationSchemesAsync();

            if (esquemaAutenticacao != null)
            {
                foreach (var provider in esquemaAutenticacao)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e5a0a8a2eb306969dfb8b553dd51e57f0cd704ea7099", async() => {
                WriteLiteral("\r\n                        Entrar com ");
#nullable restore
#line 38 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Registrar.cshtml"
                              Write(provider.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "value", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 37 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Registrar.cshtml"
AddHtmlAttributeValue("", 1167, provider.Name, 1167, 14, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 40 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Registrar.cshtml"
                }
            }
        

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n");
#nullable restore
#line 44 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Registrar.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<UsuarioAplicacao> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ByteBank.Forum.ViewModels.ContaRegistrarViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
