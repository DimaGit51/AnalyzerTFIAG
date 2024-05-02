namespace Dvoryanchikov
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
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
            this.button1 = new System.Windows.Forms.Button();
            this.TextBox_Chain = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.info = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(423, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(198, 42);
            this.button1.TabIndex = 9;
            this.button1.Text = "Анализ";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TextBox_Chain
            // 
            this.TextBox_Chain.Location = new System.Drawing.Point(48, 36);
            this.TextBox_Chain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBox_Chain.Name = "TextBox_Chain";
            this.TextBox_Chain.Size = new System.Drawing.Size(573, 123);
            this.TextBox_Chain.TabIndex = 8;
            this.TextBox_Chain.Text = "";
            this.TextBox_Chain.TextChanged += new System.EventHandler(this.TextBox_Chain_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 429);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 11;
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.info.Location = new System.Drawing.Point(43, 381);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(86, 25);
            this.info.TabIndex = 16;
            this.info.Text = "Ошибки";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(651, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(561, 421);
            this.dataGridView1.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1383, 543);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.info);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBox_Chain);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Анализатор";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox TextBox_Chain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label info;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

