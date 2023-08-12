# SubatomicCanvas

![Unity Version](https://img.shields.io/badge/Unity-2022.3-blue)

![fe7bf931-8428-453c-8e50-52e68047b6d9_base_resized](https://github.com/VMelville/subatomic-canvas/assets/43390710/bc55a1c1-1eff-4a20-baee-2af69769678c)

SubatomicCanvas is an easy-to-run particle simulation application made in Unity.

You can place parts of the measuring instrument wherever you like, generate any particle of your choice, and view the simulation results using Geant4.

It's currently in beta.<br>
The simulation results do not guarantee scientific accuracy.

At present, only Windows (64-bit) is supported.

You can also download the build version from [here](https://melville.booth.pm/items/4741968).

---

SubatomicCanvasは、素粒子物理のシミュレーションができるアプリケーションです。<br>
測定器のパーツを好きな場所に配置して好きな粒子を発生させる、Geant4を利用したシミュレーション結果を見ることができます。

現在ベータ版です。<br>
シミュレーション結果は科学的な正しさを保証するものではありません。

現状はWindows(64bit)のみをサポートしています。

ビルド版は[こちら](https://melville.booth.pm/items/4741968)からもダウンロードできます。

## Release Notes / リリースノート

[2023-08-12: v0.2.0]
- Released as open-source software (OSS). Consequently, the design has undergone significant changes.
- It's now possible to configure the number of cells, cell size, magnetic field, depth of the simulation range, and the energy range of initial particles.
- The display of particle names and particle trajectories can now be toggled.
- Particle names are now displayed in Japanese.

[2023-05-09: v0.1.2] Fixed an issue in v0.1.1 where the application didn't work depending on the installation destination.<br>
[2023-05-09: v0.1.1.1] Corrected a missing file issue in v0.1.1.<br>
[2023-05-09: v0.1.1] OS reboot is no longer required during installation.<br>
[2023-05-07: v0.1.0] Initial release.

---

[2023-08-12: v0.2.0]
- OSSとして公開。それに伴い、設計が大きく変更されました。
- セル数、セルサイズ、磁場、シミュレーションの範囲の奥行き、初期粒子のエネルギー範囲などを設定できるようになりました。
- 粒子名の表示、飛跡の表示が切り替えられるようになりました。
- 粒子の表示が日本語になりました。

[2023-05-09: v0.1.2] v0.1.1においてインストール先によって動作しない不具合を修正<br>
[2023-05-09: v0.1.1.1] v0.1.1で必要なファイルが欠損していたため修正<br>
[2023-05-09: v0.1.1] インストール時のOS再起動が不要になりました<br>
[2023-05-07: v0.1.0] 初版

## Running Locally / ローカルで動作させる場合

If you wish to run it on your local Unity, you'll need separate Dataset files.

1. Clone this repository.
2. Create a directory named `Geant4_Dataset` directly under the `subatomic-canvas` repository.
3. Visit [https://geant4.web.cern.ch/download](https://geant4.web.cern.ch/download) and download at least the following datasets: `G4EMLOW`, `G4ENSDFSTATE`, `G4PARTICLEXS`, `G4SAIDDATA`, and `PhotonEvaporation5.7`. Store these under the `Geant4_Dataset` directory.

It should work with these steps, but if you're unsure, feel free to ask me via [Twitter](https://twitter.com/MelvilleTw) or from X. If you notice any apparent issues, you can also contact us through the Issues section.

---

お手元のUnity上で動作させたい場合、別途Datasetファイルが必要です。

1. このリポジトリをクローンする。
2. subatomic-canvas のリポジトリ直下に `Geant4_Dataset` という名前のディレクトリを作成する。
3. https://geant4.web.cern.ch/download にアクセスし、Datasetsのうち少なくとも `G4EMLOW` `G4ENSDFSTATE` `G4PARTICLEXS` `G4SAIDDATA` `PhotonEvaporation5.7` をダウンロードし、 `Geant4_Dataset` 以下に格納する。

これで動作するはずですが、よくわからない場合は、Xから私( https://twitter.com/MelvilleTw )に質問するなどをしてください。<br>
明らかな問題が見られる場合には Issues からご連絡頂いても大丈夫です。


## Dependencies
- particle-sim-unity
  - Geant4
- VContainer
- UniRx
  - UniTask
