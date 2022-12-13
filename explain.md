### 설명

C:\Users\seoy\source\repos\seoyhaein\nodify\Examples\Nodify.Playground\Editor\NodifyEditorView.xaml
파일을 분석하면서 설명을 남겨 놓는다.

```
<UserControl.Resources>
        <shared:RandomBrushConverter x:Key="RandomBrushConverter" />
        <local:FlowToDirectionConverter x:Key="FlowToDirectionConverter" />

        <GeometryDrawing x:Key="SmallGridGeometry"
                         Geometry="M0,0 L0,1 0.03,1 0.03,0.03 1,0.03 1,0 Z"
                         Brush="{DynamicResource GridLinesBrush}" />

        <GeometryDrawing x:Key="LargeGridGeometry"
                         Geometry="M0,0 L0,1 0.015,1 0.015,0.015 1,0.015 1,0 Z"
                         Brush="{DynamicResource GridLinesBrush}" />

        <DrawingBrush x:Key="SmallGridLinesDrawingBrush"
                      TileMode="Tile"
                      ViewportUnits="Absolute"
                      Viewport="{Binding GridSpacing, Source={x:Static local:EditorSettings.Instance}, Converter={local:UIntToRectConverter}}"
                      Transform="{Binding ViewportTransform, ElementName=Editor}"
                      Drawing="{StaticResource SmallGridGeometry}" />

        <DrawingBrush x:Key="LargeGridLinesDrawingBrush"
                      TileMode="Tile"
                      ViewportUnits="Absolute"
                      Opacity="0.5"
                      Viewport="{Binding GridSpacing, Source={x:Static local:EditorSettings.Instance}, Converter={local:UIntToRectConverter  Multiplier=10}}"
                      Transform="{Binding ViewportTransform, ElementName=Editor}"
                      Drawing="{StaticResource LargeGridGeometry}" />

        <SolidColorBrush x:Key="SquareConnectorColor" Color="MediumSlateBlue" />
        <SolidColorBrush x:Key="TriangleConnectorColor" Color="MediumVioletRed" />

        <Storyboard x:Key="HighlightConnection">
            <DoubleAnimation Storyboard.TargetProperty="StrokeThickness" 
                         Duration="0:0:0.3" From="3" To="6"  />
        </Storyboard>

        <Style x:Key="ConnectionStyle" TargetType="{x:Type nodify:BaseConnection}">
            <Style.Triggers>
                <DataTrigger Binding="{Binding Input.Shape}" 
                             Value="{x:Static local:ConnectorShape.Square}">
                    <Setter Property="Stroke" Value="{StaticResource SquareConnectorColor}"/>
                    <Setter Property="Fill" Value="{StaticResource SquareConnectorColor}"/>
                </DataTrigger>
                <DataTrigger Binding="{Binding Input.Shape}" 
                             Value="{x:Static local:ConnectorShape.Triangle}">
                    <Setter Property="Stroke" Value="{StaticResource TriangleConnectorColor}"/>
                    <Setter Property="Fill" Value="{StaticResource TriangleConnectorColor}"/>
                </DataTrigger>
                <Trigger Property="IsMouseDirectlyOver" Value="True">
                    <Trigger.EnterActions>
                        <BeginStoryboard Name="HighlightConnection" Storyboard="{StaticResource HighlightConnection}" />
                    </Trigger.EnterActions>
                    <Trigger.ExitActions>
                        <RemoveStoryboard BeginStoryboardName="HighlightConnection" />
                    </Trigger.ExitActions>
                </Trigger>
            </Style.Triggers>
            <Setter Property="Stroke" Value="{DynamicResource Connection.StrokeBrush}"/>
            <Setter Property="Fill" Value="{DynamicResource Connection.StrokeBrush}"/>
            <Setter Property="Cursor" Value="Hand"/>
            <Setter Property="ToolTip" Value="Double click to split"/>
            <Setter Property="Source" Value="{Binding Output.Anchor}" />
            <Setter Property="Target" Value="{Binding Input.Anchor}" />
            <Setter Property="SplitCommand" Value="{Binding SplitCommand}" />
            <Setter Property="DisconnectCommand" Value="{Binding DisconnectCommand}" />
            <Setter Property="OffsetMode" Value="{Binding ConnectionOffsetMode, Source={x:Static local:EditorSettings.Instance}}" />
            <Setter Property="SourceOffset" Value="{Binding ConnectionSourceOffset.Size, Source={x:Static local:EditorSettings.Instance}}" />
            <Setter Property="TargetOffset" Value="{Binding ConnectionTargetOffset.Size, Source={x:Static local:EditorSettings.Instance}}" />
            <Setter Property="ArrowSize" Value="{Binding ConnectionArrowSize.Size, Source={x:Static local:EditorSettings.Instance}}" />
            <Setter Property="ArrowEnds" Value="{Binding ArrowHeadEnds, Source={x:Static local:EditorSettings.Instance}}" />
            <Setter Property="Spacing" Value="{Binding ConnectionSpacing, Source={x:Static local:EditorSettings.Instance}}" />
        </Style>

        <DataTemplate x:Key="CircuitConnectionTemplate">
            <nodify:CircuitConnection Angle="{Binding CircuitConnectionAngle, Source={x:Static local:EditorSettings.Instance}}"
                                      Style="{StaticResource ConnectionStyle}" />
        </DataTemplate>

        <DataTemplate x:Key="ConnectionTemplate">
            <nodify:Connection Style="{StaticResource ConnectionStyle}" />
        </DataTemplate>

        <DataTemplate x:Key="LineConnectionTemplate">
            <nodify:LineConnection Style="{StaticResource ConnectionStyle}" />
        </DataTemplate>

        <ControlTemplate x:Key="SquareConnector" TargetType="Control">
            <Rectangle Width="14"
                       Height="14"
                       StrokeDashCap="Round"
                       StrokeLineJoin="Round"
                       StrokeStartLineCap="Round"
                       StrokeEndLineCap="Round"
                       Stroke="{TemplateBinding BorderBrush}"
                       Fill="{TemplateBinding Background}"
                       StrokeThickness="2" />
        </ControlTemplate>

        <ControlTemplate x:Key="TriangleConnector" TargetType="Control">
            <Polygon Width="14"
                     Height="14"
                     Points="1,13 13,13 7,1"
                     StrokeDashCap="Round"
                     StrokeLineJoin="Round"
                     StrokeStartLineCap="Round"
                     StrokeEndLineCap="Round"
                     Stroke="{TemplateBinding BorderBrush}"
                     Fill="{TemplateBinding Background}"
                     StrokeThickness="2" />
        </ControlTemplate>

        <Storyboard x:Key="MarchingAnts">
            <DoubleAnimation RepeatBehavior="Forever"
                             Storyboard.TargetProperty="StrokeDashOffset" 
                             BeginTime="00:00:00"
                             Duration="0:3:0"
                             From="1000"
                             To="0"/>
        </Storyboard>

        <Style x:Key="SelectionRectangleStyle" TargetType="Rectangle" BasedOn="{StaticResource NodifyEditor.SelectionRectangleStyle}">
            <Setter Property="StrokeDashArray" Value="4 4" />
            <Setter Property="StrokeThickness" Value="2" />
            <Style.Triggers>
                <EventTrigger RoutedEvent="FrameworkElement.Loaded">
                    <BeginStoryboard Storyboard="{StaticResource MarchingAnts}" />
                </EventTrigger>
            </Style.Triggers>
        </Style>
    </UserControl.Resources>
```

