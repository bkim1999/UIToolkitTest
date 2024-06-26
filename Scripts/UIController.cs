using System;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
public class NewBehaviourScript : MonoBehaviour
{

    // 바텀 컨테이너
    private VisualElement _bottomContainer;

    // 열기 버튼
    private Button _openButton;

    // 닫기 버튼
    private Button _closeButton;

    // 바텀 시트
    private VisualElement _bottomSheet;

    // 가림막
    private VisualElement _scrim;

    // 소년 사진
    private VisualElement _boyImage;

    // 바텀 시트 소년 사진
    private VisualElement _boyImage2;

    // 대사
    private Label _message;


    // Start is called before the first frame update
    void Start()
    {
        // UI Document의 최상위 Visual Component 참조
        var root = GetComponent<UIDocument>().rootVisualElement;

        // 바텀컨테이너
        _bottomContainer = root.Q<VisualElement>("Container_Bottom");

        // 열기/닫기 버튼
        _openButton = root.Q<Button>("Button_Open");
        _closeButton = root.Q<Button>("Button_Close");

        // 바텀 시트와 가림막
        _bottomSheet = root.Q<VisualElement>("Bottom_Sheet");
        _scrim = root.Q<VisualElement>("Scrim");

        // 소년 사진들
        _boyImage = root.Q<VisualElement>("Image_Boy");
        _boyImage2 = root.Q<VisualElement>("Image_Boy2");

        // 메세지
        _message = root.Q<Label>("Message");

        // 바텀 컨테이너 감추기
        _bottomContainer.style.display = DisplayStyle.None;

        // 버튼 클릭 이벤트
        _openButton.RegisterCallback<ClickEvent>(OnOpenButtonClicked);
        _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClicked);

        // 트랜지션 애니메이션 끝날 때 실행
        _bottomSheet.RegisterCallback<TransitionEndEvent>(OnBottomShutDown);

        Invoke("AnimateBoy", 1f);

    }

    private void AnimateBoy() {
        _boyImage.RemoveFromClassList("img--boy--inair");
    }    

    private void OnOpenButtonClicked(ClickEvent evt) {
        
        // 바텀 컨테이너 보여주기
        _bottomContainer.style.display = DisplayStyle.Flex;

        // 바텀 시트와 가림막 애니메이션
        _bottomSheet.AddToClassList("bottomsheet--up");
        _scrim.AddToClassList("scrim--fadein");

        // 바텀 시트 소년 사진 애니메이션
        AnimateBoy2();

    }

    private void AnimateBoy2() {

        // 바텀 시트 소년 이미지 위아래로 애니메이션
        _boyImage2.ToggleInClassList("img--boy2--up");
        _boyImage2.RegisterCallback<TransitionEndEvent>(
            evt => _boyImage2.ToggleInClassList("img--boy2--up")
        );

        // 대사 한글자씩 출력
        _message.text = String.Empty;
        string m = "오른쪽 위 X를 눌러 창을 닫으시오.";
        DOTween.To( () => _message.text, x => _message.text = x, m, 3f).SetEase(Ease.Linear);

    }

    private void OnCloseButtonClicked(ClickEvent evt) {

         // 바텀 시트와 가림막 애니메이션
        _bottomSheet.RemoveFromClassList("bottomsheet--up");
        _scrim.RemoveFromClassList("scrim--fadein");        

    }

    private void OnBottomShutDown(TransitionEndEvent evt) {

        // 바텀 시트가 내려갈 때
        if(!_bottomSheet.ClassListContains("bottomsheet--up")) {
            // 바텀 컨테이너 감추기
            _bottomContainer.style.display = DisplayStyle.None;
        }

    }

}
