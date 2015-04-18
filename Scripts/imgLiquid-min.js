



<!DOCTYPE html>
<html lang="en" class="">
  <head prefix="og: http://ogp.me/ns# fb: http://ogp.me/ns/fb# object: http://ogp.me/ns/object# article: http://ogp.me/ns/article# profile: http://ogp.me/ns/profile#">
    <meta charset='utf-8'>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta http-equiv="Content-Language" content="en">
    
    
    <title>imgLiquid/imgLiquid-min.js at master · karacas/imgLiquid · GitHub</title>
    <link rel="search" type="application/opensearchdescription+xml" href="/opensearch.xml" title="GitHub">
    <link rel="fluid-icon" href="https://github.com/fluidicon.png" title="GitHub">
    <link rel="apple-touch-icon" sizes="57x57" href="/apple-touch-icon-114.png">
    <link rel="apple-touch-icon" sizes="114x114" href="/apple-touch-icon-114.png">
    <link rel="apple-touch-icon" sizes="72x72" href="/apple-touch-icon-144.png">
    <link rel="apple-touch-icon" sizes="144x144" href="/apple-touch-icon-144.png">
    <meta property="fb:app_id" content="1401488693436528">

      <meta content="@github" name="twitter:site" /><meta content="summary" name="twitter:card" /><meta content="karacas/imgLiquid" name="twitter:title" /><meta content="imgLiquid - jQuery plugin to resize images to fit in a container." name="twitter:description" /><meta content="https://avatars0.githubusercontent.com/u/1050937?v=3&amp;s=400" name="twitter:image:src" />
<meta content="GitHub" property="og:site_name" /><meta content="object" property="og:type" /><meta content="https://avatars0.githubusercontent.com/u/1050937?v=3&amp;s=400" property="og:image" /><meta content="karacas/imgLiquid" property="og:title" /><meta content="https://github.com/karacas/imgLiquid" property="og:url" /><meta content="imgLiquid - jQuery plugin to resize images to fit in a container." property="og:description" />

      <meta name="browser-stats-url" content="/_stats">
    <link rel="assets" href="https://assets-cdn.github.com/">
    <link rel="conduit-xhr" href="https://ghconduit.com:25035">
    
    <meta name="pjax-timeout" content="1000">
    

    <meta name="msapplication-TileImage" content="/windows-tile.png">
    <meta name="msapplication-TileColor" content="#ffffff">
    <meta name="selected-link" value="repo_source" data-pjax-transient>
      <meta name="google-analytics" content="UA-3769691-2">

    <meta content="collector.githubapp.com" name="octolytics-host" /><meta content="collector-cdn.github.com" name="octolytics-script-host" /><meta content="github" name="octolytics-app-id" /><meta content="52015C5C:06DC:62AF01:54AA93C6" name="octolytics-dimension-request_id" />
    
    <meta content="Rails, view, blob#show" name="analytics-event" />

    
    
    <link rel="icon" type="image/x-icon" href="https://assets-cdn.github.com/favicon.ico">


    <meta content="authenticity_token" name="csrf-param" />
<meta content="7m38/M1H9m8fhzU2MPA+FuNEPck6F2l7l95H/oRUR2vayBofs+5BQmORWz3JpuQzgigysCDLERhM79f6Ndm1MA==" name="csrf-token" />

    <link href="https://assets-cdn.github.com/assets/github-9bcf5def7eb44e2a101b20aaecf3707f4b0a10ab8f4d6eebec29371f821c4b29.css" media="all" rel="stylesheet" type="text/css" />
    <link href="https://assets-cdn.github.com/assets/github2-129934a361e0bd5744fbb282dc3aff37971ec4a1f88c5a47e17cc75e591bec22.css" media="all" rel="stylesheet" type="text/css" />
    
    


    <meta http-equiv="x-pjax-version" content="3083797b26bb5fbc834b9df83cea0187">

      
  <meta name="description" content="imgLiquid - jQuery plugin to resize images to fit in a container.">
  <meta name="go-import" content="github.com/karacas/imgLiquid git https://github.com/karacas/imgLiquid.git">

  <meta content="1050937" name="octolytics-dimension-user_id" /><meta content="karacas" name="octolytics-dimension-user_login" /><meta content="6094321" name="octolytics-dimension-repository_id" /><meta content="karacas/imgLiquid" name="octolytics-dimension-repository_nwo" /><meta content="true" name="octolytics-dimension-repository_public" /><meta content="false" name="octolytics-dimension-repository_is_fork" /><meta content="6094321" name="octolytics-dimension-repository_network_root_id" /><meta content="karacas/imgLiquid" name="octolytics-dimension-repository_network_root_nwo" />
  <link href="https://github.com/karacas/imgLiquid/commits/master.atom" rel="alternate" title="Recent Commits to imgLiquid:master" type="application/atom+xml">

  </head>


  <body class="logged_out  env-production windows vis-public page-blob">
    <a href="#start-of-content" tabindex="1" class="accessibility-aid js-skip-to-content">Skip to content</a>
    <div class="wrapper">
      
      
      
      


      
      <div class="header header-logged-out" role="banner">
  <div class="container clearfix">

    <a class="header-logo-wordmark" href="https://github.com/" ga-data-click="(Logged out) Header, go to homepage, icon:logo-wordmark">
      <span class="mega-octicon octicon-logo-github"></span>
    </a>

    <div class="header-actions" role="navigation">
        <a class="button primary" href="/join" data-ga-click="(Logged out) Header, clicked Sign up, text:sign-up">Sign up</a>
      <a class="button" href="/login?return_to=%2Fkaracas%2FimgLiquid%2Fblob%2Fmaster%2Fjs%2FimgLiquid-min.js" data-ga-click="(Logged out) Header, clicked Sign in, text:sign-in">Sign in</a>
    </div>

    <div class="site-search repo-scope js-site-search" role="search">
      <form accept-charset="UTF-8" action="/karacas/imgLiquid/search" class="js-site-search-form" data-global-search-url="/search" data-repo-search-url="/karacas/imgLiquid/search" method="get"><div style="margin:0;padding:0;display:inline"><input name="utf8" type="hidden" value="&#x2713;" /></div>
  <input type="text"
    class="js-site-search-field is-clearable"
    data-hotkey="s"
    name="q"
    placeholder="Search"
    data-global-scope-placeholder="Search GitHub"
    data-repo-scope-placeholder="Search"
    tabindex="1"
    autocapitalize="off">
  <div class="scope-badge">This repository</div>