위의 코드에서 살펴보아야 하는 부분은 아래 코드 이다.

```
        <Style x:Key="ConnectionStyle" TargetType="{x:Type nodify:BaseConnection}">
            <Style.Triggers>
                <DataTrigger Binding="{Binding Input.Shape}" 
                             Value="{x:Static local:ConnectorShape.Square}">
                    <Setter Property="Stroke" Value="{StaticResource SquareConnectorColor}"/>
                    <Setter Property="Fill" Value="{StaticResource SquareConnectorColor}"/>
                </DataTrigger>
                <DataTrigger Binding="{Binding Input.Shape}" 
                             Value="{x:Static local:ConnectorShape.Triangle}">
                    <Setter Property="Stroke" Value="{StaticResource TriangleConnectorColor}"/>
                    <Setter Property="Fill" Value="{StaticResource TriangleConnectorColor}"/>
                </DataTrigger>
                <Trigger Property="IsMouseDirectlyOver" Value="True">
                    <Trigger.EnterActions>
                        <BeginStoryboard Name="HighlightConnection" Storyboard="{StaticResource HighlightConnection}" />
                    </Trigger.EnterActions>
                    <Trigger.ExitActions>
                        <RemoveStoryboard BeginStoryboardName="HighlightConnection" />
                    </Trigger.ExitActions>
                </Trigger>
            </Style.Triggers>
            <Setter Property="Stroke" Value="{DynamicResource Connection.StrokeBrush}"/>
            <Setter Property="Fill" Value="{DynamicResource Connection.StrokeBrush}"/>
            <Setter Property="Cursor" Value="Hand"/>
            <Setter Property="ToolTip" Value="Double click to split"/>
            <Setter Property="Source" Value="{Binding Output.Anchor}" />
            <Setter Property="Target" Value="{Binding Input.Anchor}" />
            <Setter Property="SplitCommand" Value="{Binding SplitCommand}" />
            <Setter Property="DisconnectCommand" Value="{Binding DisconnectCommand}" />
            <Setter Property="OffsetMode" Value="{Binding ConnectionOffsetMode, Source={x:Static local:EditorSettings.Instance}}" />
            <Setter Property="SourceOffset" Value="{Binding ConnectionSourceOffset.Size, Source={x:Static local:EditorSettings.Instance}}" />
            <Setter Property="TargetOffset" Value="{Binding ConnectionTargetOffset.Size, Source={x:Static local:EditorSettings.Instance}}" />
            <Setter Property="ArrowSize" Value="{Binding ConnectionArrowSize.Size, Source={x:Static local:EditorSettings.Instance}}" />
            <Setter Property="ArrowEnds" Value="{Binding ArrowHeadEnds, Source={x:Static local:EditorSettings.Instance}}" />
            <Setter Property="Spacing" Value="{Binding ConnectionSpacing, Source={x:Static local:EditorSettings.Instance}}" />
        </Style>

        <DataTemplate x:Key="CircuitConnectionTemplate">
            <nodify:CircuitConnection Angle="{Binding CircuitConnectionAngle, Source={x:Static local:EditorSettings.Instance}}"
                                      Style="{StaticResource ConnectionStyle}" />
        </DataTemplate>

        <DataTemplate x:Key="ConnectionTemplate">
            <nodify:Connection Style="{StaticResource ConnectionStyle}" />
        </DataTemplate>

        <DataTemplate x:Key="LineConnectionTemplate">
            <nodify:LineConnection Style="{StaticResource ConnectionStyle}" />
        </DataTemplate>

        <ControlTemplate x:Key="SquareConnector" TargetType="Control">
            <Rectangle Width="14"
                       Height="14"
                       StrokeDashCap="Round"
                       StrokeLineJoin="Round"
                       StrokeStartLineCap="Round"
                       StrokeEndLineCap="Round"
                       Stroke="{TemplateBinding BorderBrush}"
                       Fill="{TemplateBinding Background}"
                       StrokeThickness="2" />
        </ControlTemplate>

        <ControlTemplate x:Key="TriangleConnector" TargetType="Control">
            <Polygon Width="14"
                     Height="14"
                     Points="1,13 13,13 7,1"
                     StrokeDashCap="Round"
                     StrokeLineJoin="Round"
                     StrokeStartLineCap="Round"
                     StrokeEndLineCap="Round"
                     Stroke="{TemplateBinding BorderBrush}"
                     Fill="{TemplateBinding Background}"
                     StrokeThickness="2" />
        </ControlTemplate>
```

