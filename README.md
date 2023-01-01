# WpfLibrary
Collection of various useful modules for WPF

## AssemblyInfo ##
A class to support providing version and other info from the assembly for display in the About window or elsewhere.

## AboutWindow ##
XAML class that provides the About window.

## AboutProperties ##
A class for supporting general display of the AboutPanel

## AboutVM ##
The view model for use with the AboutPanel

## RelayCommand ##
For implementing commands.  (NOTE: The [RelayCommand] attribute in Community.MVVM.Toolkit is now a better implementation.)

## WaitCursor ##
A class for managing changing the cursor from the arrow to a wait icon.

## WindowClosingBehavior ##
A class that implements the ability to manage closing a window by checking whether an attempt to close the window has occurred, whether there are actions needed (e.g., saving data, etc.) before the save can continue, etc.

## ObservableList ##
A class similar to ObservableCollection but which includes additional methods and properties.

### IsNotifying ###
### Add ###
Add an Item T to the list

### AddRange ###
Add a range of items as IEnumerable<T>

### Clear ###
Clear the list of all Items.

### Insert ###
Insert an Item at index.

### InsertRange ###
Insert a range of Items at index.

### Remove ###
Remove an Item T.

### RemoveAll ###
Remove all Items matchinbg a predicate T from the list.

### RemoveAt ###
Remove the Item at index.

### RemoveRange ###
Remove a range of Items starting at index for count.

## ListItem ##
A class for use as a source for a ListBox or ListView. A class with Index, Key, and Value properties. An improvement over Enum-based sources as it allows for numberic values as the Key.

## ListItems ##
A class that is an ObservableCollection of ListItems.  Exposes additional methods.

### Find ###
Find the Value for a specific Key

### Add ###
Add a Value
Add a Value for a specific Key
Add a Value with a specific Index
Add a Value with a specific Key and Index


