using System.Collections.Generic;

namespace Game.Entities
{
    /// <summary>
    /// ゲームを実行する際の、プレイヤーごとの設定を表現するクラス
    /// </summary>
    public class PlayerSetting
    {
        public List<ICharacter> SelectedUnits { get; set; }
    }
}