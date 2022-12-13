### Template 에 대하여

wpf 에서는 기본적으로 ControlTemplat, DataTemplate, ItemPanelTemplate 가 있음을 파악하였다. 
이러한 클래스들은 FrameworkTemplate 를 기반으로 하고 있다.

기본적으로 공통점은 시각적인 부분을 설정할 수 있다는 점이고 그 대상을 달리한다는 차이점이 있다.

또한 이들 Template class 들은 리소스로 등록해서 사용할 수 있다.

이때, 생각해봐야 할 점은 Style, Template, Resource 를 구분해서 생각해봐야 한다는 점이다.
Style 은 기본적으로 Setter 를 통해서 Property 의 값을 설정할 수 있는데, Style 은 Trigger, EventTrigger 및 StoryBoard, ControlTemplate, DataTemplate 을 포함할 수 있다.

이러한 Style 이나 Template 은 Resource 를 동록해서 사용할 수 있다.

참고 : https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.style?view=windowsdesktop-7.0

참고 : C:\Users\seoy\source\repos\seoyhaein\nodify\Nodify\NodifyEditor.cs

avalonia 에 대해서 별도의 조사가 필요하다.

### avalonia 에서 Template

https://docs.avaloniaui.net/docs/templates/data-templates

#### TODO
1. 링크에서 설명된 코드를 wpf 로 작성하고 비교한다.

2. avalonia 에서는 DataTrigger 가 없다. 어떻게 처리할지 고민한다.

참고 : https://stackoverflow.com/questions/68979876/how-to-simulate-datatrigger-with-avalonia

nodify 에서 작성된 wpf 코드를 avalonia 로 바꾸는 작업을 해야함.

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