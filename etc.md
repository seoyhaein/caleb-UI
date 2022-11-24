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



