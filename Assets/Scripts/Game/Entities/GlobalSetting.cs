using System.Collections.Generic;

namespace Game.Entities
{
    /// <summary>
    /// ゲーム全体の設定を表現するクラス
    /// </summary>
    public class GlobalSetting
    {
        /// <summary>
        /// 各プレイヤーの設定
        /// </summary>
        public IEnumerable<PlayerSetting> Players { get; set; }

        /// <summary>
        /// 会場となるマップ
        /// </summary>
        public IMap SelectedMap { get; set; }
    }
}