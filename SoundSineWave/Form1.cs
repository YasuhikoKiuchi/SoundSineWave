using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SoundSineWave
{
    /// <summary>
    /// フォームクラス
    /// </summary>
    public partial class Form1 : Form
    {
        #region フィールド

        /// <summary>WAVE作成オブジェクト</summary>
        private readonly WaveMaker maker = null;

        /// <summary>WAVE出力オブジェクト</summary>
        private readonly WaveWriter writer = null;

        /// <summary>WAVE数値配列</summary>
        private int[] arr = null;

        /// <summary>ピクチャボックスの横幅</summary>
        private readonly int picrtureBoxWidth = 0;

        /// <summary>ピクチャボックスの高さの半分</summary>
        private readonly float hPicrtureBoxHeight = 0;

        #endregion

        #region 初期処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            maker = new WaveMaker();
            writer = new WaveWriter();
            picrtureBoxWidth = pictureBox1.Width;
            hPicrtureBoxHeight = pictureBox1.Height / 2;
            Make();
        }

        /// <summary>
        /// 作成
        /// </summary>
        private void Make()
        {
            var p = MakeParam();
            arr = maker.Execute(p);
            pictureBox1.Invalidate();
        }

        #endregion

        #region ボタン押下時処理

        /// <summary>
        /// 再生ボタン押下時処理
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            var filepath = GetTempWaveFilepath();

            if (Execute(filepath))
            {
                Process.Start(filepath);
            }
        }

        /// <summary>
        /// 保存ボタン押下時処理
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Execute(saveFileDialog1.FileName);
            }
        }

        #endregion

        #region 処理

        /// <summary>
        /// テンポラリWAVEファイルパスの取得
        /// </summary>
        /// <returns></returns>
        private string GetTempWaveFilepath()
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory;
            string filename = "test.wav";
            return Path.Combine(folderPath, filename);
        }

        /// <summary>
        /// WAVE作成
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private bool Execute(string filepath)
        {
            var p = MakeParam();
            if (arr == null) arr = maker.Execute(p);
            return writer.Execute(filepath, arr);
        }

        /// <summary>
        /// パラメータ取得
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private WaveMakerParam MakeParam()
        {
            double dt = 0.1;
            int dataSize = 128000;
            int r = 2000;
            return new WaveMakerParam(dt, dataSize, r);
        }

        #endregion

        #region 終了処理

        /// <summary>
        /// フォームクローズ時処理
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeleteTempFile();
        }

        /// <summary>
        /// テンポラリWAVEファイルがあったら削除する
        /// </summary>
        private void DeleteTempFile()
        {
            var filepath = GetTempWaveFilepath();
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }

        #endregion

        #region 描画処理

        private float CalcY(int v)
        {
            return v * (hPicrtureBoxHeight - 20) / 12000 + hPicrtureBoxHeight;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (arr != null)
            {
                float x = 0;
                float y = CalcY(arr[0]);
                int max = arr.Length;
                if (max > picrtureBoxWidth) max = picrtureBoxWidth;
                for (int i = 1; i < max; i++)
                {
                    float x2 = x + 1;
                    float y2 = CalcY(arr[i]);
                    e.Graphics.DrawLine(Pens.Yellow, x, y, x2, y2);
                    x = x2;
                    y = y2;
                }
            }
        }

        #endregion
    }
}
