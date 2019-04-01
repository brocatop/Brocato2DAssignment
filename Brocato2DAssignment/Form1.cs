using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Brocato2DAssignment
{
    public partial class Form1 : Form
    {
        //Initialization variables
        //pointList to hold the points for drawing, the pen is created for users seeing where they are drawing, and the ready variables are meant to toggle if the
        //drawing panel is ready to be cleared or if it is ready to be drawn on
        List<Point> pointList = new List<Point>();
        Pen pen = new Pen(Color.Black, 2);
        bool readyToDraw = false;
        bool readyToClear = false;


        //Form Initialization
        //Sets the proper settings for how the UI needs to look, including instructions on the UI for how to use the application
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            LoadComboBox();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;
            label1.Text = "Step 1: Click inside the drawing panel to create a point";
            label2.Text = "Step 2: After you make your polygon choose your brush";
            label3.Text = "Step 3: Choose the color(s) of the polygon";
            label4.Text = "Step 4: Click the Start Over Button to Clear the canvas";
            label6.Text = "Step 5: Repeat and enjoy the program!  -PB";
        }

        //Method for drawing the drawing panel, allows the user to choose their type of brush and color before painting onto the drawing panel
        private void drawingPanel_Paint(object sender, PaintEventArgs e)
        {
            //Graphics object that will be used throughout the method
            Graphics g = e.Graphics;
            //If the user has clicked on the canvas, creating a point, then the following code allows the user to choose their polygon's colors and brush
            if (pointList.Count > 0)
            {
                //If the polygon is ready to be drawn
                if (readyToDraw == true)
                {
                    //Switch statement that takes in what the user decides is their brush from the combobox on the UI. By default, the program selects a solid brush
                    switch (comboBox1.SelectedIndex)
                    {
                        //Chooses the color for the solid brush
                        case 0:
                            MessageBox.Show("Please choose the color of your polygon");

                            ColorDialog solidColorDialog = new ColorDialog();

                            solidColorDialog.ShowDialog();

                            Brush solidBrush = new SolidBrush(solidColorDialog.Color);

                            g.FillPolygon(solidBrush, pointList.ToArray());
                            pointList.Clear();
                            pointList = new List<Point>();
                            readyToDraw = false;
                            readyToClear = true;
                            break;


                        //Chooses the two colors for the gradient effect of the polygon
                        case 1:
                            MessageBox.Show("Please choose the first color for your gradient polygon");

                            ColorDialog gradientDialog1 = new ColorDialog();

                            gradientDialog1.ShowDialog();

                            Color gradientColor1 = gradientDialog1.Color;

                            MessageBox.Show("Please choose the second color for your gradient polygon");

                            ColorDialog gradientDialog2 = new ColorDialog();

                            gradientDialog2.ShowDialog();

                            Color gradientColor2 = gradientDialog2.Color;

                            LinearGradientBrush brush = new LinearGradientBrush(pointList.ToArray()[0], pointList.ToArray()[pointList.ToArray().Length - 1], gradientColor1, gradientColor2);

                            g.FillPolygon(brush, pointList.ToArray());
                            pointList.Clear();
                            pointList = new List<Point>();
                            readyToDraw = false;
                            readyToClear = true;
                            break;

                        //Chooses the two colors for the horizontal hatch pattern of the polygon
                        case 2:
                            MessageBox.Show("Please choose the first color for your horizontal hatch polygon");

                            ColorDialog hatchDialog1 = new ColorDialog();

                            hatchDialog1.ShowDialog();

                            Color hatchColor1 = hatchDialog1.Color;

                            MessageBox.Show("Please choose the second color for your horizontal hatch polygon");

                            ColorDialog hatchDialog2 = new ColorDialog();

                            hatchDialog2.ShowDialog();

                            Color hatchColor2 = hatchDialog2.Color;

                            HatchBrush hatchBrush = new HatchBrush(HatchStyle.Horizontal, hatchColor1, hatchColor2);
                            g.FillPolygon(hatchBrush, pointList.ToArray());
                            pointList.Clear();
                            pointList = new List<Point>();
                            readyToDraw = false;
                            readyToClear = true;
                            break;


                        //Chooses the two colors for the forward diagonal hatch pattern of the polygon
                        case 3:
                            MessageBox.Show("Please choose the first color for your diagonal hatch polygon");

                            ColorDialog diagDialog1 = new ColorDialog();

                            diagDialog1.ShowDialog();

                            Color diagColor1 = diagDialog1.Color;

                            MessageBox.Show("Please choose the second color for your diagonal hatch polygon");
                            ColorDialog diagDialog2 = new ColorDialog();

                            diagDialog2.ShowDialog();

                            Color diagColor2 = diagDialog2.Color;

                            HatchBrush diagBrush = new HatchBrush(HatchStyle.ForwardDiagonal, diagColor1, diagColor2);
                            g.FillPolygon(diagBrush, pointList.ToArray());
                            pointList.Clear();
                            pointList = new List<Point>();
                            readyToDraw = false;
                            readyToClear = true;
                            break;


                        //Chooses the two colors for the cross hatch pattern of the polygon
                        case 4:
                            MessageBox.Show("Please choose the first color for your cross hatch polygon");

                            ColorDialog crossDialog1 = new ColorDialog();

                            crossDialog1.ShowDialog();

                            Color crossColor1 = crossDialog1.Color;

                            MessageBox.Show("Please choose the second color for your cross hatch polygon");

                            ColorDialog crossDialog2 = new ColorDialog();

                            crossDialog2.ShowDialog();

                            Color crossColor2 = crossDialog2.Color;

                            HatchBrush crossBrush = new HatchBrush(HatchStyle.Cross, crossColor1, crossColor2);
                            g.FillPolygon(crossBrush, pointList.ToArray());
                            pointList.Clear();
                            pointList = new List<Point>();
                            readyToDraw = false;
                            readyToClear = true;
                            break;
                    }

                }
                //If the polygon is not ready to be drawn
                else if (readyToDraw == false)
                {
                    foreach(Point p in pointList)
                    {
                        Rectangle r = new Rectangle(p.X, p.Y, 2, 2);
                        e.Graphics.DrawEllipse(pen, r);
                    }
                }
            }          
        }

        //When the user clicks on the drawing panel, add the point to the list of points that the polygon will be created from
        private void drawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            pointList.Add(p);
            drawingPanel.Refresh();
        }

        //When the user is satisfied with their polygon, then panel refreshes with their polygon
        private void drawPolygonButton_Click(object sender, EventArgs e)
        {
            if(readyToClear == true)
            {
                readyToClear = false;
                drawingPanel.Refresh();
            }
            else
            {
                readyToDraw = true;
                drawingPanel.Refresh();
            }
        }

        //Loads the combobox with the choices of brushes
        private void LoadComboBox()
        {
            comboBox1.Items.Add("Solid Brush");
            comboBox1.Items.Add("Gradient Brush");
            comboBox1.Items.Add("Horizontel Hatch Brush");
            comboBox1.Items.Add("Horizontel Diagonal Brush");
            comboBox1.Items.Add("Horizontel Cross Brush");
        }
    }
}
