using Foundation;
using ObjCRuntime;
using System;
using UIKit;

namespace Scannn.iOS
{
    public partial class FirstTab : UIView
    {
        public FirstTab (IntPtr handle) : base (handle)
        {
        }

        public static FirstTab Create()
        {
            var arr = NSBundle.MainBundle.LoadNib("FirstTab", null, null);
            var v = Runtime.GetNSObject<FirstTab>(arr.ValueAt(0));
            return v;
        }

        public override void AwakeFromNib()
        {
            label1.Text = "hello from the SomeView class";
        }

    }
}