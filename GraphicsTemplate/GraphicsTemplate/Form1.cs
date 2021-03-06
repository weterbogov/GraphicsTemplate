﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsTemplate
{
    public partial class Form1 : Form
    {
        private enum CurrentDrawing
        {
            Line,
            Ray,
            LineSegment,
            Ellipse,
            Circle,
            Poligon,
            RegularPolygon,
            Parallelogram,
            Rectangle,
            Rhomb,
            IsoscelesTriangle,
            RightTriangle
        }

        private CurrentDrawing drawing = CurrentDrawing.Line;
        bool drawingEnabled = false;
        bool drawingByPoints = false;
        bool moving = false;

        Shape shape;

        Color currentBorderColor = Color.Red;
        Color currentFillColor = Color.Red;
        Point firstPoint;
        decimal tapsNumber = 0;
        List<Shape> shapes = new List<Shape>();
        Graphics formGraphics;
        private Shape selectedItem;
        private Color lastSelectedItemColor;

        public Form1()
        {
            InitializeComponent();
            this.shape = new Line();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.formGraphics = this.panel1.CreateGraphics();
        }

        private void Reload()
        {
            foreach (var shape in shapes)
            {
                shape.draw(this.formGraphics);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            this.formGraphics.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            drawing = CurrentDrawing.Line;
            drawingByPoints = false;
            this.moving = false;
        }

        private void createCurrentShape()
        {
            switch (drawing)
            {
                case CurrentDrawing.Ellipse:
                    Ellipse ellipse = new Ellipse();
                    ellipse.setFillColor(this.currentFillColor);
                    ellipse.setBorderColor(this.currentBorderColor);
                    this.shape = ellipse;
                    break;
                case CurrentDrawing.Circle:
                    Circle circle = new Circle();
                    circle.setFillColor(this.currentFillColor);
                    circle.setBorderColor(this.currentBorderColor);
                    this.shape = circle;
                    break;
                case CurrentDrawing.Line:
                    Shape line = new Line();
                    line.setBorderColor(this.currentBorderColor);
                    this.shape = line;
                    break;
                case CurrentDrawing.Ray:
                    Shape ray = new Ray();
                    ray.setBorderColor(this.currentBorderColor);
                    this.shape = ray;
                    break;
                case CurrentDrawing.LineSegment:
                    Shape lineSegment = new LineSegment();
                    lineSegment.setBorderColor(this.currentBorderColor);
                    this.shape = lineSegment;
                    break;
                case CurrentDrawing.Poligon:
                    Poligon poligon = new Poligon();
                    poligon.setFillColor(this.currentFillColor);
                    poligon.setBorderColor(this.currentBorderColor);
                    this.shape = poligon;
                    break;
                case CurrentDrawing.RegularPolygon:
                    RegularPolygon regularPoligon = new RegularPolygon((int)this.numericUpDown1.Value);
                    regularPoligon.setFillColor(this.currentFillColor);
                    regularPoligon.setBorderColor(this.currentBorderColor);
                    this.shape = regularPoligon;
                    break;
                case CurrentDrawing.RightTriangle:
                    RightTriangle rightTriangle = new RightTriangle();
                    rightTriangle.setFillColor(this.currentFillColor);
                    rightTriangle.setBorderColor(this.currentBorderColor);
                    this.shape = rightTriangle;
                    break;
                case CurrentDrawing.IsoscelesTriangle:
                    IsoscelesTriangle isoscelesTriangle = new IsoscelesTriangle();
                    isoscelesTriangle.setFillColor(this.currentFillColor);
                    isoscelesTriangle.setBorderColor(this.currentBorderColor);
                    this.shape = isoscelesTriangle;
                    break;
                case CurrentDrawing.Parallelogram:
                    Parallelogram parallelogram = new Parallelogram();
                    parallelogram.setFillColor(this.currentFillColor);
                    parallelogram.setBorderColor(this.currentBorderColor);
                    this.shape = parallelogram;
                    break;
                case CurrentDrawing.Rectangle:
                    Rectangle rectangle = new Rectangle();
                    rectangle.setFillColor(this.currentFillColor);
                    rectangle.setBorderColor(this.currentBorderColor);
                    this.shape = rectangle;
                    break;
                case CurrentDrawing.Rhomb:
                    Rhomb rhomb = new Rhomb();
                    rhomb.setFillColor(this.currentFillColor);
                    rhomb.setBorderColor(this.currentBorderColor);
                    this.shape = rhomb;
                    break;
            }
        }

        private void FalseAll()
        {
            drawingEnabled = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (tapsNumber >= 3)
                {
                    this.shapes.Add(this.shape);
                    this.listBox1.Items.Add("(" + this.shapes.Count + ")" + this.shape.GetType().ToString() + "(" + this.shape.getCenter().X + "," + this.shape.getCenter().Y + ")");
                    this.shape.draw(this.formGraphics);
                    this.tapsNumber = 0;
                    this.shape = null;
                    this.drawingEnabled = false;
                }
                this.FalseAll();
                this.formGraphics.Clear(Color.White);
                this.Reload();
                return;
            }
            drawingEnabled = true;
            if (moving)
            {
                this.shape = this.selectedItem;
                firstPoint = e.Location;
                this.shape.move(e.Location);
            }
            else
            {
                if (!this.drawingByPoints)
                {

                    this.createCurrentShape();
                    firstPoint = e.Location;
                    this.shape.move(e.Location);


                }
                else
                {
                    if (this.tapsNumber == 0)
                        this.createCurrentShape();
                    bool canRedraw = this.shape.tapOnCreate(e.Location);
                    if (canRedraw)
                    {
                        this.shapes.Add(this.shape);
                        this.listBox1.Items.Add("(" + this.shapes.Count + ")" + this.shape.GetType().ToString() + "(" + this.shape.getCenter().X + "," + this.shape.getCenter().Y + ")");
                        this.shape = null;
                        this.drawingEnabled = false;
                        this.formGraphics.Clear(Color.White);
                        this.Reload();
                        this.tapsNumber = 0;
                    }
                    else
                    {
                        this.tapsNumber++;
                    }
                }
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawingEnabled && !drawingByPoints && !moving)
            {
                this.shape.dragOnCreate(firstPoint, e.Location);
                this.formGraphics.Clear(Color.White);
                this.Reload();
                this.shape.draw(this.formGraphics);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawingEnabled && !moving)
            {
                if (!drawingByPoints)
                {
                    this.shapes.Add(this.shape);
                    this.listBox1.Items.Add("(" + this.shapes.Count + ")" + this.shape.GetType().ToString() + "(" + this.shape.getCenter().X + "," + this.shape.getCenter().Y + ")");
                    this.shape = null;
                    this.drawingEnabled = false;
                }
            }
            else if(moving)
            {
                this.formGraphics.Clear(Color.White);
                this.shape = null;
                this.Reload();
                this.drawingEnabled = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.Reload();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.currentBorderColor = this.colorDialog1.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.currentFillColor = this.colorDialog1.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            drawing = CurrentDrawing.Ellipse;
            drawingByPoints = false;
            this.moving = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            drawing = CurrentDrawing.Ray;
            drawingByPoints = false;
            this.moving = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            drawing = CurrentDrawing.LineSegment;
            drawingByPoints = false;
            this.moving = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            drawing = CurrentDrawing.Poligon;
            drawingByPoints = true;
            this.moving = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            drawing = CurrentDrawing.Circle;
            this.createCurrentShape();
            drawingByPoints = false;
            this.moving = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                this.shapes.RemoveAt(this.listBox1.SelectedIndex);
                this.listBox1.Items.Remove(this.listBox1.SelectedItem);
                this.selectedItem = null;
                this.lastSelectedItemColor = Color.AliceBlue;
                this.formGraphics.Clear(Color.White);
                this.moving = false;
                this.Reload();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                if (this.selectedItem != null)
                {
                    this.selectedItem.setBorderColor(this.lastSelectedItemColor);
                }
                selectedItem = this.shapes.ElementAt(this.listBox1.SelectedIndex);
                this.moving = true;
                this.shape = selectedItem;
                this.lastSelectedItemColor = selectedItem.getBorderColor();
                selectedItem.setBorderColor(Color.Aqua);
                this.formGraphics.Clear(Color.White);
                this.Reload();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            drawing = CurrentDrawing.RegularPolygon;
            drawingByPoints = false;
            this.moving = false;
            this.createCurrentShape();
        }
        // private void button10_Click(object sender, EventArgs e)
        // {
        //     drawing = CurrentDrawing.Parallelogram;
        //     drawingByPoints = true;
        // }

        private void button11_Click(object sender, EventArgs e)
        {
            drawing = CurrentDrawing.RightTriangle;
            drawingByPoints = false;
            this.moving = false;
            this.createCurrentShape();
        }
        // private void button11_Click(object sender, EventArgs e)
        // {
        //     drawing = CurrentDrawing.Rectangle;
        //     drawingByPoints = true;
        // }

        private void button12_Click(object sender, EventArgs e)
        {
            drawing = CurrentDrawing.IsoscelesTriangle;
            drawingByPoints = false;
            this.moving = false;
            this.createCurrentShape();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            drawing = CurrentDrawing.Rectangle;
            drawingByPoints = true;
            this.moving = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            drawing = CurrentDrawing.Rhomb;
            drawingByPoints = true;
            this.moving = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            drawing = CurrentDrawing.Parallelogram;
            drawingByPoints = true;
            this.moving = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.shapes.Clear();
            this.listBox1.Items.Clear();
            this.formGraphics.Clear(Color.White);
            this.moving = false;
            this.Reload();
        }
        // private void button12_Click(object sender, EventArgs e)
        // {
        //     drawing = CurrentDrawing.Rhomb;
        //     drawingByPoints = true;
        // }
    }
}
