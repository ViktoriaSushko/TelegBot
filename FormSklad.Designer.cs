namespace WinFormsApp2
{
    partial class FormSklad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            btnShow = new Button();
            btnAddPr = new Button();
            btnEditPr = new Button();
            btnDelPr = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(27, 22);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(718, 284);
            dataGridView1.TabIndex = 0;
            // 
            // btnShow
            // 
            btnShow.Location = new Point(52, 378);
            btnShow.Name = "btnShow";
            btnShow.Size = new Size(94, 29);
            btnShow.TabIndex = 1;
            btnShow.Text = "show";
            btnShow.UseVisualStyleBackColor = true;
            btnShow.Click += btnShow_Click;
            // 
            // btnAddPr
            // 
            btnAddPr.Location = new Point(186, 378);
            btnAddPr.Name = "btnAddPr";
            btnAddPr.Size = new Size(144, 29);
            btnAddPr.TabIndex = 2;
            btnAddPr.Text = "Add product";
            btnAddPr.UseVisualStyleBackColor = true;
            btnAddPr.Click += btnAddPr_Click;
            // 
            // btnEditPr
            // 
            btnEditPr.Location = new Point(354, 378);
            btnEditPr.Name = "btnEditPr";
            btnEditPr.Size = new Size(131, 29);
            btnEditPr.TabIndex = 3;
            btnEditPr.Text = "Edit product";
            btnEditPr.UseVisualStyleBackColor = true;
            btnEditPr.Click += btnEditPr_Click;
            // 
            // btnDelPr
            // 
            btnDelPr.Location = new Point(523, 378);
            btnDelPr.Name = "btnDelPr";
            btnDelPr.Size = new Size(157, 29);
            btnDelPr.TabIndex = 4;
            btnDelPr.Text = "Delete product";
            btnDelPr.UseVisualStyleBackColor = true;
            btnDelPr.Click += btnDelPr_Click;
            // 
            // FormSklad
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnDelPr);
            Controls.Add(btnEditPr);
            Controls.Add(btnAddPr);
            Controls.Add(btnShow);
            Controls.Add(dataGridView1);
            Name = "FormSklad";
            Text = "FormSklad";
            Load += FormSklad_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnShow;
        private Button btnAddPr;
        private Button btnEditPr;
        private Button btnDelPr;
    }
}