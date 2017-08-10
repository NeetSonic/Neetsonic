namespace Neetsonic.Control
{
    partial class CheckedDateTimePicker
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.chk = new System.Windows.Forms.CheckBox();
            this.date = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // chk
            // 
            this.chk.AutoSize = true;
            this.chk.Dock = System.Windows.Forms.DockStyle.Left;
            this.chk.Location = new System.Drawing.Point(0, 0);
            this.chk.Name = "chk";
            this.chk.Size = new System.Drawing.Size(15, 26);
            this.chk.TabIndex = 0;
            this.chk.UseVisualStyleBackColor = true;
            this.chk.CheckedChanged += new System.EventHandler(this.Chk_CheckedChanged);
            // 
            // date
            // 
            this.date.Dock = System.Windows.Forms.DockStyle.Fill;
            this.date.Location = new System.Drawing.Point(15, 0);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(210, 26);
            this.date.TabIndex = 1;
            // 
            // CheckedDateTimePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.date);
            this.Controls.Add(this.chk);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(2000, 26);
            this.MinimumSize = new System.Drawing.Size(30, 26);
            this.Name = "CheckedDateTimePicker";
            this.Size = new System.Drawing.Size(225, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chk;
        private System.Windows.Forms.DateTimePicker date;
    }
}
