using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GameView.Strategy.Units
{
    public class UnitHealthHearts : MonoBehaviour
    {
        [Header("Components")]
        public GridLayoutGroup grid;
        public GameObject overflow;

        [Header("Prefabs"), Space]
        public Image heartPrefab;

        [Header("Properties"), Space]
        [Min(float.Epsilon)] public float healthPerHeart = 10.0f;
        [Min(1)] public int row = 2;
        [Min(1)] public int minColumn = 4;
        [Min(1)] public int maxColumn = 6;

        [Header("States"), Space]
        public int initialHealth;
        public int currentHealth;

        private RectTransform _rectTransform;
        private List<Image> _hearts;

        /// <summary>
        /// 横一列に表示するハートの数を返す。
        /// </summary>
        private int HeartsColumn()
        {
            var column =  Mathf.CeilToInt(initialHealth / healthPerHeart);
            return Mathf.Clamp(column, minColumn, maxColumn);
        }

        /// <summary>
        /// 見える状態にするハートの数を返す。
        /// </summary>
        private int VisibleHeartsCount(int column) {
            var maxCount = column * row;
            var count = Mathf.CeilToInt(currentHealth / healthPerHeart);
            return Mathf.Clamp(count, 1, maxCount);
        }

        private int RequireHeartsCount(int column)
        {
            return column * row;
        }

        private void Awake()
        {
            _hearts = new List<Image>();
        }

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            var column = HeartsColumn();
            var visibleCount = VisibleHeartsCount(column);
            var requireCount = RequireHeartsCount(column);

            UpdateColumn(column);
            UpdateHeartsExist(requireCount);
            UpdateHeartsVisible(visibleCount);
            UpdateHeartsFill();
            UpdateOverflowVisible(column);
        }

        private void UpdateHeartsExist(int require)
        {
            if (require == _hearts.Count)
            {
                return;
            }

            if (require > _hearts.Count && heartPrefab != null)
            {
                var addition = Enumerable.Range(0, require - _hearts.Count)
                    .Select(_ => Instantiate(heartPrefab, transform));
                _hearts.AddRange(addition);
            }
            else if (require < _hearts.Count)
            {
                for (var i = require; i < _hearts.Count; i++)
                {
                    Destroy(_hearts[i].gameObject);
                }
                _hearts.RemoveRange(require, _hearts.Count - require);
            }
        }

        /// <summary>
        /// ハートの表示列数を更新する。
        /// </summary>
        private void UpdateColumn(int column)
        {
            if (grid == null)
            {
                return;
            }

            grid.constraintCount = column;
            var xMargin = (_rectTransform.rect.width - column * grid.cellSize.x) / (column - 1);
            grid.spacing = new Vector2(Mathf.Min(xMargin, 0f), grid.spacing.y);
        }

        /// <summary>
        /// すべてのハートの可視状態を更新する。
        /// </summary>
        private void UpdateHeartsVisible(int visibleCount)
        {
            for (var i = 0; i < _hearts.Count; i++)
            {
                _hearts[i].gameObject.SetActive(i < visibleCount);
            }
        }

        /// <summary>
        /// すべてのハートのゲージ量を更新する。
        /// </summary>
        private void UpdateHeartsFill()
        {
            for (var i = 0; i < _hearts.Count; i++)
            {
                var min = healthPerHeart * i;
                var max = min + healthPerHeart;
                var percent = (currentHealth - min) / (max - min);
                _hearts[i].fillAmount = Mathf.Clamp01(percent);
            }
        }

        /// <summary>
        /// オーバーフローマークの可視状態を更新する。
        /// </summary>
        private void UpdateOverflowVisible(int column)
        {
            if (overflow == null)
            {
                return;
            }

            var limit = column * row * healthPerHeart;
            overflow.SetActive(limit < currentHealth);
        }
    }
}