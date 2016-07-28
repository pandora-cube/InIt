using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {
    public float moveSpeed = 3f;    // 이동 속도
    public int animSpeed = 3;       // 애니메이션 재생 속도
    public new GameObject camera;   // 카메라 오브젝트
    public Sprite[] runningSprites; // 달리기 스프라이트

    float cameraZ;                  // 카메라의 Z 좌표
    Sprite standingSprite;          // 정지상태 스프라이트
    bool canmove = true;            // 캐릭터 이동 가능 여부
    int animIndex;                  // 애니메이션 재생 인덱스
    
	void Start () {
        // 초기화
        cameraZ = camera.transform.position.z;
        standingSprite = GetComponent<SpriteRenderer>().sprite;
	    animIndex = 0;
	}
	
	void Update () {
        /*
         *  CharacterMove.Update()
         *      캐릭터 이동
         *      카메라 이동
         */
        
        // 이동 불가능한 경우
        if(!canmove)
            return;

        // 이동중 여부 체크 변수
        bool isrunning = false;
        // 캐릭터 스프라이트
        SpriteRenderer charspr = GetComponent<SpriteRenderer>();

        /* 캐릭터 이동 */
        Vector3 scale = transform.localScale;
        scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;

        // 좌향 이동
        if(Input.GetKey(KeyCode.LeftArrow)) {
            isrunning = true;
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            // 방향 처리
            charspr.flipX = false;
        }
        // 우향 이동
        if(Input.GetKey(KeyCode.RightArrow)) {
            isrunning = true;
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            // 방향 처리
            charspr.flipX = true;
        }
        // 상향 이동
        if(Input.GetKey(KeyCode.UpArrow)) {
            isrunning = true;
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        // 하향 이동
        if(Input.GetKey(KeyCode.DownArrow)) {
            isrunning = true;
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        else
            // 스프라이트 초기화
            charspr.sprite = standingSprite;
        
        // 이동중인 경우
        if(isrunning) {
            // 달리기 애니메이션 인덱스 처리
            animIndex++;
            if(animIndex/animSpeed >= runningSprites.Length)
                animIndex = 0;
            // 달리기 스프라이트 이미지로 전환
            charspr.sprite = runningSprites[animIndex/animSpeed];
        }
        
        /* 카메라 이동 */
        Vector3 charpos = charspr.transform.position;
        // 캐릭터 좌표와 동기화
        camera.transform.position = new Vector3(charpos.x, charpos.y, cameraZ);
	}

    void onCollisionEnter2D(Collision2D col) {
        /*
         *  onCollisionEnter2D(Collision2d col)
         *      이동 불가 처리
         *      특정 오브젝트와의 접촉 검사
         */

        /* 이동 불가 처리 */
        canmove = false;

        /* 특정 오브젝트와의 접촉 검사 */
        if(col.gameObject.name == "") {
        }
    }
}
