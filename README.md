# MMO_Unity

게임 제작을 하기위한 프레임워크 설계.  
나중에 게임을 제작할때 참고하기 위한 프로젝트입니다.

## Manager 코드 구현 내용

### [Managers.cs](https://github.com/twozeronine/MMO_unity/blob/main/Assets/Scripts/Managers/Managers.cs)

모든 매니저를 갖고있는 매니저 싱글톤 패턴으로 구현하여 다른 매니저도 접근 할 수 있다.

### [InputManager.cs](https://github.com/twozeronine/MMO_unity/blob/main/Assets/Scripts/Managers/Core/InputManager.cs)

디자인 패턴인 옵저버 패턴을 이용해서만든 InputManager 사용자의 입력이 들어오면 KeyAction에 등록된 함수를 실행시킨다.

### [ResourceManager.cs](https://github.com/twozeronine/MMO_unity/blob/main/Assets/Scripts/Managers/Core/ResourceManager.cs)

프리팹화 시킨 오브젝트를 불러오는 방법중에 하나로 유니티에서 Resources 폴더를 만들고 그 폴더를 통하여 오브젝트를 불러오는 방법이 있는데 그 방법으로 리소스를 불러오는 역할을 하는 Manager이다.

### [UIManager.cs](https://github.com/twozeronine/MMO_unity/blob/main/Assets/Scripts/Managers/Core/UIManager.cs)

PopupUI : 게임 진행 시 팝업으로 뜨고 사라지는 Dialog식 UI들
SceneUI : 게임 진행 시 설정창이나 인벤토리와 같은 UI

PopupUI 활성화 시 순서(sorting order : 화면에 보여지는 순서 )를 관리해준다. UI를 생성 혹은 삭제등을 해주는 Manager이다.

### [SceneManagerEx.cs](https://github.com/twozeronine/MMO_unity/blob/main/Assets/Scripts/Managers/Core/SceneManagerEx.cs)

> 유니티에 이미 SceneManager라는 이름이 예약되있기 때문에 이름을 조금 바꾼것이다.

씬을 관리하는 매니저이다.
기존에 존재하던 SceneManager의 기능들을 매핑하여 다시 구현을 하여서 하나의 Manager에서 관리하도록 해주었습니다.

### [SoundManager.cs](https://github.com/twozeronine/MMO_unity/blob/main/Assets/Scripts/Managers/Core/SoundManager.cs)

사운드를 관리하는 매니저이다.

사운드를 내는 AudioSource같은 경우 특정 오브젝트에 붙이게되면 오브젝트가 비활성화되거나 삭제될때 더이상 소리를 못내거나 접근할 수 없기 때문에 사운드를 관리할 매니저가 필요하다.

### [PoolManager.cs](https://github.com/twozeronine/MMO_unity/blob/main/Assets/Scripts/Managers/Core/PoolManager.cs)

풀을 관리하는 매니저이다.

유니티에서 생성과 삭제는 부하가 큰 기능이기 때문에 미리 오브젝트를 생성하여 캐싱하여 풀에 비활성화 상태로 넣어놓고 필요할때만 활성화시켜 사용하는 방법을 주로 쓴다.

### [DataManager.cs](https://github.com/twozeronine/MMO_unity/blob/main/Assets/Scripts/Managers/Core/DataManager.cs)

데이터를 관리하는 매니저이다. 프로젝트의 규모가 커질수록 하드코딩으로 데이터를 입력 하는것보다 어느 한 곳에서 데이터를 저장하여 불러오는 식으로 해야하는데 그때 사용할 매니저이다.

나중에 서버와 연동할 것을 생각하여 유니티에서 제공하는 ScriptableObject를 사용하지 않고 처음부터 json으로 설계를 하였다.