</form>
    </div>

      <ul class="header-nav left" role="navigation">
          <li class="header-nav-item">
            <a class="header-nav-link" href="/explore" data-ga-click="(Logged out) Header, go to explore, text:explore">Explore</a>
          </li>
          <li class="header-nav-item">
            <a class="header-nav-link" href="/features" data-ga-click="(Logged out) Header, go to features, text:features">Features</a>
          </li>
          <li class="header-nav-item">
            <a class="header-nav-link" href="https://enterprise.github.com/" data-ga-click="(Logged out) Header, go to enterprise, text:enterprise">Enterprise</a>
          </li>
          <li class="header-nav-item">
            <a class="header-nav-link" href="/blog" data-ga-click="(Logged out) Header, go to blog, text:blog">Blog</a>
          </li>
      </ul>

  </div>
</div>



      <div id="start-of-content" class="accessibility-aid"></div>
          <div class="site" itemscope itemtype="http://schema.org/WebPage">
    <div id="js-flash-container">
      
    </div>
    <div class="pagehead repohead instapaper_ignore readability-menu">
      <div class="container">
        
<ul class="pagehead-actions">


  <li>
      <a href="/login?return_to=%2Fkaracas%2FimgLiquid"
    class="minibutton with-count star-button tooltipped tooltipped-n"
    aria-label="You must be signed in to star a repository" rel="nofollow">
    <span class="octicon octicon-star"></span>
    Star
  </a>

    <a class="social-count js-social-count" href="/karacas/imgLiquid/stargazers">
      804
    </a>

  </li>

    <li>
      <a href="/login?return_to=%2Fkaracas%2FimgLiquid"
        class="minibutton with-count js-toggler-target fork-button tooltipped tooltipped-n"
        aria-label="You must be signed in to fork a repository" rel="nofollow">
        <span class="octicon octicon-repo-forked"></span>
        Fork
      </a>
      <a href="/karacas/imgLiquid/network" class="social-count">
        169
      </a>
    </li>
</ul>

        <h1 itemscope itemtype="http://data-vocabulary.org/Breadcrumb" class="entry-title public">
          <span class="mega-octicon octicon-repo"></span>
          <span class="author"><a href="/karacas" class="url fn" itemprop="url" rel="author"><span itemprop="title">karacas</span></a></span><!--
       --><span class="path-divider">/</span><!--
       --><strong><a href="/karacas/imgLiquid" class="js-current-repository" data-pjax="#js-repo-pjax-container">imgLiquid</a></strong>

          <span class="page-context-loader">
            <img alt="" height="16" src="https://assets-cdn.github.com/images/spinners/octocat-spinner-32.gif" width="16" />
          </span>

        </h1>
      </div><!-- /.container -->
    </div><!-- /.repohead -->

    <div class="container">
      <div class="repository-with-sidebar repo-container new-discussion-timeline  ">
        <div class="repository-sidebar clearfix">
            
<nav class="sunken-menu repo-nav js-repo-nav js-sidenav-container-pjax js-octicon-loaders"
     role="navigation"
     data-pjax="#js-repo-pjax-container"
     data-issue-count-url="/karacas/imgLiquid/issues/counts">
  <ul class="sunken-menu-group">
    <li class="tooltipped tooltipped-w" aria-label="Code">
      <a href="/karacas/imgLiquid" aria-label="Code" class="selected js-selected-navigation-item sunken-menu-item" data-hotkey="g c" data-selected-links="repo_source repo_downloads repo_commits repo_releases repo_tags repo_branches /karacas/imgLiquid">
        <span class="octicon octicon-code"></span> <span class="full-word">Code</span>
        <img alt="" class="mini-loader" height="16" src="https://assets-cdn.github.com/images/spinners/octocat-spinner-32.gif" width="16" />
