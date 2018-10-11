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
        int typeOfGame = 0;
        private int[,] _gameField = new int[4, 4];
        public int zeroX { get; private set; }
        public int zeroY { get; private set; }
        public void GetTypeOfGame(int gameType)
        { typeOfGame = gameType; }
        public bool IsWin()
        {
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };
            if (typeOfGame == 0)
            {
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (arr[i * 4 + j] != _gameField[j, i])
                            return false;
                return true;
            }
            if (typeOfGame == 1)
            {
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (arr[i * 4 + j] != _gameField[i, j])
                            return false;
                return true;
            }
            if (typeOfGame == 2)
            {
                bool flag = true;
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (arr[i * 4 + j] != _gameField[j, i])
                            flag = false;
                if (flag == false)
                {
                    flag = true;
                    for (int i = 0; i < 4; i++)
                        for (int j = 0; j < 4; j++)
                            if (arr[i * 4 + j] != _gameField[i, j])
                                flag = false;
                }
                return flag;
            }
            return false;
        }
        public void zeroUp()
        {
            if (zeroY == 3) return;
            _gameField[zeroX, zeroY] = _gameField[zeroX, zeroY + 1];
            ++zeroY;
            _gameField[zeroX, zeroY] = 0;

        }
        public void zeroDown()
        {
            if (zeroY == 0) return;

            _gameField[zeroX, zeroY] = _gameField[zeroX, zeroY - 1];
            --zeroY;
            _gameField[zeroX, zeroY] = 0;
        }
        public void zeroLeft()
        {
            if (zeroX == 3) return;
            _gameField[zeroX, zeroY] = _gameField[zeroX + 1, zeroY];
            ++zeroX;
            _gameField[zeroX, zeroY] = 0;
        }
        public void zeroRight()
        {
            if (zeroX == 0) return;
            _gameField[zeroX, zeroY] = _gameField[zeroX - 1, zeroY];
            --zeroX;
            _gameField[zeroX, zeroY] = 0;
        }
        public GameLogic()
        {
            InitGame();
        }
        public void InitGame()
        {
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };
            Shuffle(arr);
            check_it(arr);
            for (int n = 0, i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j, ++n)
                {
                    _gameField[j, i] = arr[n];
                    if (arr[n] == 0)
                    {
                        zeroX = j;
                        zeroY = i;
                    }
                }
            }
        }

        public bool CheckAndGo(int value)
        {
            if (zeroX > 0 && _gameField[zeroX - 1, zeroY] == value)
            {
                zeroRight();
                return true;
            }
            if (zeroX < 3 && _gameField[zeroX + 1, zeroY] == value)
            {
                zeroLeft();
                return true;
            }
            if (zeroY > 0 && _gameField[zeroX, zeroY - 1] == value)
            {
                zeroDown();
                return true;
            }
            if (zeroY < 3 && _gameField[zeroX, zeroY + 1] == value)
            {
                zeroUp();
                return true;
            }
            return false;
        }

        private void check_it(int[] arr)
        {
            int inv = inv_puzzle(arr);
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
        private int inv_puzzle(int[] arr1)
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
