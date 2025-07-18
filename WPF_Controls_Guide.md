# WPF Copy-Paste Templates

## DataGrid Template
```xml
<DataGrid
    Width="Auto"
    Height="300"
    Margin="0,10"
    AlternatingRowBackground="LightGray"
    AutoGenerateColumns="False"
    Background="#f0f0f0"
    CanUserAddRows="False"
    CanUserDeleteRows="False"
    GridLinesVisibility="All"
    IsReadOnly="True"
    ItemsSource="{Binding YourCollection}"
    SelectedItem="{Binding SelectedItem, Mode=TwoWay}"
    SelectionMode="Single">
    
    <DataGrid.Columns>
        <DataGridTextColumn Width="70" Binding="{Binding Id}" Header="ID" IsReadOnly="True" />
        <DataGridTextColumn Width="130" Binding="{Binding Name}" Header="Name" />
        <DataGridTextColumn Width="150" Binding="{Binding Email}" Header="Email" />
        <DataGridTextColumn Width="Auto" Binding="{Binding DateProperty}" Header="Date" />
    </DataGrid.Columns>
</DataGrid>
```

## ComboBox Template
```xml
<ComboBox
    Width="200"
    Height="25"
    DisplayMemberPath="PropertyToShow"
    ItemsSource="{Binding YourCollection}"
    SelectedItem="{Binding SelectedItem, Mode=TwoWay}"
    IsEditable="False"
    IsReadOnly="True"
    MaxDropDownHeight="150" />
```

## TextBox Templates
```xml
<!-- Regular TextBox -->
<TextBox
    Text="{Binding YourProperty, UpdateSourceTrigger=PropertyChanged}"
    MaxLength="100"
    Height="25"
    VerticalContentAlignment="Center"
    Margin="0,0,0,8" />

<!-- Search TextBox -->
<TextBox
    Text="{Binding SearchText, UpdateSourceTrigger=PropertyChanged}"
    Width="200"
    Height="25" />

<!-- Read-only TextBox -->
<TextBox
    Text="{Binding DisplayText}"
    IsReadOnly="True"
    Background="LightGray"
    Height="25" />
```

## Label Template
```xml
<Label
    Content="Field Name:"
    FontWeight="Bold"
    Foreground="DarkBlue"
    VerticalAlignment="Center"
    Margin="0,0,10,8" />
```

## RadioButton Template
```xml
<StackPanel Orientation="Horizontal" Margin="0,0,0,8">
    <RadioButton Content="Male" GroupName="Gender" Margin="0,0,15,0" VerticalAlignment="Center" />
    <RadioButton Content="Female" GroupName="Gender" Margin="0,0,15,0" VerticalAlignment="Center" />
</StackPanel>
```

## DatePicker Template
```xml
<DatePicker
    SelectedDate="{Binding YourDateProperty, UpdateSourceTrigger=PropertyChanged}"
    DisplayDateStart="1900-01-01"
    IsTodayHighlighted="True"
    Margin="0,0,0,8" />
```

## CheckBox Template
```xml
<CheckBox
    IsChecked="{Binding YourBooleanProperty, UpdateSourceTrigger=PropertyChanged}"
    Content="Has Scholarship"
    VerticalAlignment="Center"
    Margin="0,0,0,8" />
```

## Button Templates
```xml
<!-- Single Button -->
<Button
    Content="Button Text"
    Width="80"
    Height="30"
    Click="ButtonName_Click" />

<!-- CRUD Buttons -->
<StackPanel Orientation="Horizontal" HorizontalAlignment="Center" Margin="10">
    <Button Content="Add" Width="80" Height="30" Margin="5,0" />
    <Button Content="Edit" Width="80" Height="30" Margin="5,0" />
    <Button Content="Delete" Width="80" Height="30" Margin="5,0" />
    <Button Content="Save" Width="80" Height="30" Margin="5,0" />
    <Button Content="Cancel" Width="80" Height="30" Margin="5,0" />
    <Button Content="Refresh" Width="80" Height="30" Margin="5,0" />
</StackPanel>
```

## Layout Templates

### Grid Layout
```xml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="120" />
        <ColumnDefinition Width="*" />
    </Grid.ColumnDefinitions>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>
    
    <Label Grid.Row="0" Grid.Column="0" Content="Name:" FontWeight="Bold" Foreground="DarkBlue" />
    <TextBox Grid.Row="0" Grid.Column="1" Text="{Binding Name, UpdateSourceTrigger=PropertyChanged}" />
</Grid>
```

