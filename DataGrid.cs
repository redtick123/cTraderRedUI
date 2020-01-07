using System;
using System.Collections.Generic;

namespace cAlgo.API
{
    public class DataGrid : Grid
    {
        private readonly Dictionary<Tuple<int, int>, TextBlock> Cells = new Dictionary<Tuple<int, int>, TextBlock>();

        public DataGrid()
        {
            IsHitTestVisible = false;
        }

        private Thickness _cellsPadding;
        public Thickness CellsPadding
        {
            get { return _cellsPadding; }
            set
            {
                _cellsPadding = value;
                UpdateAllCellsPadding();
            }
        }

        private Thickness _cellsMargin;
        public Thickness CellsMargin
        {
            get { return _cellsMargin; }
            set
            {
                _cellsMargin = value;
                UpdateAllCellsMargin();
            }
        }

        private Color _cellsForegroundcolor;
        public Color CellsForegroundColor
        {
            get { return _cellsForegroundcolor; }
            set
            {
                _cellsForegroundcolor = value;
                UpdateAllCellsForegroundColors();
            }
        }

        private Color _cellsBackgroundColor;
        public Color CellsBackgroundColor
        {
            get { return _cellsBackgroundColor; }
            set
            {
                _cellsBackgroundColor = value;
                UpdateAllCellsBackgroundColors();
            }
        }

        private double _fontSize;
        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                UpdateAllCellsFontSize();
            }
        }

        public void SetText(string text, int row, int column)
        {
            if (row < 0 || column < 0)
                throw new ArgumentOutOfRangeException();

            var cell = GetOrCreateCell(row, column);
            cell.Text = text;
        }

        private TextBlock GetOrCreateCell(int row, int column)
        {
            var key = new Tuple<int, int>(row, column);
            TextBlock cell;
            var isCellExeists = Cells.TryGetValue(key, out cell);
            if (!isCellExeists)
            {
                cell = new TextBlock();
                Cells[key] = cell;

                if (row + 1 > Rows.Count)
                    AddRows(Rows.Count - row + 1);

                if (column + 1 > Columns.Count)
                    AddColumns(Columns.Count - column + 1);

                AddChild(cell, row, column);
                SetCellPadding(cell);
                SetCellMargin(cell);
                SetCellForegroundColor(cell);
                SetCellBackgroundColor(cell);
                SetCellFontSize(cell);
            }
            return cell;
        }

        private void UpdateAllCellsPadding()
        {
            foreach (var cell in Cells.Values)
                SetCellPadding(cell);
        }

        private void SetCellPadding(TextBlock cell)
        {
            cell.Padding = CellsPadding;
        }

        private void UpdateAllCellsMargin()
        {
            foreach (var cell in Cells.Values)
                SetCellMargin(cell);
        }

        private void SetCellMargin(TextBlock cell)
        {
            cell.Margin = CellsMargin;
        }

        private void UpdateAllCellsForegroundColors()
        {
            foreach (var cell in Cells.Values)
                SetCellForegroundColor(cell);
        }

        private void SetCellForegroundColor(TextBlock cell)
        {
            cell.ForegroundColor = CellsForegroundColor;
        }

        private void UpdateAllCellsBackgroundColors()
        {
            foreach (var cell in Cells.Values)
                SetCellBackgroundColor(cell);
        }

        private void SetCellBackgroundColor(TextBlock cell)
        {
            cell.BackgroundColor = CellsBackgroundColor;
        }

        private void UpdateAllCellsFontSize()
        {
            foreach (var cell in Cells.Values)
                SetCellFontSize(cell);
        }

        private void SetCellFontSize(TextBlock cell)
        {
            cell.FontSize = FontSize;
        }

        public void SetCellColor(int row, int column, Color color)
        {
            var textBlock = GetOrCreateCell(row, column);
            textBlock.ForegroundColor = color;
        }

        public void SetCellBackgroundColor(int row, int column, Color color)
        {
            var textBlock = GetOrCreateCell(row, column);
            textBlock.BackgroundColor = color;
        }
    }
}
