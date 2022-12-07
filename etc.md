# 자료 정리 중.

### ? 에 대해서
```
C# 코드에서 가끔 물음표 두개(??)를 볼 수 있다. 기본적으로 물음표 한개(?) 는 Nullable을 뜻 한다. 그러니까 이 값이 null 일 수도 있다는 것을 명시해준다.

string ? str;    // str가 null일수도 있다.
그렇다면 물음표 두개(??)는 무엇을 뜻할까? 위의 어트리뷰트와 비슷하다. 얘가 null이야 아니야? null이면 이렇게 해주고, null이 아니면 저렇게 해줘. 약간 조건문의 냄새가 나지 않는가? 맞다. 조건문의 성격을 띈 연산자다. 아래 코드를 보자.

string name = param ?? "default";
// param이 null이 아니라면 name에 param을 넣고, null 이라면 default를 넣어라. 이다.
// param은 메소드의 파라미터든, 위에서 사용된 것이든, 어디선가 가져온 것이다.
조건문의 역할을 하기 때문에, 조건문으로 대체될 수도 있다. 아래 코드를 보자.

string name = "";
if(param != null){
	name = param;
}else{
	name = "default";
}
// 조건문을 간략하게 만든 다음과도 같다.
string name = param != null ? param : "default";
자기 상황과 기호에 맞춰서 사용하면 될 것 같다. 단지 저런 코드가 나왔을 때, 본인이 사용하지 않는다고 뭔지 몰라서는 안될 것 같다.

```

### generic 에 대해서
```
C# 코드에서 가끔 물음표 두개(??)를 볼 수 있다. 기본적으로 물음표 한개(?) 는 Nullable을 뜻 한다. 그러니까 이 값이 null 일 수도 있다는 것을 명시해준다.

string ? str;    // str가 null일수도 있다.
그렇다면 물음표 두개(??)는 무엇을 뜻할까? 위의 어트리뷰트와 비슷하다. 얘가 null이야 아니야? null이면 이렇게 해주고, null이 아니면 저렇게 해줘. 약간 조건문의 냄새가 나지 않는가? 맞다. 조건문의 성격을 띈 연산자다. 아래 코드를 보자.

string name = param ?? "default";
// param이 null이 아니라면 name에 param을 넣고, null 이라면 default를 넣어라. 이다.
// param은 메소드의 파라미터든, 위에서 사용된 것이든, 어디선가 가져온 것이다.
조건문의 역할을 하기 때문에, 조건문으로 대체될 수도 있다. 아래 코드를 보자.

string name = "";
if(param != null){
	name = param;
}else{
	name = "default";
}
// 조건문을 간략하게 만든 다음과도 같다.
string name = param != null ? param : "default";
자기 상황과 기호에 맞춰서 사용하면 될 것 같다. 단지 저런 코드가 나왔을 때, 본인이 사용하지 않는다고 뭔지 몰라서는 안될 것 같다.

```

### nameof 

```
C# 6.0의 nameof 연산자는 Type이나 메서드, 속성 등의 이름을 리턴하는 것으로 이러한 명칭들을 하드코딩하지 않게 하는 잇점이 있다. 
즉, 이는 하드코딩에 의한 타이핑 오류 방지나 혹은 차후 리팩토링에서 유연한 구조를 만들어 준다는 잇점이 있다. 
예를 들어, 아래 예제와 같이 ArgumentException을 발생시킬 때, 파라미터명을 직접 하드코딩하지 않고 nameof()를 사용하면,
만약 리팩토링을 통해 id가 identity로 변경하더라도 아무런 문제가 없게 된다.


// 1. 파마미터명 id (Hard coding 하지 않음)
throw new ArgumentException("Invalid argument", nameof(id));

// 2. 속성명을 nameof 로 추출
Console.WriteLine("{0}: {1}", nameof(objPerson.Height), objPerson.Height);

// 3. 메서드명 로깅에 추가
void Run() {
   Log(nameof(Run) + " : Started");
}

```

참고해서 테스트 하고, 추후 테스트 파일 작성해서 테스트 하는 방식으로 진행한다.
https://icodebroker.tistory.com/9474#recentComments


