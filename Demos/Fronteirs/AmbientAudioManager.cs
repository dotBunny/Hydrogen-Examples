using System;
using UnityEngine;

/// <summary>
/// An example of how image data maps can be used to control the playing of all sorts of different things.
/// </summary>
public class AmbientAudioManager : MonoBehaviour
{
		/* Simple Emulation for the internal Mod Class used in the game*/
		public AudioClip[] DemoBank;

		public AudioClip Get (string key)
		{
				if (string.IsNullOrEmpty (key)) {
						Debug.Log ("Key Not Set");
						return null;
				}
				return DemoBank [Convert.ToInt32 (key)];
		}
		/*
		 * Create a look up table to simulate mod (get clip by string)
		 * manager setting are just the key (
		 * 
		 * always just use the stacks key reference to look up things never actually storing the item here
		 * 
		 * 
		 */
		public float FadeInSpeed = 0.5f;
		public float FadeOutSpeed = 0.5f;
		/// <summary>
		/// The current ChunkAudioSettings to be used.
		/// </summary>
		ChunkAudioSettings _chunkSettings;
		/// <summary>
		/// The current StructureInteriorAudio to be used.
		/// </summary>
		ChunkAudioItem _structureSettings;
		/// <summary>
		/// Is it daytime?
		/// </summary>
		bool _isDaytime;
		/// <summary>
		/// Are we underground?
		/// </summary>
		bool _isUnderground;
		/// <summary>
		/// Living in a world inside of walls?
		/// </summary>
		bool _isInsideStructure;
		/// <summary>
		/// Rain Intensity
		/// </summary>
		float _intensityRain;
		/// <summary>
		/// Thunder Intensity
		/// </summary>
		float _intensityThunder;
		/// <summary>
		/// Wind Intensity
		/// </summary>
		float _intensityWind;
		//public ChunkAudioItem UpdateChunkAudio
		/// <summary>
		/// Gets or sets the current ChunkAudioSettings.
		/// </summary>
		/// <value>The current ChunkAudioSettings.</value>
		public ChunkAudioSettings ChunkSettings {
				get { return _chunkSettings; }
				set { 
						if (value != _chunkSettings) {

								// Fade our old guys, but only if they are different then what were gonna be pushing
								if (_chunkSettings != null) {

										if (!string.IsNullOrEmpty (ChunkSettings.UgDeep.Key) &&
										    ChunkSettings.UgDeep.Key != value.UgDeep.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.UgDeep.Key)) {
												UpdateFlagBased (false, ChunkSettings.UgDeep.Key, null, 0f);
										}
										if (!string.IsNullOrEmpty (ChunkSettings.UgEnclosed.Key) &&
										    ChunkSettings.UgEnclosed.Key != value.UgEnclosed.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.UgEnclosed.Key)) {
												UpdateFlagBased (false, ChunkSettings.UgEnclosed.Key, null, 0f);
										}
										if (!string.IsNullOrEmpty (ChunkSettings.UgOpen.Key) &&
										    ChunkSettings.UgOpen.Key != value.UgOpen.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.UgOpen.Key)) {
												UpdateFlagBased (false, ChunkSettings.UgOpen.Key, null, 0f);
										}
										if (!string.IsNullOrEmpty (ChunkSettings.UgShallow.Key) &&
										    ChunkSettings.UgShallow.Key != value.UgShallow.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.UgShallow.Key)) {
												UpdateFlagBased (false, ChunkSettings.UgShallow.Key, null, 0f);
										}

												
										if (!string.IsNullOrEmpty (ChunkSettings.AgDayCoastal.Key) &&
										    ChunkSettings.AgDayCoastal.Key != value.AgDayCoastal.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.AgDayCoastal.Key)) {
												UpdateFlagBased (false, ChunkSettings.AgDayCoastal.Key, null, 0f);
										}
										if (!string.IsNullOrEmpty (ChunkSettings.AgDayCivilized.Key) &&
										    ChunkSettings.AgDayCivilized.Key != value.AgDayCivilized.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.AgDayCivilized.Key)) {
												UpdateFlagBased (false, ChunkSettings.AgDayCivilized.Key, null, 0f);
										}
										if (!string.IsNullOrEmpty (ChunkSettings.AgDayForest.Key) &&
										    ChunkSettings.AgDayForest.Key != value.AgDayForest.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.AgDayForest.Key)) {
												UpdateFlagBased (false, ChunkSettings.AgDayForest.Key, null, 0f);
										}
										if (!string.IsNullOrEmpty (ChunkSettings.AgDayOpen.Key) &&
										    ChunkSettings.AgDayOpen.Key != value.AgDayOpen.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.AgDayOpen.Key)) {
												UpdateFlagBased (false, ChunkSettings.AgDayOpen.Key, null, 0f);
										}
							
										if (!string.IsNullOrEmpty (ChunkSettings.AgNightCoastal.Key) &&
										    ChunkSettings.AgNightCoastal.Key != value.AgNightCoastal.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.AgNightCoastal.Key)) {
												UpdateFlagBased (false, ChunkSettings.AgNightCoastal.Key, null, 0f);
										}
										if (!string.IsNullOrEmpty (ChunkSettings.AgNightCivilized.Key) &&
										    ChunkSettings.AgNightCivilized.Key != value.AgNightCivilized.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.AgNightCivilized.Key)) {
												UpdateFlagBased (false, ChunkSettings.AgNightCivilized.Key, null, 0f);
										}
										if (!string.IsNullOrEmpty (ChunkSettings.AgNightForested.Key) &&
										    ChunkSettings.AgNightForested.Key != value.AgNightForested.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.AgNightForested.Key)) {
												UpdateFlagBased (false, ChunkSettings.AgNightForested.Key, null, 0f);
										}
										if (!string.IsNullOrEmpty (ChunkSettings.AgNightOpen.Key) &&
										    ChunkSettings.AgNightOpen.Key != value.AgNightOpen.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.AgNightOpen.Key)) {
												UpdateFlagBased (false, ChunkSettings.AgNightOpen.Key, null, 0f);
										}

										if (!string.IsNullOrEmpty (ChunkSettings.Rain.Key) &&
										    ChunkSettings.Rain.Key != value.Rain.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.Rain.Key)) {
												UpdateFlagBased (false, ChunkSettings.Rain.Key, null, 0f);
										}


										if (!string.IsNullOrEmpty (ChunkSettings.Thunder.Key) &&
										    ChunkSettings.Thunder.Key != value.Thunder.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.Thunder.Key)) {
												UpdateFlagBased (false, ChunkSettings.Thunder.Key, null, 0f);
										}


										if (!string.IsNullOrEmpty (ChunkSettings.Wind.Key) &&
										    ChunkSettings.Wind.Key != value.Wind.Key
										    && hAudioStack.Instance.IsPlaying (ChunkSettings.Wind.Key)) {
												UpdateFlagBased (false, ChunkSettings.Wind.Key, null, 0f);
										}

							
								}

								if (value != null) {
										if (_isUnderground) {
												if (!string.IsNullOrEmpty (value.UgDeep.Key)) {
														UpdateFlagBased (true, value.UgDeep.Key, 
																Get (value.UgDeep.Key), value.UgDeep.TargetVolume);
												}

												if (!string.IsNullOrEmpty (value.UgEnclosed.Key)) {
														UpdateFlagBased (true, value.UgEnclosed.Key, 
																Get (value.UgEnclosed.Key), value.UgEnclosed.TargetVolume);
												}

												if (!string.IsNullOrEmpty (value.UgShallow.Key)) {
														UpdateFlagBased (true, value.UgShallow.Key, 
																Get (value.UgShallow.Key), value.UgShallow.TargetVolume);
												}
												if (!string.IsNullOrEmpty (value.UgOpen.Key)) {
														UpdateFlagBased (true, value.UgOpen.Key, 
																Get (value.UgOpen.Key), value.UgOpen.TargetVolume);
												}
											
										} else if (!_isInsideStructure) {
												if (_isDaytime) {
														if (!string.IsNullOrEmpty (value.AgDayOpen.Key)) {
																UpdateFlagBased (true, value.AgDayOpen.Key, 
																		Get (value.AgDayOpen.Key), value.AgDayOpen.TargetVolume);
														}

														if (!string.IsNullOrEmpty (value.AgDayForest.Key)) {
																UpdateFlagBased (true, value.AgDayForest.Key, 
																		Get (value.AgDayForest.Key), value.AgDayForest.TargetVolume);
														}

														if (!string.IsNullOrEmpty (value.AgDayCoastal.Key)) {
																Debug.Log ("Called");
																UpdateFlagBased (true, value.AgDayCoastal.Key, 
																		Get (value.AgDayCoastal.Key), value.AgDayCoastal.TargetVolume);
														}

														if (!string.IsNullOrEmpty (value.AgDayCivilized.Key)) {
																UpdateFlagBased (true, value.AgDayCivilized.Key, 
																		Get (value.AgDayCivilized.Key), value.AgDayCivilized.TargetVolume);
														}
												} else {
														if (!string.IsNullOrEmpty (value.AgNightOpen.Key)) {
																UpdateFlagBased (true, value.AgNightOpen.Key, 
																		Get (value.AgNightOpen.Key), value.AgNightOpen.TargetVolume);
														}
														if (!string.IsNullOrEmpty (value.AgNightForested.Key)) {
																UpdateFlagBased (true, value.AgNightForested.Key, 
																		Get (value.AgNightForested.Key), value.AgNightForested.TargetVolume);
														}
														if (!string.IsNullOrEmpty (value.AgNightCoastal.Key)) {
																UpdateFlagBased (true, value.AgNightCoastal.Key, 
																		Get (value.AgNightCoastal.Key), value.AgNightCoastal.TargetVolume);
														}
														if (!string.IsNullOrEmpty (value.AgNightCivilized.Key)) {
																UpdateFlagBased (true, value.AgNightCivilized.Key, 
																		Get (value.AgNightCivilized.Key), value.AgNightCivilized.TargetVolume);
														}
												}
										}
								}
								_chunkSettings = value;

								if (_chunkSettings != null) {
										_chunkSettings.Wind.BaseVolume = _chunkSettings.Wind.TargetVolume;
										_chunkSettings.Rain.BaseVolume = _chunkSettings.Rain.TargetVolume;
										_chunkSettings.Thunder.BaseVolume = _chunkSettings.Thunder.TargetVolume;

										_chunkSettings.Thunder.TargetVolume = _chunkSettings.Thunder.BaseVolume * _intensityThunder;

										if (_chunkSettings.Thunder.TargetVolume > 0 &&
										    !string.IsNullOrEmpty (ChunkSettings.Thunder.Key)) {

												UpdateFlagBased (true,
														ChunkSettings.Thunder.Key, 
														Get (ChunkSettings.Thunder.Key), 
														_chunkSettings.Thunder.TargetVolume);


										}

										_chunkSettings.Rain.TargetVolume = _chunkSettings.Rain.BaseVolume * _intensityRain;
										if (_chunkSettings.Rain.TargetVolume > 0 &&
										    !string.IsNullOrEmpty (ChunkSettings.Rain.Key)) {

												UpdateFlagBased (true,
														ChunkSettings.Rain.Key, 
														Get (ChunkSettings.Rain.Key), 
														_chunkSettings.Rain.TargetVolume);


										}
										_chunkSettings.Wind.TargetVolume = _chunkSettings.Wind.BaseVolume * _intensityWind;
										if (_chunkSettings.Wind.TargetVolume > 0 &&
										    !string.IsNullOrEmpty (ChunkSettings.Wind.Key)) {

												UpdateFlagBased (true,
														ChunkSettings.Wind.Key, 
														Get (ChunkSettings.Wind.Key), 
														_chunkSettings.Wind.TargetVolume);
										}


								}
						}

				}
		}

		/// <summary>
		/// Gets or sets the current AmbientAudioSetting for structures.
		/// </summary>
		/// <value>The current structure AmbientAudioSetting</value>
		public ChunkAudioItem StructureSettings {
				get { return _structureSettings; }
				set { 
						if (value != _structureSettings) {

								if (_structureSettings != null) {
										if (!string.IsNullOrEmpty (_structureSettings.Key) && hAudioStack.Instance.IsPlaying (_structureSettings.Key))
												UpdateFlagBased (false, _structureSettings.Key, null, 0f);
								}
								// Turn off the old one

								if (value != null) {
										if (IsInsideStructure && !string.IsNullOrEmpty (value.Key)) {
												UpdateFlagBased (true, value.Key, 
														Get (value.Key), value.TargetVolume);
										}
								}

								_structureSettings = value;
						}
				}
		}

		public float ThunderIntensity {
				get { return _intensityThunder; }
				set {
						if (value != _intensityThunder) {
								if (ChunkSettings != null && !string.IsNullOrEmpty (ChunkSettings.Thunder.Key)) {
										ChunkSettings.Thunder.TargetVolume = ChunkSettings.Thunder.BaseVolume * value;
										if (ChunkSettings.Thunder.TargetVolume > 0) {
												UpdateFlagBased (true, 
														ChunkSettings.Thunder.Key, 
														Get (ChunkSettings.Thunder.Key), 
														ChunkSettings.Thunder.TargetVolume);

										} else {
												UpdateFlagBased (false, ChunkSettings.Thunder.Key, null, 0f);
										}
								}
						}

						_intensityThunder = value;

				}
		}

		public float WindIntensity {
				get { return _intensityWind; }
				set {
						if (value != _intensityWind) {
								if (ChunkSettings != null && !string.IsNullOrEmpty (ChunkSettings.Wind.Key)) {
										ChunkSettings.Wind.TargetVolume = ChunkSettings.Wind.BaseVolume * value;
										if (ChunkSettings.Wind.TargetVolume > 0) {
												UpdateFlagBased (true, 
														ChunkSettings.Wind.Key, 
														Get (ChunkSettings.Wind.Key), 
														ChunkSettings.Wind.TargetVolume);

										} else {
												UpdateFlagBased (false, ChunkSettings.Wind.Key, null, 0f);
										}
								}
								_intensityWind = value;
						}

				}
		}

		public float RainIntensity {
				get { return _intensityRain; }
				set {
						if (value != _intensityRain) {
								if (ChunkSettings != null && !string.IsNullOrEmpty (ChunkSettings.Rain.Key)) {
										ChunkSettings.Rain.TargetVolume = ChunkSettings.Wind.BaseVolume * value;
										if (ChunkSettings.Rain.TargetVolume > 0) {
												UpdateFlagBased (true, 
														ChunkSettings.Rain.Key, 
														Get (ChunkSettings.Rain.Key), 
														ChunkSettings.Rain.TargetVolume);

										} else {
												UpdateFlagBased (false, ChunkSettings.Rain.Key, null, 0f);
										}
								}
								_intensityRain = value;
						}

				}
		}

		/// <summary>
		/// Gets or sets a value indicating whether it is daytime.
		/// </summary>
		/// <value><c>true</c> if its daytime; otherwise, <c>false</c>.</value>
		public bool IsDaytime {
				get { return _isDaytime; }
				set {
						if (ChunkSettings == null) {
								Debug.Log ("No AboveGround (ChunkSettings) Ambience Settings Found");
								_isDaytime = value;
								return;
						}

						if (value != _isDaytime) {

								if (!_isUnderground && !_isInsideStructure) {
										if (value) {
												ToggleAboveGroundDayAmbience (true);
												ToggleAboveGroundNightAmbience (false);
										} else {
												ToggleAboveGroundDayAmbience (false);
												ToggleAboveGroundNightAmbience (true);
										}

								}
								_isDaytime = value;
						}
				}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the player is inside structure.
		/// </summary>
		/// <value><c>true</c> if inside a structure otherwise, <c>false</c>.</value>
		public bool IsInsideStructure {
				get { return _isInsideStructure; }
				set {
						if (value != _isInsideStructure) {

								// Update Our Structure
								if (StructureSettings != null) {
										ToggleStructureAmbience (value);
								}

								// Deal with Above Ground Sounds
								if (!_isUnderground) {

										ToggleAboveGroundEffects (!value);

										if (_isDaytime) {
												ToggleAboveGroundDayAmbience (!value);
										} else {
												ToggleAboveGroundNightAmbience (!value);
										}
								} else if (_isUnderground) {
										// Turn on underground
										ToggleUnderGroundAmbience (!value);
								}

								// Add turn on underground or aboveground
								_isInsideStructure = value;
						}
				}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the player is underground.
		/// </summary>
		/// <value><c>true</c> if underground; otherwise, <c>false</c>.</value>
		public bool IsUnderground {
				get { return _isUnderground; }
				set {
						if (ChunkSettings == null) {
								Debug.Log ("No Underground (ChunkSettings) Ambience Settings Found");
								_isUnderground = value;
								return;
						}
						if (value != _isUnderground) {

								ToggleUnderGroundAmbience (value);

								// Turn off above aground 
								if (value) {
										if (_isDaytime) {
												ToggleAboveGroundDayAmbience (false);

										} else {
												ToggleAboveGroundNightAmbience (false);
										}
										ToggleAboveGroundEffects (false);

								} else {
										if (_isDaytime) {
												ToggleAboveGroundDayAmbience (true);
										} else {
												ToggleAboveGroundNightAmbience (true);
										}
										ToggleAboveGroundEffects (true);
								}
								_isUnderground = value;
						}
				}
		}

		/// <summary>
		/// Updates the current ChunkAudioSettings classes volume levels according to the passed color
		/// </summary>
		/// <param name="color">Color values to use to determine volume levels.</param>
		public void UpdateStackVolumes (Color color)
		{
				ChunkSettings.AgDayCoastal.TargetVolume = color.r;
				ChunkSettings.AgNightCoastal.TargetVolume = color.r;
				ChunkSettings.UgShallow.TargetVolume = color.r;

				ChunkSettings.AgDayForest.TargetVolume = color.r;
				ChunkSettings.AgNightForested.TargetVolume = color.r;
				ChunkSettings.UgDeep.TargetVolume = color.r;

				ChunkSettings.AgDayCivilized.TargetVolume = color.r;
				ChunkSettings.AgNightCivilized.TargetVolume = color.r;
				ChunkSettings.UgEnclosed.TargetVolume = color.r;

				ChunkSettings.AgDayOpen.TargetVolume = color.r;
				ChunkSettings.AgNightOpen.TargetVolume = color.r;
				ChunkSettings.UgOpen.TargetVolume = color.r;

				PushVolumesToStack ();
		}

		public static ChunkAudioSettings UpdateChunkAudioSettingsVolumes (ChunkAudioSettings settings, Color color)
		{
				settings.AgDayCoastal.TargetVolume = color.r;
				settings.AgNightCoastal.TargetVolume = color.r;
				settings.UgShallow.TargetVolume = color.r;

				settings.AgDayForest.TargetVolume = color.r;
				settings.AgNightForested.TargetVolume = color.r;
				settings.UgDeep.TargetVolume = color.r;

				settings.AgDayCivilized.TargetVolume = color.r;
				settings.AgNightCivilized.TargetVolume = color.r;
				settings.UgEnclosed.TargetVolume = color.r;

				settings.AgDayOpen.TargetVolume = color.r;
				settings.AgNightOpen.TargetVolume = color.r;
				settings.UgOpen.TargetVolume = color.r;

				return settings;
		}

		void PushVolumesToStack ()
		{
				if (_isInsideStructure && hAudioStack.Instance.IsLoaded (StructureSettings.Key)) {
						hAudioStack.Instance.LoadedItems [StructureSettings.Key].TargetVolume = 
								StructureSettings.TargetVolume;

				} else if (_isUnderground) {

						if (hAudioStack.Instance.IsLoaded (ChunkSettings.UgDeep.Key)) {
								hAudioStack.Instance.LoadedItems [ChunkSettings.UgDeep.Key].TargetVolume = 
										ChunkSettings.UgDeep.TargetVolume;
						}
						if (hAudioStack.Instance.IsLoaded (ChunkSettings.UgEnclosed.Key)) {
								hAudioStack.Instance.LoadedItems [ChunkSettings.UgEnclosed.Key].TargetVolume = 
										ChunkSettings.UgEnclosed.TargetVolume;
						}

						if (hAudioStack.Instance.IsLoaded (ChunkSettings.UgOpen.Key)) {
								hAudioStack.Instance.LoadedItems [ChunkSettings.UgOpen.Key].TargetVolume = 
										ChunkSettings.UgOpen.TargetVolume;
						}

						if (hAudioStack.Instance.IsLoaded (ChunkSettings.UgShallow.Key)) {
								hAudioStack.Instance.LoadedItems [ChunkSettings.UgShallow.Key].TargetVolume = 
										ChunkSettings.UgShallow.TargetVolume;
						}

				} else if (_isDaytime) {
						if (hAudioStack.Instance.IsLoaded (ChunkSettings.AgDayOpen.Key)) {
								hAudioStack.Instance.LoadedItems [ChunkSettings.AgDayOpen.Key].TargetVolume = 
										ChunkSettings.AgDayOpen.TargetVolume;
						}
						if (hAudioStack.Instance.IsLoaded (ChunkSettings.AgDayForest.Key)) {
								hAudioStack.Instance.LoadedItems [ChunkSettings.AgDayForest.Key].TargetVolume = 
										ChunkSettings.AgDayForest.TargetVolume;
						}

						if (hAudioStack.Instance.IsLoaded (ChunkSettings.AgDayCoastal.Key)) {
								hAudioStack.Instance.LoadedItems [ChunkSettings.AgDayCoastal.Key].TargetVolume = 
										ChunkSettings.AgDayCoastal.TargetVolume;
						}

						if (hAudioStack.Instance.IsLoaded (ChunkSettings.AgDayCivilized.Key)) {
								hAudioStack.Instance.LoadedItems [ChunkSettings.AgDayCivilized.Key].TargetVolume = 
										ChunkSettings.AgDayCivilized.TargetVolume;
						}
				} else {
						if (hAudioStack.Instance.IsLoaded (ChunkSettings.AgNightOpen.Key)) {
								hAudioStack.Instance.LoadedItems [ChunkSettings.AgNightOpen.Key].TargetVolume = 
										ChunkSettings.AgNightOpen.TargetVolume;
						}
						if (hAudioStack.Instance.IsLoaded (ChunkSettings.AgNightForested.Key)) {
								hAudioStack.Instance.LoadedItems [ChunkSettings.AgNightForested.Key].TargetVolume = 
										ChunkSettings.AgNightForested.TargetVolume;
						}

						if (hAudioStack.Instance.IsLoaded (ChunkSettings.AgNightCoastal.Key)) {
								hAudioStack.Instance.LoadedItems [ChunkSettings.AgNightCoastal.Key].TargetVolume = 
										ChunkSettings.AgNightCoastal.TargetVolume;
						}

						if (hAudioStack.Instance.IsLoaded (ChunkSettings.AgNightCivilized.Key)) {
								hAudioStack.Instance.LoadedItems [ChunkSettings.AgNightCivilized.Key].TargetVolume = 
										ChunkSettings.AgNightCivilized.TargetVolume;
						}
				}
		}

		void ToggleAboveGroundDayAmbience (bool flag)
		{

				if (flag) {
						if (!string.IsNullOrEmpty (ChunkSettings.AgDayCoastal.Key)) {
								UpdateFlagBased (flag,
										ChunkSettings.AgDayCoastal.Key, 
										Get (ChunkSettings.AgDayCoastal.Key), 
										ChunkSettings.AgDayCoastal.TargetVolume);
						}

						if (!string.IsNullOrEmpty (ChunkSettings.AgDayForest.Key)) {
						
								UpdateFlagBased (flag, 
										ChunkSettings.AgDayForest.Key, 
										Get (ChunkSettings.AgDayForest.Key), 
										ChunkSettings.AgDayForest.TargetVolume);
						}



						if (!string.IsNullOrEmpty (ChunkSettings.AgDayCivilized.Key)) {

								UpdateFlagBased (flag, 
										ChunkSettings.AgDayCivilized.Key, 
										Get (ChunkSettings.AgDayCivilized.Key), 
										ChunkSettings.AgDayCivilized.TargetVolume);
						}


						if (!string.IsNullOrEmpty (ChunkSettings.AgDayOpen.Key)) {

								UpdateFlagBased (flag, 
										ChunkSettings.AgDayOpen.Key, 
										Get (ChunkSettings.AgDayOpen.Key), 
										ChunkSettings.AgDayOpen.TargetVolume);
						}

					

				} else {
						if (!string.IsNullOrEmpty (ChunkSettings.AgDayCoastal.Key)) {

								UpdateFlagBased (flag,
										ChunkSettings.AgDayCoastal.Key, 
										null, 
										ChunkSettings.AgDayCoastal.TargetVolume);
						}

						if (!string.IsNullOrEmpty (ChunkSettings.AgDayForest.Key)) {

								UpdateFlagBased (flag, 
										ChunkSettings.AgDayForest.Key, 
										null, 
										ChunkSettings.AgDayForest.TargetVolume);
						}

						if (!string.IsNullOrEmpty (ChunkSettings.AgDayCivilized.Key)) {

								UpdateFlagBased (flag, 
										ChunkSettings.AgDayCivilized.Key, 
										null, 
										ChunkSettings.AgDayCivilized.TargetVolume);
						}

						if (!string.IsNullOrEmpty (ChunkSettings.AgDayOpen.Key)) {
								UpdateFlagBased (flag, 
										ChunkSettings.AgDayOpen.Key, 
										null, 
										ChunkSettings.AgDayOpen.TargetVolume);
						}


				}
		}

		void ToggleAboveGroundEffects (bool flag)
		{
				if (flag) {
						if (!string.IsNullOrEmpty (ChunkSettings.Rain.Key)) {
								UpdateFlagBased (flag, ChunkSettings.Rain.Key, 
										Get (ChunkSettings.Rain.Key), 
										ChunkSettings.Rain.TargetVolume);
						}

						if (!string.IsNullOrEmpty (ChunkSettings.Thunder.Key)) {
								UpdateFlagBased (flag, ChunkSettings.Thunder.Key, 
										Get (ChunkSettings.Thunder.Key), 
										ChunkSettings.Wind.TargetVolume);
						}

						if (!string.IsNullOrEmpty (ChunkSettings.Wind.Key)) {
								UpdateFlagBased (flag, ChunkSettings.Wind.Key, 
										Get (ChunkSettings.Wind.Key), 
										ChunkSettings.Wind.TargetVolume);
						}

				} else {
						if (!string.IsNullOrEmpty (ChunkSettings.Rain.Key)) {
								UpdateFlagBased (flag, ChunkSettings.Rain.Key, 
										null, 
										ChunkSettings.Rain.TargetVolume);
						}

						if (!string.IsNullOrEmpty (ChunkSettings.Thunder.Key)) {
								UpdateFlagBased (flag, ChunkSettings.Thunder.Key, 
										null, 
										ChunkSettings.Wind.TargetVolume);
						}

						if (!string.IsNullOrEmpty (ChunkSettings.Wind.Key)) {
								UpdateFlagBased (flag, ChunkSettings.Wind.Key, 
										null, 
										ChunkSettings.Wind.TargetVolume);
						}
				}
		}

		void ToggleAboveGroundNightAmbience (bool flag)
		{
				if (flag) {
						if (!string.IsNullOrEmpty (ChunkSettings.AgNightCoastal.Key)) {
								UpdateFlagBased (flag,
										ChunkSettings.AgNightCoastal.Key, 
										Get (ChunkSettings.AgNightCoastal.Key), 
										ChunkSettings.AgNightCoastal.TargetVolume);
						}

						if (!string.IsNullOrEmpty (ChunkSettings.AgNightForested.Key)) {
								UpdateFlagBased (flag, 
										ChunkSettings.AgNightForested.Key, 
										Get (ChunkSettings.AgNightForested.Key), 
										ChunkSettings.AgNightForested.TargetVolume);
						}

						if (!string.IsNullOrEmpty (ChunkSettings.AgNightCivilized.Key)) {
								UpdateFlagBased (flag, 
										ChunkSettings.AgNightCivilized.Key, 
										Get (ChunkSettings.AgNightCivilized.Key), 
										ChunkSettings.AgNightCivilized.TargetVolume);
						}

						if (!string.IsNullOrEmpty (ChunkSettings.AgNightOpen.Key)) {
								UpdateFlagBased (flag, 
										ChunkSettings.AgNightOpen.Key, 
										Get (ChunkSettings.AgNightOpen.Key), 
										ChunkSettings.AgNightOpen.TargetVolume);
						}

				} else {
						if (!string.IsNullOrEmpty (ChunkSettings.AgNightCoastal.Key)) {
								UpdateFlagBased (flag,
										ChunkSettings.AgNightCoastal.Key, 
										null, 
										ChunkSettings.AgNightCoastal.TargetVolume);
						}
						if (!string.IsNullOrEmpty (ChunkSettings.AgNightForested.Key)) {
								UpdateFlagBased (flag, 
										ChunkSettings.AgNightForested.Key, 
										null, 
										ChunkSettings.AgNightForested.TargetVolume);
						}
						if (!string.IsNullOrEmpty (ChunkSettings.AgNightCivilized.Key)) {
								UpdateFlagBased (flag, 
										ChunkSettings.AgNightCivilized.Key, 
										null, 
										ChunkSettings.AgNightCivilized.TargetVolume);
						}
						if (!string.IsNullOrEmpty (ChunkSettings.AgNightOpen.Key)) {
								UpdateFlagBased (flag, 
										ChunkSettings.AgNightOpen.Key, 
										null, 
										ChunkSettings.AgNightOpen.TargetVolume);
						}
				}
		}

		void ToggleStructureAmbience (bool flag)
		{
				if (!string.IsNullOrEmpty (StructureSettings.Key)) {
						UpdateFlagBased (flag, StructureSettings.Key, 
								Get (StructureSettings.Key),
								StructureSettings.TargetVolume);
				}
		}

		void ToggleUnderGroundAmbience (bool flag)
		{
				if (flag) {
						if (!string.IsNullOrEmpty (ChunkSettings.UgShallow.Key)) {
								UpdateFlagBased (flag,
										ChunkSettings.UgShallow.Key, 
										Get (ChunkSettings.UgShallow.Key), 
										ChunkSettings.UgShallow.TargetVolume);
						}
						if (!string.IsNullOrEmpty (ChunkSettings.UgDeep.Key)) {
								UpdateFlagBased (flag, 
										ChunkSettings.UgDeep.Key, 
										Get (ChunkSettings.UgDeep.Key), 
										ChunkSettings.UgDeep.TargetVolume);
						}
						if (!string.IsNullOrEmpty (ChunkSettings.UgEnclosed.Key)) {
								UpdateFlagBased (flag, 
										ChunkSettings.UgDeep.Key, 
										Get (ChunkSettings.UgEnclosed.Key), 
										ChunkSettings.UgEnclosed.TargetVolume);
						}
						if (!string.IsNullOrEmpty (ChunkSettings.UgOpen.Key)) {
								UpdateFlagBased (flag, 
										ChunkSettings.UgOpen.Key, 
										Get (ChunkSettings.UgOpen.Key), 
										ChunkSettings.UgOpen.TargetVolume);
						}
				} else {
						if (!string.IsNullOrEmpty (ChunkSettings.UgShallow.Key)) {
								UpdateFlagBased (flag,
										ChunkSettings.UgShallow.Key, 
										null, 
										ChunkSettings.UgShallow.TargetVolume);
						}
						if (!string.IsNullOrEmpty (ChunkSettings.UgDeep.Key)) {
								UpdateFlagBased (flag, 
										ChunkSettings.UgDeep.Key, 
										null, 
										ChunkSettings.UgDeep.TargetVolume);
						}
						if (!string.IsNullOrEmpty (ChunkSettings.UgDeep.Key)) {
								UpdateFlagBased (flag, 
										ChunkSettings.UgEnclosed.Key, 
										null, 
										ChunkSettings.UgEnclosed.TargetVolume);
						}
						if (!string.IsNullOrEmpty (ChunkSettings.UgOpen.Key)) {
								UpdateFlagBased (flag, 
										ChunkSettings.UgOpen.Key, 
										null, 
										ChunkSettings.UgOpen.TargetVolume);
						}
				}
		}

		void UpdateFlagBased (bool flag, string key, AudioClip clip, float targetVolume)
		{
				// Without the key we cannot do anything
				if (string.IsNullOrEmpty (key)) {
						return;
				}

				if (flag && !hAudioStack.Instance.IsLoaded (key)) {
						var newAudio = new Hydrogen.Core.AudioStackItem (clip, key);

						// Settings On Interior Audio
						newAudio.Loop = true;
						newAudio.Fade = true;
						newAudio.FadeInSpeed = FadeInSpeed;
						newAudio.FadeOutSpeed = FadeOutSpeed;
						newAudio.StartVolume = 0f;

						// Add to Stack
						hAudioStack.Instance.Add (newAudio);

				} else if (flag && hAudioStack.Instance.IsPlaying (key)) {
						hAudioStack.Instance.LoadedItems [key].TargetVolume = targetVolume;
				} else if (flag && hAudioStack.Instance.IsLoaded (key)) {
						hAudioStack.Instance.LoadedItems [key].TargetVolume = targetVolume;
						hAudioStack.Instance.LoadedItems [key].Play ();
				} else if (!flag && hAudioStack.Instance.IsLoaded (key)) {
						hAudioStack.Instance.LoadedItems [key].TargetVolume = 0f;
				}
		}
		//check to see which audio clips are supposed to be playing
		/*				

						
				use RainIntensity / WindIntensity / ThunderIntensity to set volumes of:
				- Rain
				- Wind
				- Thunder
				
				//if there are clips that are supposed to be playing but aren't, load the clip and start them at volume 0
				//don't wait for other clips to finish fading out - start playing them right away
				//if there are clips playing that aren't at max volume, fade them from 0 to AmbientAudioSetting.MaxVolume over FadeInTime seconds
				
				//in the case of Rain / Wind / Thunder, max volume will be RainIntensity * AmbientAudioSetting.MaxVolume
				//RainIntensity etc. will be different on each refresh so the max volume will have to be adjusted on each refresh


								
				AudioClip newClip = Mods.Get.Clip (CurrentChunkName, StructureInteriorAudio.ClipName);

				*/
		[Serializable]
		public class ChunkAudioItem
		{
				public string Key;
				public float TargetVolume = 1f;
				[System.NonSerialized]
				public float BaseVolume;
		}

		[Serializable]
		public class ChunkAudioSettings
		{
				public ChunkAudioItem AgDayCoastal;
				//R
				public ChunkAudioItem AgDayForest;
				//G
				public ChunkAudioItem AgDayCivilized;
				//B
				public ChunkAudioItem AgDayOpen;
				//A
				public ChunkAudioItem AgNightCoastal;
				//R
				public ChunkAudioItem AgNightForested;
				//G
				public ChunkAudioItem AgNightCivilized;
				//B
				public ChunkAudioItem AgNightOpen;
				//A
				public ChunkAudioItem UgShallow;
				//R
				public ChunkAudioItem UgDeep;
				//G
				public ChunkAudioItem UgEnclosed;
				//B
				public ChunkAudioItem UgOpen;
				//A
				public ChunkAudioItem Rain;
				public ChunkAudioItem Wind;
				public ChunkAudioItem Thunder;
		}
}
