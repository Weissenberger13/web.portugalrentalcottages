var tagBox,commentsBox,editPermalink,makeSlugeditClickable,WPSetThumbnailHTML,WPSetThumbnailID,WPRemoveThumbnail;function array_unique_noempty(b){var c=[];jQuery.each(b,function(a,d){d=jQuery.trim(d);if(d&&jQuery.inArray(d,c)==-1){c.push(d)}});return c}(function(a){tagBox={clean:function(b){return b.replace(/\s*,\s*/g,",").replace(/,+/g,",").replace(/[,\s]+$/,"").replace(/^[,\s]+/,"")},parseTags:function(e){var h=e.id,b=h.split("-check-num-")[1],d=a(e).closest(".tagsdiv"),g=d.find(".the-tags"),c=g.val().split(","),f=[];delete c[b];a.each(c,function(i,j){j=a.trim(j);if(j){f.push(j)}});g.val(this.clean(f.join(",")));this.quickClicks(d);return false},quickClicks:function(d){var g=a(".the-tags",d),e=a(".tagchecklist",d),f=a(d).attr("id"),b,c;if(!g.length){return}c=g.attr("disabled");b=g.val().split(",");e.empty();a.each(b,function(i,k){var j,h;k=a.trim(k);if(!k){return}j=a("<span />").text(k);if(!c){h=a('<a id="'+f+"-check-num-"+i+'" class="ntdelbutton">X</a>');h.click(function(){tagBox.parseTags(this)});j.prepend("&nbsp;").prepend(h)}e.append(j)})},flushTags:function(e,b,g){b=b||false;var i,c=a(".the-tags",e),h=a("input.newtag",e),d;i=b?a(b).text():h.val();tagsval=c.val();d=tagsval?tagsval+","+i:i;d=this.clean(d);d=array_unique_noempty(d.split(",")).join(",");c.val(d);this.quickClicks(e);if(!b){h.val("")}if("undefined"==typeof(g)){h.focus()}return false},get:function(c){var b=c.substr(c.indexOf("-")+1);a.post(ajaxurl,{action:"get-tagcloud",tax:b},function(e,d){if(0==e||"success"!=d){e=wpAjax.broken}e=a('<p id="tagcloud-'+b+'" class="the-tagcloud">'+e+"</p>");a("a",e).click(function(){tagBox.flushTags(a(this).closest(".inside").children(".tagsdiv"),this);return false});a("#"+c).after(e)})},init:function(){var b=this,c=a("div.ajaxtag");a(".tagsdiv").each(function(){tagBox.quickClicks(this)});a("input.tagadd",c).click(function(){b.flushTags(a(this).closest(".tagsdiv"))});a("div.taghint",c).click(function(){a(this).css("visibility","hidden").parent().siblings(".newtag").focus()});a("input.newtag",c).blur(function(){if(this.value==""){a(this).parent().siblings(".taghint").css("visibility","")}}).focus(function(){a(this).parent().siblings(".taghint").css("visibility","hidden")}).keyup(function(d){if(13==d.which){tagBox.flushTags(a(this).closest(".tagsdiv"));return false}}).keypress(function(d){if(13==d.which){d.preventDefault();return false}}).each(function(){var d=a(this).closest("div.tagsdiv").attr("id");a(this).suggest(ajaxurl+"?action=ajax-tag-search&tax="+d,{delay:500,minchars:2,multiple:true,multipleSep:","})});a("#post").submit(function(){a("div.tagsdiv").each(function(){tagBox.flushTags(this,false,1)})});a("a.tagcloud-link").click(function(){tagBox.get(a(this).attr("id"));a(this).unbind().click(function(){a(this).siblings(".the-tagcloud").toggle();return false});return false})}};commentsBox={st:0,get:function(d,c){var b=this.st,e;if(!c){c=20}this.st+=c;this.total=d;a("#commentsdiv img.waiting").show();e={action:"get-comments",mode:"single",_ajax_nonce:a("#add_comment_nonce").val(),p:a("#post_ID").val(),start:b,number:c};a.post(ajaxurl,e,function(f){f=wpAjax.parseAjaxResponse(f);a("#commentsdiv .widefat").show();a("#commentsdiv img.waiting").hide();if("object"==typeof f&&f.responses[0]){a("#the-comment-list").append(f.responses[0].data);theList=theExtraList=null;a("a[className*=':']").unbind();if(commentsBox.st>commentsBox.total){a("#show-comments").hide()}else{a("#show-comments").html(postL10n.showcomm)}return}else{if(1==f){a("#show-comments").parent().html(postL10n.endcomm);return}}a("#the-comment-list").append('<tr><td colspan="2">'+wpAjax.broken+"</td></tr>")});return false}};WPSetThumbnailHTML=function(b){a(".inside","#postimagediv").html(b)};WPSetThumbnailID=function(c){var b=a("input[value=_thumbnail_id]","#list-table");if(b.size()>0){a("#meta\\["+b.attr("id").match(/[0-9]+/)+"\\]\\[value\\]").text(c)}};WPRemoveThumbnail=function(b){a.post(ajaxurl,{action:"set-post-thumbnail",post_id:a("#post_ID").val(),thumbnail_id:-1,_ajax_nonce:b,cookie:encodeURIComponent(document.cookie)},function(c){if(c=="0"){alert(setPostThumbnailL10n.error)}else{WPSetThumbnailHTML(c)}})}})(jQuery);jQuery(document).ready(function(e){var b,a,f="";postboxes.add_postbox_toggles(pagenow);if(e("#tagsdiv-post_tag").length){tagBox.init()}else{e("#side-sortables, #normal-sortables, #advanced-sortables").children("div.postbox").each(function(){if(this.id.indexOf("tagsdiv-")===0){tagBox.init();return false}})}e(".categorydiv").each(function(){var l=e(this).attr("id"),h=false,k,m,j,g,i;j=l.split("-");j.shift();g=j.join("-");i=g+"_tab";if(g=="category"){i="cats"}e("a","#"+g+"-tabs").click(function(){var n=e(this).attr("href");e(this).parent().addClass("tabs").siblings("li").removeClass("tabs");e("#"+g+"-tabs").siblings(".tabs-panel").hide();e(n).show();if("#"+g+"-all"==n){deleteUserSetting(i)}else{setUserSetting(i,"pop")}return false});if(getUserSetting(i)){e('a[href="#'+g+'-pop"]',"#"+g+"-tabs").click()}e("#new"+g).one("focus",function(){e(this).val("").removeClass("form-input-tip")});e("#"+g+"-add-submit").click(function(){e("#new"+g).focus()});k=function(){if(h){return}h=true;var n=jQuery(this),p=n.is(":checked"),o=n.val().toString();e("#in-"+g+"-"+o+", #in-"+g+"-category-"+o).attr("checked",p);h=false};catAddBefore=function(n){if(!e("#new"+g).val()){return false}n.data+="&"+e(":checked","#"+g+"checklist").serialize();return n};m=function(q,p){var o,n=e("#new"+g+"_parent");if("undefined"!=p.parsed.responses[0]&&(o=p.parsed.responses[0].supplemental.newcat_parent)){n.before(o);n.remove()}};e("#"+g+"checklist").wpList({alt:"",response:g+"-ajax-response",addBefore:catAddBefore,addAfter:m});e("#"+g+"-add-toggle").click(function(){e("#"+g+"-adder").toggleClass("wp-hidden-children");e('a[href="#'+g+'-all"]',"#"+g+"-tabs").click();e("#new"+g).focus();return false});e("#"+g+"checklist li.popular-category :checkbox, #"+g+"checklist-pop :checkbox").live("click",function(){var n=e(this),p=n.is(":checked"),o=n.val();if(o&&n.parents("#taxonomy-"+g).length){e("#in-"+g+"-"+o+", #in-popular-"+g+"-"+o).attr("checked",p)}})});if(e("#postcustom").length){e("#the-list").wpList({addAfter:function(g,h){e("table#list-table").show()},addBefore:function(g){g.data+="&post_id="+e("#post_ID").val();return g}})}if(e("#submitdiv").length){b=e("#timestamp").html();a=e("#post-visibility-display").html();function d(){var g=e("#post-visibility-select");if(e("input:radio:checked",g).val()!="public"){e("#sticky").attr("checked",false);e("#sticky-span").hide()}else{e("#sticky-span").show()}if(e("input:radio:checked",g).val()!="password"){e("#password-span").hide()}else{e("#password-span").show()}}function c(){var n,o,h,q,p=e("#post_status"),i=e("option[value=publish]",p),g=e("#aa").val(),l=e("#mm").val(),m=e("#jj").val(),k=e("#hh").val(),j=e("#mn").val();n=new Date(g,l-1,m,k,j);o=new Date(e("#hidden_aa").val(),e("#hidden_mm").val()-1,e("#hidden_jj").val(),e("#hidden_hh").val(),e("#hidden_mn").val());h=new Date(e("#cur_aa").val(),e("#cur_mm").val()-1,e("#cur_jj").val(),e("#cur_hh").val(),e("#cur_mn").val());if(n.getFullYear()!=g||(1+n.getMonth())!=l||n.getDate()!=m||n.getMinutes()!=j){e(".timestamp-wrap","#timestampdiv").addClass("form-invalid");return false}else{e(".timestamp-wrap","#timestampdiv").removeClass("form-invalid")}if(n>h&&e("#original_post_status").val()!="future"){q=postL10n.publishOnFuture;e("#publish").val(postL10n.schedule)}else{if(n<=h&&e("#original_post_status").val()!="publish"){q=postL10n.publishOn;e("#publish").val(postL10n.publish)}else{q=postL10n.publishOnPast;e("#publish").val(postL10n.update)}}if(o.toUTCString()==n.toUTCString()){e("#timestamp").html(b)}else{e("#timestamp").html(q+" <b>"+e("option[value="+e("#mm").val()+"]","#mm").text()+" "+m+", "+g+" @ "+k+":"+j+"</b> ")}if(e("input:radio:checked","#post-visibility-select").val()=="private"){e("#publish").val(postL10n.update);if(i.length==0){p.append('<option value="publish">'+postL10n.privatelyPublished+"</option>")}else{i.html(postL10n.privatelyPublished)}e("option[value=publish]",p).attr("selected",true);e(".edit-post-status","#misc-publishing-actions").hide()}else{if(e("#original_post_status").val()=="future"||e("#original_post_status").val()=="draft"){if(i.length){i.remove();p.val(e("#hidden_post_status").val())}}else{i.html(postL10n.published)}if(p.is(":hidden")){e(".edit-post-status","#misc-publishing-actions").show()}}e("#post-status-display").html(e("option:selected",p).text());if(e("option:selected",p).val()=="private"||e("option:selected",p).val()=="publish"){e("#save-post").hide()}else{e("#save-post").show();if(e("option:selected",p).val()=="pending"){e("#save-post").show().val(postL10n.savePending)}else{e("#save-post").show().val(postL10n.saveDraft)}}return true}e(".edit-visibility","#visibility").click(function(){if(e("#post-visibility-select").is(":hidden")){d();e("#post-visibility-select").slideDown("normal");e(this).hide()}return false});e(".cancel-post-visibility","#post-visibility-select").click(function(){e("#post-visibility-select").slideUp("normal");e("#visibility-radio-"+e("#hidden-post-visibility").val()).attr("checked",true);e("#post_password").val(e("#hidden_post_password").val());e("#sticky").attr("checked",e("#hidden-post-sticky").attr("checked"));e("#post-visibility-display").html(a);e(".edit-visibility","#visibility").show();c();return false});e(".save-post-visibility","#post-visibility-select").click(function(){var g=e("#post-visibility-select");g.slideUp("normal");e(".edit-visibility","#visibility").show();c();if(e("input:radio:checked",g).val()!="public"){e("#sticky").attr("checked",false)}if(true==e("#sticky").attr("checked")){f="Sticky"}else{f=""}e("#post-visibility-display").html(postL10n[e("input:radio:checked",g).val()+f]);return false});e("input:radio","#post-visibility-select").change(function(){d()});e("#timestampdiv").siblings("a.edit-timestamp").click(function(){if(e("#timestampdiv").is(":hidden")){e("#timestampdiv").slideDown("normal");e(this).hide()}return false});e(".cancel-timestamp","#timestampdiv").click(function(){e("#timestampdiv").slideUp("normal");e("#mm").val(e("#hidden_mm").val());e("#jj").val(e("#hidden_jj").val());e("#aa").val(e("#hidden_aa").val());e("#hh").val(e("#hidden_hh").val());e("#mn").val(e("#hidden_mn").val());e("#timestampdiv").siblings("a.edit-timestamp").show();c();return false});e(".save-timestamp","#timestampdiv").click(function(){if(c()){e("#timestampdiv").slideUp("normal");e("#timestampdiv").siblings("a.edit-timestamp").show()}return false});e("#post-status-select").siblings("a.edit-post-status").click(function(){if(e("#post-status-select").is(":hidden")){e("#post-status-select").slideDown("normal");e(this).hide()}return false});e(".save-post-status","#post-status-select").click(function(){e("#post-status-select").slideUp("normal");e("#post-status-select").siblings("a.edit-post-status").show();c();return false});e(".cancel-post-status","#post-status-select").click(function(){e("#post-status-select").slideUp("normal");e("#post_status").val(e("#hidden_post_status").val());e("#post-status-select").siblings("a.edit-post-status").show();c();return false})}if(e("#edit-slug-box").length){editPermalink=function(g){var h,l=0,k=e("#editable-post-name"),m=k.html(),p=e("#post_name"),q=p.val(),n=e("#edit-slug-buttons"),o=n.html(),j=e("#editable-post-name-full").html();e("#view-post-btn").hide();n.html('<a href="#" class="save button">'+postL10n.ok+'</a> <a class="cancel" href="#">'+postL10n.cancel+"</a>");n.children(".save").click(function(){var i=k.children("input").val();e.post(ajaxurl,{action:"sample-permalink",post_id:g,new_slug:i,new_title:e("#title").val(),samplepermalinknonce:e("#samplepermalinknonce").val()},function(r){e("#edit-slug-box").html(r);n.html(o);p.attr("value",i);makeSlugeditClickable();e("#view-post-btn").show()});return false});e(".cancel","#edit-slug-buttons").click(function(){e("#view-post-btn").show();k.html(m);n.html(o);p.attr("value",q);return false});for(h=0;h<j.length;++h){if("%"==j.charAt(h)){l++}}slug_value=(l>j.length/4)?"":j;k.html('<input type="text" id="new-post-slug" value="'+slug_value+'" />').children("input").keypress(function(r){var i=r.keyCode||0;if(13==i){n.children(".save").click();return false}if(27==i){n.children(".cancel").click();return false}p.attr("value",this.value)}).focus()};makeSlugeditClickable=function(){e("#editable-post-name").click(function(){e("#edit-slug-buttons").children(".edit-slug").click()})};makeSlugeditClickable()}if(e("#title").val()==""){e("#title").siblings("#title-prompt-text").css("visibility","")}e("#title-prompt-text").click(function(){e(this).css("visibility","hidden").siblings("#title").focus()});e("#title").blur(function(){if(this.value==""){e(this).siblings("#title-prompt-text").css("visibility","")}}).focus(function(){e(this).siblings("#title-prompt-text").css("visibility","hidden")}).keydown(function(g){e(this).siblings("#title-prompt-text").css("visibility","hidden");e(this).unbind(g)})});