### TODO
```
 xmlns:local="clr-namespace:calebUI"

<local:BaseConnection/>
```
axaml 에 적용할때 문제가 발생한다.
class 문제인지 아니면 잘못처리해서 하는지 파악해야 한다.

-> avalonia sample 참고하자. 구글링이 안되어서 일단 이건 추후에 진행한다.

https://sourcegraph.com/github.com/AvaloniaUI/Avalonia/-/blob/samples/MobileSandbox/MainWindow.xaml?L11

nodify 로도 테스트 해보자.

해당 문제 해결
Unable to find a setter that allows multiple assignments to the property Content of type Avalonia.Controls:Avalonia.Controls.ContentControl 

-> that one says you are putting too many children in a control that can only have one


WPF 에서 아래 코드 대응 되는 avaloniaUI 찾기.
[TemplatePart(Name = ElementConnector, Type = typeof(FrameworkElement))]

일단 먼저, WPf 에서 TemplatePart 부분
https://learn.microsoft.com/ko-kr/dotnet/desktop/wpf/controls/creating-a-control-that-has-a-customizable-appearance?view=netframeworkdesktop-4.8

https://kaki104.tistory.com/473

대략적으로 빠르게 이해했음.

avaloniaUI 에서도 구현 되었음을 확인함.
https://github.com/AvaloniaUI/Avalonia/issues/7432

[중요!!]event handler 부분 차이점을 파악하고 해결해야함.

### WPF / Avalonia
BeginFigure 에서 isClosed 파라미터의 경우
WPF 에서는 마지막 파라미터로 사용하여 해당 세그멘트가 closed 인지 아닌지 판단하지만

Avalonia 에서는 EndFigure 별도의 메스드를 사용하여 해당 입력 파라미터로 동일하게 closed 인지 아닌지 판단한다.

또한, IsStorke 의 경우, Avalonia 에서는 해당 파라미터가 존재하지 않으나 별도로 상속된 stroke 를 설정하면 간단히 동일한 rendering 을 해준다.
즉, WPF 에서  isStroke 를 false 로 설정하면 stroke 를 설정하더라도 렌더링이 안되어서 추가적으로 isStroke 를 설정해줘야 하지만 avalonia 에서는 이것을 생략해서
좀더 간단하게 구현하였다. 하지만, 깊이 있게 분석한 것이 아니라서, 내부적인 랜더링 방식에 대해서는 의문이다.

결론은, WPF 와 동일하게 구현할 수 있다.

참고:
https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.media.arcsegment?view=windowsdesktop-7.0


### 정적 생성자
https://developer-talk.tistory.com/449


### avalonia style

avalonia 의 style 은 wpf 다른 방식을적용한 것이 있다. CSS 스타일로 Control 의 스타일을 적용할 수 있도록 하게 한다.

아래 예저를 살펴보면,

```
<StackPanel>
		<StackPanel.Styles>
			<Style Selector="Border:pointerover">
				<Setter Property="Background" Value="Red"/>
			</Style>
		</StackPanel.Styles>
		<Border x:Name="tester">
			<TextBlock>I will have red background when hovered.</TextBlock>
		</Border>
</StackPanel>
```

StackPanel Styles 에서 Border pseudoclass  를 사용하였다.
pseudoclass 는 대략적으로 여기서 -> https://docs.avaloniaui.net/docs/styling/styles  참고 하면 됨.

Avalonia 소스에서 살펴보면 Border 에 아래와 같은 구문이 있는 것을 확인 할 수 있다.

PseudoClasses.Set(":pointerover", isPointerOver.Value);

위 코드는 pseudoclass 를 등록해 줄 수 있는 코드 이다.

추가적인 설명을 하자면, Avalonia.Controls 에서 Border.cs 를 살펴보면

Border <- Decorator <- Control <- InputElement 으로 상속을 하게 되는데 InputElement 에서 PseudoClasses.Set(":pointerover", isPointerOver.Value); 를 구현하고 있다.

다음으로, 보충해서 아래 stackoverflow 글을 보면 좀더 깊이 이해할 수 있을 것이다.

https://stackoverflow.com/questions/66442508/avaloniaui-styles-pseudoclasses