### StackPanel Layout
```xml
<StackPanel Orientation="Horizontal">
    <Label Content="Search:" FontWeight="Bold" Foreground="DarkBlue" Width="80" />
    <TextBox Width="200" Height="25" Text="{Binding SearchText, UpdateSourceTrigger=PropertyChanged}" />
    <Button Content="Search" Width="80" Height="30" />
</StackPanel>
```

### GroupBox Layout
```xml
<GroupBox Header="Object Details" Margin="15">
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="120" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>
        
        <!-- Form controls here -->
    </Grid>
</GroupBox>
```

## Master-Detail Layout
```xml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="60*" />
        <ColumnDefinition Width="40*" />
    </Grid.ColumnDefinitions>
    
    <!-- Master Section -->
    <Border Grid.Column="0" Background="#a0a0a0" BorderBrush="Black" BorderThickness="2">
        <StackPanel>
            <!-- Search controls -->
            <!-- DataGrid -->
        </StackPanel>
    </Border>
    
    <!-- Detail Section -->
    <GroupBox Grid.Column="1" Header="Details" Margin="15">
        <!-- Detail form -->
    </GroupBox>
</Grid>
```

## Complete Object Form
```xml
<GroupBox Header="Object Details" Margin="15">
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="120" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="Auto" />
            <RowDefinition Height="Auto" />
            <RowDefinition Height="Auto" />
            <RowDefinition Height="Auto" />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>

        <Label Grid.Row="0" Grid.Column="0" Content="ID:" FontWeight="Bold" Foreground="DarkBlue" />
        <TextBox Grid.Row="0" Grid.Column="1" Text="{Binding SelectedObject.Id}" IsReadOnly="True" Background="LightGray" />

        <Label Grid.Row="1" Grid.Column="0" Content="Name:" FontWeight="Bold" Foreground="DarkBlue" />
        <TextBox Grid.Row="1" Grid.Column="1" Text="{Binding SelectedObject.Name, UpdateSourceTrigger=PropertyChanged}" />

        <Label Grid.Row="2" Grid.Column="0" Content="Email:" FontWeight="Bold" Foreground="DarkBlue" />
        <TextBox Grid.Row="2" Grid.Column="1" Text="{Binding SelectedObject.Email, UpdateSourceTrigger=PropertyChanged}" />

        <Label Grid.Row="3" Grid.Column="0" Content="Gender:" FontWeight="Bold" Foreground="DarkBlue" />
        <StackPanel Grid.Row="3" Grid.Column="1" Orientation="Horizontal">
            <RadioButton Content="Male" GroupName="Gender" Margin="0,0,15,0" />
            <RadioButton Content="Female" GroupName="Gender" />
        </StackPanel>

        <Label Grid.Row="4" Grid.Column="0" Content="Date of Birth:" FontWeight="Bold" Foreground="DarkBlue" />
        <DatePicker Grid.Row="4" Grid.Column="1" SelectedDate="{Binding SelectedObject.DateOfBirth, UpdateSourceTrigger=PropertyChanged}" />

        <Label Grid.Row="5" Grid.Column="0" Content="Status:" FontWeight="Bold" Foreground="DarkBlue" />
        <ComboBox Grid.Row="5" Grid.Column="1" ItemsSource="{Binding StatusList}" SelectedItem="{Binding SelectedObject.Status}" DisplayMemberPath="StatusName" />
    </Grid>
</GroupBox>
```

## Search Filter Panel
```xml
<StackPanel Orientation="Vertical" Margin="10">
    <StackPanel Orientation="Horizontal" Margin="0,5">
        <Label Content="Search Name:" FontWeight="Bold" Foreground="DarkBlue" Width="100" />
        <TextBox Width="200" Height="25" Text="{Binding SearchName, UpdateSourceTrigger=PropertyChanged}" />
    </StackPanel>
    
    <StackPanel Orientation="Horizontal" Margin="0,5">
        <Label Content="Search Email:" FontWeight="Bold" Foreground="DarkBlue" Width="100" />
        <TextBox Width="200" Height="25" Text="{Binding SearchEmail, UpdateSourceTrigger=PropertyChanged}" />
    </StackPanel>
    
    <StackPanel Orientation="Horizontal" Margin="0,5">
        <Label Content="Category:" FontWeight="Bold" Foreground="DarkBlue" Width="100" />
        <ComboBox Width="200" Height="25" ItemsSource="{Binding Categories}" SelectedItem="{Binding SelectedCategory}" DisplayMemberPath="CategoryName" />
    </StackPanel>
    
    <Button Content="Search" Width="100" Height="30" Margin="0,10,0,0" HorizontalAlignment="Left" />
</StackPanel>
```