</a>    </li>

      <li class="tooltipped tooltipped-w" aria-label="Issues">
        <a href="/karacas/imgLiquid/issues" aria-label="Issues" class="js-selected-navigation-item sunken-menu-item" data-hotkey="g i" data-selected-links="repo_issues repo_labels repo_milestones /karacas/imgLiquid/issues">
          <span class="octicon octicon-issue-opened"></span> <span class="full-word">Issues</span>
          <span class="js-issue-replace-counter"></span>
          <img alt="" class="mini-loader" height="16" src="https://assets-cdn.github.com/images/spinners/octocat-spinner-32.gif" width="16" />
</a>      </li>

    <li class="tooltipped tooltipped-w" aria-label="Pull Requests">
      <a href="/karacas/imgLiquid/pulls" aria-label="Pull Requests" class="js-selected-navigation-item sunken-menu-item" data-hotkey="g p" data-selected-links="repo_pulls /karacas/imgLiquid/pulls">
          <span class="octicon octicon-git-pull-request"></span> <span class="full-word">Pull Requests</span>
          <span class="js-pull-replace-counter"></span>
          <img alt="" class="mini-loader" height="16" src="https://assets-cdn.github.com/images/spinners/octocat-spinner-32.gif" width="16" />
</a>    </li>


  </ul>
  <div class="sunken-menu-separator"></div>
  <ul class="sunken-menu-group">

    <li class="tooltipped tooltipped-w" aria-label="Pulse">
      <a href="/karacas/imgLiquid/pulse" aria-label="Pulse" class="js-selected-navigation-item sunken-menu-item" data-selected-links="pulse /karacas/imgLiquid/pulse">
        <span class="octicon octicon-pulse"></span> <span class="full-word">Pulse</span>
        <img alt="" class="mini-loader" height="16" src="https://assets-cdn.github.com/images/spinners/octocat-spinner-32.gif" width="16" />
</a>    </li>

    <li class="tooltipped tooltipped-w" aria-label="Graphs">
      <a href="/karacas/imgLiquid/graphs" aria-label="Graphs" class="js-selected-navigation-item sunken-menu-item" data-selected-links="repo_graphs repo_contributors /karacas/imgLiquid/graphs">
        <span class="octicon octicon-graph"></span> <span class="full-word">Graphs</span>
        <img alt="" class="mini-loader" height="16" src="https://assets-cdn.github.com/images/spinners/octocat-spinner-32.gif" width="16" />
</a>    </li>
  </ul>


</nav>

              <div class="only-with-full-nav">
                
  
<div class="clone-url open"
  data-protocol-type="http"
  data-url="/users/set_protocol?protocol_selector=http&amp;protocol_type=clone">
  <h3><span class="text-emphasized">HTTPS</span> clone URL</h3>
  <div class="input-group js-zeroclipboard-container">
    <input type="text" class="input-mini input-monospace js-url-field js-zeroclipboard-target"
           value="https://github.com/karacas/imgLiquid.git" readonly="readonly">
    <span class="input-group-button">
      <button aria-label="Copy to clipboard" class="js-zeroclipboard minibutton zeroclipboard-button" data-copied-hint="Copied!" type="button"><span class="octicon octicon-clippy"></span></button>
    </span>
  </div>
</div>

  
<div class="clone-url "
  data-protocol-type="subversion"
  data-url="/users/set_protocol?protocol_selector=subversion&amp;protocol_type=clone">
  <h3><span class="text-emphasized">Subversion</span> checkout URL</h3>
  <div class="input-group js-zeroclipboard-container">
    <input type="text" class="input-mini input-monospace js-url-field js-zeroclipboard-target"
           value="https://github.com/karacas/imgLiquid" readonly="readonly">
    <span class="input-group-button">
      <button aria-label="Copy to clipboard" class="js-zeroclipboard minibutton zeroclipboard-button" data-copied-hint="Copied!" type="button"><span class="octicon octicon-clippy"></span></button>
    </span>
  </div>
</div>



<p class="clone-options">You can clone with
  <a href="#" class="js-clone-selector" data-protocol="http">HTTPS</a> or <a href="#" class="js-clone-selector" data-protocol="subversion">Subversion</a>.
  <a href="https://help.github.com/articles/which-remote-url-should-i-use" class="help tooltipped tooltipped-n" aria-label="Get help on which URL is right for you.">
    <span class="octicon octicon-question"></span>
  </a>
