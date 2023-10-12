using System.Collections.Generic;

namespace UnityEngine.UI
{
    [AddComponentMenu("UI/Effects/TextSpacing")]
    public class TextSpacing : BaseMeshEffect
    {
        #region Struct
        public enum HorizontalAligmentType
        {
            Left,
            Center,
            Right
        }

        public class Line
        {
            // Start index
            public int StartVertexIndex
            {
                get
                {
                    return _startVertexIndex;
                }
            }
            private int _startVertexIndex = 0;

            // End intex
            public int EndVertexIndex
            {
                get
                {
                    return _endVertexIndex;
                }
            }
            private int _endVertexIndex = 0;

            // Vertex count
            public int VertexCount
            {
                get
                {
                    return _vertexCount;
                }
            }
            private int _vertexCount = 0;

            public Line(int startVertexIndex, int length)
            {
                _startVertexIndex = startVertexIndex;
                _endVertexIndex = length * 6 - 1 + startVertexIndex;
                _vertexCount = length * 6;
            }
        }
        #endregion

        public float Spacing = 1f;

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive() || vh.currentVertCount == 0)
            {
                return;
            }

            var text = GetComponent<Text>();

            if (text == null)
            {
                Debug.LogError("Missing Text component");
                return;
            }

            // Horizontall aligment function
            HorizontalAligmentType alignment;
            if (text.alignment == TextAnchor.LowerLeft || text.alignment == TextAnchor.MiddleLeft || text.alignment == TextAnchor.UpperLeft)
            {
                alignment = HorizontalAligmentType.Left;
            }
            else if (text.alignment == TextAnchor.LowerCenter || text.alignment == TextAnchor.MiddleCenter || text.alignment == TextAnchor.UpperCenter)
            {
                alignment = HorizontalAligmentType.Center;
            }
            else
            {
                alignment = HorizontalAligmentType.Right;
            }

            var vertexs = new List<UIVertex>();
            vh.GetUIVertexStream(vertexs);
            // var indexCount = vh.currentIndexCount;

            var lineTexts = text.text.Split('\n');

            var lines = new Line[lineTexts.Length];

            // Calculate the index of the first point in each line according to the length of each element in the lines array, each word, letter, and empty letter occupy 6 points 
            for (var i = 0; i < lines.Length; i++)
            {
                // Except for the last line, vertexs have carriage returns for the first few lines and occupy 6 points 
                if (i == 0)
                {
                    lines[i] = new Line(0, lineTexts[i].Length + 1);
                }
                else if (i > 0 && i < lines.Length - 1)
                {
                    lines[i] = new Line(lines[i - 1].EndVertexIndex + 1, lineTexts[i].Length + 1);
                }
                else
                {
                    lines[i] = new Line(lines[i - 1].EndVertexIndex + 1, lineTexts[i].Length);
                }
            }

            UIVertex vt;

            for (var i = 0; i < lines.Length; i++)
            {
                for (var j = lines[i].StartVertexIndex; j <= lines[i].EndVertexIndex; j++)
                {
                    if (j < 0 || j >= vertexs.Count)
                    {
                        continue;
                    }
                    vt = vertexs[j];

                    var charCount = lines[i].EndVertexIndex - lines[i].StartVertexIndex;
                    if (i == lines.Length - 1)
                    {
                        charCount += 6;
                    }

                    if (alignment == HorizontalAligmentType.Left)
                    {
                        vt.position += new Vector3(Spacing * ((j - lines[i].StartVertexIndex) / 6), 0, 0);
                    }
                    else if (alignment == HorizontalAligmentType.Right)
                    {
                        vt.position += new Vector3(Spacing * (-(charCount - j + lines[i].StartVertexIndex) / 6 + 1), 0, 0);
                    }
                    else if (alignment == HorizontalAligmentType.Center)
                    {
                        var offset = (charCount / 6) % 2 == 0 ? 0.5f : 0f;
                        vt.position += new Vector3(Spacing * ((j - lines[i].StartVertexIndex) / 6 - charCount / 12 + offset), 0, 0);
                    }

                    vertexs[j] = vt;
                    // The correspondence between the following attention points and indexes 
                    if (j % 6 <= 2)
                    {
                        vh.SetUIVertex(vt, (j / 6) * 4 + j % 6);
                    }
                    if (j % 6 == 4)
                    {
                        vh.SetUIVertex(vt, (j / 6) * 4 + j % 6 - 1);
                    }
                }
            }
        }
    }
}