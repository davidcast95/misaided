using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;

public enum PostProcessType {vignette, blackout, chromatic, grain, blue};
public enum InterpolationType {lerp, damp};

public class D_PostProcess : MonoBehaviour {
	// Use this for initialization

	private PostProcessingProfile profile;
	[SerializeField] Camera targetCam;

	public bool isFading;

	void Awake () {
		profile = targetCam.GetComponent<PostProcessingBehaviour> ().profile;
		isFading = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetInitialPostProcess(PostProcessType pType, float value){
		switch (pType) {
		case PostProcessType.vignette:
			VignetteModel.Settings confv = profile.vignette.settings;
			confv.intensity = value;
			profile.vignette.settings = confv;
			break;
		case PostProcessType.blackout:
			ColorGradingModel.Settings confg = profile.colorGrading.settings;
			confg.tonemapping.neutralBlackOut = value;
			profile.colorGrading.settings = confg;
			break;
		case PostProcessType.chromatic:
			ChromaticAberrationModel.Settings confc = profile.chromaticAberration.settings;
			confc.intensity = value;
			profile.chromaticAberration.settings = confc;
			break;
		case PostProcessType.grain:
			GrainModel.Settings confgr = profile.grain.settings;
			confgr.intensity = value;
			profile.grain.settings = confgr;
			break;
		}
	}

	public void SetInitialPostProcessVector(PostProcessType pType, Vector3 value){
		switch (pType) {
		case PostProcessType.blue:
			ColorGradingModel.Settings confb = profile.colorGrading.settings;
			confb.channelMixer.blue = value;
			profile.colorGrading.settings = confb;
			break;
		}
	}

	public void StartInterpolation(PostProcessType pType, float target, float speed, float limit = 0.002f, InterpolationType iType = InterpolationType.damp){
		isFading = true;
		switch (pType) {
		case PostProcessType.vignette:
			StartCoroutine (InterpolateVignette (target, speed, limit, iType));
			break;
		case PostProcessType.blackout:
			StartCoroutine (InterpolateBlackOut (target, speed, limit, iType));
			break;
		case PostProcessType.chromatic:
			StartCoroutine (InterpolateChromatic (target, speed, limit, iType));
			break;
		case PostProcessType.grain:
			StartCoroutine (InterpolateGrain (target, speed, limit, iType));
			break;
		}
	}

	public void StartInterpolationVector(PostProcessType pType, Vector3 target, float speed, float limit = 0.002f, InterpolationType iType = InterpolationType.damp){
		isFading = true;
		switch (pType) {
		case PostProcessType.blue:
			StartCoroutine (InterpolateBlue (target, speed, limit, iType));
			break;
		}
	}


	IEnumerator InterpolateGrain(float target, float speed, float limit, InterpolationType iType){
		float velocity = 0;
		GrainModel.Settings conf = profile.grain.settings;
		while(Mathf.Abs(conf.intensity - target) > limit){
			switch (iType) {
			case InterpolationType.damp:
				conf.intensity = Mathf.SmoothDamp (conf.intensity, target, ref velocity, speed);
				break;
			case InterpolationType.lerp:
				conf.intensity = Mathf.Lerp (conf.intensity, target, speed);
				break;
			}

			profile.grain.settings = conf;
			yield return new WaitForEndOfFrame();
		}
		conf.intensity = target;
		profile.grain.settings = conf;
		isFading = false;
	}

	IEnumerator InterpolateVignette(float target, float speed, float limit, InterpolationType iType){
		float velocity = 0;
		VignetteModel.Settings conf = profile.vignette.settings;
		while(Mathf.Abs(conf.intensity - target) > limit){
			switch (iType) {
			case InterpolationType.damp:
				conf.intensity = Mathf.SmoothDamp (conf.intensity, target, ref velocity, speed);
				break;
			case InterpolationType.lerp:
				conf.intensity = Mathf.Lerp (conf.intensity, target, speed);
				break;
			}
			profile.vignette.settings = conf;
			yield return new WaitForEndOfFrame();
		}
		conf.intensity = target;
		profile.vignette.settings = conf;
		isFading = false;
	}

	IEnumerator InterpolateChromatic(float target, float speed, float limit, InterpolationType iType){
		float velocity = 0;
		ChromaticAberrationModel.Settings conf = profile.chromaticAberration.settings;
		while(Mathf.Abs(conf.intensity - target) > limit){
			switch (iType) {
			case InterpolationType.damp:
				conf.intensity = Mathf.SmoothDamp (conf.intensity, target, ref velocity, speed);
				break;
			case InterpolationType.lerp:
				conf.intensity = Mathf.Lerp (conf.intensity, target, speed);
				break;
			}
			profile.chromaticAberration.settings = conf;
			yield return new WaitForEndOfFrame();
		}
		conf.intensity = target;
		profile.chromaticAberration.settings = conf;
		isFading = false;
	}

	IEnumerator InterpolateBlackOut(float target, float speed, float limit, InterpolationType iType){
		float velocity = 0;
		ColorGradingModel.Settings conf = profile.colorGrading.settings;
		while(Mathf.Abs(conf.tonemapping.neutralBlackOut - target) > limit){
			switch (iType) {
			case InterpolationType.damp:
				conf.tonemapping.neutralBlackOut = Mathf.SmoothDamp (conf.tonemapping.neutralBlackOut, target, ref velocity, speed);
				break;
			case InterpolationType.lerp:
				conf.tonemapping.neutralBlackOut = Mathf.Lerp (conf.tonemapping.neutralBlackOut, target, speed);
				break;
			}
			profile.colorGrading.settings = conf;
			yield return new WaitForEndOfFrame();
		}
		conf.tonemapping.neutralBlackOut  = target;
		profile.colorGrading.settings = conf;
		isFading = false;
	}

	IEnumerator InterpolateBlue(Vector3 target, float speed, float limit, InterpolationType iType){
		Vector3 velocity = Vector3.zero;
		ColorGradingModel.Settings conf = profile.colorGrading.settings;
		while((conf.channelMixer.blue - target).magnitude > limit){
			switch (iType) {
			case InterpolationType.damp:
				conf.channelMixer.blue = Vector3.SmoothDamp (conf.channelMixer.blue, target, ref velocity, speed);
				break;
			case InterpolationType.lerp:
				conf.channelMixer.blue = Vector3.Lerp (conf.channelMixer.blue, target, speed);
				break;
			}
			profile.colorGrading.settings = conf;
			yield return new WaitForEndOfFrame();
		}
		conf.channelMixer.blue = target;
		profile.colorGrading.settings = conf;
		isFading = false;
	}

}