</p>


  <a href="http://windows.github.com" class="minibutton sidebar-button" title="Save karacas/imgLiquid to your computer and use it in GitHub Desktop." aria-label="Save karacas/imgLiquid to your computer and use it in GitHub Desktop.">
    <span class="octicon octicon-device-desktop"></span>
    Clone in Desktop
  </a>

                <a href="/karacas/imgLiquid/archive/master.zip"
                   class="minibutton sidebar-button"
                   aria-label="Download the contents of karacas/imgLiquid as a zip file"
                   title="Download the contents of karacas/imgLiquid as a zip file"
                   rel="nofollow">
                  <span class="octicon octicon-cloud-download"></span>
                  Download ZIP
                </a>
              </div>
        </div><!-- /.repository-sidebar -->

        <div id="js-repo-pjax-container" class="repository-content context-loader-container" data-pjax-container>
          

<a href="/karacas/imgLiquid/blob/2409266b35c2eec9eca7815052abd2d92a5131e4/js/imgLiquid-min.js" class="hidden js-permalink-shortcut" data-hotkey="y">Permalink</a>

<!-- blob contrib key: blob_contributors:v21:9f3bcb37fc676d1e7fceecf7c4af1b35 -->

<div class="file-navigation js-zeroclipboard-container">
  
<div class="select-menu js-menu-container js-select-menu left">
  <span class="minibutton select-menu-button js-menu-target css-truncate" data-hotkey="w"
    data-master-branch="master"
    data-ref="master"
    title="master"
    role="button" aria-label="Switch branches or tags" tabindex="0" aria-haspopup="true">
    <span class="octicon octicon-git-branch"></span>
    <i>branch:</i>
    <span class="js-select-button css-truncate-target">master</span>
  </span>

  <div class="select-menu-modal-holder js-menu-content js-navigation-container" data-pjax aria-hidden="true">

    <div class="select-menu-modal">
      <div class="select-menu-header">
        <span class="select-menu-title">Switch branches/tags</span>
        <span class="octicon octicon-x js-menu-close" role="button" aria-label="Close"></span>
      </div> <!-- /.select-menu-header -->

      <div class="select-menu-filters">
        <div class="select-menu-text-filter">
          <input type="text" aria-label="Filter branches/tags" id="context-commitish-filter-field" class="js-filterable-field js-navigation-enable" placeholder="Filter branches/tags">
        </div>
        <div class="select-menu-tabs">
          <ul>
            <li class="select-menu-tab">
              <a href="#" data-tab-filter="branches" class="js-select-menu-tab">Branches</a>
            </li>
            <li class="select-menu-tab">
              <a href="#" data-tab-filter="tags" class="js-select-menu-tab">Tags</a>
            </li>
          </ul>
        </div><!-- /.select-menu-tabs -->
      </div><!-- /.select-menu-filters -->

      <div class="select-menu-list select-menu-tab-bucket js-select-menu-tab-bucket" data-tab-filter="branches">

        <div data-filterable-for="context-commitish-filter-field" data-filterable-type="substring">


            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/blob/dev/js/imgLiquid-min.js"
                 data-name="dev"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="dev">dev</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item selected">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/blob/master/js/imgLiquid-min.js"
                 data-name="master"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="master">master</a>
            </div> <!-- /.select-menu-item -->
        </div>

          <div class="select-menu-no-results">Nothing to show</div>
      </div> <!-- /.select-menu-list -->

      <div class="select-menu-list select-menu-tab-bucket js-select-menu-tab-bucket" data-tab-filter="tags">
        <div data-filterable-for="context-commitish-filter-field" data-filterable-type="substring">


            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.944/js/imgLiquid-min.js"
                 data-name="0.9.944"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.944">0.9.944</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.943/js/imgLiquid-min.js"
                 data-name="0.9.943"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.943">0.9.943</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.942/js/imgLiquid-min.js"
                 data-name="0.9.942"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.942">0.9.942</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.941/js/imgLiquid-min.js"
                 data-name="0.9.941"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.941">0.9.941</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.940/js/imgLiquid-min.js"
                 data-name="0.9.940"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.940">0.9.940</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.921/js/imgLiquid-min.js"
                 data-name="0.9.921"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.921">0.9.921</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.920/js/imgLiquid-min.js"
                 data-name="0.9.920"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.920">0.9.920</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.901/js/imgLiquid-min.js"
                 data-name="0.9.901"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.901">0.9.901</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.861/js/imgLiquid-min.js"
                 data-name="0.9.861"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.861">0.9.861</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.92/js/imgLiquid-min.js"
                 data-name="0.9.92"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.92">0.9.92</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.90/js/imgLiquid-min.js"
                 data-name="0.9.90"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.90">0.9.90</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.86/js/imgLiquid-min.js"
                 data-name="0.9.86"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.86">0.9.86</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.84/js/imgLiquid-min.js"
                 data-name="0.9.84"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.84">0.9.84</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.75/js/imgLiquid-min.js"
                 data-name="0.9.75"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.75">0.9.75</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.70/js/imgLiquid-min.js"
                 data-name="0.9.70"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.70">0.9.70</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.65/js/imgLiquid-min.js"
                 data-name="0.9.65"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.65">0.9.65</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.52/js/imgLiquid-min.js"
                 data-name="0.9.52"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.52">0.9.52</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.51/js/imgLiquid-min.js"
                 data-name="0.9.51"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.51">0.9.51</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.7/js/imgLiquid-min.js"
                 data-name="0.9.7"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.7">0.9.7</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.6/js/imgLiquid-min.js"
                 data-name="0.9.6"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.6">0.9.6</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.5/js/imgLiquid-min.js"
                 data-name="0.9.5"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.5">0.9.5</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.4/js/imgLiquid-min.js"
                 data-name="0.9.4"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.4">0.9.4</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.2/js/imgLiquid-min.js"
                 data-name="0.9.2"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.2">0.9.2</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.9.0/js/imgLiquid-min.js"
                 data-name="0.9.0"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.9.0">0.9.0</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.8.8/js/imgLiquid-min.js"
                 data-name="0.8.8"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.8.8">0.8.8</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.8.4/js/imgLiquid-min.js"
                 data-name="0.8.4"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.8.4">0.8.4</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.7.7/js/imgLiquid-min.js"
                 data-name="0.7.7"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.7.7">0.7.7</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.7.6/js/imgLiquid-min.js"
                 data-name="0.7.6"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.7.6">0.7.6</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/karacas/imgLiquid/tree/0.1.0/js/imgLiquid-min.js"
                 data-name="0.1.0"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text css-truncate-target"
                 title="0.1.0">0.1.0</a>
            </div> <!-- /.select-menu-item -->
        </div>

        <div class="select-menu-no-results">Nothing to show</div>
      </div> <!-- /.select-menu-list -->

    </div> <!-- /.select-menu-modal -->
  </div> <!-- /.select-menu-modal-holder -->
