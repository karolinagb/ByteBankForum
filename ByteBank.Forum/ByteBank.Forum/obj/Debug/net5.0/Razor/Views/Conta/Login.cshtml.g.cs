#pragma checksum "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5cd9c13f9406656b566c9ec213eb7a7b07e2e74b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Conta_Login), @"mvc.1.0.view", @"/Views/Conta/Login.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5cd9c13f9406656b566c9ec213eb7a7b07e2e74b", @"/Views/Conta/Login.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f6d0493028697888a33ccbd549fb1fe1480fcf56", @"/Views/_ViewImports.cshtml")]
    public class Views_Conta_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ByteBank.Forum.ViewModels.ContaLoginViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h2>Login</h2>\r\n\r\n");
#nullable restore
#line 5 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Login.cshtml"
 using (Html.BeginForm())
{
    //O primeiro parametro é a mensagem e o segundo é a classe do bootstrap
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Login.cshtml"
Write(Html.ValidationSummary("", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Login.cshtml"
                                                               

    //Com esse código o asp.net vai criar todos os campos definidos na model automaticamente
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Login.cshtml"
Write(Html.EditorForModel());

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-group\">\r\n        <div class=\"offset-2 col-md-10\">\r\n            <input type=\"submit\" value=\"Fazer Login\" class=\"btn\" />\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 18 "C:\projetos\ByteBankForum\ByteBank.Forum\ByteBank.Forum\Views\Conta\Login.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ByteBank.Forum.ViewModels.ContaLoginViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
