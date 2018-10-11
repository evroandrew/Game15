using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_15
{
    class GameLogic
    {
        private Random rnd = new Random();
        public int typeOfGame = 0;
        private readonly int[,] _gameField = new int[4, 4];
        public int ZeroX { get; private set; }
        public int ZeroY { get; private set; }
        readonly int[] arr1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };
        readonly int[] arr2 = { 13, 9, 5, 1, 14, 10, 6, 2, 15, 11, 7, 3, 0, 12, 8, 4 };
        public bool IsWin()
        {
            if (typeOfGame == 0)
            {
                if (Check_game1()) return true;
                return Check_game12();
            }
            if (typeOfGame == 1)
            {
                if (Check_game2()) return true;
                return Check_game21();
            }
            if (typeOfGame == 2)
            {
                if (Check_game1()) return true;
                if (Check_game12()) return true;
                if (Check_game2()) return true;
                return Check_game21();
            }
            return false;
        }
        private bool Check_game1()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (arr1[i * 4 + j] != _gameField[j, i])
                        return false;
            return true;
        }
        private bool Check_game12()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (arr1[i * 4 + j] != _gameField[3-j, 3-i])
                        return false;
            return true;
        }
        private bool Check_game2()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (arr2[i * 4 + j] != _gameField[j, i])
                        return false;
            return true;
        }
        private bool Check_game21()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (arr2[i * 4 + j] != _gameField[3-j, 3-i])
                        return false;
            return true;
        }
        public void zeroUp()
        {
            if (ZeroY == 3) return;
            _gameField[ZeroX, ZeroY] = _gameField[ZeroX, ZeroY + 1];
            ++ZeroY;
            _gameField[ZeroX, ZeroY] = 0;

        }
        public void zeroDown()
        {
            if (ZeroY == 0) return;
            _gameField[ZeroX, ZeroY] = _gameField[ZeroX, ZeroY - 1];
            --ZeroY;
            _gameField[ZeroX, ZeroY] = 0;
        }
        public void zeroLeft()
        {
            if (ZeroX == 3) return;
            _gameField[ZeroX, ZeroY] = _gameField[ZeroX + 1, ZeroY];
            ++ZeroX;
            _gameField[ZeroX, ZeroY] = 0;
        }
        public void zeroRight()
        {
            if (ZeroX == 0) return;
            _gameField[ZeroX, ZeroY] = _gameField[ZeroX - 1, ZeroY];
            --ZeroX;
            _gameField[ZeroX, ZeroY] = 0;
        }
        public GameLogic()
        {
            InitGame();
        }
        public void InitGame()
        {
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };
            Shuffle(arr);
            Check_it(arr);
            for (int n = 0, i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j, ++n)
                {
                    _gameField[j, i] = arr[n];
                    if (arr[n] == 0)
                    {
                        ZeroX = j;
                        ZeroY = i;
                    }
                }
            }
        }

        public bool CheckAndGo(int value)
        {
            if (ZeroX > 0 && _gameField[ZeroX - 1, ZeroY] == value)
            {
                zeroRight();
                return true;
            }
            if (ZeroX < 3 && _gameField[ZeroX + 1, ZeroY] == value)
            {
                zeroLeft();
                return true;
            }
            if (ZeroY > 0 && _gameField[ZeroX, ZeroY - 1] == value)
            {
                zeroDown();
                return true;
            }
            if (ZeroY < 3 && _gameField[ZeroX, ZeroY + 1] == value)
            {
                zeroUp();
                return true;
            }
            return false;
        }

        private void Check_it(int[] arr)
        {
            int inv = Inv_puzzle(arr);
            if (typeOfGame == 0)
                if (inv % 2 == 0)
                    return;
                else
                    Shuffle(arr);
            if (typeOfGame == 1)
                if (inv % 2 == 1)
                    return;
                else
                    Shuffle(arr);
        }
        private int Inv_puzzle(int[] arr1)
        {
            int inv = 0;
            for (int i = 0; i < 16; ++i)
                if (arr1[i] != 0)
                    for (int j = 0; j < i; ++j)
                        if (arr1[j] > arr1[i])
                            ++inv;
            for (int i = 0; i < 16; ++i)
                if (arr1[i] == 0)
                    inv += 1 + i / 4;
            return inv;
        }
        private void Shuffle(int[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                int r = rnd.Next(arr.Length);
                int tmp = arr[i];
                arr[i] = arr[r];
                arr[r] = tmp;
            }
        }
        public int this[int x, int y]
        {
            get => _gameField[y, x];
        }
    }
}
