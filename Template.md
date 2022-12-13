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

