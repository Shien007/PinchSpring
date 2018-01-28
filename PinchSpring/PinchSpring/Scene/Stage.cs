using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyLib.Device;
using MyLib.Utility;
using PinchSpring.Def;
using PinchSpring.Objects;
using System.Collections.Generic;

namespace PinchSpring.Scene
{
    enum E_ObjectType {
        None = -1,
        Player,
        Block,
    }
    class Stage
    {
        public static GameObject[,] mapData;
        private GameDevice gameDevice;
        private GraphicsDevice graphicsDevice;
        Dictionary<E_ObjectType, GameObject> objectList;

        public Stage(GameDevice gameDevice, GraphicsDevice graphicsDevice) {
            this.gameDevice = gameDevice;
            this.graphicsDevice = graphicsDevice;
            ProtoType();
        }

        private void ProtoType() {
            objectList = new Dictionary<E_ObjectType, GameObject>();
            objectList.Add(E_ObjectType.Player, new Player(gameDevice, Vector2.Zero, graphicsDevice));
            objectList.Add(E_ObjectType.Block, new Block("block", gameDevice, Vector2.Zero));
        }

        public void Load(string filename) {
            CSVReader.Read(filename);
            int[,] data = CSVReader.GetIntMatrix();
            mapData = new GameObject[data.GetLength(0), data.GetLength(1)];
            for (int num = 0; num < data.GetLength(0) * data.GetLength(1); num++) {
                int lineCount = num / data.GetLength(1);
                int colCount = num % data.GetLength(1);
                E_ObjectType type = (E_ObjectType)data[lineCount, colCount];
                if (type == E_ObjectType.None) {
                    mapData[lineCount, colCount] = null;
                    continue;
                }
                mapData[lineCount, colCount] = AddBlock(lineCount, colCount, type);
            }
        }

        private GameObject AddBlock(int lineCount, int colCount, E_ObjectType type) {
            GameObject newObject = (GameObject)objectList[type].Clone();
            newObject.Position = new Vector2(colCount * Parameter.TileSize, lineCount * Parameter.TileSize);
            return newObject;
        }

        public void Unload() { mapData = null; }

        public static GameObject[,] MapData { get { return mapData; } }
        public static int Map_XMax { get { return mapData.GetLength(1); } }
        public static int Map_YMax { get { return mapData.GetLength(0); } }

        public static Vector2 GetStageScale() {
            int width = Map_XMax * Parameter.TileSize;
            int height = Map_YMax * Parameter.TileSize;
            return new Vector2(width, height);
        }
    }
}
