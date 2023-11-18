using Engine;

namespace KursachWinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_step_grid = new System.Windows.Forms.Label();
            this.label_camera_rot = new System.Windows.Forms.Label();
            this.label_camera_pos = new System.Windows.Forms.Label();
            this.label_camera_mode = new System.Windows.Forms.Label();
            this.label_camera_zoom = new System.Windows.Forms.Label();
            this.label_render = new System.Windows.Forms.Label();
            this.label_for_render = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_step_grid
            // 
            this.label_step_grid.AutoSize = true;
            this.label_step_grid.Location = new System.Drawing.Point(456, 22);
            this.label_step_grid.Name = "label_step_grid";
            this.label_step_grid.Size = new System.Drawing.Size(95, 13);
            this.label_step_grid.TabIndex = 0;
            this.label_step_grid.Text = "Шаг сетки(+-): 0.1";
            // 
            // label_camera_rot
            // 
            this.label_camera_rot.AutoSize = true;
            this.label_camera_rot.Location = new System.Drawing.Point(12, 22);
            this.label_camera_rot.Name = "label_camera_rot";
            this.label_camera_rot.Size = new System.Drawing.Size(192, 13);
            this.label_camera_rot.TabIndex = 1;
            this.label_camera_rot.Text = "Угол камеры(ad/ws/qe)(x/y/z):(0,0,0)";
            // 
            // label_camera_pos
            // 
            this.label_camera_pos.AutoSize = true;
            this.label_camera_pos.Location = new System.Drawing.Point(12, 9);
            this.label_camera_pos.Name = "label_camera_pos";
            this.label_camera_pos.Size = new System.Drawing.Size(211, 13);
            this.label_camera_pos.TabIndex = 2;
            this.label_camera_pos.Text = "Позиция камеры(ad/ws/qe)(x/y/z):(0,0,0)";
            // 
            // label_camera_mode
            // 
            this.label_camera_mode.AutoSize = true;
            this.label_camera_mode.Location = new System.Drawing.Point(456, 35);
            this.label_camera_mode.Name = "label_camera_mode";
            this.label_camera_mode.Size = new System.Drawing.Size(105, 13);
            this.label_camera_mode.TabIndex = 3;
            this.label_camera_mode.Text = "Режим(m): Поворот";
            // 
            // label_camera_zoom
            // 
            this.label_camera_zoom.AutoSize = true;
            this.label_camera_zoom.Location = new System.Drawing.Point(456, 9);
            this.label_camera_zoom.Name = "label_camera_zoom";
            this.label_camera_zoom.Size = new System.Drawing.Size(105, 13);
            this.label_camera_zoom.TabIndex = 4;
            this.label_camera_zoom.Text = "Приближение(zx): 1";
            // 
            // label_render
            // 
            this.label_render.AutoSize = true;
            this.label_render.Location = new System.Drawing.Point(266, 9);
            this.label_render.Name = "label_render";
            this.label_render.Size = new System.Drawing.Size(0, 13);
            this.label_render.TabIndex = 5;
            // 
            // label_for_render
            // 
            this.label_for_render.AutoSize = true;
            this.label_for_render.Location = new System.Drawing.Point(456, 48);
            this.label_for_render.Name = "label_for_render";
            this.label_for_render.Size = new System.Drawing.Size(88, 13);
            this.label_for_render.TabIndex = 6;
            this.label_for_render.Text = "Для рендера (v)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label_for_render);
            this.Controls.Add(this.label_render);
            this.Controls.Add(this.label_camera_zoom);
            this.Controls.Add(this.label_camera_mode);
            this.Controls.Add(this.label_camera_pos);
            this.Controls.Add(this.label_camera_rot);
            this.Controls.Add(this.label_step_grid);
            this.Name = "MainForm";
            this.Text = "Титов Г. А. о721б";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_step_grid;
        private System.Windows.Forms.Label label_camera_rot;
        private System.Windows.Forms.Label label_camera_pos;
        private System.Windows.Forms.Label label_camera_mode;
        private System.Windows.Forms.Label label_camera_zoom;
        private System.Windows.Forms.Label label_render;
        private System.Windows.Forms.Label label_for_render;
    }
}

