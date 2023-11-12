namespace FormsLib.Platformer;

internal static class SpriteManager
{
    public static List<Bitmap> GetSpritesFromSheet(Bitmap image, int spriteCount)
    {
        List<Bitmap> sprites = new();
        int segmentWidth = image.Width / 8;

        for(int i = 0; i < spriteCount; i++)
        {
            Bitmap segment = new Bitmap(segmentWidth, image.Height);
            Graphics graphics = Graphics.FromImage(segment);

            graphics.DrawImage(image, new Rectangle(0, 0, segmentWidth, image.Height),
                new Rectangle(i * segmentWidth, 0, segmentWidth, image.Height), GraphicsUnit.Pixel);
            graphics.Dispose();

            sprites.Add(segment);
        }


        return sprites;
    }

    public static Bitmap Crop(Bitmap image)
    {
        Rectangle bounds = GetBounds(image);

        Bitmap croppedImage = new Bitmap(bounds.Width, bounds.Height);

        using(Graphics graphics = Graphics.FromImage(croppedImage))
        {
            graphics.DrawImage(image, 0, 0, bounds, GraphicsUnit.Pixel);
        }

        return croppedImage;
    }

    public static Rectangle GetBounds(Bitmap bitmap)
    {
        int left = int.MaxValue;
        int right = int.MinValue;
        int top = int.MaxValue;
        int bottom = int.MinValue;

        for(var x = 0; x < bitmap.Width; x++)
        {
            for(int y  = 0; y < bitmap.Height; y++)
            {
                Color pixelColour = bitmap.GetPixel(x, y);

                if (pixelColour.A == 0) continue;

                left = Math.Min(left, x);
                right = Math.Max(right, x);
                top = Math.Min(top, y);
                bottom = Math.Max(bottom, y);
            }
        }

        if(left > right || top > bottom)
            return Rectangle.Empty;

        Rectangle boundBox = new Rectangle(left, top, right - left + 1, bottom - top + 1);

        return boundBox;
    } 
}
