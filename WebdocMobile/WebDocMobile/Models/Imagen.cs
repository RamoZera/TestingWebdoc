//-----------------------------------------------------------------------
// <copyright file="Imagen.cs" company="Home">
//     Author: Horus Ra
//     Copyright (c) Home. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using HeyRed.Mime;
using Microsoft.Maui.Graphics.Platform;

namespace WebDocMobile.Models
{
    public  class Imagen
    {
        public static byte[] ResizeImage(byte[] barr, float width, float height, bool keepRatio)
        {
            using(Microsoft.Maui.Graphics.IImage image = ConvertByteArrayToImage(barr))
                using(Microsoft.Maui.Graphics.IImage resized = image.Resize(width, height, keepRatio ? ResizeMode.Fit : ResizeMode.Stretch))
                    return resized.AsBytes();
        }

        public static Microsoft.Maui.Graphics.IImage ConvertByteArrayToImage(byte[] barr)
        {
            using(MemoryStream stream = new MemoryStream(barr))
                return PlatformImage.FromStream(stream);
        }

        public static string GetDataString(byte[] content, string name, bool useMime) => $"{(useMime ? $"data:{MimeTypesMap.GetMimeType(name)};base64," : string.Empty)}{Convert.ToBase64String(content)}";
    }
}
