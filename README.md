# 環境構築
----------

- [Unityの最新バージョン][4]取得
- GitHubの[アカウント](https://github.com/)を取得
- [Gitをサポートできるソフトウェア](Gitをサポートできるソフトウェア)を取得
- このリポジトリをcloneする

## Gitをサポートできるソフトウェア
----------

[SourceTree][1]のほうが機能が充実しているためをすすめですが、そのほか[Github For Windows][2]、[Github For Mac][3]もある。


## Gitについての簡単な説明
----------

SVNのワークフロー： 

    svn checkout
    svn update -> svn commit

Gitのワークフロー： 

    git clone
    git pull -> git commit -> git push
    
SVNは、リモートの一つのリビジョンと同期するため、リポジトリをcheckoutした後、updateで作業フォルダを更新し、そしてcommitで自分の変更をリモートに反映させるワークフローにある。歴史はローカルに保存しないため、ログを見るや、過去のリビジョンに戻るときに通信が発生する。

Gitは、作業フォルダとリモートのリポジトリと同期するため、リポジトリをcloneした後、基本ローカルでのcommitとなる、pullとpushは、リポジトリの同期とコマンドとなる。pullでローカルリポジトリのないものをリモートから取得し、pushでその逆、リモートリポジトリにローカルリポジトリの更新を反映させる。詳しくは[公式サイトのドキュメント][5]で見てください。


  [1]: https://www.atlassian.com/software/sourcetree/overview
  [2]: https://windows.github.com
  [3]: https://mac.github.com/
  [4]: http://japan.unity3d.com/unity/download/
  [5]: http://git-scm.com/doc