</div> <!-- /.select-menu -->

  <div class="button-group right">
    <a href="/karacas/imgLiquid/find/master"
          class="js-show-file-finder minibutton empty-icon tooltipped tooltipped-s"
          data-pjax
          data-hotkey="t"
          aria-label="Quickly jump between files">
      <span class="octicon octicon-list-unordered"></span>
    </a>
    <button aria-label="Copy file path to clipboard" class="js-zeroclipboard minibutton zeroclipboard-button" data-copied-hint="Copied!" type="button"><span class="octicon octicon-clippy"></span></button>
  </div>

  <div class="breadcrumb js-zeroclipboard-target">
    <span class='repo-root js-repo-root'><span itemscope="" itemtype="http://data-vocabulary.org/Breadcrumb"><a href="/karacas/imgLiquid" class="" data-branch="master" data-direction="back" data-pjax="true" itemscope="url"><span itemprop="title">imgLiquid</span></a></span></span><span class="separator">/</span><span itemscope="" itemtype="http://data-vocabulary.org/Breadcrumb"><a href="/karacas/imgLiquid/tree/master/js" class="" data-branch="master" data-direction="back" data-pjax="true" itemscope="url"><span itemprop="title">js</span></a></span><span class="separator">/</span><strong class="final-path">imgLiquid-min.js</strong>
  </div>
</div>


  <div class="commit file-history-tease">
    <div class="file-history-tease-header">
        <img alt="Karacas" class="avatar" data-user="1050937" height="24" src="https://avatars0.githubusercontent.com/u/1050937?v=3&amp;s=48" width="24" />
        <span class="author"><a href="/karacas" rel="author">karacas</a></span>
        <time datetime="2013-11-05T00:09:09Z" is="relative-time">Nov 4, 2013</time>
        <div class="commit-title">
            <a href="/karacas/imgLiquid/commit/8bdaca11228440f742aa327479ae203d9c4812bc" class="message" data-pjax="true" title="0.9.944 / Fix URL fails if url filespec contains parenthesis">0.9.944 / Fix URL fails if url filespec contains parenthesis</a>
        </div>
    </div>

    <div class="participation">
      <p class="quickstat">
        <a href="#blob_contributors_box" rel="facebox">
          <strong>1</strong>
           contributor
        </a>
      </p>
      
    </div>
    <div id="blob_contributors_box" style="display:none">
      <h2 class="facebox-header">Users who have contributed to this file</h2>
      <ul class="facebox-user-list">
          <li class="facebox-user-list-item">
            <img alt="Karacas" data-user="1050937" height="24" src="https://avatars0.githubusercontent.com/u/1050937?v=3&amp;s=48" width="24" />
            <a href="/karacas">karacas</a>
          </li>
      </ul>
    </div>
  </div>

