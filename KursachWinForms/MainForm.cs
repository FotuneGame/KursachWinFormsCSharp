using Engine;
using Engine.Object;

using KursachWinForms.CustomForm;

using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace KursachWinForms
{
    public partial class MainForm : Form
    {

        public PictureBox window_draw;

        private int[] pixels;
        private EngineRender engine;
        private readonly int width, height, depth; // глубина прорисовки
        private double step_grid;//шаг сетки
        private char mode_camera; // 'p' - смена позиции камеры, 'r' -  поворота камеры, f (final) - конечный рендер



        public MainForm()
        {
            InitializeComponent();
            width = depth = height = this.Height;
            Start();
        }

        // создание объектов
        public void Start()
        {
            // камера по умолчанию на режиме поворота
            mode_camera = 'r';
            //шаг сетки по умолчанию
            step_grid = 0.1;

            //инициализация движка в этом окне
            window_draw = new PictureBox();
            window_draw.BackColor = Color.White; // цвет фона
            window_draw.Width = width;  // ширина элемента 
            window_draw.Height = height; // высота элемента 
            window_draw.Paint += new PaintEventHandler(window_Paint);
            window_draw.MouseClick += new MouseEventHandler(window_Click);
            

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.Controls.Add(window_draw); // добавляем PictureBox на форму

            //инициализируем движок набором пикселей выостой и шириной фото
            pixels = new int[window_draw.Width * window_draw.Height];
            engine = new EngineRender(pixels, window_draw.Width, window_draw.Height, depth, step_grid, window_draw.BackColor, true);

            engine.MainCamera.angle.set(0, 0, 0);
            label_step_grid.Invalidate();
            label_camera_rot.Invalidate();
        }




        // обновление отрисовки картинки с 3д сценой
        private void window_Paint(object sender, PaintEventArgs e)
        {
            engine.step_grid = step_grid;
            engine.RenderAndSelect();
            // Отображение массива пикселей на window_draw
            Bitmap bitmap = new Bitmap(width, height);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bitmap.UnlockBits(bitmapData);

            e.Graphics.DrawImageUnscaled(bitmap, new Point(0, 0));
        }

        // проверка позиции мыши на 3д сцене
        private void window_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                if (!this.Controls.ContainsKey("window_add_obj"))
                {
                    DefaultModelMenu add_obj_box = new DefaultModelMenu(engine,window_draw, new Point(e.X, e.Y),step_grid);
                    this.Controls.Add(add_obj_box);
                    add_obj_box.BringToFront();
                }
                else
                {
                    var add_obj_box = this.Controls["window_add_obj"];
                    add_obj_box.Location = new Point(e.X, e.Y);
                    add_obj_box.BringToFront();
                }
            }
            else
            {
                if (this.Controls.ContainsKey("window_add_obj"))
                    this.Controls.Remove(this.Controls["window_add_obj"]);
            }

            if (e.Button == MouseButtons.Left)
            {
                engine.ClearSelectEobjects();
                EObject select_obj = engine.RenderAndSelect(e.X,e.Y);
                if (select_obj != null)
                {
                    // если окно было то пересоздаем его
                    if (this.Controls.ContainsKey("component_menu"))
                        this.Controls.Remove(this.Controls["component_menu"]);
                   
                    ComponentMenu componentMenu = new ComponentMenu(this,engine,window_draw,select_obj,new Point(width,0),new Size(200,this.Height));
                    this.Controls.Add(componentMenu);
                    componentMenu.BringToFront();
                }
                else
                {
                    engine.ClearSelectEobjects();
                    if (this.Controls.ContainsKey("component_menu"))
                        this.Controls.Remove(this.Controls["component_menu"]);
                }
                window_draw.Invalidate();
            }

            this.Focus(); // вернуть фокус ввода на форму
        }

    }
}
