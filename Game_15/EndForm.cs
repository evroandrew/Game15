using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_15
{
    public partial class EndForm : Form
    {
        int typeOfGame = 0;
        Tree<int, string> Table = new Tree<int, string>();
        int pointer; string pointer_name;
        public EndForm(int gameType)
        {
            typeOfGame = gameType;
            InitializeComponent();
            string winText = "";
            if (typeOfGame == 0) winText = "Победители горизонтального режима";
            if (typeOfGame == 1) winText = "Победители вертикального режима";
            if (typeOfGame == 2) winText = "Победители свободного режима";
            labelWin.Text = winText;
        }

        private void EndForm_Load(object sender, EventArgs e)
        {
            var names = new List<string>();
            var points = new List<string>();
            GetLists(names, points);
            for (int i = 0; i < names.Count; i++)
                Table.Add(Int32.Parse(points[i]), names[i]);
            pointer = Int32.Parse(points[names.Count - 1]);
            pointer_name = names[names.Count - 1];
            GridData();
            SetSelection();
        }
        private void GetLists(List<string> names, List<string> points)
        {
            string fileName = GetFileName();
            using (System.IO.StreamReader file =
            new System.IO.StreamReader(Environment.CurrentDirectory + fileName, true))
            {
                string str = file.ReadToEnd();
                var str_k = str.Split('\\');
                for (int i = 0; i < str_k.Length - 1; i++)
                {
                    var buf = str_k[i].Split('/');
                    names.Add(buf[0]);
                    points.Add(buf[1]);
                }
            }
        }
        private string GetFileName()
        {
            if (typeOfGame == 0) return "\\123.txt";
            if (typeOfGame == 1) return "\\321.txt";
            if (typeOfGame == 2) return "\\222.txt";
            return "";
        }
        private void SetSelection()
        {
            dataGridView.ClearSelection();
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                if ((dataGridView.Rows[i].Cells["name"].Value.ToString().Equals(pointer_name))
                    && ((int)dataGridView.Rows[i].Cells["points"].Value == pointer))
                {
                    dataGridView.Rows[i].Cells["name"].Selected = true;
                    dataGridView.Rows[i].Cells["points"].Selected = true;
                }
            }
        }
        private void GridData()
        {
            var column1 = new DataGridViewColumn
            {
                HeaderText = "Имя",
                Name = "name",
                Width = 100,
                ReadOnly = true,
                CellTemplate = new DataGridViewTextBoxCell()
            };

            var column2 = new DataGridViewColumn
            {
                HeaderText = "Время",
                Name = "points",
                CellTemplate = new DataGridViewTextBoxCell()
            };

            dataGridView.Columns.Add(column1);
            dataGridView.Columns.Add(column2);
            int i = 0;
            foreach (var t in Table)
            {
                dataGridView.Rows.Add();
                dataGridView["name", i].Value = t.Value;
                dataGridView["points", i].Value = t.Key;
                i++;
            }
        }
    }
    class MyStack<T>
    {
        private readonly T[] arr = new T[100];
        public void Push(T val)
        {
            arr[Count++] = val;
        }
        public T Pop()
        {
            return arr[--Count];
        }
        public int Count { get; private set; }

    }
    class Tree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        private class TreeItem
        {
            public KeyValuePair<TKey, TValue> Pair { get; set; }
            public TreeItem Parent { get; set; }
            public TreeItem Left { get; set; }
            public TreeItem Right { get; set; }
        }
        private TreeItem root = null;
        public bool IsRepeatKeys { get; private set; }
        public Tree(bool isRepeatKeys = true)
        {
            IsRepeatKeys = isRepeatKeys;
        }

        public void Add(TKey key, TValue val)
        {
            Add(new KeyValuePair<TKey, TValue>(key, val));
        }
        public void Add(KeyValuePair<TKey, TValue> pair)
        {
            if (root == null)
                root = new TreeItem { Pair = pair };
            else
                Add(pair, root);
        }
        private void Add(KeyValuePair<TKey, TValue> pair, TreeItem item)
        {
            if (!IsRepeatKeys & item.Pair.Key.CompareTo(pair.Key) == 0)
            {
                item.Pair = pair;
                return;
            }

            if (item.Pair.Key.CompareTo(pair.Key) > 0)
                if (item.Left == null)
                    item.Left = new TreeItem { Pair = pair, Parent = item };
                else
                    Add(pair, item.Left);
            else
            {
                if (item.Right == null)
                    item.Right = new TreeItem { Pair = pair, Parent = item };
                else
                    Add(pair, item.Right);
            }
        }

        private IEnumerator<TreeItem> GetTreeItemEnumerator(TreeItem item)
        {
            MyStack<TreeItem> items = new MyStack<TreeItem>();
            while (item != null || items.Count != 0)
            {
                if (items.Count != 0)
                {
                    item = items.Pop();
                    yield return item;
                    if (item.Right != null)
                        item = item.Right;
                    else
                        item = null;
                }
                while (item != null)
                {
                    items.Push(item);
                    item = item.Left;
                }
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            using (IEnumerator<TreeItem> en = GetTreeItemEnumerator(root))
            {
                while (en.MoveNext())
                {
                    yield return en.Current.Pair;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