<div class="file-box">
  <div class="file">
    <div class="meta clearfix">
      <div class="info file-name">
          <span>1 lines (1 sloc)</span>
          <span class="meta-divider"></span>
        <span>5.106 kb</span>
      </div>
      <div class="actions">
        <div class="button-group">
          <a href="/karacas/imgLiquid/raw/master/js/imgLiquid-min.js" class="minibutton " id="raw-url">Raw</a>
            <a href="/karacas/imgLiquid/blame/master/js/imgLiquid-min.js" class="minibutton js-update-url-with-hash">Blame</a>
          <a href="/karacas/imgLiquid/commits/master/js/imgLiquid-min.js" class="minibutton " rel="nofollow">History</a>
        </div><!-- /.button-group -->

          <a class="octicon-button tooltipped tooltipped-nw"
             href="http://windows.github.com" aria-label="Open this file in GitHub for Windows">
              <span class="octicon octicon-device-desktop"></span>
          </a>

            <a class="octicon-button disabled tooltipped tooltipped-w" href="#"
               aria-label="You must be signed in to make or propose changes"><span class="octicon octicon-pencil"></span></a>

          <a class="octicon-button danger disabled tooltipped tooltipped-w" href="#"
             aria-label="You must be signed in to make or propose changes">
          <span class="octicon octicon-trashcan"></span>
        </a>
      </div><!-- /.actions -->
    </div>
    

  <div class="blob-wrapper data type-javascript">
      <table class="highlight tab-size-8 js-file-line-container">
      <tr>
        <td id="L1" class="blob-num js-line-number" data-line-number="1"></td>
        <td id="LC1" class="blob-code js-file-line">var imgLiquid=imgLiquid||{VER:&quot;0.9.944&quot;};imgLiquid.bgs_Available=!1,imgLiquid.bgs_CheckRunned=!1,imgLiquid.injectCss=&quot;.imgLiquid img {visibility:hidden}&quot;,function(i){function t(){if(!imgLiquid.bgs_CheckRunned){imgLiquid.bgs_CheckRunned=!0;var t=i(&#39;&lt;span style=&quot;background-size:cover&quot; /&gt;&#39;);i(&quot;body&quot;).append(t),!function(){var i=t[0];if(i&amp;&amp;window.getComputedStyle){var e=window.getComputedStyle(i,null);e&amp;&amp;e.backgroundSize&amp;&amp;(imgLiquid.bgs_Available=&quot;cover&quot;===e.backgroundSize)}}(),t.remove()}}i.fn.extend({imgLiquid:function(e){this.defaults={fill:!0,verticalAlign:&quot;center&quot;,horizontalAlign:&quot;center&quot;,useBackgroundSize:!0,useDataHtmlAttr:!0,responsive:!0,delay:0,fadeInTime:0,removeBoxBackground:!0,hardPixels:!0,responsiveCheckTime:500,timecheckvisibility:500,onStart:null,onFinish:null,onItemStart:null,onItemFinish:null,onItemError:null},t();var a=this;return this.options=e,this.settings=i.extend({},this.defaults,this.options),this.settings.onStart&amp;&amp;this.settings.onStart(),this.each(function(t){function e(){-1===u.css(&quot;background-image&quot;).indexOf(encodeURI(c.attr(&quot;src&quot;)))&amp;&amp;u.css({&quot;background-image&quot;:&#39;url(&quot;&#39;+encodeURI(c.attr(&quot;src&quot;))+&#39;&quot;)&#39;}),u.css({&quot;background-size&quot;:g.fill?&quot;cover&quot;:&quot;contain&quot;,&quot;background-position&quot;:(g.horizontalAlign+&quot; &quot;+g.verticalAlign).toLowerCase(),&quot;background-repeat&quot;:&quot;no-repeat&quot;}),i(&quot;a:first&quot;,u).css({display:&quot;block&quot;,width:&quot;100%&quot;,height:&quot;100%&quot;}),i(&quot;img&quot;,u).css({display:&quot;none&quot;}),g.onItemFinish&amp;&amp;g.onItemFinish(t,u,c),u.addClass(&quot;imgLiquid_bgSize&quot;),u.addClass(&quot;imgLiquid_ready&quot;),l()}function d(){function e(){c.data(&quot;imgLiquid_error&quot;)||c.data(&quot;imgLiquid_loaded&quot;)||c.data(&quot;imgLiquid_oldProcessed&quot;)||(u.is(&quot;:visible&quot;)&amp;&amp;c[0].complete&amp;&amp;c[0].width&gt;0&amp;&amp;c[0].height&gt;0?(c.data(&quot;imgLiquid_loaded&quot;,!0),setTimeout(r,t*g.delay)):setTimeout(e,g.timecheckvisibility))}if(c.data(&quot;oldSrc&quot;)&amp;&amp;c.data(&quot;oldSrc&quot;)!==c.attr(&quot;src&quot;)){var a=c.clone().removeAttr(&quot;style&quot;);return a.data(&quot;imgLiquid_settings&quot;,c.data(&quot;imgLiquid_settings&quot;)),c.parent().prepend(a),c.remove(),c=a,c[0].width=0,setTimeout(d,10),void 0}return c.data(&quot;imgLiquid_oldProcessed&quot;)?(r(),void 0):(c.data(&quot;imgLiquid_oldProcessed&quot;,!1),c.data(&quot;oldSrc&quot;,c.attr(&quot;src&quot;)),i(&quot;img:not(:first)&quot;,u).css(&quot;display&quot;,&quot;none&quot;),u.css({overflow:&quot;hidden&quot;}),c.fadeTo(0,0).removeAttr(&quot;width&quot;).removeAttr(&quot;height&quot;).css({visibility:&quot;visible&quot;,&quot;max-width&quot;:&quot;none&quot;,&quot;max-height&quot;:&quot;none&quot;,width:&quot;auto&quot;,height:&quot;auto&quot;,display:&quot;block&quot;}),c.on(&quot;error&quot;,n),c[0].onerror=n,e(),o(),void 0)}function o(){(g.responsive||c.data(&quot;imgLiquid_oldProcessed&quot;))&amp;&amp;c.data(&quot;imgLiquid_settings&quot;)&amp;&amp;(g=c.data(&quot;imgLiquid_settings&quot;),u.actualSize=u.get(0).offsetWidth+u.get(0).offsetHeight/1e4,u.sizeOld&amp;&amp;u.actualSize!==u.sizeOld&amp;&amp;r(),u.sizeOld=u.actualSize,setTimeout(o,g.responsiveCheckTime))}function n(){c.data(&quot;imgLiquid_error&quot;,!0),u.addClass(&quot;imgLiquid_error&quot;),g.onItemError&amp;&amp;g.onItemError(t,u,c),l()}function s(){var i={};if(a.settings.useDataHtmlAttr){var t=u.attr(&quot;data-imgLiquid-fill&quot;),e=u.attr(&quot;data-imgLiquid-horizontalAlign&quot;),d=u.attr(&quot;data-imgLiquid-verticalAlign&quot;);(&quot;true&quot;===t||&quot;false&quot;===t)&amp;&amp;(i.fill=Boolean(&quot;true&quot;===t)),void 0===e||&quot;left&quot;!==e&amp;&amp;&quot;center&quot;!==e&amp;&amp;&quot;right&quot;!==e&amp;&amp;-1===e.indexOf(&quot;%&quot;)||(i.horizontalAlign=e),void 0===d||&quot;top&quot;!==d&amp;&amp;&quot;bottom&quot;!==d&amp;&amp;&quot;center&quot;!==d&amp;&amp;-1===d.indexOf(&quot;%&quot;)||(i.verticalAlign=d)}return imgLiquid.isIE&amp;&amp;a.settings.ieFadeInDisabled&amp;&amp;(i.fadeInTime=0),i}function r(){var i,e,a,d,o,n,s,r,m=0,h=0,f=u.width(),v=u.height();void 0===c.data(&quot;owidth&quot;)&amp;&amp;c.data(&quot;owidth&quot;,c[0].width),void 0===c.data(&quot;oheight&quot;)&amp;&amp;c.data(&quot;oheight&quot;,c[0].height),g.fill===f/v&gt;=c.data(&quot;owidth&quot;)/c.data(&quot;oheight&quot;)?(i=&quot;100%&quot;,e=&quot;auto&quot;,a=Math.floor(f),d=Math.floor(f*(c.data(&quot;oheight&quot;)/c.data(&quot;owidth&quot;)))):(i=&quot;auto&quot;,e=&quot;100%&quot;,a=Math.floor(v*(c.data(&quot;owidth&quot;)/c.data(&quot;oheight&quot;))),d=Math.floor(v)),o=g.horizontalAlign.toLowerCase(),s=f-a,&quot;left&quot;===o&amp;&amp;(h=0),&quot;center&quot;===o&amp;&amp;(h=.5*s),&quot;right&quot;===o&amp;&amp;(h=s),-1!==o.indexOf(&quot;%&quot;)&amp;&amp;(o=parseInt(o.replace(&quot;%&quot;,&quot;&quot;),10),o&gt;0&amp;&amp;(h=.01*s*o)),n=g.verticalAlign.toLowerCase(),r=v-d,&quot;left&quot;===n&amp;&amp;(m=0),&quot;center&quot;===n&amp;&amp;(m=.5*r),&quot;bottom&quot;===n&amp;&amp;(m=r),-1!==n.indexOf(&quot;%&quot;)&amp;&amp;(n=parseInt(n.replace(&quot;%&quot;,&quot;&quot;),10),n&gt;0&amp;&amp;(m=.01*r*n)),g.hardPixels&amp;&amp;(i=a,e=d),c.css({width:i,height:e,&quot;margin-left&quot;:Math.floor(h),&quot;margin-top&quot;:Math.floor(m)}),c.data(&quot;imgLiquid_oldProcessed&quot;)||(c.fadeTo(g.fadeInTime,1),c.data(&quot;imgLiquid_oldProcessed&quot;,!0),g.removeBoxBackground&amp;&amp;u.css(&quot;background-image&quot;,&quot;none&quot;),u.addClass(&quot;imgLiquid_nobgSize&quot;),u.addClass(&quot;imgLiquid_ready&quot;)),g.onItemFinish&amp;&amp;g.onItemFinish(t,u,c),l()}function l(){t===a.length-1&amp;&amp;a.settings.onFinish&amp;&amp;a.settings.onFinish()}var g=a.settings,u=i(this),c=i(&quot;img:first&quot;,u);return c.length?(c.data(&quot;imgLiquid_settings&quot;)?(u.removeClass(&quot;imgLiquid_error&quot;).removeClass(&quot;imgLiquid_ready&quot;),g=i.extend({},c.data(&quot;imgLiquid_settings&quot;),a.options)):g=i.extend({},a.settings,s()),c.data(&quot;imgLiquid_settings&quot;,g),g.onItemStart&amp;&amp;g.onItemStart(t,u,c),imgLiquid.bgs_Available&amp;&amp;g.useBackgroundSize?e():d(),void 0):(n(),void 0)})}})}(jQuery),!function(){var i=imgLiquid.injectCss,t=document.getElementsByTagName(&quot;head&quot;)[0],e=document.createElement(&quot;style&quot;);e.type=&quot;text/css&quot;,e.styleSheet?e.styleSheet.cssText=i:e.appendChild(document.createTextNode(i)),t.appendChild(e)}();</td>
      </tr>
</table>

  </div>

  </div>
</div>

<a href="#jump-to-line" rel="facebox[.linejump]" data-hotkey="l" style="display:none">Jump to Line</a>
<div id="jump-to-line" style="display:none">
  <form accept-charset="UTF-8" class="js-jump-to-line-form">
    <input class="linejump-input js-jump-to-line-field" type="text" placeholder="Jump to line&hellip;" autofocus>
    <button type="submit" class="button">Go</button>
  </form>
</div>

        </div>

      </div><!-- /.repo-container -->
      <div class="modal-backdrop"></div>
    </div><!-- /.container -->
  </div><!-- /.site -->


    </div><!-- /.wrapper -->

      <div class="container">
  <div class="site-footer" role="contentinfo">
    <ul class="site-footer-links right">
      <li><a href="https://status.github.com/">Status</a></li>
      <li><a href="https://developer.github.com">API</a></li>
      <li><a href="http://training.github.com">Training</a></li>
      <li><a href="http://shop.github.com">Shop</a></li>
      <li><a href="/blog">Blog</a></li>
      <li><a href="/about">About</a></li>

    </ul>

    <a href="/" aria-label="Homepage">
      <span class="mega-octicon octicon-mark-github" title="GitHub"></span>
    </a>

    <ul class="site-footer-links">
      <li>&copy; 2015 <span title="0.03259s from github-fe127-cp1-prd.iad.github.net">GitHub</span>, Inc.</li>
        <li><a href="/site/terms">Terms</a></li>
        <li><a href="/site/privacy">Privacy</a></li>
        <li><a href="/security">Security</a></li>
        <li><a href="/contact">Contact</a></li>
    </ul>
  </div><!-- /.site-footer -->
</div><!-- /.container -->


    <div class="fullscreen-overlay js-fullscreen-overlay" id="fullscreen_overlay">
  <div class="fullscreen-container js-suggester-container">
    <div class="textarea-wrap">
      <textarea name="fullscreen-contents" id="fullscreen-contents" class="fullscreen-contents js-fullscreen-contents" placeholder=""></textarea>
      <div class="suggester-container">
        <div class="suggester fullscreen-suggester js-suggester js-navigation-container"></div>
      </div>
    </div>
  </div>
  <div class="fullscreen-sidebar">
    <a href="#" class="exit-fullscreen js-exit-fullscreen tooltipped tooltipped-w" aria-label="Exit Zen Mode">
      <span class="mega-octicon octicon-screen-normal"></span>
    </a>
    <a href="#" class="theme-switcher js-theme-switcher tooltipped tooltipped-w"
      aria-label="Switch themes">
      <span class="octicon octicon-color-mode"></span>
    </a>
  </div>
</div>



    <div id="ajax-error-message" class="flash flash-error">
      <span class="octicon octicon-alert"></span>
      <a href="#" class="octicon octicon-x flash-close js-ajax-error-dismiss" aria-label="Dismiss error"></a>
      Something went wrong with that request. Please try again.
    </div>


      <script crossorigin="anonymous" src="https://assets-cdn.github.com/assets/frameworks-fc447938e306b7b2c26a33cfee9dfda9052aeb1aa6ad84b72f1b35fd008efe9e.js" type="text/javascript"></script>
      <script async="async" crossorigin="anonymous" src="https://assets-cdn.github.com/assets/github-56c56f7fe2ed90ca50b9eefebccd56f3b9729a85d7ba17f0f9c9ebd02f20a7e3.js" type="text/javascript"></script>
      
      
  </body>
</html>