일단 먼저, Style 은 다음에 다루도록 하고, DataTemplate 을 보면,
1. https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.datatemplate?view=windowsdesktop-7.0

2. https://learn.microsoft.com/ko-kr/dotnet/desktop/wpf/data/data-templating-overview?view=netframeworkdesktop-4.8

데이터 개체의 시각적 구조를 나타내는 것을 확인할 수 있다.

2 의 예제를 살펴보면, 

```
<ListBox Width="400" Margin="10"
         ItemsSource="{Binding Source={StaticResource myTodoList}}">
   <ListBox.ItemTemplate>
     <DataTemplate>
       <StackPanel>
         <TextBlock Text="{Binding Path=TaskName}" />
         <TextBlock Text="{Binding Path=Description}"/>
         <TextBlock Text="{Binding Path=Priority}"/>
       </StackPanel>
     </DataTemplate>
   </ListBox.ItemTemplate>
 </ListBox>
```

위의 코드에서 ItemTemplate 에 DataTemplate 를 적용하는 것을 볼 수 있다. 여기서, 
https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.controls.itemscontrol.itemtemplate?view=windowsdesktop-7.0#system-windows-controls-itemscontrol-itemtemplate

를 참고하면, 

```
[System.ComponentModel.Bindable(true)]
public System.Windows.DataTemplate ItemTemplate { get; set; }
```

