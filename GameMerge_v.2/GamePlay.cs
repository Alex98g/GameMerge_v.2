using System.Diagnostics;

namespace GameMerge_v._2
{
    public partial class GamePlay : Form
    {
        private const int GridSize = 8;
        private const int CellSize = 50; // Adjust the cell size as needed

        private GameElement currentlyDraggedElement = null;
        private GameElement currentlyClickedElement = null;
        private int Count_Element_OnField = 0;
        private Random random = new Random();
        private GameElement field;
        private int total_score = 0;
        private int count_orders = 0;
        public GamePlay()
        {
            InitializeComponent();
            // Set the form properties
            this.Text = "Game Screen";
            label_score.Text = total_score.ToString();
            Panel_Orders.SelectedIndexChanged += itemActivate;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    putWhiteElements(new GameElement(0, Color.White, 0), new Point(col * 50, row * 50));
                }
            }

            for (int i = 0; i < 8; i++) //adding 8 blue element 1 level
            {
                GameElement checkfield = (GameElement)panel1.Controls[random.Next(64)];
                CheckFieldHasElement(checkfield);
                PutElements(null, field, new GameElement(1, Color.Blue, 0));
            }
            for (int i = 0; i < 2; i++) // Adding 2 orders
            {
                AddOrders();
            }
        }
        private void AddOrders() //Finction of adding orders
        {
            int item = random.Next(0, 11);
            Items_Panel_Order itemPanelOrder = null;
            if ((item == 0) || (item == 4) || (item == 8))
            {
                itemPanelOrder = new Items_Panel_Order(item, "level 1", 1);
            }
            if ((item == 1) || (item == 5) || (item == 9))
            {
                itemPanelOrder = new Items_Panel_Order(item, "level 2", 3);
            }
            if ((item == 2) || (item == 6) || (item == 10))
            {
                itemPanelOrder = new Items_Panel_Order(item, "level 3", 9);
            }
            if ((item == 3) || (item == 7) || (item == 11))
            {
                itemPanelOrder = new Items_Panel_Order(item, "level 4", 27);
            }
            Panel_Orders.Items.Add(itemPanelOrder);
        }

        private void itemActivate(Object sender, EventArgs e) //event click on order
        {
            if (Panel_Orders.SelectedItems.Count > 0)
            {
                Items_Panel_Order selectedItem = (Items_Panel_Order)Panel_Orders.SelectedItems[0];
                int selectedText = selectedItem.ImageIndex;
                foreach (GameElement itemGameField in panel1.Controls)
                {
                    if (itemGameField.BackColor != Color.White)
                    {
                        //GameElement itemGameElement = (GameElement)itemGameField.Controls[0];
                        if (itemGameField.ImageIndex == selectedItem.ImageIndex)
                        {
                            putWhiteElements(new GameElement(0, Color.White, 0), itemGameField.Location);
                            panel1.Controls.Remove(itemGameField);
                            Panel_Orders.Items.Remove(selectedItem);
                            count_orders += 1;
                            AddOrders();
                            total_score += selectedItem.Score;
                            label_score.Text = total_score.ToString();
                            checkwin();
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }
        private void checkwin() // checking win
        {
            if (count_orders == 10)
            {
                Win win = new Win(total_score);
                this.Hide();
                win.Show();
            }
        }
        private void putWhiteElements(GameElement fields, Point locationFields)
        {
            fields.Location = locationFields;
            fields.DragEnter += DestinationPanel_DragEnter;
            fields.DragDrop += DestinationPanel_DragDrop;
            panel1.Controls.Add(fields);
        }

        private void DestinationPanel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void DestinationPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(GameElement)))
            {
                GameElement fieldsOfElement = currentlyDraggedElement;
                GameElement fieldsOfElement1 = (GameElement)sender;
                Point locationfieldsOfElement = fieldsOfElement.Location;
                if (fieldsOfElement1.BackColor == Color.White)
                {
                    currentlyDraggedElement.Location = fieldsOfElement1.Location;
                    fieldsOfElement1.Location = locationfieldsOfElement;
                }
            }
        }
        private void CheckFieldHasElement(GameElement whiteElement)// check if field has element
        {
            if (Count_Element_OnField < 64)
            {
                Debug.WriteLine(whiteElement.BackColor);
                if ((whiteElement.BackColor != Color.White)
                    || (whiteElement.BackColor == Color.Blue) || (whiteElement.BackColor == Color.Green) || (whiteElement.BackColor == Color.Red))
                {
                    whiteElement = (GameElement)panel1.Controls[random.Next(64)];
                    CheckFieldHasElement(whiteElement);
                } else
                {
                    field = whiteElement;
                    if (currentlyClickedElement != null)
                    {
                        PutNewElementsColor(currentlyClickedElement);
                    }
                }
            }
            else
            {
                MessageBox.Show("Here isn't place");
            }

        }


        private void Element_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (e.Clicks == 1))
            {
                currentlyDraggedElement = (GameElement)sender;
                currentlyDraggedElement.DoDragDrop(currentlyDraggedElement, DragDropEffects.Move);
            }
        }
        private void Element_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private async void Element_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(GameElement)))
            {
                GameElement targetElement = (GameElement)sender;
                Point locationtargetElement = targetElement.Location;
                Point locationcurrentlyDraggedElement = currentlyDraggedElement.Location;
                if (currentlyDraggedElement == targetElement)
                {
                    Debug.WriteLine("Одинаковые элементы");
                }
                else
                {
                    if ((currentlyDraggedElement.Level == targetElement.Level)
                        && (currentlyDraggedElement.BackColor == targetElement.BackColor)
                        && (currentlyDraggedElement.Level < 4))
                    {

                        panel1.Controls.Remove(currentlyDraggedElement);
                        panel1.Controls.Remove(targetElement);

                        Count_Element_OnField -= 1;
                        GameElement element_newLvl = new GameElement(currentlyDraggedElement.Level + 1, currentlyDraggedElement.BackColor, currentlyDraggedElement.ImageIndex + 1);
                        if (element_newLvl.Level == 4)
                        {
                            element_newLvl.MouseDoubleClick += Element_4Lvl_MouseDoubleClick;
                        }
                        element_newLvl.MouseDown += Element_MouseDown;
                        element_newLvl.DragEnter += Element_DragEnter;
                        element_newLvl.DragDrop += Element_DragDrop;
                        element_newLvl.Location = locationtargetElement;
                        putWhiteElements(new GameElement(0, Color.White, 0), locationcurrentlyDraggedElement);
                        panel1.Controls.Add(element_newLvl);

                    }
                    else
                    {
                        Point initialLocation = locationcurrentlyDraggedElement;
                        Debug.WriteLine(initialLocation);
                        Point finalLocation = locationtargetElement;
                        int animationSteps = 10; // Number of animation steps
                        int currentStep = 0;
                        Size stepSize = new Size(
                            (finalLocation.X - initialLocation.X) / animationSteps,
                            (finalLocation.Y - initialLocation.Y) / animationSteps
                        );

                        System.Windows.Forms.Timer animationTimer = new System.Windows.Forms.Timer();
                        animationTimer.Interval = 100; // Adjust the animation speed
                        animationTimer.Tick += (s, args) =>
                        {
                            if (currentStep < animationSteps)
                            {
                                panel1.Enabled = false;
                                currentlyDraggedElement.BringToFront();
                                currentlyDraggedElement.Location = new Point(
                                    currentlyDraggedElement.Location.X + stepSize.Width,
                                    currentlyDraggedElement.Location.Y + stepSize.Height
                                );
                                
                                targetElement.BringToFront();
                                targetElement.Location = new Point(
                                    targetElement.Location.X - stepSize.Width,
                                    targetElement.Location.Y - stepSize.Height
                                );
                                currentStep++;
                            }
                            else
                            {
                                animationTimer.Stop();
                                panel1.Enabled = true;
                            }

                        };
                        animationTimer.Start();
                    }
                }
            }
        }
        private void Element_4Lvl_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (e.Clicks == 2))
            {
                currentlyClickedElement = (GameElement)sender;
                GameElement whiteElement = (GameElement)panel1.Controls[random.Next(64)];
                CheckFieldHasElement(whiteElement);
                

            }
        }
        private void PutElements(GameElement prevElement, GameElement checkfield, GameElement element)
        {
            Point locationtargetElement = checkfield.Location;
            if (prevElement != null)
            {
                element.Location = new Point(prevElement.Location.X, prevElement.Location.Y - 10);

                element.MouseDown += Element_MouseDown;
                element.DragEnter += Element_DragEnter;
                element.DragDrop += Element_DragDrop;
                Count_Element_OnField += 1;
                panel1.Controls.Add(element);
                element.BringToFront();

                Point initialLocation = element.Location;
                Debug.WriteLine(initialLocation);
                Point finalLocation = locationtargetElement;
                int animationSteps = 10; // Number of animation steps
                int currentStep = 0;
                Size stepSize = new Size(
                    (finalLocation.X - initialLocation.X) / animationSteps,
                    (finalLocation.Y - initialLocation.Y) / animationSteps
                );

                System.Windows.Forms.Timer animationTimer = new System.Windows.Forms.Timer();
                animationTimer.Interval = 100; // Adjust the animation speed
                animationTimer.Tick += (s, args) =>
                {
                    if (currentStep < animationSteps)
                    {
                        element.Enabled = false;
                        
                        element.Location = new Point(
                            element.Location.X + stepSize.Width,
                            element.Location.Y + stepSize.Height
                        );
                        currentStep++;
                        panel1.Controls.Remove(checkfield);
                    }
                    else
                    {
                        animationTimer.Stop();
                        element.Enabled = true;
                        
                    }

                };
                animationTimer.Start();


            }
            else
            {
                element.Location = checkfield.Location;
                panel1.Controls.Remove(checkfield);
                element.MouseDown += Element_MouseDown;
                element.DragEnter += Element_DragEnter;
                element.DragDrop += Element_DragDrop;
                Count_Element_OnField += 1;
                panel1.Controls.Add(element);
            }
        }
        private void PutNewElementsColor(GameElement prevElement)
        {
            switch (prevElement.BackColor.Name)
            {
                case "Blue":
                    if (random.Next(1, 100) > 20)
                    {
                        PutElements(prevElement, field, new GameElement(1, Color.Green, 4));
                    }
                    else
                    {
                        PutElements(prevElement, field, new GameElement(1, Color.Red, 8));
                    }
                    break;
                case "Green":
                    if (random.Next(1, 100) > 20)
                    {
                        PutElements(prevElement, field, new GameElement(1, Color.Red, 8));
                    }
                    else
                    {
                        PutElements(prevElement, field, new GameElement(1, Color.Blue, 0));
                    }
                    break;
                case "Red":
                    if (random.Next(1, 100) > 20)
                    {
                        PutElements(prevElement, field, new GameElement(1, Color.Blue, 0));
                    }
                    else
                    {
                        PutElements(prevElement, field, new GameElement(1, Color.Green, 4));
                    }
                    break;
            }
        }
    }
}