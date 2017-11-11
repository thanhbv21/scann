using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Scannn.Views;
using Scannn.iOS;
using WebKit;
using Xamarin.Forms.Platform.iOS;
using System.IO;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace Scannn.iOS
{
    class HybridWebViewRenderer : ViewRenderer<HybridWebView, WKWebView>, IWKScriptMessageHandler
    {
        const string JavaScriptFunction = "function readcontent(){data = document.getElementById('ios-img').innetHTML; window.webkit.messageHandlers.invokeAction.postMessage(data);});});";
        WKUserContentController userController;
        private WKWebView webView;

        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                userController = new WKUserContentController();
                var script = new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false);
                userController.AddUserScript(script);
                userController.AddScriptMessageHandler(this, "invokeAction");
                //Console.WriteLine("active render");
                var config = new WKWebViewConfiguration { UserContentController = userController };
                webView = new WKWebView(Frame, config);
                SetNativeControl(webView);
                
            }
            if (e.OldElement != null)
            {
                userController.RemoveAllUserScripts();
                userController.RemoveScriptMessageHandler("invokeAction");
                var hybridWebView = e.OldElement as HybridWebView;
                hybridWebView.Cleanup();
            }
            if (e.NewElement != null)
            {
                //string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", Element.Uri));
                if (Element.Uri != null)  Control.LoadRequest(new NSUrlRequest(new NSUrl(Element.Uri)));

                //webView.EvaluateJavaScriptAsync("readcontent();");
                //Console.WriteLine("active render");
            }
        }
        [Export("webView:didFinishNavigation:")]
        public void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            Console.WriteLine("active render");
            this.webView.EvaluateJavaScriptAsync("readcontent();").ContinueWith(t =>
            {
                if (t.Status != System.Threading.Tasks.TaskStatus.RanToCompletion)
                {
                    System.Diagnostics.Debug.WriteLine(t.Exception.InnerException.Message);
                }
            });
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            Element.InvokeAction(message.Body.ToString());
        }
        
    }
}