ItemTemplate은 DataTemplate 인 것을 확인 할 수 있다.

이러한 기본적인 정보를 확인 한후 다시 돌아 가보면, 
Resources 에서 CircuitConnectionTemplate, ConnectionTemplate, ConnectionTemplate 등이 선언되어 있다.
각 key 에 적용되는 의존속성을(DependencyProperty) 을 찾아본다.




ConnectionTemplate


C:\Users\seoy\source\repos\seoyhaein\nodify\Nodify\NodifyEditor.cs 에서

public static readonly DependencyProperty ConnectionTemplateProperty = DependencyProperty.Register(nameof(ConnectionTemplate), typeof(DataTemplate), typeof(NodifyEditor));

위와 같은 의존 속성이 있다.

그리고, 

```
 <nodify:NodifyEditor x:Name="Editor"
                             ItemsSource="{Binding Nodes}"
                             Connections="{Binding Connections}"
                             PendingConnection="{Binding PendingConnection}"
                             SelectedItems="{Binding SelectedNodes}"
                             DisconnectConnectorCommand="{Binding DisconnectConnectorCommand}"
                             ViewportLocation="{Binding Location.Value, Source={x:Static local:EditorSettings.Instance}}"
                             ViewportSize="{Binding ViewportSize, Mode=OneWayToSource}"
                             ViewportZoom="{Binding Zoom, Source={x:Static local:EditorSettings.Instance}}"
                             MinViewportZoom="{Binding MinZoom, Source={x:Static local:EditorSettings.Instance}}"
                             MaxViewportZoom="{Binding MaxZoom, Source={x:Static local:EditorSettings.Instance}}"
                             AutoPanSpeed="{Binding AutoPanningSpeed, Source={x:Static local:EditorSettings.Instance}}"
                             AutoPanEdgeDistance="{Binding AutoPanningEdgeDistance, Source={x:Static local:EditorSettings.Instance}}"
                             GridCellSize="{Binding GridSpacing, Source={x:Static local:EditorSettings.Instance}}"
                             EnableRealtimeSelection="{Binding EnableRealtimeSelection, Source={x:Static local:EditorSettings.Instance}}"
                             DisableAutoPanning="{Binding DisableAutoPanning, Source={x:Static local:EditorSettings.Instance}}"
                             DisablePanning="{Binding DisablePanning, Source={x:Static local:EditorSettings.Instance}}"
                             DisableZooming="{Binding DisableZooming, Source={x:Static local:EditorSettings.Instance}}"
                             DisplayConnectionsOnTop="{Binding DisplayConnectionsOnTop, Source={x:Static local:EditorSettings.Instance}}"
                             BringIntoViewSpeed="{Binding BringIntoViewSpeed, Source={x:Static local:EditorSettings.Instance}}"
                             BringIntoViewMaxDuration="{Binding BringIntoViewMaxDuration, Source={x:Static local:EditorSettings.Instance}}"
                             SelectionRectangleStyle="{StaticResource SelectionRectangleStyle}">
            <nodify:NodifyEditor.Style>
                <Style TargetType="{x:Type nodify:NodifyEditor}"
                       BasedOn="{StaticResource {x:Type nodify:NodifyEditor}}">
                    <Setter Property="ConnectionTemplate"
                            Value="{StaticResource ConnectionTemplate}" />
                    <Style.Triggers>
                        <DataTrigger Binding="{Binding ShowGridLines, Source={x:Static local:PlaygroundSettings.Instance}}"
                                     Value="True">
                            <Setter Property="Background"
                                    Value="{StaticResource SmallGridLinesDrawingBrush}" />
                        </DataTrigger>
                        <DataTrigger Binding="{Binding ConnectionStyle, Mode=TwoWay, Source={x:Static local:EditorSettings.Instance}}"
                                     Value="Line">
                            <Setter Property="ConnectionTemplate"
                                    Value="{StaticResource LineConnectionTemplate}" />
                        </DataTrigger>
                        <DataTrigger Binding="{Binding ConnectionStyle, Mode=TwoWay, Source={x:Static local:EditorSettings.Instance}}"
                                     Value="Circuit">
                            <Setter Property="ConnectionTemplate"
                                    Value="{StaticResource CircuitConnectionTemplate}" />
                        </DataTrigger>
                    </Style.Triggers>
                </Style>
            </nodify:NodifyEditor.Style>

            <nodify:NodifyEditor.InputBindings>
                <KeyBinding Key="Delete"
                            Command="{Binding DeleteSelectionCommand}" />
                <KeyBinding Key="C"
                            Command="{Binding CommentSelectionCommand}" />
            </nodify:NodifyEditor.InputBindings>

            <nodify:NodifyEditor.Resources>
                <Style TargetType="{x:Type nodify:PendingConnection}"
                       BasedOn="{StaticResource {x:Type nodify:PendingConnection}}">
                    <Setter Property="CompletedCommand"
                            Value="{Binding Graph.CreateConnectionCommand}" />
                    <Setter Property="Source"
                            Value="{Binding Source, Mode=OneWayToSource}" />
                    <Setter Property="Target"
                            Value="{Binding PreviewTarget, Mode=OneWayToSource}" />
                    <Setter Property="PreviewTarget"
                            Value="{Binding PreviewTarget, Mode=OneWayToSource}" />
                    <Setter Property="Content"
                            Value="{Binding PreviewText}" />
                    <Setter Property="EnablePreview"
                            Value="{Binding EnablePendingConnectionPreview, Source={x:Static local:EditorSettings.Instance}}" />
                    <Setter Property="EnableSnapping"
                            Value="{Binding EnablePendingConnectionSnapping, Source={x:Static local:EditorSettings.Instance}}" />
                    <Setter Property="AllowOnlyConnectors"
                            Value="{Binding AllowConnectingToConnectorsOnly, Source={x:Static local:EditorSettings.Instance}}" />
                    <Setter Property="Direction"
                            Value="{Binding Source.Flow, Converter={StaticResource FlowToDirectionConverter}}" />
                    <Setter Property="Template">
                        <Setter.Value>
                            <ControlTemplate TargetType="{x:Type nodify:PendingConnection}">
                                <Canvas>
                                    <nodify:Connection Source="{TemplateBinding SourceAnchor}"
                                                       Target="{TemplateBinding TargetAnchor}"
                                                       Direction="{TemplateBinding Direction}"
                                                       Stroke="{TemplateBinding Stroke}"
                                                       StrokeThickness="{TemplateBinding StrokeThickness}"
                                                       OffsetMode="{Binding ConnectionOffsetMode, Source={x:Static local:EditorSettings.Instance}}"
                                                       SourceOffset="{Binding ConnectionSourceOffset.Size, Source={x:Static local:EditorSettings.Instance}}"
                                                       TargetOffset="{Binding ConnectionTargetOffset.Size, Source={x:Static local:EditorSettings.Instance}}"
                                                       ArrowSize="{Binding ConnectionArrowSize.Size, Source={x:Static local:EditorSettings.Instance}}"
                                                       ArrowEnds="{Binding ArrowHeadEnds, Source={x:Static local:EditorSettings.Instance}}"
                                                       Spacing="{Binding ConnectionSpacing, Source={x:Static local:EditorSettings.Instance}}" />
                                    <Border Background="{TemplateBinding Background}"
                                            Canvas.Left="{Binding TargetAnchor.X, RelativeSource={RelativeSource TemplatedParent}}"
                                            Canvas.Top="{Binding TargetAnchor.Y, RelativeSource={RelativeSource TemplatedParent}}"
                                            Visibility="{TemplateBinding EnablePreview, Converter={shared:BooleanToVisibilityConverter}}"
                                            Padding="{TemplateBinding Padding}"
                                            BorderThickness="{TemplateBinding BorderThickness}"
                                            BorderBrush="{TemplateBinding BorderBrush}"
                                            CornerRadius="3"
                                            Margin="15">
                                        <ContentPresenter />
                                    </Border>
                                </Canvas>
                            </ControlTemplate>
                        </Setter.Value>
                    </Setter>
                </Style>

                <Style TargetType="{x:Type nodify:Connector}"
                       BasedOn="{StaticResource {x:Type nodify:Connector}}">
                    <Setter Property="Anchor"
                            Value="{Binding Anchor, Mode=OneWayToSource}" />
                    <Setter Property="IsConnected"
                            Value="{Binding IsConnected}" />
                </Style>

                <Style TargetType="{x:Type nodify:NodeInput}"
                       BasedOn="{StaticResource {x:Type nodify:NodeInput}}">
                    <Style.Triggers>
                        <DataTrigger Binding="{Binding Shape}" 
                                     Value="{x:Static local:ConnectorShape.Square}">
                            <Setter Property="ConnectorTemplate" Value="{StaticResource SquareConnector}" />
                            <Setter Property="BorderBrush" Value="{StaticResource SquareConnectorColor}"/>
                        </DataTrigger>
                        <DataTrigger Binding="{Binding Shape}" 
                                     Value="{x:Static local:ConnectorShape.Triangle}">
                            <Setter Property="ConnectorTemplate" Value="{StaticResource TriangleConnector}" />
                            <Setter Property="BorderBrush" Value="{StaticResource TriangleConnectorColor}"/>
                        </DataTrigger>
                    </Style.Triggers>
                    <Setter Property="Header"
                            Value="{Binding Title}" />
                    <Setter Property="Anchor"
                            Value="{Binding Anchor, Mode=OneWayToSource}" />
                    <Setter Property="IsConnected"
                            Value="{Binding IsConnected}" />
                </Style>

                <Style TargetType="{x:Type nodify:NodeOutput}"
                       BasedOn="{StaticResource {x:Type nodify:NodeOutput}}">
                    <Style.Triggers>
                        <DataTrigger Binding="{Binding Shape}" 
                                     Value="{x:Static local:ConnectorShape.Square}">
                            <Setter Property="ConnectorTemplate" Value="{StaticResource SquareConnector}" />
                            <Setter Property="BorderBrush" Value="{StaticResource SquareConnectorColor}"/>
                        </DataTrigger>
                        <DataTrigger Binding="{Binding Shape}" 
                                     Value="{x:Static local:ConnectorShape.Triangle}">
                            <Setter Property="ConnectorTemplate" Value="{StaticResource TriangleConnector}" />
                            <Setter Property="BorderBrush" Value="{StaticResource TriangleConnectorColor}"/>
                        </DataTrigger>
                    </Style.Triggers>
                    <Setter Property="Header"
                            Value="{Binding Title}" />
                    <Setter Property="Anchor"
                            Value="{Binding Anchor, Mode=OneWayToSource}" />
                    <Setter Property="IsConnected"
                            Value="{Binding IsConnected}" />
                </Style>

                <DataTemplate DataType="{x:Type local:KnotNodeViewModel}">
                    <nodify:KnotNode Content="{Binding Connector}" />
                </DataTemplate>

                <DataTemplate DataType="{x:Type local:CommentNodeViewModel}">
                    <nodify:GroupingNode ActualSize="{Binding Size}"
                                         Header="{Binding Title}"
                                         MovementMode="{Binding GroupingNodeMovement, Mode=TwoWay, Source={x:Static local:EditorSettings.Instance}}"/>
                </DataTemplate>

                <DataTemplate DataType="{x:Type local:FlowNodeViewModel}">
                    <nodify:Node Input="{Binding Input}"
                                 Output="{Binding Output}"
                                 Header="{Binding Title}" />
                </DataTemplate>
            </nodify:NodifyEditor.Resources>

            <nodify:NodifyEditor.ItemContainerStyle>
                <Style TargetType="{x:Type nodify:ItemContainer}"
                       BasedOn="{StaticResource {x:Type nodify:ItemContainer}}">
                    <Setter Property="Location"
                            Value="{Binding Location}" />
                </Style>
            </nodify:NodifyEditor.ItemContainerStyle>
        </nodify:NodifyEditor>

```

