using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using MadaAssociationMobile.CustomRenderers;
using MadaAssociationMobile.Droid.CustomRenderers;
using Android.Content.Res;
using Android.Graphics;
using AndroidX.Core.Content;
using System.Drawing;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace MadaAssociationMobile.Droid.CustomRenderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            try
            {
                base.OnElementChanged(e);

                if (Control == null || e.NewElement == null) return;

                if (e.NewElement is CustomEntry customEntry)
                {
                    Control.SetHighlightColor(color: customEntry.HighlightColor.ToAndroid());

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                    {
                        //Control.SetTextCursorDrawable(0);
                        //This API Intrduced in android 10
                        Control.SetTextCursorDrawable(Resource.Drawable.custom_cursor);
                        //Control.SetTextSelectHandle(Resource.Drawable.cursor_blue);
                        //Control.SetTextSelectHandleLeft(Resource.Drawable.cursor_blue);
                        //Control.SetTextSelectHandleRight(Resource.Drawable.cursor_blue);
                        Control.BackgroundTintList = ColorStateList.ValueOf(customEntry.TintColor.ToAndroid());
                    }
                    else
                    {
                        JNIEnv.SetField(Control.Handle, JNIEnv.GetFieldID(JNIEnv.FindClass(typeof(TextView)), "mCursorDrawableRes", "I"), 0);
                        TextView textViewTemplate = new TextView(Control.Context);

                        var field = textViewTemplate.Class.GetDeclaredField("mEditor");
                        field.Accessible = true;
                        var editor = field.Get(Control);

                        string[]
                        fieldsNames = { "mTextSelectHandleLeftRes", "mTextSelectHandleRightRes", "mTextSelectHandleRes" },
                        drawablesNames = { "mSelectHandleLeft", "mSelectHandleRight", "mSelectHandleCenter" };

                        for (int index = 0; index < fieldsNames.Length && index < drawablesNames.Length; index++)
                        {
                            field = textViewTemplate.Class.GetDeclaredField(fieldsNames[index]);
                            field.Accessible = true;
                            Drawable handleDrawable = ContextCompat.GetDrawable(Context, field.GetInt(Control));

                            handleDrawable.SetColorFilter(new PorterDuffColorFilter(customEntry.HandleColor.ToAndroid(), PorterDuff.Mode.SrcIn));

                            field = editor.Class.GetDeclaredField(drawablesNames[index]);
                            field.Accessible = true;
                            field.Set(editor, handleDrawable);
                        }

                        if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                        {
                            Control.BackgroundTintList = ColorStateList.ValueOf(customEntry.TintColor.ToAndroid());
                        }
                        else
                        {
                            Control.Background.SetColorFilter(new PorterDuffColorFilter(customEntry.TintColor.ToAndroid(), PorterDuff.Mode.SrcAtop));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Crashes.TrackError(ex, new Dictionary<string, string> { { "Warning", "Entry Renderer" } });
            }
        }
    }
}