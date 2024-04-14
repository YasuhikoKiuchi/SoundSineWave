namespace SoundSineWave
{
    class WaveMakerParam
    {
        /// <summary>θ増分</summary>
        public double Dt { get; set; }

        /// <summary>データサイズ</summary>
        public int DataSize { get; set; }

        /// <summary>振幅</summary>
        public int R { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filepath">出力ファイルパス</param>
        /// <param name="dt">θ増分</param>
        /// <param name="dataSize">データサイズ</param>
        /// <param name="r">振幅</param>
        public WaveMakerParam(double dt, int dataSize, int r)
        {
            Dt = dt;
            DataSize = dataSize;
            R = r;
        }
    }
}
