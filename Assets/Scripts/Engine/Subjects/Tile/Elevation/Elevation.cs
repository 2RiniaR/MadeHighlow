namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     タイルの高さ
    /// </summary>
    /// <param name="Placeable">オブジェクトを配置できるかどうか</param>
    public abstract record Elevation(in bool Placeable);
}