using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class myCaptcha : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CreateCaptCha();
    }
    static Random r = new Random();
    void CreateCaptCha()
    {
        var w = 70;// 110;
        var h = 30;

        string[] arrFont = { "Times New Roman", "Arial", "Verdana"
                               , "Courier New", "Arial Black", "Microsoft Sans Serif" };
        FontStyle[] arrFontStyle = { FontStyle.Bold, FontStyle.Italic
                                       , FontStyle.Regular, FontStyle.Strikeout 
                                       ,FontStyle.Underline};
        var code = "";// r.Next(100000, 999999).ToString(); //"1234567890";//

        var alphaBet = "qw1er2ty3ui4op5as6df7gh8jk9lz0xcvbnm";
        alphaBet = "1234567890";
        for (int i = 0; i < 4; i++)
        {
            code += alphaBet[r.Next(0, alphaBet.Length)].ToString();
        }

       HttpContext.Current.Session["CaptchaVerifyCode"] = code;

        HttpContext.Current.Response.ContentType = "image/jpeg";
        var bmp = new Bitmap(w, h);
        var g = Graphics.FromImage(bmp);
        g.SmoothingMode = SmoothingMode.HighSpeed;

        var arr = code.ToCharArray();

        #region draw background color

        for (var i = 0; i < w; i = i + w / 6)
        {
            g.FillRectangle(new SolidBrush(Color.FromName("#333333")), i, 0, w / 6, h);

        }
        //for (int i = 0; i < 2 * h / 3; i = i + h / 4)
        //{
        //    g.FillRectangle(new SolidBrush(Color.FromArgb(r.Next(200, 255), r.Next(200, 255), r.Next(200, 255))), 0, i, w, i + h / 4);

        //}
        //g.FillRectangle(new SolidBrush(Color.FromArgb(r.Next(200, 255), r.Next(200, 255), r.Next(200, 255))), 0, h/2, w, h);
        #endregion

        #region draw caro
        //for (int i = r.Next(1, 10); i < w; i = i + r.Next(3, 15))
        //{
        //    g.DrawLine(new Pen(
        //       Color.FromArgb(r.Next(80, 255), r.Next(80, 255), r.Next(80, 255)), r.Next(1, 3)),
        //        new Point(i, r.Next(0, h / 4)),
        //       new Point(i, r.Next(h / 4, h * 2 / 3)));
        //}
        //for (int i = 0; i < h; i = i + r.Next(3, 10))
        //{
        //    g.DrawLine(new Pen(
        //       Color.FromArgb(r.Next(100, 255), r.Next(100, 255), r.Next(100, 255)), r.Next(1, 1)),
        //        new Point(0, i),
        //       new Point(w, i));
        //}
        #endregion

        #region draw background line
        //for (int i = 0; i < r.Next(3, 10); i++)
        //{
        //    g.DrawLine(new Pen(
        //        Color.AntiqueWhite),
        //        new Point(r.Next(0, w / 4), r.Next(0, h / 2)),
        //        new Point(r.Next(w * 3 / 4, w), r.Next(0, h / 2)));
        //}

        for (int i = 0; i < r.Next(3, 7); i++)
        {
            g.DrawLine(new Pen(
                Color.Beige),
                 new Point(r.Next(0, w / 4), r.Next(h / 3, h)),
                new Point(r.Next(6 * w / 10, w), r.Next(h / 2, h)));
        }
        #endregion

        #region draw string code
        for (var i = 0; i < arr.Length; i++)
        {
            //g.DrawString(arr[i].ToString(),
            //    new Font(arrFont[r.Next(0, arrFont.Length)], r.Next(18, 27),arrFontStyle[ r.Next(0, arrFontStyle.Length)],GraphicsUnit.Point),
            //    new SolidBrush( Color.FromArgb(r.Next(0, 200), r.Next(0, 200), r.Next(0, 200))),
            //    new PointF(5 + i * 16, r.Next(1, h-35)),StringFormat.GenericTypographic);
            g.DrawString(arr[i].ToString(),
                new Font(arrFont[0], 18, arrFontStyle[0], GraphicsUnit.Point),
                new SolidBrush(Color.FromArgb(r.Next(200, 255), r.Next(200, 255), r.Next(200, 255))),
                new PointF(5 + i * 16, h / 8), StringFormat.GenericTypographic);
        }
        #endregion

        #region draw border
        //g.DrawRectangle(
        //    new Pen(Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)), 1),
        //    0, 0, w - 1, h - 1);
        //g.DrawRectangle(
        //  new Pen(Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)), r.Next(1, 4)),
        //  1, 1, w - 2, h - 2);
        #endregion

        bmp.Save(HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        g.Dispose();
        bmp.Dispose();
        HttpContext.Current.Response.Flush();

    }
}