위의 코드 중에서, 아래의 코드를 확인하면 위에서 언급한 ConnectionTemplateProperty 의존 속성에(ConnectionTemplate) CircuitConnectionTemplate, ConnectionTemplate, ConnectionTemplate 가 매핑되어 있다.

```
            <nodify:NodifyEditor.Style>
                <Style TargetType="{x:Type nodify:NodifyEditor}"
                       BasedOn="{StaticResource {x:Type nodify:NodifyEditor}}">
                    <Setter Property="ConnectionTemplate"
                            Value="{StaticResource ConnectionTemplate}" />
                    <Style.Triggers>
                        <DataTrigger Binding="{Binding ShowGridLines, Source={x:Static local:PlaygroundSettings.Instance}}"
                                     Value="True">
                            <Setter Property="Background"
                                    Value="{StaticResource SmallGridLinesDrawingBrush}" />
                        </DataTrigger>
                        <DataTrigger Binding="{Binding ConnectionStyle, Mode=TwoWay, Source={x:Static local:EditorSettings.Instance}}"
                                     Value="Line">
                            <Setter Property="ConnectionTemplate"
                                    Value="{StaticResource LineConnectionTemplate}" />
                        </DataTrigger>
                        <DataTrigger Binding="{Binding ConnectionStyle, Mode=TwoWay, Source={x:Static local:EditorSettings.Instance}}"
                                     Value="Circuit">
                            <Setter Property="ConnectionTemplate"
                                    Value="{StaticResource CircuitConnectionTemplate}" />
                        </DataTrigger>
                    </Style.Triggers>
                </Style>
            </nodify:NodifyEditor.Style>
```

참고 내용

```
    // 그냥 참고
    // https://chriskim10.tistory.com/4


    // 아래 링크를 꼭 읽어보자.
    // https://github.com/dotnet/wpf/issues/1402
    // https://stackoverflow.com/questions/4239714/why-cant-i-style-a-control-with-the-aero-theme-applied-in-wpf-4-0

    // https://afsdzvcx123.tistory.com/entry/6%EC%9E%A5-WPF-Data-Trigger-%EB%9E%80
    // DataTrigger

    // 시작!
    // https://learn.microsoft.com/ko-kr/dotnet/desktop/wpf/data/data-templating-overview?view=netframeworkdesktop-4.8

    // DataTemplae 타임의 의존속성 하나 만들어서 테스트 진행한다.

    // 아래 링크 확인하고 테스트 컨트롤 만들어보자.
    // https://forum.dotnetdev.kr/t/wpf-datatemplate-custom-control-style/3857/6
```