using System.Threading.Tasks;
using cdit.ezcheck;
using Scannn.Data;
using Xamarin.Forms;
using cdit.ezcheck.Extensions;
using Android.Graphics;
using System.IO;
using Android.Text;

[assembly: Dependency(typeof(DrawText))]
namespace cdit.ezcheck
{
    class DrawText : IDraw
    {
        public async Task<ImageSource> Drawtext(ImageSource image, string message, string author)
        {
            var handler = image.GetHandler();

            if (handler == null) return null;

            var bitmapimage = await handler.LoadImageAsync(image, Android.App.Application.Context);
            var resultimage = DrawTextImage(bitmapimage, message, author);
            return ImageSource.FromStream(() =>
            {
                MemoryStream ms = new MemoryStream();
                resultimage.Compress(Bitmap.CompressFormat.Png, 100, ms);
                ms.Seek(0L, SeekOrigin.Begin);
                return ms;
            });
            //return null;
            throw new System.NotImplementedException();
        }

        public static Bitmap DrawTextImage(Bitmap image, string text, string author)
        {
            var resultimage = image;
            
            Bitmap.Config bitmapconfig = image.GetConfig();

            System.Diagnostics.Debug.WriteLine("kích thước ảnh: " + image.Width + " " + image.Height);
            if (bitmapconfig == null)
            {
                bitmapconfig = Bitmap.Config.Argb8888;
            }

            Bitmap bg_bitmap = Bitmap.CreateBitmap(image.Width, image.Height + image.Width / 5, Bitmap.Config.Argb8888);

            image = image.Copy(bitmapconfig, true);
            var image1 = image;
            Canvas bgcanvas = new Canvas(bg_bitmap);
            Paint p = new Paint();
            p.Color = Android.Graphics.Color.White;

            TextPaint paint = new TextPaint(PaintFlags.AntiAlias);
            TextPaint paint_author = new TextPaint(PaintFlags.AntiAlias);

            Rect bounds = new Rect();
            paint.Color = Android.Graphics.Color.White;
            paint.TextSize = 150;
            paint.SetTypeface(Typeface.Create(Typeface.Default, TypefaceStyle.Bold));

            paint_author.Color = Android.Graphics.Color.Black;
            paint_author.TextSize = 120;
            
            System.Diagnostics.Debug.WriteLine("kích thước ảnh: " + image.Width + " " + image.Height);
            int textWidth = bgcanvas.Width - 200;
            int textHeight = 0;
            StaticLayout textLayout = new StaticLayout(text, paint, textWidth, Android.Text.Layout.Alignment.AlignCenter, 1, 0, false);
            textHeight = textLayout.Height;
            while (textHeight > image.Height - 200)
            {
                paint.TextSize = paint.TextSize - 1;
                textLayout = new StaticLayout(text, paint, textWidth, Android.Text.Layout.Alignment.AlignCenter, 1, 0, false);
                textHeight = textLayout.Height;
            };
            
            //StaticLayout textLayout = new StaticLayout(text, paint, textWidth, Android.Text.Layout.Alignment.AlignCenter, 1, 0, false);

            StaticLayout textLayout_author = new StaticLayout(author, paint_author, textWidth, Android.Text.Layout.Alignment.AlignCenter, 1, 0, false);

            //textHeight = textLayout.Height;

            int textHeight_author = textLayout_author.Height;

            float x = (image.Width - textWidth) / 2;
            float y = (image.Height - textHeight) / 2;

            float x_author = (image.Width - textWidth) / 2;
            float y_author = image.Height + (image.Width / 5 - textHeight_author) / 2;

            //draw background + image
            bgcanvas.DrawColor(Android.Graphics.Color.White);
            bgcanvas.DrawBitmap(bg_bitmap, 0, 0, p);
            bgcanvas.DrawBitmap(image, 0, 0, null);
            //write content
            bgcanvas.Save();
            bgcanvas.Translate(x, y);
            textLayout.Draw(bgcanvas);
            bgcanvas.Restore();
            //write author
            bgcanvas.Save();
            bgcanvas.Translate(x_author, y_author);
            textLayout_author.Draw(bgcanvas);
            bgcanvas.Restore();

            return bg_bitmap;
        }
    }
}