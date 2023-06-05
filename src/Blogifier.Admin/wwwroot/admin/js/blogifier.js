/*!
  * Bootstrap v5.1.0 (https://getbootstrap.com/)
  * Copyright 2011-2021 The Bootstrap Authors (https://github.com/twbs/bootstrap/graphs/contributors)
  * Licensed under MIT (https://github.com/twbs/bootstrap/blob/main/LICENSE)
  */
!function(t,e){"object"==typeof exports&&"undefined"!=typeof module?module.exports=e():"function"==typeof define&&define.amd?define(e):(t="undefined"!=typeof globalThis?globalThis:t||self).bootstrap=e();}(undefined,(function(){const t=t=>{let e=t.getAttribute("data-bs-target");if(!e||"#"===e){let i=t.getAttribute("href");if(!i||!i.includes("#")&&!i.startsWith("."))return null;i.includes("#")&&!i.startsWith("#")&&(i="#"+i.split("#")[1]),e=i&&"#"!==i?i.trim():null;}return e},e=e=>{const i=t(e);return i&&document.querySelector(i)?i:null},i=e=>{const i=t(e);return i?document.querySelector(i):null},n=t=>{t.dispatchEvent(new Event("transitionend"));},s=t=>!(!t||"object"!=typeof t)&&(void 0!==t.jquery&&(t=t[0]),void 0!==t.nodeType),o=t=>s(t)?t.jquery?t[0]:t:"string"==typeof t&&t.length>0?document.querySelector(t):null,r=(t,e,i)=>{Object.keys(i).forEach(n=>{const o=i[n],r=e[n],a=r&&s(r)?"element":null==(l=r)?""+l:{}.toString.call(l).match(/\s([a-z]+)/i)[1].toLowerCase();var l;if(!new RegExp(o).test(a))throw new TypeError(`${t.toUpperCase()}: Option "${n}" provided type "${a}" but expected type "${o}".`)});},a=t=>!(!s(t)||0===t.getClientRects().length)&&"visible"===getComputedStyle(t).getPropertyValue("visibility"),l=t=>!t||t.nodeType!==Node.ELEMENT_NODE||!!t.classList.contains("disabled")||(void 0!==t.disabled?t.disabled:t.hasAttribute("disabled")&&"false"!==t.getAttribute("disabled")),c=t=>{if(!document.documentElement.attachShadow)return null;if("function"==typeof t.getRootNode){const e=t.getRootNode();return e instanceof ShadowRoot?e:null}return t instanceof ShadowRoot?t:t.parentNode?c(t.parentNode):null},h=()=>{},d=t=>{t.offsetHeight;},u=()=>{const{jQuery:t}=window;return t&&!document.body.hasAttribute("data-bs-no-jquery")?t:null},f=[],p=()=>"rtl"===document.documentElement.dir,m=t=>{var e;e=()=>{const e=u();if(e){const i=t.NAME,n=e.fn[i];e.fn[i]=t.jQueryInterface,e.fn[i].Constructor=t,e.fn[i].noConflict=()=>(e.fn[i]=n,t.jQueryInterface);}},"loading"===document.readyState?(f.length||document.addEventListener("DOMContentLoaded",()=>{f.forEach(t=>t());}),f.push(e)):e();},g=t=>{"function"==typeof t&&t();},_=(t,e,i=!0)=>{if(!i)return void g(t);const s=(t=>{if(!t)return 0;let{transitionDuration:e,transitionDelay:i}=window.getComputedStyle(t);const n=Number.parseFloat(e),s=Number.parseFloat(i);return n||s?(e=e.split(",")[0],i=i.split(",")[0],1e3*(Number.parseFloat(e)+Number.parseFloat(i))):0})(e)+5;let o=!1;const r=({target:i})=>{i===e&&(o=!0,e.removeEventListener("transitionend",r),g(t));};e.addEventListener("transitionend",r),setTimeout(()=>{o||n(e);},s);},b=(t,e,i,n)=>{let s=t.indexOf(e);if(-1===s)return t[!i&&n?t.length-1:0];const o=t.length;return s+=i?1:-1,n&&(s=(s+o)%o),t[Math.max(0,Math.min(s,o-1))]},v=/[^.]*(?=\..*)\.|.*/,y=/\..*/,w=/::\d+$/,E={};let A=1;const T={mouseenter:"mouseover",mouseleave:"mouseout"},O=/^(mouseenter|mouseleave)/i,C=new Set(["click","dblclick","mouseup","mousedown","contextmenu","mousewheel","DOMMouseScroll","mouseover","mouseout","mousemove","selectstart","selectend","keydown","keypress","keyup","orientationchange","touchstart","touchmove","touchend","touchcancel","pointerdown","pointermove","pointerup","pointerleave","pointercancel","gesturestart","gesturechange","gestureend","focus","blur","change","reset","select","submit","focusin","focusout","load","unload","beforeunload","resize","move","DOMContentLoaded","readystatechange","error","abort","scroll"]);function k(t,e){return e&&`${e}::${A++}`||t.uidEvent||A++}function L(t){const e=k(t);return t.uidEvent=e,E[e]=E[e]||{},E[e]}function x(t,e,i=null){const n=Object.keys(t);for(let s=0,o=n.length;s<o;s++){const o=t[n[s]];if(o.originalHandler===e&&o.delegationSelector===i)return o}return null}function D(t,e,i){const n="string"==typeof e,s=n?i:e;let o=I(t);return C.has(o)||(o=t),[n,s,o]}function S(t,e,i,n,s){if("string"!=typeof e||!t)return;if(i||(i=n,n=null),O.test(e)){const t=t=>function(e){if(!e.relatedTarget||e.relatedTarget!==e.delegateTarget&&!e.delegateTarget.contains(e.relatedTarget))return t.call(this,e)};n?n=t(n):i=t(i);}const[o,r,a]=D(e,i,n),l=L(t),c=l[a]||(l[a]={}),h=x(c,r,o?i:null);if(h)return void(h.oneOff=h.oneOff&&s);const d=k(r,e.replace(v,"")),u=o?function(t,e,i){return function n(s){const o=t.querySelectorAll(e);for(let{target:r}=s;r&&r!==this;r=r.parentNode)for(let a=o.length;a--;)if(o[a]===r)return s.delegateTarget=r,n.oneOff&&P.off(t,s.type,e,i),i.apply(r,[s]);return null}}(t,i,n):function(t,e){return function i(n){return n.delegateTarget=t,i.oneOff&&P.off(t,n.type,e),e.apply(t,[n])}}(t,i);u.delegationSelector=o?i:null,u.originalHandler=r,u.oneOff=s,u.uidEvent=d,c[d]=u,t.addEventListener(a,u,o);}function N(t,e,i,n,s){const o=x(e[i],n,s);o&&(t.removeEventListener(i,o,Boolean(s)),delete e[i][o.uidEvent]);}function I(t){return t=t.replace(y,""),T[t]||t}const P={on(t,e,i,n){S(t,e,i,n,!1);},one(t,e,i,n){S(t,e,i,n,!0);},off(t,e,i,n){if("string"!=typeof e||!t)return;const[s,o,r]=D(e,i,n),a=r!==e,l=L(t),c=e.startsWith(".");if(void 0!==o){if(!l||!l[r])return;return void N(t,l,r,o,s?i:null)}c&&Object.keys(l).forEach(i=>{!function(t,e,i,n){const s=e[i]||{};Object.keys(s).forEach(o=>{if(o.includes(n)){const n=s[o];N(t,e,i,n.originalHandler,n.delegationSelector);}});}(t,l,i,e.slice(1));});const h=l[r]||{};Object.keys(h).forEach(i=>{const n=i.replace(w,"");if(!a||e.includes(n)){const e=h[i];N(t,l,r,e.originalHandler,e.delegationSelector);}});},trigger(t,e,i){if("string"!=typeof e||!t)return null;const n=u(),s=I(e),o=e!==s,r=C.has(s);let a,l=!0,c=!0,h=!1,d=null;return o&&n&&(a=n.Event(e,i),n(t).trigger(a),l=!a.isPropagationStopped(),c=!a.isImmediatePropagationStopped(),h=a.isDefaultPrevented()),r?(d=document.createEvent("HTMLEvents"),d.initEvent(s,l,!0)):d=new CustomEvent(e,{bubbles:l,cancelable:!0}),void 0!==i&&Object.keys(i).forEach(t=>{Object.defineProperty(d,t,{get:()=>i[t]});}),h&&d.preventDefault(),c&&t.dispatchEvent(d),d.defaultPrevented&&void 0!==a&&a.preventDefault(),d}},j=new Map;var M={set(t,e,i){j.has(t)||j.set(t,new Map);const n=j.get(t);n.has(e)||0===n.size?n.set(e,i):console.error(`Bootstrap doesn't allow more than one instance per element. Bound instance: ${Array.from(n.keys())[0]}.`);},get:(t,e)=>j.has(t)&&j.get(t).get(e)||null,remove(t,e){if(!j.has(t))return;const i=j.get(t);i.delete(e),0===i.size&&j.delete(t);}};class H{constructor(t){(t=o(t))&&(this._element=t,M.set(this._element,this.constructor.DATA_KEY,this));}dispose(){M.remove(this._element,this.constructor.DATA_KEY),P.off(this._element,this.constructor.EVENT_KEY),Object.getOwnPropertyNames(this).forEach(t=>{this[t]=null;});}_queueCallback(t,e,i=!0){_(t,e,i);}static getInstance(t){return M.get(o(t),this.DATA_KEY)}static getOrCreateInstance(t,e={}){return this.getInstance(t)||new this(t,"object"==typeof e?e:null)}static get VERSION(){return "5.1.0"}static get NAME(){throw new Error('You have to implement the static method "NAME", for each component!')}static get DATA_KEY(){return "bs."+this.NAME}static get EVENT_KEY(){return "."+this.DATA_KEY}}const B=(t,e="hide")=>{const n="click.dismiss"+t.EVENT_KEY,s=t.NAME;P.on(document,n,`[data-bs-dismiss="${s}"]`,(function(n){if(["A","AREA"].includes(this.tagName)&&n.preventDefault(),l(this))return;const o=i(this)||this.closest("."+s);t.getOrCreateInstance(o)[e]();}));};class R extends H{static get NAME(){return "alert"}close(){if(P.trigger(this._element,"close.bs.alert").defaultPrevented)return;this._element.classList.remove("show");const t=this._element.classList.contains("fade");this._queueCallback(()=>this._destroyElement(),this._element,t);}_destroyElement(){this._element.remove(),P.trigger(this._element,"closed.bs.alert"),this.dispose();}static jQueryInterface(t){return this.each((function(){const e=R.getOrCreateInstance(this);if("string"==typeof t){if(void 0===e[t]||t.startsWith("_")||"constructor"===t)throw new TypeError(`No method named "${t}"`);e[t](this);}}))}}B(R,"close"),m(R);class W extends H{static get NAME(){return "button"}toggle(){this._element.setAttribute("aria-pressed",this._element.classList.toggle("active"));}static jQueryInterface(t){return this.each((function(){const e=W.getOrCreateInstance(this);"toggle"===t&&e[t]();}))}}function z(t){return "true"===t||"false"!==t&&(t===Number(t).toString()?Number(t):""===t||"null"===t?null:t)}function q(t){return t.replace(/[A-Z]/g,t=>"-"+t.toLowerCase())}P.on(document,"click.bs.button.data-api",'[data-bs-toggle="button"]',t=>{t.preventDefault();const e=t.target.closest('[data-bs-toggle="button"]');W.getOrCreateInstance(e).toggle();}),m(W);const F={setDataAttribute(t,e,i){t.setAttribute("data-bs-"+q(e),i);},removeDataAttribute(t,e){t.removeAttribute("data-bs-"+q(e));},getDataAttributes(t){if(!t)return {};const e={};return Object.keys(t.dataset).filter(t=>t.startsWith("bs")).forEach(i=>{let n=i.replace(/^bs/,"");n=n.charAt(0).toLowerCase()+n.slice(1,n.length),e[n]=z(t.dataset[i]);}),e},getDataAttribute:(t,e)=>z(t.getAttribute("data-bs-"+q(e))),offset(t){const e=t.getBoundingClientRect();return {top:e.top+window.pageYOffset,left:e.left+window.pageXOffset}},position:t=>({top:t.offsetTop,left:t.offsetLeft})},U={find:(t,e=document.documentElement)=>[].concat(...Element.prototype.querySelectorAll.call(e,t)),findOne:(t,e=document.documentElement)=>Element.prototype.querySelector.call(e,t),children:(t,e)=>[].concat(...t.children).filter(t=>t.matches(e)),parents(t,e){const i=[];let n=t.parentNode;for(;n&&n.nodeType===Node.ELEMENT_NODE&&3!==n.nodeType;)n.matches(e)&&i.push(n),n=n.parentNode;return i},prev(t,e){let i=t.previousElementSibling;for(;i;){if(i.matches(e))return [i];i=i.previousElementSibling;}return []},next(t,e){let i=t.nextElementSibling;for(;i;){if(i.matches(e))return [i];i=i.nextElementSibling;}return []},focusableChildren(t){const e=["a","button","input","textarea","select","details","[tabindex]",'[contenteditable="true"]'].map(t=>t+':not([tabindex^="-"])').join(", ");return this.find(e,t).filter(t=>!l(t)&&a(t))}},$={interval:5e3,keyboard:!0,slide:!1,pause:"hover",wrap:!0,touch:!0},V={interval:"(number|boolean)",keyboard:"boolean",slide:"(boolean|string)",pause:"(string|boolean)",wrap:"boolean",touch:"boolean"},K="next",X="prev",Y="left",Q="right",G={ArrowLeft:Q,ArrowRight:Y};class Z extends H{constructor(t,e){super(t),this._items=null,this._interval=null,this._activeElement=null,this._isPaused=!1,this._isSliding=!1,this.touchTimeout=null,this.touchStartX=0,this.touchDeltaX=0,this._config=this._getConfig(e),this._indicatorsElement=U.findOne(".carousel-indicators",this._element),this._touchSupported="ontouchstart"in document.documentElement||navigator.maxTouchPoints>0,this._pointerEvent=Boolean(window.PointerEvent),this._addEventListeners();}static get Default(){return $}static get NAME(){return "carousel"}next(){this._slide(K);}nextWhenVisible(){!document.hidden&&a(this._element)&&this.next();}prev(){this._slide(X);}pause(t){t||(this._isPaused=!0),U.findOne(".carousel-item-next, .carousel-item-prev",this._element)&&(n(this._element),this.cycle(!0)),clearInterval(this._interval),this._interval=null;}cycle(t){t||(this._isPaused=!1),this._interval&&(clearInterval(this._interval),this._interval=null),this._config&&this._config.interval&&!this._isPaused&&(this._updateInterval(),this._interval=setInterval((document.visibilityState?this.nextWhenVisible:this.next).bind(this),this._config.interval));}to(t){this._activeElement=U.findOne(".active.carousel-item",this._element);const e=this._getItemIndex(this._activeElement);if(t>this._items.length-1||t<0)return;if(this._isSliding)return void P.one(this._element,"slid.bs.carousel",()=>this.to(t));if(e===t)return this.pause(),void this.cycle();const i=t>e?K:X;this._slide(i,this._items[t]);}_getConfig(t){return t={...$,...F.getDataAttributes(this._element),..."object"==typeof t?t:{}},r("carousel",t,V),t}_handleSwipe(){const t=Math.abs(this.touchDeltaX);if(t<=40)return;const e=t/this.touchDeltaX;this.touchDeltaX=0,e&&this._slide(e>0?Q:Y);}_addEventListeners(){this._config.keyboard&&P.on(this._element,"keydown.bs.carousel",t=>this._keydown(t)),"hover"===this._config.pause&&(P.on(this._element,"mouseenter.bs.carousel",t=>this.pause(t)),P.on(this._element,"mouseleave.bs.carousel",t=>this.cycle(t))),this._config.touch&&this._touchSupported&&this._addTouchEventListeners();}_addTouchEventListeners(){const t=t=>{!this._pointerEvent||"pen"!==t.pointerType&&"touch"!==t.pointerType?this._pointerEvent||(this.touchStartX=t.touches[0].clientX):this.touchStartX=t.clientX;},e=t=>{this.touchDeltaX=t.touches&&t.touches.length>1?0:t.touches[0].clientX-this.touchStartX;},i=t=>{!this._pointerEvent||"pen"!==t.pointerType&&"touch"!==t.pointerType||(this.touchDeltaX=t.clientX-this.touchStartX),this._handleSwipe(),"hover"===this._config.pause&&(this.pause(),this.touchTimeout&&clearTimeout(this.touchTimeout),this.touchTimeout=setTimeout(t=>this.cycle(t),500+this._config.interval));};U.find(".carousel-item img",this._element).forEach(t=>{P.on(t,"dragstart.bs.carousel",t=>t.preventDefault());}),this._pointerEvent?(P.on(this._element,"pointerdown.bs.carousel",e=>t(e)),P.on(this._element,"pointerup.bs.carousel",t=>i(t)),this._element.classList.add("pointer-event")):(P.on(this._element,"touchstart.bs.carousel",e=>t(e)),P.on(this._element,"touchmove.bs.carousel",t=>e(t)),P.on(this._element,"touchend.bs.carousel",t=>i(t)));}_keydown(t){if(/input|textarea/i.test(t.target.tagName))return;const e=G[t.key];e&&(t.preventDefault(),this._slide(e));}_getItemIndex(t){return this._items=t&&t.parentNode?U.find(".carousel-item",t.parentNode):[],this._items.indexOf(t)}_getItemByOrder(t,e){const i=t===K;return b(this._items,e,i,this._config.wrap)}_triggerSlideEvent(t,e){const i=this._getItemIndex(t),n=this._getItemIndex(U.findOne(".active.carousel-item",this._element));return P.trigger(this._element,"slide.bs.carousel",{relatedTarget:t,direction:e,from:n,to:i})}_setActiveIndicatorElement(t){if(this._indicatorsElement){const e=U.findOne(".active",this._indicatorsElement);e.classList.remove("active"),e.removeAttribute("aria-current");const i=U.find("[data-bs-target]",this._indicatorsElement);for(let e=0;e<i.length;e++)if(Number.parseInt(i[e].getAttribute("data-bs-slide-to"),10)===this._getItemIndex(t)){i[e].classList.add("active"),i[e].setAttribute("aria-current","true");break}}}_updateInterval(){const t=this._activeElement||U.findOne(".active.carousel-item",this._element);if(!t)return;const e=Number.parseInt(t.getAttribute("data-bs-interval"),10);e?(this._config.defaultInterval=this._config.defaultInterval||this._config.interval,this._config.interval=e):this._config.interval=this._config.defaultInterval||this._config.interval;}_slide(t,e){const i=this._directionToOrder(t),n=U.findOne(".active.carousel-item",this._element),s=this._getItemIndex(n),o=e||this._getItemByOrder(i,n),r=this._getItemIndex(o),a=Boolean(this._interval),l=i===K,c=l?"carousel-item-start":"carousel-item-end",h=l?"carousel-item-next":"carousel-item-prev",u=this._orderToDirection(i);if(o&&o.classList.contains("active"))return void(this._isSliding=!1);if(this._isSliding)return;if(this._triggerSlideEvent(o,u).defaultPrevented)return;if(!n||!o)return;this._isSliding=!0,a&&this.pause(),this._setActiveIndicatorElement(o),this._activeElement=o;const f=()=>{P.trigger(this._element,"slid.bs.carousel",{relatedTarget:o,direction:u,from:s,to:r});};if(this._element.classList.contains("slide")){o.classList.add(h),d(o),n.classList.add(c),o.classList.add(c);const t=()=>{o.classList.remove(c,h),o.classList.add("active"),n.classList.remove("active",h,c),this._isSliding=!1,setTimeout(f,0);};this._queueCallback(t,n,!0);}else n.classList.remove("active"),o.classList.add("active"),this._isSliding=!1,f();a&&this.cycle();}_directionToOrder(t){return [Q,Y].includes(t)?p()?t===Y?X:K:t===Y?K:X:t}_orderToDirection(t){return [K,X].includes(t)?p()?t===X?Y:Q:t===X?Q:Y:t}static carouselInterface(t,e){const i=Z.getOrCreateInstance(t,e);let{_config:n}=i;"object"==typeof e&&(n={...n,...e});const s="string"==typeof e?e:n.slide;if("number"==typeof e)i.to(e);else if("string"==typeof s){if(void 0===i[s])throw new TypeError(`No method named "${s}"`);i[s]();}else n.interval&&n.ride&&(i.pause(),i.cycle());}static jQueryInterface(t){return this.each((function(){Z.carouselInterface(this,t);}))}static dataApiClickHandler(t){const e=i(this);if(!e||!e.classList.contains("carousel"))return;const n={...F.getDataAttributes(e),...F.getDataAttributes(this)},s=this.getAttribute("data-bs-slide-to");s&&(n.interval=!1),Z.carouselInterface(e,n),s&&Z.getInstance(e).to(s),t.preventDefault();}}P.on(document,"click.bs.carousel.data-api","[data-bs-slide], [data-bs-slide-to]",Z.dataApiClickHandler),P.on(window,"load.bs.carousel.data-api",()=>{const t=U.find('[data-bs-ride="carousel"]');for(let e=0,i=t.length;e<i;e++)Z.carouselInterface(t[e],Z.getInstance(t[e]));}),m(Z);const J={toggle:!0,parent:null},tt={toggle:"boolean",parent:"(null|element)"};class et extends H{constructor(t,i){super(t),this._isTransitioning=!1,this._config=this._getConfig(i),this._triggerArray=[];const n=U.find('[data-bs-toggle="collapse"]');for(let t=0,i=n.length;t<i;t++){const i=n[t],s=e(i),o=U.find(s).filter(t=>t===this._element);null!==s&&o.length&&(this._selector=s,this._triggerArray.push(i));}this._initializeChildren(),this._config.parent||this._addAriaAndCollapsedClass(this._triggerArray,this._isShown()),this._config.toggle&&this.toggle();}static get Default(){return J}static get NAME(){return "collapse"}toggle(){this._isShown()?this.hide():this.show();}show(){if(this._isTransitioning||this._isShown())return;let t,e=[];if(this._config.parent){const t=U.find(".collapse .collapse",this._config.parent);e=U.find(".show, .collapsing",this._config.parent).filter(e=>!t.includes(e));}const i=U.findOne(this._selector);if(e.length){const n=e.find(t=>i!==t);if(t=n?et.getInstance(n):null,t&&t._isTransitioning)return}if(P.trigger(this._element,"show.bs.collapse").defaultPrevented)return;e.forEach(e=>{i!==e&&et.getOrCreateInstance(e,{toggle:!1}).hide(),t||M.set(e,"bs.collapse",null);});const n=this._getDimension();this._element.classList.remove("collapse"),this._element.classList.add("collapsing"),this._element.style[n]=0,this._addAriaAndCollapsedClass(this._triggerArray,!0),this._isTransitioning=!0;const s="scroll"+(n[0].toUpperCase()+n.slice(1));this._queueCallback(()=>{this._isTransitioning=!1,this._element.classList.remove("collapsing"),this._element.classList.add("collapse","show"),this._element.style[n]="",P.trigger(this._element,"shown.bs.collapse");},this._element,!0),this._element.style[n]=this._element[s]+"px";}hide(){if(this._isTransitioning||!this._isShown())return;if(P.trigger(this._element,"hide.bs.collapse").defaultPrevented)return;const t=this._getDimension();this._element.style[t]=this._element.getBoundingClientRect()[t]+"px",d(this._element),this._element.classList.add("collapsing"),this._element.classList.remove("collapse","show");const e=this._triggerArray.length;for(let t=0;t<e;t++){const e=this._triggerArray[t],n=i(e);n&&!this._isShown(n)&&this._addAriaAndCollapsedClass([e],!1);}this._isTransitioning=!0,this._element.style[t]="",this._queueCallback(()=>{this._isTransitioning=!1,this._element.classList.remove("collapsing"),this._element.classList.add("collapse"),P.trigger(this._element,"hidden.bs.collapse");},this._element,!0);}_isShown(t=this._element){return t.classList.contains("show")}_getConfig(t){return (t={...J,...F.getDataAttributes(this._element),...t}).toggle=Boolean(t.toggle),t.parent=o(t.parent),r("collapse",t,tt),t}_getDimension(){return this._element.classList.contains("collapse-horizontal")?"width":"height"}_initializeChildren(){if(!this._config.parent)return;const t=U.find(".collapse .collapse",this._config.parent);U.find('[data-bs-toggle="collapse"]',this._config.parent).filter(e=>!t.includes(e)).forEach(t=>{const e=i(t);e&&this._addAriaAndCollapsedClass([t],this._isShown(e));});}_addAriaAndCollapsedClass(t,e){t.length&&t.forEach(t=>{e?t.classList.remove("collapsed"):t.classList.add("collapsed"),t.setAttribute("aria-expanded",e);});}static jQueryInterface(t){return this.each((function(){const e={};"string"==typeof t&&/show|hide/.test(t)&&(e.toggle=!1);const i=et.getOrCreateInstance(this,e);if("string"==typeof t){if(void 0===i[t])throw new TypeError(`No method named "${t}"`);i[t]();}}))}}P.on(document,"click.bs.collapse.data-api",'[data-bs-toggle="collapse"]',(function(t){("A"===t.target.tagName||t.delegateTarget&&"A"===t.delegateTarget.tagName)&&t.preventDefault();const i=e(this);U.find(i).forEach(t=>{et.getOrCreateInstance(t,{toggle:!1}).toggle();});})),m(et);var it="top",nt="bottom",st="right",ot="left",rt=[it,nt,st,ot],at=rt.reduce((function(t,e){return t.concat([e+"-start",e+"-end"])}),[]),lt=[].concat(rt,["auto"]).reduce((function(t,e){return t.concat([e,e+"-start",e+"-end"])}),[]),ct=["beforeRead","read","afterRead","beforeMain","main","afterMain","beforeWrite","write","afterWrite"];function ht(t){return t?(t.nodeName||"").toLowerCase():null}function dt(t){if(null==t)return window;if("[object Window]"!==t.toString()){var e=t.ownerDocument;return e&&e.defaultView||window}return t}function ut(t){return t instanceof dt(t).Element||t instanceof Element}function ft(t){return t instanceof dt(t).HTMLElement||t instanceof HTMLElement}function pt(t){return "undefined"!=typeof ShadowRoot&&(t instanceof dt(t).ShadowRoot||t instanceof ShadowRoot)}var mt={name:"applyStyles",enabled:!0,phase:"write",fn:function(t){var e=t.state;Object.keys(e.elements).forEach((function(t){var i=e.styles[t]||{},n=e.attributes[t]||{},s=e.elements[t];ft(s)&&ht(s)&&(Object.assign(s.style,i),Object.keys(n).forEach((function(t){var e=n[t];!1===e?s.removeAttribute(t):s.setAttribute(t,!0===e?"":e);})));}));},effect:function(t){var e=t.state,i={popper:{position:e.options.strategy,left:"0",top:"0",margin:"0"},arrow:{position:"absolute"},reference:{}};return Object.assign(e.elements.popper.style,i.popper),e.styles=i,e.elements.arrow&&Object.assign(e.elements.arrow.style,i.arrow),function(){Object.keys(e.elements).forEach((function(t){var n=e.elements[t],s=e.attributes[t]||{},o=Object.keys(e.styles.hasOwnProperty(t)?e.styles[t]:i[t]).reduce((function(t,e){return t[e]="",t}),{});ft(n)&&ht(n)&&(Object.assign(n.style,o),Object.keys(s).forEach((function(t){n.removeAttribute(t);})));}));}},requires:["computeStyles"]};function gt(t){return t.split("-")[0]}var _t=Math.round;function bt(t,e){void 0===e&&(e=!1);var i=t.getBoundingClientRect(),n=1,s=1;return ft(t)&&e&&(n=i.width/t.offsetWidth||1,s=i.height/t.offsetHeight||1),{width:_t(i.width/n),height:_t(i.height/s),top:_t(i.top/s),right:_t(i.right/n),bottom:_t(i.bottom/s),left:_t(i.left/n),x:_t(i.left/n),y:_t(i.top/s)}}function vt(t){var e=bt(t),i=t.offsetWidth,n=t.offsetHeight;return Math.abs(e.width-i)<=1&&(i=e.width),Math.abs(e.height-n)<=1&&(n=e.height),{x:t.offsetLeft,y:t.offsetTop,width:i,height:n}}function yt(t,e){var i=e.getRootNode&&e.getRootNode();if(t.contains(e))return !0;if(i&&pt(i)){var n=e;do{if(n&&t.isSameNode(n))return !0;n=n.parentNode||n.host;}while(n)}return !1}function wt(t){return dt(t).getComputedStyle(t)}function Et(t){return ["table","td","th"].indexOf(ht(t))>=0}function At(t){return ((ut(t)?t.ownerDocument:t.document)||window.document).documentElement}function Tt(t){return "html"===ht(t)?t:t.assignedSlot||t.parentNode||(pt(t)?t.host:null)||At(t)}function Ot(t){return ft(t)&&"fixed"!==wt(t).position?t.offsetParent:null}function Ct(t){for(var e=dt(t),i=Ot(t);i&&Et(i)&&"static"===wt(i).position;)i=Ot(i);return i&&("html"===ht(i)||"body"===ht(i)&&"static"===wt(i).position)?e:i||function(t){var e=-1!==navigator.userAgent.toLowerCase().indexOf("firefox");if(-1!==navigator.userAgent.indexOf("Trident")&&ft(t)&&"fixed"===wt(t).position)return null;for(var i=Tt(t);ft(i)&&["html","body"].indexOf(ht(i))<0;){var n=wt(i);if("none"!==n.transform||"none"!==n.perspective||"paint"===n.contain||-1!==["transform","perspective"].indexOf(n.willChange)||e&&"filter"===n.willChange||e&&n.filter&&"none"!==n.filter)return i;i=i.parentNode;}return null}(t)||e}function kt(t){return ["top","bottom"].indexOf(t)>=0?"x":"y"}var Lt=Math.max,xt=Math.min,Dt=Math.round;function St(t,e,i){return Lt(t,xt(e,i))}function Nt(t){return Object.assign({},{top:0,right:0,bottom:0,left:0},t)}function It(t,e){return e.reduce((function(e,i){return e[i]=t,e}),{})}var Pt={name:"arrow",enabled:!0,phase:"main",fn:function(t){var e,i=t.state,n=t.name,s=t.options,o=i.elements.arrow,r=i.modifiersData.popperOffsets,a=gt(i.placement),l=kt(a),c=[ot,st].indexOf(a)>=0?"height":"width";if(o&&r){var h=function(t,e){return Nt("number"!=typeof(t="function"==typeof t?t(Object.assign({},e.rects,{placement:e.placement})):t)?t:It(t,rt))}(s.padding,i),d=vt(o),u="y"===l?it:ot,f="y"===l?nt:st,p=i.rects.reference[c]+i.rects.reference[l]-r[l]-i.rects.popper[c],m=r[l]-i.rects.reference[l],g=Ct(o),_=g?"y"===l?g.clientHeight||0:g.clientWidth||0:0,b=p/2-m/2,v=h[u],y=_-d[c]-h[f],w=_/2-d[c]/2+b,E=St(v,w,y),A=l;i.modifiersData[n]=((e={})[A]=E,e.centerOffset=E-w,e);}},effect:function(t){var e=t.state,i=t.options.element,n=void 0===i?"[data-popper-arrow]":i;null!=n&&("string"!=typeof n||(n=e.elements.popper.querySelector(n)))&&yt(e.elements.popper,n)&&(e.elements.arrow=n);},requires:["popperOffsets"],requiresIfExists:["preventOverflow"]},jt={top:"auto",right:"auto",bottom:"auto",left:"auto"};function Mt(t){var e,i=t.popper,n=t.popperRect,s=t.placement,o=t.offsets,r=t.position,a=t.gpuAcceleration,l=t.adaptive,c=t.roundOffsets,h=!0===c?function(t){var e=t.x,i=t.y,n=window.devicePixelRatio||1;return {x:Dt(Dt(e*n)/n)||0,y:Dt(Dt(i*n)/n)||0}}(o):"function"==typeof c?c(o):o,d=h.x,u=void 0===d?0:d,f=h.y,p=void 0===f?0:f,m=o.hasOwnProperty("x"),g=o.hasOwnProperty("y"),_=ot,b=it,v=window;if(l){var y=Ct(i),w="clientHeight",E="clientWidth";y===dt(i)&&"static"!==wt(y=At(i)).position&&(w="scrollHeight",E="scrollWidth"),y=y,s===it&&(b=nt,p-=y[w]-n.height,p*=a?1:-1),s===ot&&(_=st,u-=y[E]-n.width,u*=a?1:-1);}var A,T=Object.assign({position:r},l&&jt);return a?Object.assign({},T,((A={})[b]=g?"0":"",A[_]=m?"0":"",A.transform=(v.devicePixelRatio||1)<2?"translate("+u+"px, "+p+"px)":"translate3d("+u+"px, "+p+"px, 0)",A)):Object.assign({},T,((e={})[b]=g?p+"px":"",e[_]=m?u+"px":"",e.transform="",e))}var Ht={name:"computeStyles",enabled:!0,phase:"beforeWrite",fn:function(t){var e=t.state,i=t.options,n=i.gpuAcceleration,s=void 0===n||n,o=i.adaptive,r=void 0===o||o,a=i.roundOffsets,l=void 0===a||a,c={placement:gt(e.placement),popper:e.elements.popper,popperRect:e.rects.popper,gpuAcceleration:s};null!=e.modifiersData.popperOffsets&&(e.styles.popper=Object.assign({},e.styles.popper,Mt(Object.assign({},c,{offsets:e.modifiersData.popperOffsets,position:e.options.strategy,adaptive:r,roundOffsets:l})))),null!=e.modifiersData.arrow&&(e.styles.arrow=Object.assign({},e.styles.arrow,Mt(Object.assign({},c,{offsets:e.modifiersData.arrow,position:"absolute",adaptive:!1,roundOffsets:l})))),e.attributes.popper=Object.assign({},e.attributes.popper,{"data-popper-placement":e.placement});},data:{}},Bt={passive:!0},Rt={name:"eventListeners",enabled:!0,phase:"write",fn:function(){},effect:function(t){var e=t.state,i=t.instance,n=t.options,s=n.scroll,o=void 0===s||s,r=n.resize,a=void 0===r||r,l=dt(e.elements.popper),c=[].concat(e.scrollParents.reference,e.scrollParents.popper);return o&&c.forEach((function(t){t.addEventListener("scroll",i.update,Bt);})),a&&l.addEventListener("resize",i.update,Bt),function(){o&&c.forEach((function(t){t.removeEventListener("scroll",i.update,Bt);})),a&&l.removeEventListener("resize",i.update,Bt);}},data:{}},Wt={left:"right",right:"left",bottom:"top",top:"bottom"};function zt(t){return t.replace(/left|right|bottom|top/g,(function(t){return Wt[t]}))}var qt={start:"end",end:"start"};function Ft(t){return t.replace(/start|end/g,(function(t){return qt[t]}))}function Ut(t){var e=dt(t);return {scrollLeft:e.pageXOffset,scrollTop:e.pageYOffset}}function $t(t){return bt(At(t)).left+Ut(t).scrollLeft}function Vt(t){var e=wt(t),i=e.overflow,n=e.overflowX,s=e.overflowY;return /auto|scroll|overlay|hidden/.test(i+s+n)}function Kt(t,e){var i;void 0===e&&(e=[]);var n=function t(e){return ["html","body","#document"].indexOf(ht(e))>=0?e.ownerDocument.body:ft(e)&&Vt(e)?e:t(Tt(e))}(t),s=n===(null==(i=t.ownerDocument)?void 0:i.body),o=dt(n),r=s?[o].concat(o.visualViewport||[],Vt(n)?n:[]):n,a=e.concat(r);return s?a:a.concat(Kt(Tt(r)))}function Xt(t){return Object.assign({},t,{left:t.x,top:t.y,right:t.x+t.width,bottom:t.y+t.height})}function Yt(t,e){return "viewport"===e?Xt(function(t){var e=dt(t),i=At(t),n=e.visualViewport,s=i.clientWidth,o=i.clientHeight,r=0,a=0;return n&&(s=n.width,o=n.height,/^((?!chrome|android).)*safari/i.test(navigator.userAgent)||(r=n.offsetLeft,a=n.offsetTop)),{width:s,height:o,x:r+$t(t),y:a}}(t)):ft(e)?function(t){var e=bt(t);return e.top=e.top+t.clientTop,e.left=e.left+t.clientLeft,e.bottom=e.top+t.clientHeight,e.right=e.left+t.clientWidth,e.width=t.clientWidth,e.height=t.clientHeight,e.x=e.left,e.y=e.top,e}(e):Xt(function(t){var e,i=At(t),n=Ut(t),s=null==(e=t.ownerDocument)?void 0:e.body,o=Lt(i.scrollWidth,i.clientWidth,s?s.scrollWidth:0,s?s.clientWidth:0),r=Lt(i.scrollHeight,i.clientHeight,s?s.scrollHeight:0,s?s.clientHeight:0),a=-n.scrollLeft+$t(t),l=-n.scrollTop;return "rtl"===wt(s||i).direction&&(a+=Lt(i.clientWidth,s?s.clientWidth:0)-o),{width:o,height:r,x:a,y:l}}(At(t)))}function Qt(t){return t.split("-")[1]}function Gt(t){var e,i=t.reference,n=t.element,s=t.placement,o=s?gt(s):null,r=s?Qt(s):null,a=i.x+i.width/2-n.width/2,l=i.y+i.height/2-n.height/2;switch(o){case it:e={x:a,y:i.y-n.height};break;case nt:e={x:a,y:i.y+i.height};break;case st:e={x:i.x+i.width,y:l};break;case ot:e={x:i.x-n.width,y:l};break;default:e={x:i.x,y:i.y};}var c=o?kt(o):null;if(null!=c){var h="y"===c?"height":"width";switch(r){case"start":e[c]=e[c]-(i[h]/2-n[h]/2);break;case"end":e[c]=e[c]+(i[h]/2-n[h]/2);}}return e}function Zt(t,e){void 0===e&&(e={});var i=e,n=i.placement,s=void 0===n?t.placement:n,o=i.boundary,r=void 0===o?"clippingParents":o,a=i.rootBoundary,l=void 0===a?"viewport":a,c=i.elementContext,h=void 0===c?"popper":c,d=i.altBoundary,u=void 0!==d&&d,f=i.padding,p=void 0===f?0:f,m=Nt("number"!=typeof p?p:It(p,rt)),g="popper"===h?"reference":"popper",_=t.elements.reference,b=t.rects.popper,v=t.elements[u?g:h],y=function(t,e,i){var n="clippingParents"===e?function(t){var e=Kt(Tt(t)),i=["absolute","fixed"].indexOf(wt(t).position)>=0&&ft(t)?Ct(t):t;return ut(i)?e.filter((function(t){return ut(t)&&yt(t,i)&&"body"!==ht(t)})):[]}(t):[].concat(e),s=[].concat(n,[i]),o=s[0],r=s.reduce((function(e,i){var n=Yt(t,i);return e.top=Lt(n.top,e.top),e.right=xt(n.right,e.right),e.bottom=xt(n.bottom,e.bottom),e.left=Lt(n.left,e.left),e}),Yt(t,o));return r.width=r.right-r.left,r.height=r.bottom-r.top,r.x=r.left,r.y=r.top,r}(ut(v)?v:v.contextElement||At(t.elements.popper),r,l),w=bt(_),E=Gt({reference:w,element:b,strategy:"absolute",placement:s}),A=Xt(Object.assign({},b,E)),T="popper"===h?A:w,O={top:y.top-T.top+m.top,bottom:T.bottom-y.bottom+m.bottom,left:y.left-T.left+m.left,right:T.right-y.right+m.right},C=t.modifiersData.offset;if("popper"===h&&C){var k=C[s];Object.keys(O).forEach((function(t){var e=[st,nt].indexOf(t)>=0?1:-1,i=[it,nt].indexOf(t)>=0?"y":"x";O[t]+=k[i]*e;}));}return O}function Jt(t,e){void 0===e&&(e={});var i=e,n=i.placement,s=i.boundary,o=i.rootBoundary,r=i.padding,a=i.flipVariations,l=i.allowedAutoPlacements,c=void 0===l?lt:l,h=Qt(n),d=h?a?at:at.filter((function(t){return Qt(t)===h})):rt,u=d.filter((function(t){return c.indexOf(t)>=0}));0===u.length&&(u=d);var f=u.reduce((function(e,i){return e[i]=Zt(t,{placement:i,boundary:s,rootBoundary:o,padding:r})[gt(i)],e}),{});return Object.keys(f).sort((function(t,e){return f[t]-f[e]}))}var te={name:"flip",enabled:!0,phase:"main",fn:function(t){var e=t.state,i=t.options,n=t.name;if(!e.modifiersData[n]._skip){for(var s=i.mainAxis,o=void 0===s||s,r=i.altAxis,a=void 0===r||r,l=i.fallbackPlacements,c=i.padding,h=i.boundary,d=i.rootBoundary,u=i.altBoundary,f=i.flipVariations,p=void 0===f||f,m=i.allowedAutoPlacements,g=e.options.placement,_=gt(g),b=l||(_!==g&&p?function(t){if("auto"===gt(t))return [];var e=zt(t);return [Ft(t),e,Ft(e)]}(g):[zt(g)]),v=[g].concat(b).reduce((function(t,i){return t.concat("auto"===gt(i)?Jt(e,{placement:i,boundary:h,rootBoundary:d,padding:c,flipVariations:p,allowedAutoPlacements:m}):i)}),[]),y=e.rects.reference,w=e.rects.popper,E=new Map,A=!0,T=v[0],O=0;O<v.length;O++){var C=v[O],k=gt(C),L="start"===Qt(C),x=[it,nt].indexOf(k)>=0,D=x?"width":"height",S=Zt(e,{placement:C,boundary:h,rootBoundary:d,altBoundary:u,padding:c}),N=x?L?st:ot:L?nt:it;y[D]>w[D]&&(N=zt(N));var I=zt(N),P=[];if(o&&P.push(S[k]<=0),a&&P.push(S[N]<=0,S[I]<=0),P.every((function(t){return t}))){T=C,A=!1;break}E.set(C,P);}if(A)for(var j=function(t){var e=v.find((function(e){var i=E.get(e);if(i)return i.slice(0,t).every((function(t){return t}))}));if(e)return T=e,"break"},M=p?3:1;M>0&&"break"!==j(M);M--);e.placement!==T&&(e.modifiersData[n]._skip=!0,e.placement=T,e.reset=!0);}},requiresIfExists:["offset"],data:{_skip:!1}};function ee(t,e,i){return void 0===i&&(i={x:0,y:0}),{top:t.top-e.height-i.y,right:t.right-e.width+i.x,bottom:t.bottom-e.height+i.y,left:t.left-e.width-i.x}}function ie(t){return [it,st,nt,ot].some((function(e){return t[e]>=0}))}var ne={name:"hide",enabled:!0,phase:"main",requiresIfExists:["preventOverflow"],fn:function(t){var e=t.state,i=t.name,n=e.rects.reference,s=e.rects.popper,o=e.modifiersData.preventOverflow,r=Zt(e,{elementContext:"reference"}),a=Zt(e,{altBoundary:!0}),l=ee(r,n),c=ee(a,s,o),h=ie(l),d=ie(c);e.modifiersData[i]={referenceClippingOffsets:l,popperEscapeOffsets:c,isReferenceHidden:h,hasPopperEscaped:d},e.attributes.popper=Object.assign({},e.attributes.popper,{"data-popper-reference-hidden":h,"data-popper-escaped":d});}},se={name:"offset",enabled:!0,phase:"main",requires:["popperOffsets"],fn:function(t){var e=t.state,i=t.options,n=t.name,s=i.offset,o=void 0===s?[0,0]:s,r=lt.reduce((function(t,i){return t[i]=function(t,e,i){var n=gt(t),s=[ot,it].indexOf(n)>=0?-1:1,o="function"==typeof i?i(Object.assign({},e,{placement:t})):i,r=o[0],a=o[1];return r=r||0,a=(a||0)*s,[ot,st].indexOf(n)>=0?{x:a,y:r}:{x:r,y:a}}(i,e.rects,o),t}),{}),a=r[e.placement],l=a.x,c=a.y;null!=e.modifiersData.popperOffsets&&(e.modifiersData.popperOffsets.x+=l,e.modifiersData.popperOffsets.y+=c),e.modifiersData[n]=r;}},oe={name:"popperOffsets",enabled:!0,phase:"read",fn:function(t){var e=t.state,i=t.name;e.modifiersData[i]=Gt({reference:e.rects.reference,element:e.rects.popper,strategy:"absolute",placement:e.placement});},data:{}},re={name:"preventOverflow",enabled:!0,phase:"main",fn:function(t){var e=t.state,i=t.options,n=t.name,s=i.mainAxis,o=void 0===s||s,r=i.altAxis,a=void 0!==r&&r,l=i.boundary,c=i.rootBoundary,h=i.altBoundary,d=i.padding,u=i.tether,f=void 0===u||u,p=i.tetherOffset,m=void 0===p?0:p,g=Zt(e,{boundary:l,rootBoundary:c,padding:d,altBoundary:h}),_=gt(e.placement),b=Qt(e.placement),v=!b,y=kt(_),w="x"===y?"y":"x",E=e.modifiersData.popperOffsets,A=e.rects.reference,T=e.rects.popper,O="function"==typeof m?m(Object.assign({},e.rects,{placement:e.placement})):m,C={x:0,y:0};if(E){if(o||a){var k="y"===y?it:ot,L="y"===y?nt:st,x="y"===y?"height":"width",D=E[y],S=E[y]+g[k],N=E[y]-g[L],I=f?-T[x]/2:0,P="start"===b?A[x]:T[x],j="start"===b?-T[x]:-A[x],M=e.elements.arrow,H=f&&M?vt(M):{width:0,height:0},B=e.modifiersData["arrow#persistent"]?e.modifiersData["arrow#persistent"].padding:{top:0,right:0,bottom:0,left:0},R=B[k],W=B[L],z=St(0,A[x],H[x]),q=v?A[x]/2-I-z-R-O:P-z-R-O,F=v?-A[x]/2+I+z+W+O:j+z+W+O,U=e.elements.arrow&&Ct(e.elements.arrow),$=U?"y"===y?U.clientTop||0:U.clientLeft||0:0,V=e.modifiersData.offset?e.modifiersData.offset[e.placement][y]:0,K=E[y]+q-V-$,X=E[y]+F-V;if(o){var Y=St(f?xt(S,K):S,D,f?Lt(N,X):N);E[y]=Y,C[y]=Y-D;}if(a){var Q="x"===y?it:ot,G="x"===y?nt:st,Z=E[w],J=Z+g[Q],tt=Z-g[G],et=St(f?xt(J,K):J,Z,f?Lt(tt,X):tt);E[w]=et,C[w]=et-Z;}}e.modifiersData[n]=C;}},requiresIfExists:["offset"]};function ae(t,e,i){void 0===i&&(i=!1);var n,s,o=ft(e),r=ft(e)&&function(t){var e=t.getBoundingClientRect(),i=e.width/t.offsetWidth||1,n=e.height/t.offsetHeight||1;return 1!==i||1!==n}(e),a=At(e),l=bt(t,r),c={scrollLeft:0,scrollTop:0},h={x:0,y:0};return (o||!o&&!i)&&(("body"!==ht(e)||Vt(a))&&(c=(n=e)!==dt(n)&&ft(n)?{scrollLeft:(s=n).scrollLeft,scrollTop:s.scrollTop}:Ut(n)),ft(e)?((h=bt(e,!0)).x+=e.clientLeft,h.y+=e.clientTop):a&&(h.x=$t(a))),{x:l.left+c.scrollLeft-h.x,y:l.top+c.scrollTop-h.y,width:l.width,height:l.height}}var le={placement:"bottom",modifiers:[],strategy:"absolute"};function ce(){for(var t=arguments.length,e=new Array(t),i=0;i<t;i++)e[i]=arguments[i];return !e.some((function(t){return !(t&&"function"==typeof t.getBoundingClientRect)}))}function he(t){void 0===t&&(t={});var e=t,i=e.defaultModifiers,n=void 0===i?[]:i,s=e.defaultOptions,o=void 0===s?le:s;return function(t,e,i){void 0===i&&(i=o);var s,r,a={placement:"bottom",orderedModifiers:[],options:Object.assign({},le,o),modifiersData:{},elements:{reference:t,popper:e},attributes:{},styles:{}},l=[],c=!1,h={state:a,setOptions:function(i){d(),a.options=Object.assign({},o,a.options,i),a.scrollParents={reference:ut(t)?Kt(t):t.contextElement?Kt(t.contextElement):[],popper:Kt(e)};var s,r,c=function(t){var e=function(t){var e=new Map,i=new Set,n=[];return t.forEach((function(t){e.set(t.name,t);})),t.forEach((function(t){i.has(t.name)||function t(s){i.add(s.name),[].concat(s.requires||[],s.requiresIfExists||[]).forEach((function(n){if(!i.has(n)){var s=e.get(n);s&&t(s);}})),n.push(s);}(t);})),n}(t);return ct.reduce((function(t,i){return t.concat(e.filter((function(t){return t.phase===i})))}),[])}((s=[].concat(n,a.options.modifiers),r=s.reduce((function(t,e){var i=t[e.name];return t[e.name]=i?Object.assign({},i,e,{options:Object.assign({},i.options,e.options),data:Object.assign({},i.data,e.data)}):e,t}),{}),Object.keys(r).map((function(t){return r[t]}))));return a.orderedModifiers=c.filter((function(t){return t.enabled})),a.orderedModifiers.forEach((function(t){var e=t.name,i=t.options,n=void 0===i?{}:i,s=t.effect;if("function"==typeof s){var o=s({state:a,name:e,instance:h,options:n});l.push(o||function(){});}})),h.update()},forceUpdate:function(){if(!c){var t=a.elements,e=t.reference,i=t.popper;if(ce(e,i)){a.rects={reference:ae(e,Ct(i),"fixed"===a.options.strategy),popper:vt(i)},a.reset=!1,a.placement=a.options.placement,a.orderedModifiers.forEach((function(t){return a.modifiersData[t.name]=Object.assign({},t.data)}));for(var n=0;n<a.orderedModifiers.length;n++)if(!0!==a.reset){var s=a.orderedModifiers[n],o=s.fn,r=s.options,l=void 0===r?{}:r,d=s.name;"function"==typeof o&&(a=o({state:a,options:l,name:d,instance:h})||a);}else a.reset=!1,n=-1;}}},update:(s=function(){return new Promise((function(t){h.forceUpdate(),t(a);}))},function(){return r||(r=new Promise((function(t){Promise.resolve().then((function(){r=void 0,t(s());}));}))),r}),destroy:function(){d(),c=!0;}};if(!ce(t,e))return h;function d(){l.forEach((function(t){return t()})),l=[];}return h.setOptions(i).then((function(t){!c&&i.onFirstUpdate&&i.onFirstUpdate(t);})),h}}var de=he(),ue=he({defaultModifiers:[Rt,oe,Ht,mt]}),fe=he({defaultModifiers:[Rt,oe,Ht,mt,se,te,re,Pt,ne]}),pe=Object.freeze({__proto__:null,popperGenerator:he,detectOverflow:Zt,createPopperBase:de,createPopper:fe,createPopperLite:ue,top:it,bottom:nt,right:st,left:ot,auto:"auto",basePlacements:rt,start:"start",end:"end",clippingParents:"clippingParents",viewport:"viewport",popper:"popper",reference:"reference",variationPlacements:at,placements:lt,beforeRead:"beforeRead",read:"read",afterRead:"afterRead",beforeMain:"beforeMain",main:"main",afterMain:"afterMain",beforeWrite:"beforeWrite",write:"write",afterWrite:"afterWrite",modifierPhases:ct,applyStyles:mt,arrow:Pt,computeStyles:Ht,eventListeners:Rt,flip:te,hide:ne,offset:se,popperOffsets:oe,preventOverflow:re});const me=new RegExp("ArrowUp|ArrowDown|Escape"),ge=p()?"top-end":"top-start",_e=p()?"top-start":"top-end",be=p()?"bottom-end":"bottom-start",ve=p()?"bottom-start":"bottom-end",ye=p()?"left-start":"right-start",we=p()?"right-start":"left-start",Ee={offset:[0,2],boundary:"clippingParents",reference:"toggle",display:"dynamic",popperConfig:null,autoClose:!0},Ae={offset:"(array|string|function)",boundary:"(string|element)",reference:"(string|element|object)",display:"string",popperConfig:"(null|object|function)",autoClose:"(boolean|string)"};class Te extends H{constructor(t,e){super(t),this._popper=null,this._config=this._getConfig(e),this._menu=this._getMenuElement(),this._inNavbar=this._detectNavbar();}static get Default(){return Ee}static get DefaultType(){return Ae}static get NAME(){return "dropdown"}toggle(){return this._isShown()?this.hide():this.show()}show(){if(l(this._element)||this._isShown(this._menu))return;const t={relatedTarget:this._element};if(P.trigger(this._element,"show.bs.dropdown",t).defaultPrevented)return;const e=Te.getParentFromElement(this._element);this._inNavbar?F.setDataAttribute(this._menu,"popper","none"):this._createPopper(e),"ontouchstart"in document.documentElement&&!e.closest(".navbar-nav")&&[].concat(...document.body.children).forEach(t=>P.on(t,"mouseover",h)),this._element.focus(),this._element.setAttribute("aria-expanded",!0),this._menu.classList.add("show"),this._element.classList.add("show"),P.trigger(this._element,"shown.bs.dropdown",t);}hide(){if(l(this._element)||!this._isShown(this._menu))return;const t={relatedTarget:this._element};this._completeHide(t);}dispose(){this._popper&&this._popper.destroy(),super.dispose();}update(){this._inNavbar=this._detectNavbar(),this._popper&&this._popper.update();}_completeHide(t){P.trigger(this._element,"hide.bs.dropdown",t).defaultPrevented||("ontouchstart"in document.documentElement&&[].concat(...document.body.children).forEach(t=>P.off(t,"mouseover",h)),this._popper&&this._popper.destroy(),this._menu.classList.remove("show"),this._element.classList.remove("show"),this._element.setAttribute("aria-expanded","false"),F.removeDataAttribute(this._menu,"popper"),P.trigger(this._element,"hidden.bs.dropdown",t));}_getConfig(t){if(t={...this.constructor.Default,...F.getDataAttributes(this._element),...t},r("dropdown",t,this.constructor.DefaultType),"object"==typeof t.reference&&!s(t.reference)&&"function"!=typeof t.reference.getBoundingClientRect)throw new TypeError("dropdown".toUpperCase()+': Option "reference" provided type "object" without a required "getBoundingClientRect" method.');return t}_createPopper(t){if(void 0===pe)throw new TypeError("Bootstrap's dropdowns require Popper (https://popper.js.org)");let e=this._element;"parent"===this._config.reference?e=t:s(this._config.reference)?e=o(this._config.reference):"object"==typeof this._config.reference&&(e=this._config.reference);const i=this._getPopperConfig(),n=i.modifiers.find(t=>"applyStyles"===t.name&&!1===t.enabled);this._popper=fe(e,this._menu,i),n&&F.setDataAttribute(this._menu,"popper","static");}_isShown(t=this._element){return t.classList.contains("show")}_getMenuElement(){return U.next(this._element,".dropdown-menu")[0]}_getPlacement(){const t=this._element.parentNode;if(t.classList.contains("dropend"))return ye;if(t.classList.contains("dropstart"))return we;const e="end"===getComputedStyle(this._menu).getPropertyValue("--bs-position").trim();return t.classList.contains("dropup")?e?_e:ge:e?ve:be}_detectNavbar(){return null!==this._element.closest(".navbar")}_getOffset(){const{offset:t}=this._config;return "string"==typeof t?t.split(",").map(t=>Number.parseInt(t,10)):"function"==typeof t?e=>t(e,this._element):t}_getPopperConfig(){const t={placement:this._getPlacement(),modifiers:[{name:"preventOverflow",options:{boundary:this._config.boundary}},{name:"offset",options:{offset:this._getOffset()}}]};return "static"===this._config.display&&(t.modifiers=[{name:"applyStyles",enabled:!1}]),{...t,..."function"==typeof this._config.popperConfig?this._config.popperConfig(t):this._config.popperConfig}}_selectMenuItem({key:t,target:e}){const i=U.find(".dropdown-menu .dropdown-item:not(.disabled):not(:disabled)",this._menu).filter(a);i.length&&b(i,e,"ArrowDown"===t,!i.includes(e)).focus();}static jQueryInterface(t){return this.each((function(){const e=Te.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===e[t])throw new TypeError(`No method named "${t}"`);e[t]();}}))}static clearMenus(t){if(t&&(2===t.button||"keyup"===t.type&&"Tab"!==t.key))return;const e=U.find('[data-bs-toggle="dropdown"]');for(let i=0,n=e.length;i<n;i++){const n=Te.getInstance(e[i]);if(!n||!1===n._config.autoClose)continue;if(!n._isShown())continue;const s={relatedTarget:n._element};if(t){const e=t.composedPath(),i=e.includes(n._menu);if(e.includes(n._element)||"inside"===n._config.autoClose&&!i||"outside"===n._config.autoClose&&i)continue;if(n._menu.contains(t.target)&&("keyup"===t.type&&"Tab"===t.key||/input|select|option|textarea|form/i.test(t.target.tagName)))continue;"click"===t.type&&(s.clickEvent=t);}n._completeHide(s);}}static getParentFromElement(t){return i(t)||t.parentNode}static dataApiKeydownHandler(t){if(/input|textarea/i.test(t.target.tagName)?"Space"===t.key||"Escape"!==t.key&&("ArrowDown"!==t.key&&"ArrowUp"!==t.key||t.target.closest(".dropdown-menu")):!me.test(t.key))return;const e=this.classList.contains("show");if(!e&&"Escape"===t.key)return;if(t.preventDefault(),t.stopPropagation(),l(this))return;const i=this.matches('[data-bs-toggle="dropdown"]')?this:U.prev(this,'[data-bs-toggle="dropdown"]')[0],n=Te.getOrCreateInstance(i);if("Escape"!==t.key)return "ArrowUp"===t.key||"ArrowDown"===t.key?(e||n.show(),void n._selectMenuItem(t)):void(e&&"Space"!==t.key||Te.clearMenus());n.hide();}}P.on(document,"keydown.bs.dropdown.data-api",'[data-bs-toggle="dropdown"]',Te.dataApiKeydownHandler),P.on(document,"keydown.bs.dropdown.data-api",".dropdown-menu",Te.dataApiKeydownHandler),P.on(document,"click.bs.dropdown.data-api",Te.clearMenus),P.on(document,"keyup.bs.dropdown.data-api",Te.clearMenus),P.on(document,"click.bs.dropdown.data-api",'[data-bs-toggle="dropdown"]',(function(t){t.preventDefault(),Te.getOrCreateInstance(this).toggle();})),m(Te);class Oe{constructor(){this._element=document.body;}getWidth(){const t=document.documentElement.clientWidth;return Math.abs(window.innerWidth-t)}hide(){const t=this.getWidth();this._disableOverFlow(),this._setElementAttributes(this._element,"paddingRight",e=>e+t),this._setElementAttributes(".fixed-top, .fixed-bottom, .is-fixed, .sticky-top","paddingRight",e=>e+t),this._setElementAttributes(".sticky-top","marginRight",e=>e-t);}_disableOverFlow(){this._saveInitialAttribute(this._element,"overflow"),this._element.style.overflow="hidden";}_setElementAttributes(t,e,i){const n=this.getWidth();this._applyManipulationCallback(t,t=>{if(t!==this._element&&window.innerWidth>t.clientWidth+n)return;this._saveInitialAttribute(t,e);const s=window.getComputedStyle(t)[e];t.style[e]=i(Number.parseFloat(s))+"px";});}reset(){this._resetElementAttributes(this._element,"overflow"),this._resetElementAttributes(this._element,"paddingRight"),this._resetElementAttributes(".fixed-top, .fixed-bottom, .is-fixed, .sticky-top","paddingRight"),this._resetElementAttributes(".sticky-top","marginRight");}_saveInitialAttribute(t,e){const i=t.style[e];i&&F.setDataAttribute(t,e,i);}_resetElementAttributes(t,e){this._applyManipulationCallback(t,t=>{const i=F.getDataAttribute(t,e);void 0===i?t.style.removeProperty(e):(F.removeDataAttribute(t,e),t.style[e]=i);});}_applyManipulationCallback(t,e){s(t)?e(t):U.find(t,this._element).forEach(e);}isOverflowing(){return this.getWidth()>0}}const Ce={className:"modal-backdrop",isVisible:!0,isAnimated:!1,rootElement:"body",clickCallback:null},ke={className:"string",isVisible:"boolean",isAnimated:"boolean",rootElement:"(element|string)",clickCallback:"(function|null)"};class Le{constructor(t){this._config=this._getConfig(t),this._isAppended=!1,this._element=null;}show(t){this._config.isVisible?(this._append(),this._config.isAnimated&&d(this._getElement()),this._getElement().classList.add("show"),this._emulateAnimation(()=>{g(t);})):g(t);}hide(t){this._config.isVisible?(this._getElement().classList.remove("show"),this._emulateAnimation(()=>{this.dispose(),g(t);})):g(t);}_getElement(){if(!this._element){const t=document.createElement("div");t.className=this._config.className,this._config.isAnimated&&t.classList.add("fade"),this._element=t;}return this._element}_getConfig(t){return (t={...Ce,..."object"==typeof t?t:{}}).rootElement=o(t.rootElement),r("backdrop",t,ke),t}_append(){this._isAppended||(this._config.rootElement.append(this._getElement()),P.on(this._getElement(),"mousedown.bs.backdrop",()=>{g(this._config.clickCallback);}),this._isAppended=!0);}dispose(){this._isAppended&&(P.off(this._element,"mousedown.bs.backdrop"),this._element.remove(),this._isAppended=!1);}_emulateAnimation(t){_(t,this._getElement(),this._config.isAnimated);}}const xe={trapElement:null,autofocus:!0},De={trapElement:"element",autofocus:"boolean"};class Se{constructor(t){this._config=this._getConfig(t),this._isActive=!1,this._lastTabNavDirection=null;}activate(){const{trapElement:t,autofocus:e}=this._config;this._isActive||(e&&t.focus(),P.off(document,".bs.focustrap"),P.on(document,"focusin.bs.focustrap",t=>this._handleFocusin(t)),P.on(document,"keydown.tab.bs.focustrap",t=>this._handleKeydown(t)),this._isActive=!0);}deactivate(){this._isActive&&(this._isActive=!1,P.off(document,".bs.focustrap"));}_handleFocusin(t){const{target:e}=t,{trapElement:i}=this._config;if(e===document||e===i||i.contains(e))return;const n=U.focusableChildren(i);0===n.length?i.focus():"backward"===this._lastTabNavDirection?n[n.length-1].focus():n[0].focus();}_handleKeydown(t){"Tab"===t.key&&(this._lastTabNavDirection=t.shiftKey?"backward":"forward");}_getConfig(t){return t={...xe,..."object"==typeof t?t:{}},r("focustrap",t,De),t}}const Ne={backdrop:!0,keyboard:!0,focus:!0},Ie={backdrop:"(boolean|string)",keyboard:"boolean",focus:"boolean"};class Pe extends H{constructor(t,e){super(t),this._config=this._getConfig(e),this._dialog=U.findOne(".modal-dialog",this._element),this._backdrop=this._initializeBackDrop(),this._focustrap=this._initializeFocusTrap(),this._isShown=!1,this._ignoreBackdropClick=!1,this._isTransitioning=!1,this._scrollBar=new Oe;}static get Default(){return Ne}static get NAME(){return "modal"}toggle(t){return this._isShown?this.hide():this.show(t)}show(t){this._isShown||this._isTransitioning||P.trigger(this._element,"show.bs.modal",{relatedTarget:t}).defaultPrevented||(this._isShown=!0,this._isAnimated()&&(this._isTransitioning=!0),this._scrollBar.hide(),document.body.classList.add("modal-open"),this._adjustDialog(),this._setEscapeEvent(),this._setResizeEvent(),P.on(this._dialog,"mousedown.dismiss.bs.modal",()=>{P.one(this._element,"mouseup.dismiss.bs.modal",t=>{t.target===this._element&&(this._ignoreBackdropClick=!0);});}),this._showBackdrop(()=>this._showElement(t)));}hide(){if(!this._isShown||this._isTransitioning)return;if(P.trigger(this._element,"hide.bs.modal").defaultPrevented)return;this._isShown=!1;const t=this._isAnimated();t&&(this._isTransitioning=!0),this._setEscapeEvent(),this._setResizeEvent(),this._focustrap.deactivate(),this._element.classList.remove("show"),P.off(this._element,"click.dismiss.bs.modal"),P.off(this._dialog,"mousedown.dismiss.bs.modal"),this._queueCallback(()=>this._hideModal(),this._element,t);}dispose(){[window,this._dialog].forEach(t=>P.off(t,".bs.modal")),this._backdrop.dispose(),this._focustrap.deactivate(),super.dispose();}handleUpdate(){this._adjustDialog();}_initializeBackDrop(){return new Le({isVisible:Boolean(this._config.backdrop),isAnimated:this._isAnimated()})}_initializeFocusTrap(){return new Se({trapElement:this._element})}_getConfig(t){return t={...Ne,...F.getDataAttributes(this._element),..."object"==typeof t?t:{}},r("modal",t,Ie),t}_showElement(t){const e=this._isAnimated(),i=U.findOne(".modal-body",this._dialog);this._element.parentNode&&this._element.parentNode.nodeType===Node.ELEMENT_NODE||document.body.append(this._element),this._element.style.display="block",this._element.removeAttribute("aria-hidden"),this._element.setAttribute("aria-modal",!0),this._element.setAttribute("role","dialog"),this._element.scrollTop=0,i&&(i.scrollTop=0),e&&d(this._element),this._element.classList.add("show"),this._queueCallback(()=>{this._config.focus&&this._focustrap.activate(),this._isTransitioning=!1,P.trigger(this._element,"shown.bs.modal",{relatedTarget:t});},this._dialog,e);}_setEscapeEvent(){this._isShown?P.on(this._element,"keydown.dismiss.bs.modal",t=>{this._config.keyboard&&"Escape"===t.key?(t.preventDefault(),this.hide()):this._config.keyboard||"Escape"!==t.key||this._triggerBackdropTransition();}):P.off(this._element,"keydown.dismiss.bs.modal");}_setResizeEvent(){this._isShown?P.on(window,"resize.bs.modal",()=>this._adjustDialog()):P.off(window,"resize.bs.modal");}_hideModal(){this._element.style.display="none",this._element.setAttribute("aria-hidden",!0),this._element.removeAttribute("aria-modal"),this._element.removeAttribute("role"),this._isTransitioning=!1,this._backdrop.hide(()=>{document.body.classList.remove("modal-open"),this._resetAdjustments(),this._scrollBar.reset(),P.trigger(this._element,"hidden.bs.modal");});}_showBackdrop(t){P.on(this._element,"click.dismiss.bs.modal",t=>{this._ignoreBackdropClick?this._ignoreBackdropClick=!1:t.target===t.currentTarget&&(!0===this._config.backdrop?this.hide():"static"===this._config.backdrop&&this._triggerBackdropTransition());}),this._backdrop.show(t);}_isAnimated(){return this._element.classList.contains("fade")}_triggerBackdropTransition(){if(P.trigger(this._element,"hidePrevented.bs.modal").defaultPrevented)return;const{classList:t,scrollHeight:e,style:i}=this._element,n=e>document.documentElement.clientHeight;!n&&"hidden"===i.overflowY||t.contains("modal-static")||(n||(i.overflowY="hidden"),t.add("modal-static"),this._queueCallback(()=>{t.remove("modal-static"),n||this._queueCallback(()=>{i.overflowY="";},this._dialog);},this._dialog),this._element.focus());}_adjustDialog(){const t=this._element.scrollHeight>document.documentElement.clientHeight,e=this._scrollBar.getWidth(),i=e>0;(!i&&t&&!p()||i&&!t&&p())&&(this._element.style.paddingLeft=e+"px"),(i&&!t&&!p()||!i&&t&&p())&&(this._element.style.paddingRight=e+"px");}_resetAdjustments(){this._element.style.paddingLeft="",this._element.style.paddingRight="";}static jQueryInterface(t,e){return this.each((function(){const i=Pe.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===i[t])throw new TypeError(`No method named "${t}"`);i[t](e);}}))}}P.on(document,"click.bs.modal.data-api",'[data-bs-toggle="modal"]',(function(t){const e=i(this);["A","AREA"].includes(this.tagName)&&t.preventDefault(),P.one(e,"show.bs.modal",t=>{t.defaultPrevented||P.one(e,"hidden.bs.modal",()=>{a(this)&&this.focus();});}),Pe.getOrCreateInstance(e).toggle(this);})),B(Pe),m(Pe);const je={backdrop:!0,keyboard:!0,scroll:!1},Me={backdrop:"boolean",keyboard:"boolean",scroll:"boolean"};class He extends H{constructor(t,e){super(t),this._config=this._getConfig(e),this._isShown=!1,this._backdrop=this._initializeBackDrop(),this._focustrap=this._initializeFocusTrap(),this._addEventListeners();}static get NAME(){return "offcanvas"}static get Default(){return je}toggle(t){return this._isShown?this.hide():this.show(t)}show(t){this._isShown||P.trigger(this._element,"show.bs.offcanvas",{relatedTarget:t}).defaultPrevented||(this._isShown=!0,this._element.style.visibility="visible",this._backdrop.show(),this._config.scroll||(new Oe).hide(),this._element.removeAttribute("aria-hidden"),this._element.setAttribute("aria-modal",!0),this._element.setAttribute("role","dialog"),this._element.classList.add("show"),this._queueCallback(()=>{this._config.scroll||this._focustrap.activate(),P.trigger(this._element,"shown.bs.offcanvas",{relatedTarget:t});},this._element,!0));}hide(){this._isShown&&(P.trigger(this._element,"hide.bs.offcanvas").defaultPrevented||(this._focustrap.deactivate(),this._element.blur(),this._isShown=!1,this._element.classList.remove("show"),this._backdrop.hide(),this._queueCallback(()=>{this._element.setAttribute("aria-hidden",!0),this._element.removeAttribute("aria-modal"),this._element.removeAttribute("role"),this._element.style.visibility="hidden",this._config.scroll||(new Oe).reset(),P.trigger(this._element,"hidden.bs.offcanvas");},this._element,!0)));}dispose(){this._backdrop.dispose(),this._focustrap.deactivate(),super.dispose();}_getConfig(t){return t={...je,...F.getDataAttributes(this._element),..."object"==typeof t?t:{}},r("offcanvas",t,Me),t}_initializeBackDrop(){return new Le({className:"offcanvas-backdrop",isVisible:this._config.backdrop,isAnimated:!0,rootElement:this._element.parentNode,clickCallback:()=>this.hide()})}_initializeFocusTrap(){return new Se({trapElement:this._element})}_addEventListeners(){P.on(this._element,"keydown.dismiss.bs.offcanvas",t=>{this._config.keyboard&&"Escape"===t.key&&this.hide();});}static jQueryInterface(t){return this.each((function(){const e=He.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===e[t]||t.startsWith("_")||"constructor"===t)throw new TypeError(`No method named "${t}"`);e[t](this);}}))}}P.on(document,"click.bs.offcanvas.data-api",'[data-bs-toggle="offcanvas"]',(function(t){const e=i(this);if(["A","AREA"].includes(this.tagName)&&t.preventDefault(),l(this))return;P.one(e,"hidden.bs.offcanvas",()=>{a(this)&&this.focus();});const n=U.findOne(".offcanvas.show");n&&n!==e&&He.getInstance(n).hide(),He.getOrCreateInstance(e).toggle(this);})),P.on(window,"load.bs.offcanvas.data-api",()=>U.find(".offcanvas.show").forEach(t=>He.getOrCreateInstance(t).show())),B(He),m(He);const Be=new Set(["background","cite","href","itemtype","longdesc","poster","src","xlink:href"]),Re=/^(?:(?:https?|mailto|ftp|tel|file):|[^#&/:?]*(?:[#/?]|$))/i,We=/^data:(?:image\/(?:bmp|gif|jpeg|jpg|png|tiff|webp)|video\/(?:mpeg|mp4|ogg|webm)|audio\/(?:mp3|oga|ogg|opus));base64,[\d+/a-z]+=*$/i,ze=(t,e)=>{const i=t.nodeName.toLowerCase();if(e.includes(i))return !Be.has(i)||Boolean(Re.test(t.nodeValue)||We.test(t.nodeValue));const n=e.filter(t=>t instanceof RegExp);for(let t=0,e=n.length;t<e;t++)if(n[t].test(i))return !0;return !1};function qe(t,e,i){if(!t.length)return t;if(i&&"function"==typeof i)return i(t);const n=(new window.DOMParser).parseFromString(t,"text/html"),s=Object.keys(e),o=[].concat(...n.body.querySelectorAll("*"));for(let t=0,i=o.length;t<i;t++){const i=o[t],n=i.nodeName.toLowerCase();if(!s.includes(n)){i.remove();continue}const r=[].concat(...i.attributes),a=[].concat(e["*"]||[],e[n]||[]);r.forEach(t=>{ze(t,a)||i.removeAttribute(t.nodeName);});}return n.body.innerHTML}const Fe=new Set(["sanitize","allowList","sanitizeFn"]),Ue={animation:"boolean",template:"string",title:"(string|element|function)",trigger:"string",delay:"(number|object)",html:"boolean",selector:"(string|boolean)",placement:"(string|function)",offset:"(array|string|function)",container:"(string|element|boolean)",fallbackPlacements:"array",boundary:"(string|element)",customClass:"(string|function)",sanitize:"boolean",sanitizeFn:"(null|function)",allowList:"object",popperConfig:"(null|object|function)"},$e={AUTO:"auto",TOP:"top",RIGHT:p()?"left":"right",BOTTOM:"bottom",LEFT:p()?"right":"left"},Ve={animation:!0,template:'<div class="tooltip" role="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div>',trigger:"hover focus",title:"",delay:0,html:!1,selector:!1,placement:"top",offset:[0,0],container:!1,fallbackPlacements:["top","right","bottom","left"],boundary:"clippingParents",customClass:"",sanitize:!0,sanitizeFn:null,allowList:{"*":["class","dir","id","lang","role",/^aria-[\w-]*$/i],a:["target","href","title","rel"],area:[],b:[],br:[],col:[],code:[],div:[],em:[],hr:[],h1:[],h2:[],h3:[],h4:[],h5:[],h6:[],i:[],img:["src","srcset","alt","title","width","height"],li:[],ol:[],p:[],pre:[],s:[],small:[],span:[],sub:[],sup:[],strong:[],u:[],ul:[]},popperConfig:null},Ke={HIDE:"hide.bs.tooltip",HIDDEN:"hidden.bs.tooltip",SHOW:"show.bs.tooltip",SHOWN:"shown.bs.tooltip",INSERTED:"inserted.bs.tooltip",CLICK:"click.bs.tooltip",FOCUSIN:"focusin.bs.tooltip",FOCUSOUT:"focusout.bs.tooltip",MOUSEENTER:"mouseenter.bs.tooltip",MOUSELEAVE:"mouseleave.bs.tooltip"};class Xe extends H{constructor(t,e){if(void 0===pe)throw new TypeError("Bootstrap's tooltips require Popper (https://popper.js.org)");super(t),this._isEnabled=!0,this._timeout=0,this._hoverState="",this._activeTrigger={},this._popper=null,this._config=this._getConfig(e),this.tip=null,this._setListeners();}static get Default(){return Ve}static get NAME(){return "tooltip"}static get Event(){return Ke}static get DefaultType(){return Ue}enable(){this._isEnabled=!0;}disable(){this._isEnabled=!1;}toggleEnabled(){this._isEnabled=!this._isEnabled;}toggle(t){if(this._isEnabled)if(t){const e=this._initializeOnDelegatedTarget(t);e._activeTrigger.click=!e._activeTrigger.click,e._isWithActiveTrigger()?e._enter(null,e):e._leave(null,e);}else {if(this.getTipElement().classList.contains("show"))return void this._leave(null,this);this._enter(null,this);}}dispose(){clearTimeout(this._timeout),P.off(this._element.closest(".modal"),"hide.bs.modal",this._hideModalHandler),this.tip&&this.tip.remove(),this._popper&&this._popper.destroy(),super.dispose();}show(){if("none"===this._element.style.display)throw new Error("Please use show on visible elements");if(!this.isWithContent()||!this._isEnabled)return;const t=P.trigger(this._element,this.constructor.Event.SHOW),e=c(this._element),i=null===e?this._element.ownerDocument.documentElement.contains(this._element):e.contains(this._element);if(t.defaultPrevented||!i)return;const n=this.getTipElement(),s=(t=>{do{t+=Math.floor(1e6*Math.random());}while(document.getElementById(t));return t})(this.constructor.NAME);n.setAttribute("id",s),this._element.setAttribute("aria-describedby",s),this._config.animation&&n.classList.add("fade");const o="function"==typeof this._config.placement?this._config.placement.call(this,n,this._element):this._config.placement,r=this._getAttachment(o);this._addAttachmentClass(r);const{container:a}=this._config;M.set(n,this.constructor.DATA_KEY,this),this._element.ownerDocument.documentElement.contains(this.tip)||(a.append(n),P.trigger(this._element,this.constructor.Event.INSERTED)),this._popper?this._popper.update():this._popper=fe(this._element,n,this._getPopperConfig(r)),n.classList.add("show");const l=this._resolvePossibleFunction(this._config.customClass);l&&n.classList.add(...l.split(" ")),"ontouchstart"in document.documentElement&&[].concat(...document.body.children).forEach(t=>{P.on(t,"mouseover",h);});const d=this.tip.classList.contains("fade");this._queueCallback(()=>{const t=this._hoverState;this._hoverState=null,P.trigger(this._element,this.constructor.Event.SHOWN),"out"===t&&this._leave(null,this);},this.tip,d);}hide(){if(!this._popper)return;const t=this.getTipElement();if(P.trigger(this._element,this.constructor.Event.HIDE).defaultPrevented)return;t.classList.remove("show"),"ontouchstart"in document.documentElement&&[].concat(...document.body.children).forEach(t=>P.off(t,"mouseover",h)),this._activeTrigger.click=!1,this._activeTrigger.focus=!1,this._activeTrigger.hover=!1;const e=this.tip.classList.contains("fade");this._queueCallback(()=>{this._isWithActiveTrigger()||("show"!==this._hoverState&&t.remove(),this._cleanTipClass(),this._element.removeAttribute("aria-describedby"),P.trigger(this._element,this.constructor.Event.HIDDEN),this._popper&&(this._popper.destroy(),this._popper=null));},this.tip,e),this._hoverState="";}update(){null!==this._popper&&this._popper.update();}isWithContent(){return Boolean(this.getTitle())}getTipElement(){if(this.tip)return this.tip;const t=document.createElement("div");t.innerHTML=this._config.template;const e=t.children[0];return this.setContent(e),e.classList.remove("fade","show"),this.tip=e,this.tip}setContent(t){this._sanitizeAndSetContent(t,this.getTitle(),".tooltip-inner");}_sanitizeAndSetContent(t,e,i){const n=U.findOne(i,t);e||!n?this.setElementContent(n,e):n.remove();}setElementContent(t,e){if(null!==t)return s(e)?(e=o(e),void(this._config.html?e.parentNode!==t&&(t.innerHTML="",t.append(e)):t.textContent=e.textContent)):void(this._config.html?(this._config.sanitize&&(e=qe(e,this._config.allowList,this._config.sanitizeFn)),t.innerHTML=e):t.textContent=e)}getTitle(){const t=this._element.getAttribute("data-bs-original-title")||this._config.title;return this._resolvePossibleFunction(t)}updateAttachment(t){return "right"===t?"end":"left"===t?"start":t}_initializeOnDelegatedTarget(t,e){return e||this.constructor.getOrCreateInstance(t.delegateTarget,this._getDelegateConfig())}_getOffset(){const{offset:t}=this._config;return "string"==typeof t?t.split(",").map(t=>Number.parseInt(t,10)):"function"==typeof t?e=>t(e,this._element):t}_resolvePossibleFunction(t){return "function"==typeof t?t.call(this._element):t}_getPopperConfig(t){const e={placement:t,modifiers:[{name:"flip",options:{fallbackPlacements:this._config.fallbackPlacements}},{name:"offset",options:{offset:this._getOffset()}},{name:"preventOverflow",options:{boundary:this._config.boundary}},{name:"arrow",options:{element:`.${this.constructor.NAME}-arrow`}},{name:"onChange",enabled:!0,phase:"afterWrite",fn:t=>this._handlePopperPlacementChange(t)}],onFirstUpdate:t=>{t.options.placement!==t.placement&&this._handlePopperPlacementChange(t);}};return {...e,..."function"==typeof this._config.popperConfig?this._config.popperConfig(e):this._config.popperConfig}}_addAttachmentClass(t){this.getTipElement().classList.add(`${this._getBasicClassPrefix()}-${this.updateAttachment(t)}`);}_getAttachment(t){return $e[t.toUpperCase()]}_setListeners(){this._config.trigger.split(" ").forEach(t=>{if("click"===t)P.on(this._element,this.constructor.Event.CLICK,this._config.selector,t=>this.toggle(t));else if("manual"!==t){const e="hover"===t?this.constructor.Event.MOUSEENTER:this.constructor.Event.FOCUSIN,i="hover"===t?this.constructor.Event.MOUSELEAVE:this.constructor.Event.FOCUSOUT;P.on(this._element,e,this._config.selector,t=>this._enter(t)),P.on(this._element,i,this._config.selector,t=>this._leave(t));}}),this._hideModalHandler=()=>{this._element&&this.hide();},P.on(this._element.closest(".modal"),"hide.bs.modal",this._hideModalHandler),this._config.selector?this._config={...this._config,trigger:"manual",selector:""}:this._fixTitle();}_fixTitle(){const t=this._element.getAttribute("title"),e=typeof this._element.getAttribute("data-bs-original-title");(t||"string"!==e)&&(this._element.setAttribute("data-bs-original-title",t||""),!t||this._element.getAttribute("aria-label")||this._element.textContent||this._element.setAttribute("aria-label",t),this._element.setAttribute("title",""));}_enter(t,e){e=this._initializeOnDelegatedTarget(t,e),t&&(e._activeTrigger["focusin"===t.type?"focus":"hover"]=!0),e.getTipElement().classList.contains("show")||"show"===e._hoverState?e._hoverState="show":(clearTimeout(e._timeout),e._hoverState="show",e._config.delay&&e._config.delay.show?e._timeout=setTimeout(()=>{"show"===e._hoverState&&e.show();},e._config.delay.show):e.show());}_leave(t,e){e=this._initializeOnDelegatedTarget(t,e),t&&(e._activeTrigger["focusout"===t.type?"focus":"hover"]=e._element.contains(t.relatedTarget)),e._isWithActiveTrigger()||(clearTimeout(e._timeout),e._hoverState="out",e._config.delay&&e._config.delay.hide?e._timeout=setTimeout(()=>{"out"===e._hoverState&&e.hide();},e._config.delay.hide):e.hide());}_isWithActiveTrigger(){for(const t in this._activeTrigger)if(this._activeTrigger[t])return !0;return !1}_getConfig(t){const e=F.getDataAttributes(this._element);return Object.keys(e).forEach(t=>{Fe.has(t)&&delete e[t];}),(t={...this.constructor.Default,...e,..."object"==typeof t&&t?t:{}}).container=!1===t.container?document.body:o(t.container),"number"==typeof t.delay&&(t.delay={show:t.delay,hide:t.delay}),"number"==typeof t.title&&(t.title=t.title.toString()),"number"==typeof t.content&&(t.content=t.content.toString()),r("tooltip",t,this.constructor.DefaultType),t.sanitize&&(t.template=qe(t.template,t.allowList,t.sanitizeFn)),t}_getDelegateConfig(){const t={};for(const e in this._config)this.constructor.Default[e]!==this._config[e]&&(t[e]=this._config[e]);return t}_cleanTipClass(){const t=this.getTipElement(),e=new RegExp(`(^|\\s)${this._getBasicClassPrefix()}\\S+`,"g"),i=t.getAttribute("class").match(e);null!==i&&i.length>0&&i.map(t=>t.trim()).forEach(e=>t.classList.remove(e));}_getBasicClassPrefix(){return "bs-tooltip"}_handlePopperPlacementChange(t){const{state:e}=t;e&&(this.tip=e.elements.popper,this._cleanTipClass(),this._addAttachmentClass(this._getAttachment(e.placement)));}static jQueryInterface(t){return this.each((function(){const e=Xe.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===e[t])throw new TypeError(`No method named "${t}"`);e[t]();}}))}}m(Xe);const Ye={...Xe.Default,placement:"right",offset:[0,8],trigger:"click",content:"",template:'<div class="popover" role="tooltip"><div class="popover-arrow"></div><h3 class="popover-header"></h3><div class="popover-body"></div></div>'},Qe={...Xe.DefaultType,content:"(string|element|function)"},Ge={HIDE:"hide.bs.popover",HIDDEN:"hidden.bs.popover",SHOW:"show.bs.popover",SHOWN:"shown.bs.popover",INSERTED:"inserted.bs.popover",CLICK:"click.bs.popover",FOCUSIN:"focusin.bs.popover",FOCUSOUT:"focusout.bs.popover",MOUSEENTER:"mouseenter.bs.popover",MOUSELEAVE:"mouseleave.bs.popover"};class Ze extends Xe{static get Default(){return Ye}static get NAME(){return "popover"}static get Event(){return Ge}static get DefaultType(){return Qe}isWithContent(){return this.getTitle()||this._getContent()}setContent(t){this._sanitizeAndSetContent(t,this.getTitle(),".popover-header"),this._sanitizeAndSetContent(t,this._getContent(),".popover-body");}_getContent(){return this._resolvePossibleFunction(this._config.content)}_getBasicClassPrefix(){return "bs-popover"}static jQueryInterface(t){return this.each((function(){const e=Ze.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===e[t])throw new TypeError(`No method named "${t}"`);e[t]();}}))}}m(Ze);const Je={offset:10,method:"auto",target:""},ti={offset:"number",method:"string",target:"(string|element)"},ei=".nav-link, .list-group-item, .dropdown-item";class ii extends H{constructor(t,e){super(t),this._scrollElement="BODY"===this._element.tagName?window:this._element,this._config=this._getConfig(e),this._offsets=[],this._targets=[],this._activeTarget=null,this._scrollHeight=0,P.on(this._scrollElement,"scroll.bs.scrollspy",()=>this._process()),this.refresh(),this._process();}static get Default(){return Je}static get NAME(){return "scrollspy"}refresh(){const t=this._scrollElement===this._scrollElement.window?"offset":"position",i="auto"===this._config.method?t:this._config.method,n="position"===i?this._getScrollTop():0;this._offsets=[],this._targets=[],this._scrollHeight=this._getScrollHeight(),U.find(ei,this._config.target).map(t=>{const s=e(t),o=s?U.findOne(s):null;if(o){const t=o.getBoundingClientRect();if(t.width||t.height)return [F[i](o).top+n,s]}return null}).filter(t=>t).sort((t,e)=>t[0]-e[0]).forEach(t=>{this._offsets.push(t[0]),this._targets.push(t[1]);});}dispose(){P.off(this._scrollElement,".bs.scrollspy"),super.dispose();}_getConfig(t){return (t={...Je,...F.getDataAttributes(this._element),..."object"==typeof t&&t?t:{}}).target=o(t.target)||document.documentElement,r("scrollspy",t,ti),t}_getScrollTop(){return this._scrollElement===window?this._scrollElement.pageYOffset:this._scrollElement.scrollTop}_getScrollHeight(){return this._scrollElement.scrollHeight||Math.max(document.body.scrollHeight,document.documentElement.scrollHeight)}_getOffsetHeight(){return this._scrollElement===window?window.innerHeight:this._scrollElement.getBoundingClientRect().height}_process(){const t=this._getScrollTop()+this._config.offset,e=this._getScrollHeight(),i=this._config.offset+e-this._getOffsetHeight();if(this._scrollHeight!==e&&this.refresh(),t>=i){const t=this._targets[this._targets.length-1];this._activeTarget!==t&&this._activate(t);}else {if(this._activeTarget&&t<this._offsets[0]&&this._offsets[0]>0)return this._activeTarget=null,void this._clear();for(let e=this._offsets.length;e--;)this._activeTarget!==this._targets[e]&&t>=this._offsets[e]&&(void 0===this._offsets[e+1]||t<this._offsets[e+1])&&this._activate(this._targets[e]);}}_activate(t){this._activeTarget=t,this._clear();const e=ei.split(",").map(e=>`${e}[data-bs-target="${t}"],${e}[href="${t}"]`),i=U.findOne(e.join(","),this._config.target);i.classList.add("active"),i.classList.contains("dropdown-item")?U.findOne(".dropdown-toggle",i.closest(".dropdown")).classList.add("active"):U.parents(i,".nav, .list-group").forEach(t=>{U.prev(t,".nav-link, .list-group-item").forEach(t=>t.classList.add("active")),U.prev(t,".nav-item").forEach(t=>{U.children(t,".nav-link").forEach(t=>t.classList.add("active"));});}),P.trigger(this._scrollElement,"activate.bs.scrollspy",{relatedTarget:t});}_clear(){U.find(ei,this._config.target).filter(t=>t.classList.contains("active")).forEach(t=>t.classList.remove("active"));}static jQueryInterface(t){return this.each((function(){const e=ii.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===e[t])throw new TypeError(`No method named "${t}"`);e[t]();}}))}}P.on(window,"load.bs.scrollspy.data-api",()=>{U.find('[data-bs-spy="scroll"]').forEach(t=>new ii(t));}),m(ii);class ni extends H{static get NAME(){return "tab"}show(){if(this._element.parentNode&&this._element.parentNode.nodeType===Node.ELEMENT_NODE&&this._element.classList.contains("active"))return;let t;const e=i(this._element),n=this._element.closest(".nav, .list-group");if(n){const e="UL"===n.nodeName||"OL"===n.nodeName?":scope > li > .active":".active";t=U.find(e,n),t=t[t.length-1];}const s=t?P.trigger(t,"hide.bs.tab",{relatedTarget:this._element}):null;if(P.trigger(this._element,"show.bs.tab",{relatedTarget:t}).defaultPrevented||null!==s&&s.defaultPrevented)return;this._activate(this._element,n);const o=()=>{P.trigger(t,"hidden.bs.tab",{relatedTarget:this._element}),P.trigger(this._element,"shown.bs.tab",{relatedTarget:t});};e?this._activate(e,e.parentNode,o):o();}_activate(t,e,i){const n=(!e||"UL"!==e.nodeName&&"OL"!==e.nodeName?U.children(e,".active"):U.find(":scope > li > .active",e))[0],s=i&&n&&n.classList.contains("fade"),o=()=>this._transitionComplete(t,n,i);n&&s?(n.classList.remove("show"),this._queueCallback(o,t,!0)):o();}_transitionComplete(t,e,i){if(e){e.classList.remove("active");const t=U.findOne(":scope > .dropdown-menu .active",e.parentNode);t&&t.classList.remove("active"),"tab"===e.getAttribute("role")&&e.setAttribute("aria-selected",!1);}t.classList.add("active"),"tab"===t.getAttribute("role")&&t.setAttribute("aria-selected",!0),d(t),t.classList.contains("fade")&&t.classList.add("show");let n=t.parentNode;if(n&&"LI"===n.nodeName&&(n=n.parentNode),n&&n.classList.contains("dropdown-menu")){const e=t.closest(".dropdown");e&&U.find(".dropdown-toggle",e).forEach(t=>t.classList.add("active")),t.setAttribute("aria-expanded",!0);}i&&i();}static jQueryInterface(t){return this.each((function(){const e=ni.getOrCreateInstance(this);if("string"==typeof t){if(void 0===e[t])throw new TypeError(`No method named "${t}"`);e[t]();}}))}}P.on(document,"click.bs.tab.data-api",'[data-bs-toggle="tab"], [data-bs-toggle="pill"], [data-bs-toggle="list"]',(function(t){["A","AREA"].includes(this.tagName)&&t.preventDefault(),l(this)||ni.getOrCreateInstance(this).show();})),m(ni);const si={animation:"boolean",autohide:"boolean",delay:"number"},oi={animation:!0,autohide:!0,delay:5e3};class ri extends H{constructor(t,e){super(t),this._config=this._getConfig(e),this._timeout=null,this._hasMouseInteraction=!1,this._hasKeyboardInteraction=!1,this._setListeners();}static get DefaultType(){return si}static get Default(){return oi}static get NAME(){return "toast"}show(){P.trigger(this._element,"show.bs.toast").defaultPrevented||(this._clearTimeout(),this._config.animation&&this._element.classList.add("fade"),this._element.classList.remove("hide"),d(this._element),this._element.classList.add("show"),this._element.classList.add("showing"),this._queueCallback(()=>{this._element.classList.remove("showing"),P.trigger(this._element,"shown.bs.toast"),this._maybeScheduleHide();},this._element,this._config.animation));}hide(){this._element.classList.contains("show")&&(P.trigger(this._element,"hide.bs.toast").defaultPrevented||(this._element.classList.add("showing"),this._queueCallback(()=>{this._element.classList.add("hide"),this._element.classList.remove("showing"),this._element.classList.remove("show"),P.trigger(this._element,"hidden.bs.toast");},this._element,this._config.animation)));}dispose(){this._clearTimeout(),this._element.classList.contains("show")&&this._element.classList.remove("show"),super.dispose();}_getConfig(t){return t={...oi,...F.getDataAttributes(this._element),..."object"==typeof t&&t?t:{}},r("toast",t,this.constructor.DefaultType),t}_maybeScheduleHide(){this._config.autohide&&(this._hasMouseInteraction||this._hasKeyboardInteraction||(this._timeout=setTimeout(()=>{this.hide();},this._config.delay)));}_onInteraction(t,e){switch(t.type){case"mouseover":case"mouseout":this._hasMouseInteraction=e;break;case"focusin":case"focusout":this._hasKeyboardInteraction=e;}if(e)return void this._clearTimeout();const i=t.relatedTarget;this._element===i||this._element.contains(i)||this._maybeScheduleHide();}_setListeners(){P.on(this._element,"mouseover.bs.toast",t=>this._onInteraction(t,!0)),P.on(this._element,"mouseout.bs.toast",t=>this._onInteraction(t,!1)),P.on(this._element,"focusin.bs.toast",t=>this._onInteraction(t,!0)),P.on(this._element,"focusout.bs.toast",t=>this._onInteraction(t,!1));}_clearTimeout(){clearTimeout(this._timeout),this._timeout=null;}static jQueryInterface(t){return this.each((function(){const e=ri.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===e[t])throw new TypeError(`No method named "${t}"`);e[t](this);}}))}}return B(ri),m(ri),{Alert:R,Button:W,Carousel:Z,Collapse:et,Dropdown:Te,Modal:Pe,Offcanvas:He,Popover:Ze,ScrollSpy:ii,Tab:ni,Toast:ri,Tooltip:Xe}}));

!function(e,t){"object"==typeof exports&&"undefined"!=typeof module?module.exports=t():"function"==typeof define&&define.amd?define(t):(e||self).autosize=t();}(undefined,function(){var e=new Map;function t(t){var o=e.get(t);o&&o.destroy();}function o(t){var o=e.get(t);o&&o.update();}var r=null;return "undefined"==typeof window?((r=function(e){return e}).destroy=function(e){return e},r.update=function(e){return e}):((r=function(t,o){return t&&Array.prototype.forEach.call(t.length?t:[t],function(t){return function(t){if(t&&t.nodeName&&"TEXTAREA"===t.nodeName&&!e.has(t)){var o,r=null,n=window.getComputedStyle(t),i=(o=t.value,function(){s({testForHeightReduction:""===o||!t.value.startsWith(o),restoreTextAlign:null}),o=t.value;}),l=function(o){t.removeEventListener("autosize:destroy",l),t.removeEventListener("autosize:update",a),t.removeEventListener("input",i),window.removeEventListener("resize",a),Object.keys(o).forEach(function(e){return t.style[e]=o[e]}),e.delete(t);}.bind(t,{height:t.style.height,resize:t.style.resize,textAlign:t.style.textAlign,overflowY:t.style.overflowY,overflowX:t.style.overflowX,wordWrap:t.style.wordWrap});t.addEventListener("autosize:destroy",l),t.addEventListener("autosize:update",a),t.addEventListener("input",i),window.addEventListener("resize",a),t.style.overflowX="hidden",t.style.wordWrap="break-word",e.set(t,{destroy:l,update:a}),a();}function s(e){var o,i,l=e.restoreTextAlign,a=void 0===l?null:l,d=e.testForHeightReduction,u=void 0===d||d,f=n.overflowY;if(0!==t.scrollHeight&&("vertical"===n.resize?t.style.resize="none":"both"===n.resize&&(t.style.resize="horizontal"),u&&(o=function(e){for(var t=[];e&&e.parentNode&&e.parentNode instanceof Element;)e.parentNode.scrollTop&&t.push([e.parentNode,e.parentNode.scrollTop]),e=e.parentNode;return function(){return t.forEach(function(e){var t=e[0],o=e[1];t.style.scrollBehavior="auto",t.scrollTop=o,t.style.scrollBehavior=null;})}}(t),t.style.height=""),i="content-box"===n.boxSizing?t.scrollHeight-(parseFloat(n.paddingTop)+parseFloat(n.paddingBottom)):t.scrollHeight+parseFloat(n.borderTopWidth)+parseFloat(n.borderBottomWidth),"none"!==n.maxHeight&&i>parseFloat(n.maxHeight)?("hidden"===n.overflowY&&(t.style.overflow="scroll"),i=parseFloat(n.maxHeight)):"hidden"!==n.overflowY&&(t.style.overflow="hidden"),t.style.height=i+"px",a&&(t.style.textAlign=a),o&&o(),r!==i&&(t.dispatchEvent(new Event("autosize:resized",{bubbles:!0})),r=i),f!==n.overflow&&!a)){var c=n.textAlign;"hidden"===n.overflow&&(t.style.textAlign="start"===c?"end":"start"),s({restoreTextAlign:c,testForHeightReduction:!0});}}function a(){s({testForHeightReduction:!0,restoreTextAlign:null});}}(t)}),t}).destroy=function(e){return e&&Array.prototype.forEach.call(e.length?e:[e],t),e},r.update=function(e){return e&&Array.prototype.forEach.call(e.length?e:[e],o),e}),r});

/*
  Highlight.js 10.7.2 (00233d63)
  License: BSD-3-Clause
  Copyright (c) 2006-2021, Ivan Sagalaev
*/
var hljs = function () {
 function e(t) {
    return t instanceof Map ? t.clear = t.delete = t.set = () => {
      throw Error("map is read-only")
    } : t instanceof Set && (t.add = t.clear = t.delete = () => {
      throw Error("set is read-only")
    }), Object.freeze(t), Object.getOwnPropertyNames(t).forEach((n => {
      var i = t[n]
      ; "object" != typeof i || Object.isFrozen(i) || e(i);
    })), t
  } var t = e, n = e; t.default = n
    ; class i {
      constructor(e) {
        void 0 === e.data && (e.data = {}), this.data = e.data, this.isMatchIgnored = !1;
      }
    ignoreMatch() { this.isMatchIgnored = !0; }
  } function s(e) {
    return e.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;").replace(/'/g, "&#x27;")
  } function a(e, ...t) {
    const n = Object.create(null); for (const t in e) n[t] = e[t]
      ; return t.forEach((e => { for (const t in e) n[t] = e[t]; })), n
  } const r = e => !!e.kind
    ; class l {
      constructor(e, t) {
        this.buffer = "", this.classPrefix = t.classPrefix, e.walk(this);
      } addText(e) {
        this.buffer += s(e);
      } openNode(e) {
        if (!r(e)) return; let t = e.kind
          ; e.sublanguage || (t = `${this.classPrefix}${t}`), this.span(t);
      } closeNode(e) {
        r(e) && (this.buffer += "</span>");
      } value() { return this.buffer } span(e) {
        this.buffer += `<span class="${e}">`;
      }
  } class o {
    constructor() {
      this.rootNode = {
        children: []
      }, this.stack = [this.rootNode];
    } get top() {
      return this.stack[this.stack.length - 1]
    } get root() { return this.rootNode } add(e) {
      this.top.children.push(e);
    } openNode(e) {
      const t = { kind: e, children: [] }
      ; this.add(t), this.stack.push(t);
    } closeNode() {
      if (this.stack.length > 1) return this.stack.pop()
    } closeAllNodes() {
      for (; this.closeNode(););
    } toJSON() { return JSON.stringify(this.rootNode, null, 4) }
    walk(e) { return this.constructor._walk(e, this.rootNode) } static _walk(e, t) {
      return "string" == typeof t ? e.addText(t) : t.children && (e.openNode(t),
        t.children.forEach((t => this._walk(e, t))), e.closeNode(t)), e
    } static _collapse(e) {
      "string" != typeof e && e.children && (e.children.every((e => "string" == typeof e)) ? e.children = [e.children.join("")] : e.children.forEach((e => {
        o._collapse(e);
      })));
    }
  } class c extends o {
    constructor(e) { super(), this.options = e; }
    addKeyword(e, t) { "" !== e && (this.openNode(t), this.addText(e), this.closeNode()); }
    addText(e) { "" !== e && this.add(e); } addSublanguage(e, t) {
      const n = e.root
      ; n.kind = t, n.sublanguage = !0, this.add(n);
    } toHTML() {
      return new l(this, this.options).value()
    } finalize() { return !0 }
  } function g(e) {
    return e ? "string" == typeof e ? e : e.source : null
  }
  const u = /\[(?:[^\\\]]|\\.)*\]|\(\??|\\([1-9][0-9]*)|\\./, h = "[a-zA-Z]\\w*", d = "[a-zA-Z_]\\w*", f = "\\b\\d+(\\.\\d+)?", p = "(-?)(\\b0[xX][a-fA-F0-9]+|(\\b\\d+(\\.\\d*)?|\\.\\d+)([eE][-+]?\\d+)?)", m = "\\b(0b[01]+)", b = {
    begin: "\\\\[\\s\\S]", relevance: 0
  }, E = {
    className: "string", begin: "'", end: "'",
    illegal: "\\n", contains: [b]
  }, x = {
    className: "string", begin: '"', end: '"',
    illegal: "\\n", contains: [b]
  }, v = {
    begin: /\b(a|an|the|are|I'm|isn't|don't|doesn't|won't|but|just|should|pretty|simply|enough|gonna|going|wtf|so|such|will|you|your|they|like|more)\b/
  }, w = (e, t, n = {}) => {
    const i = a({ className: "comment", begin: e, end: t, contains: [] }, n)
    ; return i.contains.push(v), i.contains.push({
      className: "doctag",
      begin: "(?:TODO|FIXME|NOTE|BUG|OPTIMIZE|HACK|XXX):", relevance: 0
    }), i
  }, y = w("//", "$"), N = w("/\\*", "\\*/"), R = w("#", "$"); var _ = Object.freeze({
    __proto__: null, MATCH_NOTHING_RE: /\b\B/, IDENT_RE: h, UNDERSCORE_IDENT_RE: d,
    NUMBER_RE: f, C_NUMBER_RE: p, BINARY_NUMBER_RE: m,
    RE_STARTERS_RE: "!|!=|!==|%|%=|&|&&|&=|\\*|\\*=|\\+|\\+=|,|-|-=|/=|/|:|;|<<|<<=|<=|<|===|==|=|>>>=|>>=|>=|>>>|>>|>|\\?|\\[|\\{|\\(|\\^|\\^=|\\||\\|=|\\|\\||~",
    SHEBANG: (e = {}) => {
      const t = /^#![ ]*\//
      ; return e.binary && (e.begin = ((...e) => e.map((e => g(e))).join(""))(t, /.*\b/, e.binary, /\b.*/)),
        a({
          className: "meta", begin: t, end: /$/, relevance: 0, "on:begin": (e, t) => {
            0 !== e.index && t.ignoreMatch();
          }
        }, e)
    }, BACKSLASH_ESCAPE: b, APOS_STRING_MODE: E,
    QUOTE_STRING_MODE: x, PHRASAL_WORDS_MODE: v, COMMENT: w, C_LINE_COMMENT_MODE: y,
    C_BLOCK_COMMENT_MODE: N, HASH_COMMENT_MODE: R, NUMBER_MODE: {
      className: "number",
      begin: f, relevance: 0
    }, C_NUMBER_MODE: { className: "number", begin: p, relevance: 0 },
    BINARY_NUMBER_MODE: { className: "number", begin: m, relevance: 0 }, CSS_NUMBER_MODE: {
      className: "number",
      begin: f + "(%|em|ex|ch|rem|vw|vh|vmin|vmax|cm|mm|in|pt|pc|px|deg|grad|rad|turn|s|ms|Hz|kHz|dpi|dpcm|dppx)?",
      relevance: 0
    }, REGEXP_MODE: {
      begin: /(?=\/[^/\n]*\/)/, contains: [{
        className: "regexp",
        begin: /\//, end: /\/[gimuy]*/, illegal: /\n/, contains: [b, {
          begin: /\[/, end: /\]/,
          relevance: 0, contains: [b]
        }]
      }]
    }, TITLE_MODE: {
      className: "title", begin: h, relevance: 0
    }, UNDERSCORE_TITLE_MODE: { className: "title", begin: d, relevance: 0 }, METHOD_GUARD: {
      begin: "\\.\\s*[a-zA-Z_]\\w*", relevance: 0
    }, END_SAME_AS_BEGIN: e => Object.assign(e, {
      "on:begin": (e, t) => { t.data._beginMatch = e[1]; }, "on:end": (e, t) => {
        t.data._beginMatch !== e[1] && t.ignoreMatch();
      }
    })
  }); function k(e, t) {
    "." === e.input[e.index - 1] && t.ignoreMatch();
  } function M(e, t) {
    t && e.beginKeywords && (e.begin = "\\b(" + e.beginKeywords.split(" ").join("|") + ")(?!\\.)(?=\\b|\\s)",
      e.__beforeBegin = k, e.keywords = e.keywords || e.beginKeywords, delete e.beginKeywords,
      void 0 === e.relevance && (e.relevance = 0));
  } function O(e, t) {
    Array.isArray(e.illegal) && (e.illegal = ((...e) => "(" + e.map((e => g(e))).join("|") + ")")(...e.illegal));
  } function A(e, t) {
    if (e.match) {
      if (e.begin || e.end) throw Error("begin & end are not supported with match")
        ; e.begin = e.match, delete e.match;
    }
  } function L(e, t) {
    void 0 === e.relevance && (e.relevance = 1);
  }
  const I = ["of", "and", "for", "in", "not", "or", "if", "then", "parent", "list", "value"]
    ; function j(e, t, n = "keyword") {
      const i = {}
      ; return "string" == typeof e ? s(n, e.split(" ")) : Array.isArray(e) ? s(n, e) : Object.keys(e).forEach((n => {
        Object.assign(i, j(e[n], t, n));
      })), i; function s(e, n) {
        t && (n = n.map((e => e.toLowerCase()))), n.forEach((t => {
          const n = t.split("|")
          ; i[n[0]] = [e, B(n[0], n[1])];
        }));
      }
    } function B(e, t) {
      return t ? Number(t) : (e => I.includes(e.toLowerCase()))(e) ? 0 : 1
    }
  function T(e, { plugins: t }) {
    function n(t, n) {
      return RegExp(g(t), "m" + (e.case_insensitive ? "i" : "") + (n ? "g" : ""))
    } class i {
      constructor() {
        this.matchIndexes = {}, this.regexes = [], this.matchAt = 1, this.position = 0;
      }
      addRule(e, t) {
        t.position = this.position++, this.matchIndexes[this.matchAt] = t, this.regexes.push([t, e]),
          this.matchAt += (e => RegExp(e.toString() + "|").exec("").length - 1)(e) + 1;
      } compile() {
        0 === this.regexes.length && (this.exec = () => null)
          ; const e = this.regexes.map((e => e[1])); this.matcherRe = n(((e, t = "|") => {
            let n = 0
            ; return e.map((e => {
              n += 1; const t = n; let i = g(e), s = ""; for (; i.length > 0;) {
                const e = u.exec(i); if (!e) { s += i; break }
                s += i.substring(0, e.index), i = i.substring(e.index + e[0].length),
                  "\\" === e[0][0] && e[1] ? s += "\\" + (Number(e[1]) + t) : (s += e[0], "(" === e[0] && n++);
              } return s
            })).map((e => `(${e})`)).join(t)
          })(e), !0), this.lastIndex = 0;
      } exec(e) {
        this.matcherRe.lastIndex = this.lastIndex; const t = this.matcherRe.exec(e)
          ; if (!t) return null
            ; const n = t.findIndex(((e, t) => t > 0 && void 0 !== e)), i = this.matchIndexes[n]
          ; return t.splice(0, n), Object.assign(t, i)
      }
    } class s {
      constructor() {
        this.rules = [], this.multiRegexes = [],
          this.count = 0, this.lastIndex = 0, this.regexIndex = 0;
      } getMatcher(e) {
        if (this.multiRegexes[e]) return this.multiRegexes[e]; const t = new i
          ; return this.rules.slice(e).forEach((([e, n]) => t.addRule(e, n))),
            t.compile(), this.multiRegexes[e] = t, t
      } resumingScanAtSamePosition() {
        return 0 !== this.regexIndex
      } considerAll() { this.regexIndex = 0; } addRule(e, t) {
        this.rules.push([e, t]), "begin" === t.type && this.count++;
      } exec(e) {
        const t = this.getMatcher(this.regexIndex); t.lastIndex = this.lastIndex
          ; let n = t.exec(e)
          ; if (this.resumingScanAtSamePosition()) if (n && n.index === this.lastIndex); else {
            const t = this.getMatcher(0); t.lastIndex = this.lastIndex + 1, n = t.exec(e);
          }
        return n && (this.regexIndex += n.position + 1,
          this.regexIndex === this.count && this.considerAll()), n
      }
    }
    if (e.compilerExtensions || (e.compilerExtensions = []),
      e.contains && e.contains.includes("self")) throw Error("ERR: contains `self` is not supported at the top-level of a language.  See documentation.")
      ; return e.classNameAliases = a(e.classNameAliases || {}), function t(i, r) {
        const l = i
        ; if (i.isCompiled) return l
          ;[A].forEach((e => e(i, r))), e.compilerExtensions.forEach((e => e(i, r))),
            i.__beforeBegin = null, [M, O, L].forEach((e => e(i, r))), i.isCompiled = !0; let o = null
          ; if ("object" == typeof i.keywords && (o = i.keywords.$pattern,
            delete i.keywords.$pattern),
            i.keywords && (i.keywords = j(i.keywords, e.case_insensitive)),
            i.lexemes && o) throw Error("ERR: Prefer `keywords.$pattern` to `mode.lexemes`, BOTH are not allowed. (see mode reference) ")
          ; return o = o || i.lexemes || /\w+/,
            l.keywordPatternRe = n(o, !0), r && (i.begin || (i.begin = /\B|\b/),
              l.beginRe = n(i.begin), i.endSameAsBegin && (i.end = i.begin),
              i.end || i.endsWithParent || (i.end = /\B|\b/),
              i.end && (l.endRe = n(i.end)), l.terminatorEnd = g(i.end) || "",
              i.endsWithParent && r.terminatorEnd && (l.terminatorEnd += (i.end ? "|" : "") + r.terminatorEnd)),
            i.illegal && (l.illegalRe = n(i.illegal)),
            i.contains || (i.contains = []), i.contains = [].concat(...i.contains.map((e => (e => (e.variants && !e.cachedVariants && (e.cachedVariants = e.variants.map((t => a(e, {
              variants: null
            }, t)))), e.cachedVariants ? e.cachedVariants : S(e) ? a(e, {
              starts: e.starts ? a(e.starts) : null
            }) : Object.isFrozen(e) ? a(e) : e))("self" === e ? i : e)))), i.contains.forEach((e => {
              t(e, l);
            })), i.starts && t(i.starts, r), l.matcher = (e => {
              const t = new s
              ; return e.contains.forEach((e => t.addRule(e.begin, {
                rule: e, type: "begin"
              }))), e.terminatorEnd && t.addRule(e.terminatorEnd, {
                type: "end"
              }), e.illegal && t.addRule(e.illegal, { type: "illegal" }), t
            })(l), l
      }(e)
  } function S(e) {
    return !!e && (e.endsWithParent || S(e.starts))
  } function P(e) {
    const t = {
      props: ["language", "code", "autodetect"], data: () => ({
        detectedLanguage: "",
        unknownLanguage: !1
      }), computed: {
        className() {
          return this.unknownLanguage ? "" : "hljs " + this.detectedLanguage
        }, highlighted() {
          if (!this.autoDetect && !e.getLanguage(this.language)) return console.warn(`The language "${this.language}" you specified could not be found.`),
            this.unknownLanguage = !0, s(this.code); let t = {}
            ; return this.autoDetect ? (t = e.highlightAuto(this.code),
              this.detectedLanguage = t.language) : (t = e.highlight(this.language, this.code, this.ignoreIllegals),
                this.detectedLanguage = this.language), t.value
        }, autoDetect() {
          return !(this.language && (e = this.autodetect, !e && "" !== e)); var e;
        },
        ignoreIllegals: () => !0
      }, render(e) {
        return e("pre", {}, [e("code", {
          class: this.className, domProps: { innerHTML: this.highlighted }
        })])
      }
    }; return {
      Component: t, VuePlugin: { install(e) { e.component("highlightjs", t); } }
    }
  } const D = {
    "after:highlightElement": ({ el: e, result: t, text: n }) => {
      const i = H(e)
      ; if (!i.length) return; const a = document.createElement("div")
        ; a.innerHTML = t.value, t.value = ((e, t, n) => {
          let i = 0, a = ""; const r = []; function l() {
            return e.length && t.length ? e[0].offset !== t[0].offset ? e[0].offset < t[0].offset ? e : t : "start" === t[0].event ? e : t : e.length ? e : t
          } function o(e) {
            a += "<" + C(e) + [].map.call(e.attributes, (function (e) {
              return " " + e.nodeName + '="' + s(e.value) + '"'
            })).join("") + ">";
          } function c(e) {
            a += "</" + C(e) + ">";
          } function g(e) { ("start" === e.event ? o : c)(e.node); }
          for (; e.length || t.length;) {
            let t = l()
            ; if (a += s(n.substring(i, t[0].offset)), i = t[0].offset, t === e) {
              r.reverse().forEach(c)
              ; do { g(t.splice(0, 1)[0]), t = l(); } while (t === e && t.length && t[0].offset === i)
                ; r.reverse().forEach(o);
            } else "start" === t[0].event ? r.push(t[0].node) : r.pop(), g(t.splice(0, 1)[0]);
          }
          return a + s(n.substr(i))
        })(i, H(a), n);
    }
  }; function C(e) {
    return e.nodeName.toLowerCase()
  } function H(e) {
    const t = []; return function e(n, i) {
      for (let s = n.firstChild; s; s = s.nextSibling)3 === s.nodeType ? i += s.nodeValue.length : 1 === s.nodeType && (t.push({
        event: "start", offset: i, node: s
      }), i = e(s, i), C(s).match(/br|hr|img|input/) || t.push({
        event: "stop", offset: i, node: s
      })); return i
    }(e, 0), t
  } const $ = {}, U = e => {
    console.error(e);
  }, z = (e, ...t) => { console.log("WARN: " + e, ...t); }, K = (e, t) => {
    $[`${e}/${t}`] || (console.log(`Deprecated as of ${e}. ${t}`), $[`${e}/${t}`] = !0);
  }, G = s, V = a, W = Symbol("nomatch"); return (e => {
    const n = Object.create(null), s = Object.create(null), a = []; let r = !0
      ; const l = /(^(<[^>]+>|\t|)+|\n)/gm, o = "Could not find the language '{}', did you forget to load/include a language module?", g = {
        disableAutodetect: !0, name: "Plain text", contains: []
      }; let u = {
        noHighlightRe: /^(no-?highlight)$/i,
        languageDetectRe: /\blang(?:uage)?-([\w-]+)\b/i, classPrefix: "hljs-",
        tabReplace: null, useBR: !1, languages: null, __emitter: c
      }; function h(e) {
        return u.noHighlightRe.test(e)
      } function d(e, t, n, i) {
        let s = "", a = ""
        ; "object" == typeof t ? (s = e,
          n = t.ignoreIllegals, a = t.language, i = void 0) : (K("10.7.0", "highlight(lang, code, ...args) has been deprecated."),
            K("10.7.0", "Please use highlight(code, options) instead.\nhttps://github.com/highlightjs/highlight.js/issues/2277"),
            a = e, s = t); const r = { code: s, language: a }; M("before:highlight", r)
          ; const l = r.result ? r.result : f(r.language, r.code, n, i)
          ; return l.code = r.code, M("after:highlight", l), l
      } function f(e, t, s, l) {
        function c(e, t) {
          const n = v.case_insensitive ? t[0].toLowerCase() : t[0]
          ; return Object.prototype.hasOwnProperty.call(e.keywords, n) && e.keywords[n]
        }
        function g() {
          null != R.subLanguage ? (() => {
            if ("" === M) return; let e = null
              ; if ("string" == typeof R.subLanguage) {
                if (!n[R.subLanguage]) return void k.addText(M)
                  ; e = f(R.subLanguage, M, !0, _[R.subLanguage]), _[R.subLanguage] = e.top;
              } else e = p(M, R.subLanguage.length ? R.subLanguage : null)
              ; R.relevance > 0 && (O += e.relevance), k.addSublanguage(e.emitter, e.language);
          })() : (() => {
            if (!R.keywords) return void k.addText(M); let e = 0
              ; R.keywordPatternRe.lastIndex = 0; let t = R.keywordPatternRe.exec(M), n = ""; for (; t;) {
                n += M.substring(e, t.index); const i = c(R, t); if (i) {
                  const [e, s] = i
                  ; if (k.addText(n), n = "", O += s, e.startsWith("_")) n += t[0]; else {
                    const n = v.classNameAliases[e] || e; k.addKeyword(t[0], n);
                  }
                } else n += t[0]
                  ; e = R.keywordPatternRe.lastIndex, t = R.keywordPatternRe.exec(M);
              }
            n += M.substr(e), k.addText(n);
          })(), M = "";
        } function h(e) {
          return e.className && k.openNode(v.classNameAliases[e.className] || e.className),
            R = Object.create(e, { parent: { value: R } }), R
        } function d(e, t, n) {
          let s = ((e, t) => {
            const n = e && e.exec(t); return n && 0 === n.index
          })(e.endRe, n); if (s) {
            if (e["on:end"]) {
              const n = new i(e); e["on:end"](t, n), n.isMatchIgnored && (s = !1);
            } if (s) {
              for (; e.endsParent && e.parent;)e = e.parent; return e
            }
          }
          if (e.endsWithParent) return d(e.parent, t, n)
        } function m(e) {
          return 0 === R.matcher.regexIndex ? (M += e[0], 1) : (I = !0, 0)
        } function b(e) {
          const n = e[0], i = t.substr(e.index), s = d(R, e, i); if (!s) return W; const a = R
            ; a.skip ? M += n : (a.returnEnd || a.excludeEnd || (M += n), g(), a.excludeEnd && (M = n)); do {
              R.className && k.closeNode(), R.skip || R.subLanguage || (O += R.relevance), R = R.parent;
            } while (R !== s.parent)
            ; return s.starts && (s.endSameAsBegin && (s.starts.endRe = s.endRe),
              h(s.starts)), a.returnEnd ? 0 : n.length
        } let E = {}; function x(n, a) {
          const l = a && a[0]
          ; if (M += n, null == l) return g(), 0
            ; if ("begin" === E.type && "end" === a.type && E.index === a.index && "" === l) {
              if (M += t.slice(a.index, a.index + 1), !r) {
                const t = Error("0 width match regex")
                ; throw t.languageName = e, t.badRule = E.rule, t
              } return 1
            }
          if (E = a, "begin" === a.type) return function (e) {
            const t = e[0], n = e.rule, s = new i(n), a = [n.__beforeBegin, n["on:begin"]]
              ; for (const n of a) if (n && (n(e, s), s.isMatchIgnored)) return m(t)
                ; return n && n.endSameAsBegin && (n.endRe = RegExp(t.replace(/[-/\\^$*+?.()|[\]{}]/g, "\\$&"), "m")),
                  n.skip ? M += t : (n.excludeBegin && (M += t),
                    g(), n.returnBegin || n.excludeBegin || (M = t)), h(n), n.returnBegin ? 0 : t.length
          }(a)
            ; if ("illegal" === a.type && !s) {
              const e = Error('Illegal lexeme "' + l + '" for mode "' + (R.className || "<unnamed>") + '"')
                ; throw e.mode = R, e
            } if ("end" === a.type) { const e = b(a); if (e !== W) return e }
          if ("illegal" === a.type && "" === l) return 1
            ; if (L > 1e5 && L > 3 * a.index) throw Error("potential infinite loop, way more iterations than matches")
              ; return M += l, l.length
        } const v = N(e)
          ; if (!v) throw U(o.replace("{}", e)), Error('Unknown language: "' + e + '"')
            ; const w = T(v, { plugins: a }); let y = "", R = l || w; const _ = {}, k = new u.__emitter(u); (() => {
              const e = []; for (let t = R; t !== v; t = t.parent)t.className && e.unshift(t.className)
                ; e.forEach((e => k.openNode(e)));
            })(); let M = "", O = 0, A = 0, L = 0, I = !1; try {
              for (R.matcher.considerAll(); ;) {
                L++, I ? I = !1 : R.matcher.considerAll(), R.matcher.lastIndex = A
                  ; const e = R.matcher.exec(t); if (!e) break; const n = x(t.substring(A, e.index), e)
                  ; A = e.index + n;
              } return x(t.substr(A)), k.closeAllNodes(), k.finalize(), y = k.toHTML(), {
                relevance: Math.floor(O), value: y, language: e, illegal: !1, emitter: k, top: R
              }
            } catch (n) {
              if (n.message && n.message.includes("Illegal")) return {
                illegal: !0, illegalBy: {
                  msg: n.message, context: t.slice(A - 100, A + 100), mode: n.mode
                }, sofar: y, relevance: 0,
                value: G(t), emitter: k
              }; if (r) return {
                illegal: !1, relevance: 0, value: G(t), emitter: k,
                language: e, top: R, errorRaised: n
              }; throw n
            }
      } function p(e, t) {
        t = t || u.languages || Object.keys(n); const i = (e => {
          const t = {
            relevance: 0,
            emitter: new u.__emitter(u), value: G(e), illegal: !1, top: g
          }
          ; return t.emitter.addText(e), t
        })(e), s = t.filter(N).filter(k).map((t => f(t, e, !1)))
          ; s.unshift(i); const a = s.sort(((e, t) => {
            if (e.relevance !== t.relevance) return t.relevance - e.relevance
              ; if (e.language && t.language) {
                if (N(e.language).supersetOf === t.language) return 1
                  ; if (N(t.language).supersetOf === e.language) return -1
              } return 0
          })), [r, l] = a, o = r
          ; return o.second_best = l, o
      } const m = {
        "before:highlightElement": ({ el: e }) => {
          u.useBR && (e.innerHTML = e.innerHTML.replace(/\n/g, "").replace(/<br[ /]*>/g, "\n"));
        }, "after:highlightElement": ({ result: e }) => {
          u.useBR && (e.value = e.value.replace(/\n/g, "<br>"));
        }
      }, b = /^(<[^>]+>|\t)+/gm, E = {
        "after:highlightElement": ({ result: e }) => {
          u.tabReplace && (e.value = e.value.replace(b, (e => e.replace(/\t/g, u.tabReplace))));
        }
      }
      ; function x(e) {
        let t = null; const n = (e => {
          let t = e.className + " "
          ; t += e.parentNode ? e.parentNode.className : ""; const n = u.languageDetectRe.exec(t)
            ; if (n) {
              const t = N(n[1])
              ; return t || (z(o.replace("{}", n[1])), z("Falling back to no-highlight mode for this block.", e)),
                t ? n[1] : "no-highlight"
            } return t.split(/\s+/).find((e => h(e) || N(e)))
        })(e)
          ; if (h(n)) return; M("before:highlightElement", { el: e, language: n }), t = e
          ; const i = t.textContent, a = n ? d(i, { language: n, ignoreIllegals: !0 }) : p(i)
          ; M("after:highlightElement", {
            el: e, result: a, text: i
          }), e.innerHTML = a.value, ((e, t, n) => {
            const i = t ? s[t] : n
            ; e.classList.add("hljs"), i && e.classList.add(i);
          })(e, n, a.language), e.result = {
            language: a.language, re: a.relevance, relavance: a.relevance
          }, a.second_best && (e.second_best = {
            language: a.second_best.language,
            re: a.second_best.relevance, relavance: a.second_best.relevance
          });
      } const v = () => {
        v.called || (v.called = !0,
          K("10.6.0", "initHighlighting() is deprecated.  Use highlightAll() instead."),
          document.querySelectorAll("pre code").forEach(x));
      }; let w = !1; function y() {
        "loading" !== document.readyState ? document.querySelectorAll("pre code").forEach(x) : w = !0;
      } function N(e) { return e = (e || "").toLowerCase(), n[e] || n[s[e]] }
    function R(e, { languageName: t }) {
      "string" == typeof e && (e = [e]), e.forEach((e => {
        s[e.toLowerCase()] = t;
      }));
    } function k(e) {
      const t = N(e)
      ; return t && !t.disableAutodetect
    } function M(e, t) {
      const n = e; a.forEach((e => {
        e[n] && e[n](t);
      }));
    }
    "undefined" != typeof window && window.addEventListener && window.addEventListener("DOMContentLoaded", (() => {
      w && y();
    }), !1), Object.assign(e, {
      highlight: d, highlightAuto: p, highlightAll: y,
      fixMarkup: e => {
        return K("10.2.0", "fixMarkup will be removed entirely in v11.0"), K("10.2.0", "Please see https://github.com/highlightjs/highlight.js/issues/2534"),
          t = e,
          u.tabReplace || u.useBR ? t.replace(l, (e => "\n" === e ? u.useBR ? "<br>" : e : u.tabReplace ? e.replace(/\t/g, u.tabReplace) : e)) : t
          ; var t;
      }, highlightElement: x,
      highlightBlock: e => (K("10.7.0", "highlightBlock will be removed entirely in v12.0"),
        K("10.7.0", "Please use highlightElement now."), x(e)), configure: e => {
          e.useBR && (K("10.3.0", "'useBR' will be removed entirely in v11.0"),
            K("10.3.0", "Please see https://github.com/highlightjs/highlight.js/issues/2559")),
            u = V(u, e);
        }, initHighlighting: v, initHighlightingOnLoad: () => {
          K("10.6.0", "initHighlightingOnLoad() is deprecated.  Use highlightAll() instead."),
            w = !0;
        }, registerLanguage: (t, i) => {
          let s = null; try { s = i(e); } catch (e) {
            if (U("Language definition for '{}' could not be registered.".replace("{}", t)),
              !r) throw e; U(e), s = g;
          }
          s.name || (s.name = t), n[t] = s, s.rawDefinition = i.bind(null, e), s.aliases && R(s.aliases, {
            languageName: t
          });
        }, unregisterLanguage: e => {
          delete n[e]
          ; for (const t of Object.keys(s)) s[t] === e && delete s[t];
        },
      listLanguages: () => Object.keys(n), getLanguage: N, registerAliases: R,
      requireLanguage: e => {
        K("10.4.0", "requireLanguage will be removed entirely in v11."),
          K("10.4.0", "Please see https://github.com/highlightjs/highlight.js/pull/2844")
          ; const t = N(e); if (t) return t
            ; throw Error("The '{}' language is required, but not loaded.".replace("{}", e))
      },
      autoDetection: k, inherit: V, addPlugin: e => {
        (e => {
          e["before:highlightBlock"] && !e["before:highlightElement"] && (e["before:highlightElement"] = t => {
            e["before:highlightBlock"](Object.assign({ block: t.el }, t));
          }), e["after:highlightBlock"] && !e["after:highlightElement"] && (e["after:highlightElement"] = t => {
            e["after:highlightBlock"](Object.assign({ block: t.el }, t));
          });
        })(e), a.push(e);
      },
      vuePlugin: P(e).VuePlugin
    }), e.debugMode = () => { r = !1; }, e.safeMode = () => {
      r = !0;
    }, e.versionString = "10.7.2"; for (const e in _) "object" == typeof _[e] && t(_[e])
      ; return Object.assign(e, _), e.addPlugin(m), e.addPlugin(D), e.addPlugin(E), e
  })({})
}(); "object" == typeof exports && "undefined" != typeof module && (module.exports = hljs);
hljs.registerLanguage("apache", (() => {
 return e => {
    const n = {
      className: "number", begin: /\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}(:\d{1,5})?/
    }
    ; return {
      name: "Apache config", aliases: ["apacheconf"], case_insensitive: !0,
      contains: [e.HASH_COMMENT_MODE, {
        className: "section", begin: /<\/?/, end: />/,
        contains: [n, {
          className: "number", begin: /:\d{1,5}/
        }, e.inherit(e.QUOTE_STRING_MODE, { relevance: 0 })]
      }, {
        className: "attribute",
        begin: /\w+/, relevance: 0, keywords: {
          nomarkup: "order deny allow setenv rewriterule rewriteengine rewritecond documentroot sethandler errordocument loadmodule options header listen serverroot servername"
        }, starts: {
          end: /$/, relevance: 0, keywords: { literal: "on off all deny allow" },
          contains: [{ className: "meta", begin: /\s\[/, end: /\]$/ }, {
            className: "variable",
            begin: /[\$%]\{/, end: /\}/, contains: ["self", { className: "number", begin: /[$%]\d+/ }]
          }, n, { className: "number", begin: /\d+/ }, e.QUOTE_STRING_MODE]
        }
      }], illegal: /\S/
    }
  }
})());
hljs.registerLanguage("bash", (() => {
 function e(...e) {
    return e.map((e => {
      return (s = e) ? "string" == typeof s ? s : s.source : null; var s;
    })).join("")
  } return s => {
    const n = {}, t = {
      begin: /\$\{/, end: /\}/, contains: ["self", {
        begin: /:-/, contains: [n]
      }]
    }; Object.assign(n, {
      className: "variable", variants: [{
        begin: e(/\$[\w\d#@][\w\d_]*/, "(?![\\w\\d])(?![$])")
      }, t]
    }); const a = {
      className: "subst", begin: /\$\(/, end: /\)/, contains: [s.BACKSLASH_ESCAPE]
    }, i = {
      begin: /<<-?\s*(?=\w+)/, starts: {
        contains: [s.END_SAME_AS_BEGIN({
          begin: /(\w+)/,
          end: /(\w+)/, className: "string"
        })]
      }
    }, c = {
      className: "string", begin: /"/, end: /"/,
      contains: [s.BACKSLASH_ESCAPE, n, a]
    }; a.contains.push(c); const o = {
      begin: /\$\(\(/,
      end: /\)\)/, contains: [{ begin: /\d+#[0-9a-f]+/, className: "number" }, s.NUMBER_MODE, n]
    }, r = s.SHEBANG({
      binary: "(fish|bash|zsh|sh|csh|ksh|tcsh|dash|scsh)", relevance: 10
    }), l = {
      className: "function", begin: /\w[\w\d_]*\s*\(\s*\)\s*\{/, returnBegin: !0,
      contains: [s.inherit(s.TITLE_MODE, { begin: /\w[\w\d_]*/ })], relevance: 0
    }; return {
      name: "Bash", aliases: ["sh", "zsh"], keywords: {
        $pattern: /\b[a-z._-]+\b/,
        keyword: "if then else elif fi for while in do done case esac function",
        literal: "true false",
        built_in: "break cd continue eval exec exit export getopts hash pwd readonly return shift test times trap umask unset alias bind builtin caller command declare echo enable help let local logout mapfile printf read readarray source type typeset ulimit unalias set shopt autoload bg bindkey bye cap chdir clone comparguments compcall compctl compdescribe compfiles compgroups compquote comptags comptry compvalues dirs disable disown echotc echoti emulate fc fg float functions getcap getln history integer jobs kill limit log noglob popd print pushd pushln rehash sched setcap setopt stat suspend ttyctl unfunction unhash unlimit unsetopt vared wait whence where which zcompile zformat zftp zle zmodload zparseopts zprof zpty zregexparse zsocket zstyle ztcp"
      }, contains: [r, s.SHEBANG(), l, o, s.HASH_COMMENT_MODE, i, c, {
        className: "", begin: /\\"/
      }, { className: "string", begin: /'/, end: /'/ }, n]
    }
  }
})());
hljs.registerLanguage("c", (() => {
 function e(e) {
    return ((...e) => e.map((e => (e => e ? "string" == typeof e ? e : e.source : null)(e))).join(""))("(", e, ")?")
  } return t => {
    const n = t.COMMENT("//", "$", {
      contains: [{ begin: /\\\n/ }]
    }), r = "[a-zA-Z_]\\w*::", a = "(decltype\\(auto\\)|" + e(r) + "[a-zA-Z_]\\w*" + e("<[^<>]+>") + ")", i = {
      className: "keyword", begin: "\\b[a-z\\d_]*_t\\b"
    }, s = {
      className: "string",
      variants: [{
        begin: '(u8?|U|L)?"', end: '"', illegal: "\\n",
        contains: [t.BACKSLASH_ESCAPE]
      }, {
        begin: "(u8?|U|L)?'(\\\\(x[0-9A-Fa-f]{2}|u[0-9A-Fa-f]{4,8}|[0-7]{3}|\\S)|.)",
        end: "'", illegal: "."
      }, t.END_SAME_AS_BEGIN({
        begin: /(?:u8?|U|L)?R"([^()\\ ]{0,16})\(/, end: /\)([^()\\ ]{0,16})"/
      })]
    }, o = {
      className: "number", variants: [{ begin: "\\b(0b[01']+)" }, {
        begin: "(-?)\\b([\\d']+(\\.[\\d']*)?|\\.[\\d']+)((ll|LL|l|L)(u|U)?|(u|U)(ll|LL|l|L)?|f|F|b|B)"
      }, {
        begin: "(-?)(\\b0[xX][a-fA-F0-9']+|(\\b[\\d']+(\\.[\\d']*)?|\\.[\\d']+)([eE][-+]?[\\d']+)?)"
      }], relevance: 0
    }, c = {
      className: "meta", begin: /#\s*[a-z]+\b/, end: /$/, keywords: {
        "meta-keyword": "if else elif endif define undef warning error line pragma _Pragma ifdef ifndef include"
      }, contains: [{ begin: /\\\n/, relevance: 0 }, t.inherit(s, { className: "meta-string" }), {
        className: "meta-string", begin: /<.*?>/
      }, n, t.C_BLOCK_COMMENT_MODE]
    }, l = {
      className: "title", begin: e(r) + t.IDENT_RE, relevance: 0
    }, d = e(r) + t.IDENT_RE + "\\s*\\(", u = {
      keyword: "int float while private char char8_t char16_t char32_t catch import module export virtual operator sizeof dynamic_cast|10 typedef const_cast|10 const for static_cast|10 union namespace unsigned long volatile static protected bool template mutable if public friend do goto auto void enum else break extern using asm case typeid wchar_t short reinterpret_cast|10 default double register explicit signed typename try this switch continue inline delete alignas alignof constexpr consteval constinit decltype concept co_await co_return co_yield requires noexcept static_assert thread_local restrict final override atomic_bool atomic_char atomic_schar atomic_uchar atomic_short atomic_ushort atomic_int atomic_uint atomic_long atomic_ulong atomic_llong atomic_ullong new throw return and and_eq bitand bitor compl not not_eq or or_eq xor xor_eq",
      built_in: "std string wstring cin cout cerr clog stdin stdout stderr stringstream istringstream ostringstream auto_ptr deque list queue stack vector map set pair bitset multiset multimap unordered_set unordered_map unordered_multiset unordered_multimap priority_queue make_pair array shared_ptr abort terminate abs acos asin atan2 atan calloc ceil cosh cos exit exp fabs floor fmod fprintf fputs free frexp fscanf future isalnum isalpha iscntrl isdigit isgraph islower isprint ispunct isspace isupper isxdigit tolower toupper labs ldexp log10 log malloc realloc memchr memcmp memcpy memset modf pow printf putchar puts scanf sinh sin snprintf sprintf sqrt sscanf strcat strchr strcmp strcpy strcspn strlen strncat strncmp strncpy strpbrk strrchr strspn strstr tanh tan vfprintf vprintf vsprintf endl initializer_list unique_ptr _Bool complex _Complex imaginary _Imaginary",
      literal: "true false nullptr NULL"
    }, m = [c, i, n, t.C_BLOCK_COMMENT_MODE, o, s], p = {
      variants: [{ begin: /=/, end: /;/ }, { begin: /\(/, end: /\)/ }, {
        beginKeywords: "new throw return else", end: /;/
      }], keywords: u, contains: m.concat([{
        begin: /\(/, end: /\)/, keywords: u, contains: m.concat(["self"]), relevance: 0
      }]),
      relevance: 0
    }, _ = {
      className: "function", begin: "(" + a + "[\\*&\\s]+)+" + d,
      returnBegin: !0, end: /[{;=]/, excludeEnd: !0, keywords: u, illegal: /[^\w\s\*&:<>.]/,
      contains: [{ begin: "decltype\\(auto\\)", keywords: u, relevance: 0 }, {
        begin: d,
        returnBegin: !0, contains: [l], relevance: 0
      }, {
        className: "params", begin: /\(/,
        end: /\)/, keywords: u, relevance: 0, contains: [n, t.C_BLOCK_COMMENT_MODE, s, o, i, {
          begin: /\(/, end: /\)/, keywords: u, relevance: 0,
          contains: ["self", n, t.C_BLOCK_COMMENT_MODE, s, o, i]
        }]
      }, i, n, t.C_BLOCK_COMMENT_MODE, c]
    }; return {
      name: "C", aliases: ["h"], keywords: u,
      disableAutodetect: !0, illegal: "</", contains: [].concat(p, _, m, [c, {
        begin: "\\b(deque|list|queue|priority_queue|pair|stack|vector|map|set|bitset|multiset|multimap|unordered_map|unordered_set|unordered_multiset|unordered_multimap|array)\\s*<",
        end: ">", keywords: u, contains: ["self", i]
      }, { begin: t.IDENT_RE + "::", keywords: u }, {
          className: "class", beginKeywords: "enum class struct union", end: /[{;:<>=]/,
          contains: [{ beginKeywords: "final class struct" }, t.TITLE_MODE]
        }]), exports: {
          preprocessor: c, strings: s, keywords: u
        }
    }
  }
})());
hljs.registerLanguage("coffeescript", (() => {
 const e = ["as", "in", "of", "if", "for", "while", "finally", "var", "new", "function", "do", "return", "void", "else", "break", "catch", "instanceof", "with", "throw", "case", "default", "try", "switch", "continue", "typeof", "delete", "let", "yield", "const", "class", "debugger", "async", "await", "static", "import", "from", "export", "extends"], n = ["true", "false", "null", "undefined", "NaN", "Infinity"], a = [].concat(["setInterval", "setTimeout", "clearInterval", "clearTimeout", "require", "exports", "eval", "isFinite", "isNaN", "parseFloat", "parseInt", "decodeURI", "decodeURIComponent", "encodeURI", "encodeURIComponent", "escape", "unescape"], ["arguments", "this", "super", "console", "window", "document", "localStorage", "module", "global"], ["Intl", "DataView", "Number", "Math", "Date", "String", "RegExp", "Object", "Function", "Boolean", "Error", "Symbol", "Set", "Map", "WeakSet", "WeakMap", "Proxy", "Reflect", "JSON", "Promise", "Float64Array", "Int16Array", "Int32Array", "Int8Array", "Uint16Array", "Uint32Array", "Float32Array", "Array", "Uint8Array", "Uint8ClampedArray", "ArrayBuffer", "BigInt64Array", "BigUint64Array", "BigInt"], ["EvalError", "InternalError", "RangeError", "ReferenceError", "SyntaxError", "TypeError", "URIError"])
    ; return r => {
      const t = {
        keyword: e.concat(["then", "unless", "until", "loop", "by", "when", "and", "or", "is", "isnt", "not"]).filter((i = ["var", "const", "let", "function", "static"],
          e => !i.includes(e))), literal: n.concat(["yes", "no", "on", "off"]),
        built_in: a.concat(["npm", "print"])
      }; var i; const s = "[A-Za-z$_][0-9A-Za-z$_]*", o = {
        className: "subst", begin: /#\{/, end: /\}/, keywords: t
      }, c = [r.BINARY_NUMBER_MODE, r.inherit(r.C_NUMBER_MODE, {
        starts: {
          end: "(\\s*/)?",
          relevance: 0
        }
      }), {
        className: "string", variants: [{
          begin: /'''/, end: /'''/,
          contains: [r.BACKSLASH_ESCAPE]
        }, {
          begin: /'/, end: /'/, contains: [r.BACKSLASH_ESCAPE]
        }, { begin: /"""/, end: /"""/, contains: [r.BACKSLASH_ESCAPE, o] }, {
          begin: /"/, end: /"/,
          contains: [r.BACKSLASH_ESCAPE, o]
        }]
      }, {
        className: "regexp", variants: [{
          begin: "///",
          end: "///", contains: [o, r.HASH_COMMENT_MODE]
        }, {
          begin: "//[gim]{0,3}(?=\\W)",
          relevance: 0
        }, { begin: /\/(?![ *]).*?(?![\\]).\/[gim]{0,3}(?=\W)/ }]
      }, {
        begin: "@" + s
      }, {
        subLanguage: "javascript", excludeBegin: !0, excludeEnd: !0, variants: [{
          begin: "```", end: "```"
        }, { begin: "`", end: "`" }]
      }]; o.contains = c
        ; const l = r.inherit(r.TITLE_MODE, { begin: s }), d = "(\\(.*\\)\\s*)?\\B[-=]>", g = {
          className: "params", begin: "\\([^\\(]", returnBegin: !0, contains: [{
            begin: /\(/,
            end: /\)/, keywords: t, contains: ["self"].concat(c)
          }]
        }; return {
          name: "CoffeeScript",
          aliases: ["coffee", "cson", "iced"], keywords: t, illegal: /\/\*/,
          contains: c.concat([r.COMMENT("###", "###"), r.HASH_COMMENT_MODE, {
            className: "function", begin: "^\\s*" + s + "\\s*=\\s*" + d, end: "[-=]>", returnBegin: !0,
            contains: [l, g]
          }, {
            begin: /[:\(,=]\s*/, relevance: 0, contains: [{
              className: "function",
              begin: d, end: "[-=]>", returnBegin: !0, contains: [g]
            }]
          }, {
            className: "class",
            beginKeywords: "class", end: "$", illegal: /[:="\[\]]/, contains: [{
              beginKeywords: "extends", endsWithParent: !0, illegal: /[:="\[\]]/, contains: [l]
            }, l]
          }, { begin: s + ":", end: ":", returnBegin: !0, returnEnd: !0, relevance: 0 }])
        }
    }
})());
hljs.registerLanguage("cpp", (() => {
 function e(e) {
    return t("(", e, ")?")
  } function t(...e) {
    return e.map((e => {
      return (t = e) ? "string" == typeof t ? t : t.source : null; var t;
    })).join("")
  } return n => {
    const r = n.COMMENT("//", "$", {
      contains: [{ begin: /\\\n/ }]
    }), a = "[a-zA-Z_]\\w*::", i = "(decltype\\(auto\\)|" + e(a) + "[a-zA-Z_]\\w*" + e("<[^<>]+>") + ")", s = {
      className: "keyword", begin: "\\b[a-z\\d_]*_t\\b"
    }, c = {
      className: "string",
      variants: [{
        begin: '(u8?|U|L)?"', end: '"', illegal: "\\n",
        contains: [n.BACKSLASH_ESCAPE]
      }, {
        begin: "(u8?|U|L)?'(\\\\(x[0-9A-Fa-f]{2}|u[0-9A-Fa-f]{4,8}|[0-7]{3}|\\S)|.)",
        end: "'", illegal: "."
      }, n.END_SAME_AS_BEGIN({
        begin: /(?:u8?|U|L)?R"([^()\\ ]{0,16})\(/, end: /\)([^()\\ ]{0,16})"/
      })]
    }, o = {
      className: "number", variants: [{ begin: "\\b(0b[01']+)" }, {
        begin: "(-?)\\b([\\d']+(\\.[\\d']*)?|\\.[\\d']+)((ll|LL|l|L)(u|U)?|(u|U)(ll|LL|l|L)?|f|F|b|B)"
      }, {
        begin: "(-?)(\\b0[xX][a-fA-F0-9']+|(\\b[\\d']+(\\.[\\d']*)?|\\.[\\d']+)([eE][-+]?[\\d']+)?)"
      }], relevance: 0
    }, l = {
      className: "meta", begin: /#\s*[a-z]+\b/, end: /$/, keywords: {
        "meta-keyword": "if else elif endif define undef warning error line pragma _Pragma ifdef ifndef include"
      }, contains: [{ begin: /\\\n/, relevance: 0 }, n.inherit(c, { className: "meta-string" }), {
        className: "meta-string", begin: /<.*?>/
      }, r, n.C_BLOCK_COMMENT_MODE]
    }, d = {
      className: "title", begin: e(a) + n.IDENT_RE, relevance: 0
    }, u = e(a) + n.IDENT_RE + "\\s*\\(", m = {
      keyword: "int float while private char char8_t char16_t char32_t catch import module export virtual operator sizeof dynamic_cast|10 typedef const_cast|10 const for static_cast|10 union namespace unsigned long volatile static protected bool template mutable if public friend do goto auto void enum else break extern using asm case typeid wchar_t short reinterpret_cast|10 default double register explicit signed typename try this switch continue inline delete alignas alignof constexpr consteval constinit decltype concept co_await co_return co_yield requires noexcept static_assert thread_local restrict final override atomic_bool atomic_char atomic_schar atomic_uchar atomic_short atomic_ushort atomic_int atomic_uint atomic_long atomic_ulong atomic_llong atomic_ullong new throw return and and_eq bitand bitor compl not not_eq or or_eq xor xor_eq",
      built_in: "_Bool _Complex _Imaginary",
      _relevance_hints: ["asin", "atan2", "atan", "calloc", "ceil", "cosh", "cos", "exit", "exp", "fabs", "floor", "fmod", "fprintf", "fputs", "free", "frexp", "auto_ptr", "deque", "list", "queue", "stack", "vector", "map", "set", "pair", "bitset", "multiset", "multimap", "unordered_set", "fscanf", "future", "isalnum", "isalpha", "iscntrl", "isdigit", "isgraph", "islower", "isprint", "ispunct", "isspace", "isupper", "isxdigit", "tolower", "toupper", "labs", "ldexp", "log10", "log", "malloc", "realloc", "memchr", "memcmp", "memcpy", "memset", "modf", "pow", "printf", "putchar", "puts", "scanf", "sinh", "sin", "snprintf", "sprintf", "sqrt", "sscanf", "strcat", "strchr", "strcmp", "strcpy", "strcspn", "strlen", "strncat", "strncmp", "strncpy", "strpbrk", "strrchr", "strspn", "strstr", "tanh", "tan", "unordered_map", "unordered_multiset", "unordered_multimap", "priority_queue", "make_pair", "array", "shared_ptr", "abort", "terminate", "abs", "acos", "vfprintf", "vprintf", "vsprintf", "endl", "initializer_list", "unique_ptr", "complex", "imaginary", "std", "string", "wstring", "cin", "cout", "cerr", "clog", "stdin", "stdout", "stderr", "stringstream", "istringstream", "ostringstream"],
      literal: "true false nullptr NULL"
    }, p = {
      className: "function.dispatch", relevance: 0,
      keywords: m,
      begin: t(/\b/, /(?!decltype)/, /(?!if)/, /(?!for)/, /(?!while)/, n.IDENT_RE, (_ = /\s*\(/,
        t("(?=", _, ")")))
    }; var _; const g = [p, l, s, r, n.C_BLOCK_COMMENT_MODE, o, c], b = {
      variants: [{ begin: /=/, end: /;/ }, { begin: /\(/, end: /\)/ }, {
        beginKeywords: "new throw return else", end: /;/
      }], keywords: m, contains: g.concat([{
        begin: /\(/, end: /\)/, keywords: m, contains: g.concat(["self"]), relevance: 0
      }]),
      relevance: 0
    }, f = {
      className: "function", begin: "(" + i + "[\\*&\\s]+)+" + u,
      returnBegin: !0, end: /[{;=]/, excludeEnd: !0, keywords: m, illegal: /[^\w\s\*&:<>.]/,
      contains: [{ begin: "decltype\\(auto\\)", keywords: m, relevance: 0 }, {
        begin: u,
        returnBegin: !0, contains: [d], relevance: 0
      }, { begin: /::/, relevance: 0 }, {
        begin: /:/,
        endsWithParent: !0, contains: [c, o]
      }, {
        className: "params", begin: /\(/, end: /\)/,
        keywords: m, relevance: 0, contains: [r, n.C_BLOCK_COMMENT_MODE, c, o, s, {
          begin: /\(/,
          end: /\)/, keywords: m, relevance: 0, contains: ["self", r, n.C_BLOCK_COMMENT_MODE, c, o, s]
        }]
      }, s, r, n.C_BLOCK_COMMENT_MODE, l]
    }; return {
      name: "C++",
      aliases: ["cc", "c++", "h++", "hpp", "hh", "hxx", "cxx"], keywords: m, illegal: "</",
      classNameAliases: { "function.dispatch": "built_in" },
      contains: [].concat(b, f, p, g, [l, {
        begin: "\\b(deque|list|queue|priority_queue|pair|stack|vector|map|set|bitset|multiset|multimap|unordered_map|unordered_set|unordered_multiset|unordered_multimap|array)\\s*<",
        end: ">", keywords: m, contains: ["self", s]
      }, { begin: n.IDENT_RE + "::", keywords: m }, {
          className: "class", beginKeywords: "enum class struct union", end: /[{;:<>=]/,
          contains: [{ beginKeywords: "final class struct" }, n.TITLE_MODE]
        }]), exports: {
          preprocessor: l, strings: c, keywords: m
        }
    }
  }
})());
hljs.registerLanguage("csharp", (() => {
 return e => {
    const n = {
      keyword: ["abstract", "as", "base", "break", "case", "class", "const", "continue", "do", "else", "event", "explicit", "extern", "finally", "fixed", "for", "foreach", "goto", "if", "implicit", "in", "interface", "internal", "is", "lock", "namespace", "new", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "record", "ref", "return", "sealed", "sizeof", "stackalloc", "static", "struct", "switch", "this", "throw", "try", "typeof", "unchecked", "unsafe", "using", "virtual", "void", "volatile", "while"].concat(["add", "alias", "and", "ascending", "async", "await", "by", "descending", "equals", "from", "get", "global", "group", "init", "into", "join", "let", "nameof", "not", "notnull", "on", "or", "orderby", "partial", "remove", "select", "set", "unmanaged", "value|0", "var", "when", "where", "with", "yield"]),
      built_in: ["bool", "byte", "char", "decimal", "delegate", "double", "dynamic", "enum", "float", "int", "long", "nint", "nuint", "object", "sbyte", "short", "string", "ulong", "uint", "ushort"],
      literal: ["default", "false", "null", "true"]
    }, a = e.inherit(e.TITLE_MODE, {
      begin: "[a-zA-Z](\\.?\\w)*"
    }), i = {
      className: "number", variants: [{
        begin: "\\b(0b[01']+)"
      }, {
        begin: "(-?)\\b([\\d']+(\\.[\\d']*)?|\\.[\\d']+)(u|U|l|L|ul|UL|f|F|b|B)"
      }, {
        begin: "(-?)(\\b0[xX][a-fA-F0-9']+|(\\b[\\d']+(\\.[\\d']*)?|\\.[\\d']+)([eE][-+]?[\\d']+)?)"
      }], relevance: 0
    }, s = {
      className: "string", begin: '@"', end: '"', contains: [{ begin: '""' }]
    }, t = e.inherit(s, { illegal: /\n/ }), r = {
      className: "subst", begin: /\{/, end: /\}/,
      keywords: n
    }, l = e.inherit(r, { illegal: /\n/ }), c = {
      className: "string", begin: /\$"/,
      end: '"', illegal: /\n/, contains: [{ begin: /\{\{/ }, {
        begin: /\}\}/
      }, e.BACKSLASH_ESCAPE, l]
    }, o = {
      className: "string", begin: /\$@"/, end: '"', contains: [{
        begin: /\{\{/
      }, { begin: /\}\}/ }, { begin: '""' }, r]
    }, d = e.inherit(o, {
      illegal: /\n/,
      contains: [{ begin: /\{\{/ }, { begin: /\}\}/ }, { begin: '""' }, l]
    })
    ; r.contains = [o, c, s, e.APOS_STRING_MODE, e.QUOTE_STRING_MODE, i, e.C_BLOCK_COMMENT_MODE],
      l.contains = [d, c, t, e.APOS_STRING_MODE, e.QUOTE_STRING_MODE, i, e.inherit(e.C_BLOCK_COMMENT_MODE, {
        illegal: /\n/
      })]; const g = {
        variants: [o, c, s, e.APOS_STRING_MODE, e.QUOTE_STRING_MODE]
      }, E = {
        begin: "<", end: ">", contains: [{ beginKeywords: "in out" }, a]
      }, _ = e.IDENT_RE + "(<" + e.IDENT_RE + "(\\s*,\\s*" + e.IDENT_RE + ")*>)?(\\[\\])?", b = {
        begin: "@" + e.IDENT_RE, relevance: 0
      }; return {
        name: "C#", aliases: ["cs", "c#"],
        keywords: n, illegal: /::/, contains: [e.COMMENT("///", "$", {
          returnBegin: !0,
          contains: [{
            className: "doctag", variants: [{ begin: "///", relevance: 0 }, {
              begin: "\x3c!--|--\x3e"
            }, { begin: "</?", end: ">" }]
          }]
        }), e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE, {
          className: "meta", begin: "#",
          end: "$", keywords: {
            "meta-keyword": "if else elif endif define undef warning error line region endregion pragma checksum"
          }
        }, g, i, {
          beginKeywords: "class interface", relevance: 0, end: /[{;=]/,
          illegal: /[^\s:,]/, contains: [{
            beginKeywords: "where class"
          }, a, E, e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE]
        }, {
          beginKeywords: "namespace",
          relevance: 0, end: /[{;=]/, illegal: /[^\s:]/,
          contains: [a, e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE]
        }, {
          beginKeywords: "record", relevance: 0, end: /[{;=]/, illegal: /[^\s:]/,
          contains: [a, E, e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE]
        }, {
          className: "meta",
          begin: "^\\s*\\[", excludeBegin: !0, end: "\\]", excludeEnd: !0, contains: [{
            className: "meta-string", begin: /"/, end: /"/
          }]
        }, {
          beginKeywords: "new return throw await else", relevance: 0
        }, {
          className: "function",
          begin: "(" + _ + "\\s+)+" + e.IDENT_RE + "\\s*(<.+>\\s*)?\\(", returnBegin: !0,
          end: /\s*[{;=]/, excludeEnd: !0, keywords: n, contains: [{
            beginKeywords: "public private protected static internal protected abstract async extern override unsafe virtual new sealed partial",
            relevance: 0
          }, {
            begin: e.IDENT_RE + "\\s*(<.+>\\s*)?\\(", returnBegin: !0,
            contains: [e.TITLE_MODE, E], relevance: 0
          }, {
            className: "params", begin: /\(/, end: /\)/,
            excludeBegin: !0, excludeEnd: !0, keywords: n, relevance: 0,
            contains: [g, i, e.C_BLOCK_COMMENT_MODE]
          }, e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE]
        }, b]
      }
  }
})());
hljs.registerLanguage("css", (() => {
 const e = ["a", "abbr", "address", "article", "aside", "audio", "b", "blockquote", "body", "button", "canvas", "caption", "cite", "code", "dd", "del", "details", "dfn", "div", "dl", "dt", "em", "fieldset", "figcaption", "figure", "footer", "form", "h1", "h2", "h3", "h4", "h5", "h6", "header", "hgroup", "html", "i", "iframe", "img", "input", "ins", "kbd", "label", "legend", "li", "main", "mark", "menu", "nav", "object", "ol", "p", "q", "quote", "samp", "section", "span", "strong", "summary", "sup", "table", "tbody", "td", "textarea", "tfoot", "th", "thead", "time", "tr", "ul", "var", "video"], t = ["any-hover", "any-pointer", "aspect-ratio", "color", "color-gamut", "color-index", "device-aspect-ratio", "device-height", "device-width", "display-mode", "forced-colors", "grid", "height", "hover", "inverted-colors", "monochrome", "orientation", "overflow-block", "overflow-inline", "pointer", "prefers-color-scheme", "prefers-contrast", "prefers-reduced-motion", "prefers-reduced-transparency", "resolution", "scan", "scripting", "update", "width", "min-width", "max-width", "min-height", "max-height"], i = ["active", "any-link", "blank", "checked", "current", "default", "defined", "dir", "disabled", "drop", "empty", "enabled", "first", "first-child", "first-of-type", "fullscreen", "future", "focus", "focus-visible", "focus-within", "has", "host", "host-context", "hover", "indeterminate", "in-range", "invalid", "is", "lang", "last-child", "last-of-type", "left", "link", "local-link", "not", "nth-child", "nth-col", "nth-last-child", "nth-last-col", "nth-last-of-type", "nth-of-type", "only-child", "only-of-type", "optional", "out-of-range", "past", "placeholder-shown", "read-only", "read-write", "required", "right", "root", "scope", "target", "target-within", "user-invalid", "valid", "visited", "where"], o = ["after", "backdrop", "before", "cue", "cue-region", "first-letter", "first-line", "grammar-error", "marker", "part", "placeholder", "selection", "slotted", "spelling-error"], r = ["align-content", "align-items", "align-self", "animation", "animation-delay", "animation-direction", "animation-duration", "animation-fill-mode", "animation-iteration-count", "animation-name", "animation-play-state", "animation-timing-function", "auto", "backface-visibility", "background", "background-attachment", "background-clip", "background-color", "background-image", "background-origin", "background-position", "background-repeat", "background-size", "border", "border-bottom", "border-bottom-color", "border-bottom-left-radius", "border-bottom-right-radius", "border-bottom-style", "border-bottom-width", "border-collapse", "border-color", "border-image", "border-image-outset", "border-image-repeat", "border-image-slice", "border-image-source", "border-image-width", "border-left", "border-left-color", "border-left-style", "border-left-width", "border-radius", "border-right", "border-right-color", "border-right-style", "border-right-width", "border-spacing", "border-style", "border-top", "border-top-color", "border-top-left-radius", "border-top-right-radius", "border-top-style", "border-top-width", "border-width", "bottom", "box-decoration-break", "box-shadow", "box-sizing", "break-after", "break-before", "break-inside", "caption-side", "clear", "clip", "clip-path", "color", "column-count", "column-fill", "column-gap", "column-rule", "column-rule-color", "column-rule-style", "column-rule-width", "column-span", "column-width", "columns", "content", "counter-increment", "counter-reset", "cursor", "direction", "display", "empty-cells", "filter", "flex", "flex-basis", "flex-direction", "flex-flow", "flex-grow", "flex-shrink", "flex-wrap", "float", "font", "font-display", "font-family", "font-feature-settings", "font-kerning", "font-language-override", "font-size", "font-size-adjust", "font-smoothing", "font-stretch", "font-style", "font-variant", "font-variant-ligatures", "font-variation-settings", "font-weight", "height", "hyphens", "icon", "image-orientation", "image-rendering", "image-resolution", "ime-mode", "inherit", "initial", "justify-content", "left", "letter-spacing", "line-height", "list-style", "list-style-image", "list-style-position", "list-style-type", "margin", "margin-bottom", "margin-left", "margin-right", "margin-top", "marks", "mask", "max-height", "max-width", "min-height", "min-width", "nav-down", "nav-index", "nav-left", "nav-right", "nav-up", "none", "normal", "object-fit", "object-position", "opacity", "order", "orphans", "outline", "outline-color", "outline-offset", "outline-style", "outline-width", "overflow", "overflow-wrap", "overflow-x", "overflow-y", "padding", "padding-bottom", "padding-left", "padding-right", "padding-top", "page-break-after", "page-break-before", "page-break-inside", "perspective", "perspective-origin", "pointer-events", "position", "quotes", "resize", "right", "src", "tab-size", "table-layout", "text-align", "text-align-last", "text-decoration", "text-decoration-color", "text-decoration-line", "text-decoration-style", "text-indent", "text-overflow", "text-rendering", "text-shadow", "text-transform", "text-underline-position", "top", "transform", "transform-origin", "transform-style", "transition", "transition-delay", "transition-duration", "transition-property", "transition-timing-function", "unicode-bidi", "vertical-align", "visibility", "white-space", "widows", "width", "word-break", "word-spacing", "word-wrap", "z-index"].reverse()
    ; return n => {
      const a = (e => ({
        IMPORTANT: { className: "meta", begin: "!important" },
        HEXCOLOR: { className: "number", begin: "#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})" },
        ATTRIBUTE_SELECTOR_MODE: {
          className: "selector-attr", begin: /\[/, end: /\]/,
          illegal: "$", contains: [e.APOS_STRING_MODE, e.QUOTE_STRING_MODE]
        }
      }))(n), l = [n.APOS_STRING_MODE, n.QUOTE_STRING_MODE]; return {
        name: "CSS",
        case_insensitive: !0, illegal: /[=|'\$]/, keywords: { keyframePosition: "from to" },
        classNameAliases: { keyframePosition: "selector-tag" },
        contains: [n.C_BLOCK_COMMENT_MODE, {
          begin: /-(webkit|moz|ms|o)-(?=[a-z])/
        }, n.CSS_NUMBER_MODE, {
          className: "selector-id", begin: /#[A-Za-z0-9_-]+/, relevance: 0
        }, {
          className: "selector-class", begin: "\\.[a-zA-Z-][a-zA-Z0-9_-]*", relevance: 0
        }, a.ATTRIBUTE_SELECTOR_MODE, {
          className: "selector-pseudo", variants: [{
            begin: ":(" + i.join("|") + ")"
          }, { begin: "::(" + o.join("|") + ")" }]
        }, {
          className: "attribute", begin: "\\b(" + r.join("|") + ")\\b"
        }, {
          begin: ":", end: "[;}]",
          contains: [a.HEXCOLOR, a.IMPORTANT, n.CSS_NUMBER_MODE, ...l, {
            begin: /(url|data-uri)\(/, end: /\)/, relevance: 0, keywords: {
              built_in: "url data-uri"
            }, contains: [{ className: "string", begin: /[^)]/, endsWithParent: !0, excludeEnd: !0 }]
          }, { className: "built_in", begin: /[\w-]+(?=\()/ }]
        }, {
          begin: (s = /@/, ((...e) => e.map((e => (e => e ? "string" == typeof e ? e : e.source : null)(e))).join(""))("(?=", s, ")")),
          end: "[{;]", relevance: 0, illegal: /:/, contains: [{
            className: "keyword",
            begin: /@-?\w[\w]*(-\w+)*/
          }, {
            begin: /\s/, endsWithParent: !0, excludeEnd: !0,
            relevance: 0, keywords: {
              $pattern: /[a-z-]+/, keyword: "and or not only",
              attribute: t.join(" ")
            }, contains: [{
              begin: /[a-z-]+(?=:)/, className: "attribute"
            }, ...l, n.CSS_NUMBER_MODE]
          }]
        }, {
          className: "selector-tag",
          begin: "\\b(" + e.join("|") + ")\\b"
        }]
      }; var s;
    }
})());
hljs.registerLanguage("diff", (() => {
 return e => ({
    name: "Diff",
    aliases: ["patch"], contains: [{
      className: "meta", relevance: 10, variants: [{
        begin: /^@@ +-\d+,\d+ +\+\d+,\d+ +@@/
      }, { begin: /^\*\*\* +\d+,\d+ +\*\*\*\*$/ }, {
        begin: /^--- +\d+,\d+ +----$/
      }]
    }, {
      className: "comment", variants: [{
        begin: /Index: /,
        end: /$/
      }, { begin: /^index/, end: /$/ }, { begin: /={3,}/, end: /$/ }, {
        begin: /^-{3}/, end: /$/
      }, { begin: /^\*{3} /, end: /$/ }, { begin: /^\+{3}/, end: /$/ }, { begin: /^\*{15}$/ }, {
        begin: /^diff --git/, end: /$/
      }]
    }, { className: "addition", begin: /^\+/, end: /$/ }, {
      className: "deletion", begin: /^-/, end: /$/
    }, {
      className: "addition", begin: /^!/,
      end: /$/
    }]
  })
})());
hljs.registerLanguage("go", (() => {
 return e => {
    const n = {
      keyword: "break default func interface select case map struct chan else goto package switch const fallthrough if range type continue for import return var go defer bool byte complex64 complex128 float32 float64 int8 int16 int32 int64 string uint8 uint16 uint32 uint64 int uint uintptr rune",
      literal: "true false iota nil",
      built_in: "append cap close complex copy imag len make new panic print println real recover delete"
    }; return {
      name: "Go", aliases: ["golang"], keywords: n, illegal: "</",
      contains: [e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE, {
        className: "string",
        variants: [e.QUOTE_STRING_MODE, e.APOS_STRING_MODE, { begin: "`", end: "`" }]
      }, {
        className: "number", variants: [{
          begin: e.C_NUMBER_RE + "[i]", relevance: 1
        }, e.C_NUMBER_MODE]
      }, { begin: /:=/ }, {
        className: "function", beginKeywords: "func",
        end: "\\s*(\\{|$)", excludeEnd: !0, contains: [e.TITLE_MODE, {
          className: "params",
          begin: /\(/, end: /\)/, keywords: n, illegal: /["']/
        }]
      }]
    }
  }
})());
hljs.registerLanguage("http", (() => {
 function e(...e) {
    return e.map((e => {
      return (n = e) ? "string" == typeof n ? n : n.source : null; var n;
    })).join("")
  } return n => {
    const a = "HTTP/(2|1\\.[01])", s = {
      className: "attribute",
      begin: e("^", /[A-Za-z][A-Za-z0-9-]*/, "(?=\\:\\s)"), starts: {
        contains: [{
          className: "punctuation", begin: /: /, relevance: 0, starts: { end: "$", relevance: 0 }
        }]
      }
    }, t = [s, { begin: "\\n\\n", starts: { subLanguage: [], endsWithParent: !0 } }]; return {
      name: "HTTP", aliases: ["https"], illegal: /\S/, contains: [{
        begin: "^(?=" + a + " \\d{3})",
        end: /$/, contains: [{ className: "meta", begin: a }, {
          className: "number",
          begin: "\\b\\d{3}\\b"
        }], starts: { end: /\b\B/, illegal: /\S/, contains: t }
      }, {
        begin: "(?=^[A-Z]+ (.*?) " + a + "$)", end: /$/, contains: [{
          className: "string",
          begin: " ", end: " ", excludeBegin: !0, excludeEnd: !0
        }, { className: "meta", begin: a }, {
          className: "keyword", begin: "[A-Z]+"
        }], starts: { end: /\b\B/, illegal: /\S/, contains: t }
      }, n.inherit(s, { relevance: 0 })]
    }
  }
})());
hljs.registerLanguage("ini", (() => {
 function e(e) {
    return e ? "string" == typeof e ? e : e.source : null
  } function n(...n) {
    return n.map((n => e(n))).join("")
  } return s => {
    const a = {
      className: "number",
      relevance: 0, variants: [{ begin: /([+-]+)?[\d]+_[\d_]+/ }, { begin: s.NUMBER_RE }]
    }, i = s.COMMENT(); i.variants = [{ begin: /;/, end: /$/ }, { begin: /#/, end: /$/ }]; const t = {
      className: "variable", variants: [{ begin: /\$[\w\d"][\w\d_]*/ }, {
        begin: /\$\{(.*?)\}/
      }]
    }, r = { className: "literal", begin: /\bon|off|true|false|yes|no\b/ }, l = {
      className: "string", contains: [s.BACKSLASH_ESCAPE], variants: [{
        begin: "'''",
        end: "'''", relevance: 10
      }, { begin: '"""', end: '"""', relevance: 10 }, {
        begin: '"', end: '"'
      }, { begin: "'", end: "'" }]
    }, c = {
      begin: /\[/, end: /\]/, contains: [i, r, t, l, a, "self"],
      relevance: 0
    }, g = "(" + [/[A-Za-z0-9_-]+/, /"(\\"|[^"])*"/, /'[^']*'/].map((n => e(n))).join("|") + ")"
      ; return {
        name: "TOML, also INI", aliases: ["toml"], case_insensitive: !0, illegal: /\S/,
        contains: [i, { className: "section", begin: /\[+/, end: /\]+/ }, {
          begin: n(g, "(\\s*\\.\\s*", g, ")*", n("(?=", /\s*=\s*[^#\s]/, ")")), className: "attr",
          starts: { end: /$/, contains: [i, c, r, t, l, a] }
        }]
      }
  }
})());
hljs.registerLanguage("java", (() => {
 var e = "\\.([0-9](_*[0-9])*)", n = "[0-9a-fA-F](_*[0-9a-fA-F])*", a = {
      className: "number", variants: [{
        begin: `(\\b([0-9](_*[0-9])*)((${e})|\\.)?|(${e}))[eE][+-]?([0-9](_*[0-9])*)[fFdD]?\\b`
      }, { begin: `\\b([0-9](_*[0-9])*)((${e})[fFdD]?\\b|\\.([fFdD]\\b)?)` }, {
        begin: `(${e})[fFdD]?\\b`
      }, { begin: "\\b([0-9](_*[0-9])*)[fFdD]\\b" }, {
        begin: `\\b0[xX]((${n})\\.?|(${n})?\\.(${n}))[pP][+-]?([0-9](_*[0-9])*)[fFdD]?\\b`
      }, { begin: "\\b(0|[1-9](_*[0-9])*)[lL]?\\b" }, { begin: `\\b0[xX](${n})[lL]?\\b` }, {
        begin: "\\b0(_*[0-7])*[lL]?\\b"
      }, { begin: "\\b0[bB][01](_*[01])*[lL]?\\b" }],
      relevance: 0
    }; return e => {
      var n = "false synchronized int abstract float private char boolean var static null if const for true while long strictfp finally protected import native final void enum else break transient catch instanceof byte super volatile case assert short package default double public try this switch continue throws protected public private module requires exports do", s = {
        className: "meta", begin: "@[\xc0-\u02b8a-zA-Z_$][\xc0-\u02b8a-zA-Z_$0-9]*",
        contains: [{ begin: /\(/, end: /\)/, contains: ["self"] }]
      }; const r = a; return {
        name: "Java", aliases: ["jsp"], keywords: n, illegal: /<\/|#/,
        contains: [e.COMMENT("/\\*\\*", "\\*/", {
          relevance: 0, contains: [{
            begin: /\w+@/,
            relevance: 0
          }, { className: "doctag", begin: "@[A-Za-z]+" }]
        }), {
          begin: /import java\.[a-z]+\./, keywords: "import", relevance: 2
        }, e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE, e.APOS_STRING_MODE, e.QUOTE_STRING_MODE, {
          className: "class", beginKeywords: "class interface enum", end: /[{;=]/,
          excludeEnd: !0, relevance: 1, keywords: "class interface enum", illegal: /[:"\[\]]/,
          contains: [{ beginKeywords: "extends implements" }, e.UNDERSCORE_TITLE_MODE]
        }, {
          beginKeywords: "new throw return else", relevance: 0
        }, {
          className: "class",
          begin: "record\\s+" + e.UNDERSCORE_IDENT_RE + "\\s*\\(", returnBegin: !0, excludeEnd: !0,
          end: /[{;=]/, keywords: n, contains: [{ beginKeywords: "record" }, {
            begin: e.UNDERSCORE_IDENT_RE + "\\s*\\(", returnBegin: !0, relevance: 0,
            contains: [e.UNDERSCORE_TITLE_MODE]
          }, {
            className: "params", begin: /\(/, end: /\)/,
            keywords: n, relevance: 0, contains: [e.C_BLOCK_COMMENT_MODE]
          }, e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE]
        }, {
          className: "function",
          begin: "([\xc0-\u02b8a-zA-Z_$][\xc0-\u02b8a-zA-Z_$0-9]*(<[\xc0-\u02b8a-zA-Z_$][\xc0-\u02b8a-zA-Z_$0-9]*(\\s*,\\s*[\xc0-\u02b8a-zA-Z_$][\xc0-\u02b8a-zA-Z_$0-9]*)*>)?\\s+)+" + e.UNDERSCORE_IDENT_RE + "\\s*\\(",
          returnBegin: !0, end: /[{;=]/, excludeEnd: !0, keywords: n, contains: [{
            begin: e.UNDERSCORE_IDENT_RE + "\\s*\\(", returnBegin: !0, relevance: 0,
            contains: [e.UNDERSCORE_TITLE_MODE]
          }, {
            className: "params", begin: /\(/, end: /\)/,
            keywords: n, relevance: 0,
            contains: [s, e.APOS_STRING_MODE, e.QUOTE_STRING_MODE, r, e.C_BLOCK_COMMENT_MODE]
          }, e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE]
        }, r, s]
      }
    }
})());
hljs.registerLanguage("javascript", (() => {
 const e = "[A-Za-z$_][0-9A-Za-z$_]*", n = ["as", "in", "of", "if", "for", "while", "finally", "var", "new", "function", "do", "return", "void", "else", "break", "catch", "instanceof", "with", "throw", "case", "default", "try", "switch", "continue", "typeof", "delete", "let", "yield", "const", "class", "debugger", "async", "await", "static", "import", "from", "export", "extends"], a = ["true", "false", "null", "undefined", "NaN", "Infinity"], s = [].concat(["setInterval", "setTimeout", "clearInterval", "clearTimeout", "require", "exports", "eval", "isFinite", "isNaN", "parseFloat", "parseInt", "decodeURI", "decodeURIComponent", "encodeURI", "encodeURIComponent", "escape", "unescape"], ["arguments", "this", "super", "console", "window", "document", "localStorage", "module", "global"], ["Intl", "DataView", "Number", "Math", "Date", "String", "RegExp", "Object", "Function", "Boolean", "Error", "Symbol", "Set", "Map", "WeakSet", "WeakMap", "Proxy", "Reflect", "JSON", "Promise", "Float64Array", "Int16Array", "Int32Array", "Int8Array", "Uint16Array", "Uint32Array", "Float32Array", "Array", "Uint8Array", "Uint8ClampedArray", "ArrayBuffer", "BigInt64Array", "BigUint64Array", "BigInt"], ["EvalError", "InternalError", "RangeError", "ReferenceError", "SyntaxError", "TypeError", "URIError"])
    ; function r(e) { return t("(?=", e, ")") } function t(...e) {
      return e.map((e => {
        return (n = e) ? "string" == typeof n ? n : n.source : null; var n;
      })).join("")
    } return i => {
      const c = e, o = {
        begin: /<[A-Za-z0-9\\._:-]+/, end: /\/[A-Za-z0-9\\._:-]+>|\/>/,
        isTrulyOpeningTag: (e, n) => {
          const a = e[0].length + e.index, s = e.input[a]
          ; "<" !== s ? ">" === s && (((e, { after: n }) => {
            const a = "</" + e[0].slice(1)
            ; return -1 !== e.input.indexOf(a, n)
          })(e, {
            after: a
          }) || n.ignoreMatch()) : n.ignoreMatch();
        }
      }, l = {
        $pattern: e, keyword: n, literal: a,
        built_in: s
      }, g = "\\.([0-9](_?[0-9])*)", b = "0|[1-9](_?[0-9])*|0[0-7]*[89][0-9]*", d = {
        className: "number", variants: [{
          begin: `(\\b(${b})((${g})|\\.)?|(${g}))[eE][+-]?([0-9](_?[0-9])*)\\b`
        }, {
          begin: `\\b(${b})\\b((${g})\\b|\\.)?|(${g})\\b`
        }, {
          begin: "\\b(0|[1-9](_?[0-9])*)n\\b"
        }, {
          begin: "\\b0[xX][0-9a-fA-F](_?[0-9a-fA-F])*n?\\b"
        }, {
          begin: "\\b0[bB][0-1](_?[0-1])*n?\\b"
        }, { begin: "\\b0[oO][0-7](_?[0-7])*n?\\b" }, {
          begin: "\\b0[0-7]+n?\\b"
        }], relevance: 0
      }, E = {
        className: "subst", begin: "\\$\\{",
        end: "\\}", keywords: l, contains: []
      }, u = {
        begin: "html`", end: "", starts: {
          end: "`",
          returnEnd: !1, contains: [i.BACKSLASH_ESCAPE, E], subLanguage: "xml"
        }
      }, _ = {
        begin: "css`", end: "", starts: {
          end: "`", returnEnd: !1,
          contains: [i.BACKSLASH_ESCAPE, E], subLanguage: "css"
        }
      }, m = {
        className: "string",
        begin: "`", end: "`", contains: [i.BACKSLASH_ESCAPE, E]
      }, y = {
        className: "comment",
        variants: [i.COMMENT(/\/\*\*(?!\/)/, "\\*/", {
          relevance: 0, contains: [{
            className: "doctag", begin: "@[A-Za-z]+", contains: [{
              className: "type", begin: "\\{",
              end: "\\}", relevance: 0
            }, {
              className: "variable", begin: c + "(?=\\s*(-)|$)",
              endsParent: !0, relevance: 0
            }, { begin: /(?=[^\n])\s/, relevance: 0 }]
          }]
        }), i.C_BLOCK_COMMENT_MODE, i.C_LINE_COMMENT_MODE]
      }, N = [i.APOS_STRING_MODE, i.QUOTE_STRING_MODE, u, _, m, d, i.REGEXP_MODE]
        ; E.contains = N.concat({
          begin: /\{/, end: /\}/, keywords: l, contains: ["self"].concat(N)
        }); const A = [].concat(y, E.contains), f = A.concat([{
          begin: /\(/, end: /\)/, keywords: l,
          contains: ["self"].concat(A)
        }]), p = {
          className: "params", begin: /\(/, end: /\)/,
          excludeBegin: !0, excludeEnd: !0, keywords: l, contains: f
        }; return {
          name: "Javascript",
          aliases: ["js", "jsx", "mjs", "cjs"], keywords: l, exports: { PARAMS_CONTAINS: f },
          illegal: /#(?![$_A-z])/, contains: [i.SHEBANG({
            label: "shebang", binary: "node",
            relevance: 5
          }), {
            label: "use_strict", className: "meta", relevance: 10,
            begin: /^\s*['"]use (strict|asm)['"]/
          }, i.APOS_STRING_MODE, i.QUOTE_STRING_MODE, u, _, m, y, d, {
            begin: t(/[{,\n]\s*/, r(t(/(((\/\/.*$)|(\/\*(\*[^/]|[^*])*\*\/))\s*)*/, c + "\\s*:"))),
            relevance: 0, contains: [{ className: "attr", begin: c + r("\\s*:"), relevance: 0 }]
          }, {
            begin: "(" + i.RE_STARTERS_RE + "|\\b(case|return|throw)\\b)\\s*",
            keywords: "return throw case", contains: [y, i.REGEXP_MODE, {
              className: "function",
              begin: "(\\([^()]*(\\([^()]*(\\([^()]*\\)[^()]*)*\\)[^()]*)*\\)|" + i.UNDERSCORE_IDENT_RE + ")\\s*=>",
              returnBegin: !0, end: "\\s*=>", contains: [{
                className: "params", variants: [{
                  begin: i.UNDERSCORE_IDENT_RE, relevance: 0
                }, {
                  className: null, begin: /\(\s*\)/, skip: !0
                }, { begin: /\(/, end: /\)/, excludeBegin: !0, excludeEnd: !0, keywords: l, contains: f }]
              }]
            }, { begin: /,/, relevance: 0 }, { className: "", begin: /\s/, end: /\s*/, skip: !0 }, {
                variants: [{ begin: "<>", end: "</>" }, {
                  begin: o.begin, "on:begin": o.isTrulyOpeningTag,
                  end: o.end
                }], subLanguage: "xml", contains: [{
                  begin: o.begin, end: o.end, skip: !0,
                  contains: ["self"]
                }]
              }], relevance: 0
          }, {
            className: "function",
            beginKeywords: "function", end: /[{;]/, excludeEnd: !0, keywords: l,
            contains: ["self", i.inherit(i.TITLE_MODE, { begin: c }), p], illegal: /%/
          }, {
            beginKeywords: "while if switch catch for"
          }, {
            className: "function",
            begin: i.UNDERSCORE_IDENT_RE + "\\([^()]*(\\([^()]*(\\([^()]*\\)[^()]*)*\\)[^()]*)*\\)\\s*\\{",
            returnBegin: !0, contains: [p, i.inherit(i.TITLE_MODE, { begin: c })]
          }, {
            variants: [{
              begin: "\\." + c
            }, { begin: "\\$" + c }], relevance: 0
          }, {
            className: "class",
            beginKeywords: "class", end: /[{;=]/, excludeEnd: !0, illegal: /[:"[\]]/, contains: [{
              beginKeywords: "extends"
            }, i.UNDERSCORE_TITLE_MODE]
          }, {
            begin: /\b(?=constructor)/,
            end: /[{;]/, excludeEnd: !0, contains: [i.inherit(i.TITLE_MODE, { begin: c }), "self", p]
          }, {
            begin: "(get|set)\\s+(?=" + c + "\\()", end: /\{/, keywords: "get set",
            contains: [i.inherit(i.TITLE_MODE, { begin: c }), { begin: /\(\)/ }, p]
          }, { begin: /\$[(.]/ }]
        }
    }
})());
hljs.registerLanguage("json", (() => {
 return n => {
    const e = {
      literal: "true false null"
    }, i = [n.C_LINE_COMMENT_MODE, n.C_BLOCK_COMMENT_MODE], a = [n.QUOTE_STRING_MODE, n.C_NUMBER_MODE], l = {
      end: ",", endsWithParent: !0, excludeEnd: !0, contains: a, keywords: e
    }, t = {
      begin: /\{/,
      end: /\}/, contains: [{
        className: "attr", begin: /"/, end: /"/,
        contains: [n.BACKSLASH_ESCAPE], illegal: "\\n"
      }, n.inherit(l, {
        begin: /:/
      })].concat(i), illegal: "\\S"
    }, s = {
      begin: "\\[", end: "\\]", contains: [n.inherit(l)],
      illegal: "\\S"
    }; return a.push(t, s), i.forEach((n => { a.push(n); })), {
      name: "JSON",
      contains: a, keywords: e, illegal: "\\S"
    }
  }
})());
hljs.registerLanguage("kotlin", (() => {
 var e = "\\.([0-9](_*[0-9])*)", n = "[0-9a-fA-F](_*[0-9a-fA-F])*", a = {
      className: "number", variants: [{
        begin: `(\\b([0-9](_*[0-9])*)((${e})|\\.)?|(${e}))[eE][+-]?([0-9](_*[0-9])*)[fFdD]?\\b`
      }, { begin: `\\b([0-9](_*[0-9])*)((${e})[fFdD]?\\b|\\.([fFdD]\\b)?)` }, {
        begin: `(${e})[fFdD]?\\b`
      }, { begin: "\\b([0-9](_*[0-9])*)[fFdD]\\b" }, {
        begin: `\\b0[xX]((${n})\\.?|(${n})?\\.(${n}))[pP][+-]?([0-9](_*[0-9])*)[fFdD]?\\b`
      }, { begin: "\\b(0|[1-9](_*[0-9])*)[lL]?\\b" }, { begin: `\\b0[xX](${n})[lL]?\\b` }, {
        begin: "\\b0(_*[0-7])*[lL]?\\b"
      }, { begin: "\\b0[bB][01](_*[01])*[lL]?\\b" }],
      relevance: 0
    }; return e => {
      const n = {
        keyword: "abstract as val var vararg get set class object open private protected public noinline crossinline dynamic final enum if else do while for when throw try catch finally import package is in fun override companion reified inline lateinit init interface annotation data sealed internal infix operator out by constructor super tailrec where const inner suspend typealias external expect actual",
        built_in: "Byte Short Char Int Long Boolean Float Double Void Unit Nothing",
        literal: "true false null"
      }, i = {
        className: "symbol", begin: e.UNDERSCORE_IDENT_RE + "@"
      }, s = { className: "subst", begin: /\$\{/, end: /\}/, contains: [e.C_NUMBER_MODE] }, t = {
        className: "variable", begin: "\\$" + e.UNDERSCORE_IDENT_RE
      }, r = {
        className: "string",
        variants: [{ begin: '"""', end: '"""(?=[^"])', contains: [t, s] }, {
          begin: "'", end: "'",
          illegal: /\n/, contains: [e.BACKSLASH_ESCAPE]
        }, {
          begin: '"', end: '"', illegal: /\n/,
          contains: [e.BACKSLASH_ESCAPE, t, s]
        }]
      }; s.contains.push(r); const l = {
        className: "meta",
        begin: "@(?:file|property|field|get|set|receiver|param|setparam|delegate)\\s*:(?:\\s*" + e.UNDERSCORE_IDENT_RE + ")?"
      }, c = {
        className: "meta", begin: "@" + e.UNDERSCORE_IDENT_RE, contains: [{
          begin: /\(/,
          end: /\)/, contains: [e.inherit(r, { className: "meta-string" })]
        }]
      }, o = a, b = e.COMMENT("/\\*", "\\*/", { contains: [e.C_BLOCK_COMMENT_MODE] }), E = {
        variants: [{ className: "type", begin: e.UNDERSCORE_IDENT_RE }, {
          begin: /\(/, end: /\)/,
          contains: []
        }]
      }, d = E; return d.variants[1].contains = [E], E.variants[1].contains = [d],
      {
        name: "Kotlin", aliases: ["kt", "kts"], keywords: n,
        contains: [e.COMMENT("/\\*\\*", "\\*/", {
          relevance: 0, contains: [{
            className: "doctag",
            begin: "@[A-Za-z]+"
          }]
        }), e.C_LINE_COMMENT_MODE, b, {
          className: "keyword",
          begin: /\b(break|continue|return|this)\b/, starts: {
            contains: [{
              className: "symbol",
              begin: /@\w+/
            }]
          }
        }, i, l, c, {
          className: "function", beginKeywords: "fun", end: "[(]|$",
          returnBegin: !0, excludeEnd: !0, keywords: n, relevance: 5, contains: [{
            begin: e.UNDERSCORE_IDENT_RE + "\\s*\\(", returnBegin: !0, relevance: 0,
            contains: [e.UNDERSCORE_TITLE_MODE]
          }, {
            className: "type", begin: /</, end: />/,
            keywords: "reified", relevance: 0
          }, {
            className: "params", begin: /\(/, end: /\)/,
            endsParent: !0, keywords: n, relevance: 0, contains: [{
              begin: /:/, end: /[=,\/]/,
              endsWithParent: !0, contains: [E, e.C_LINE_COMMENT_MODE, b], relevance: 0
            }, e.C_LINE_COMMENT_MODE, b, l, c, r, e.C_NUMBER_MODE]
          }, b]
        }, {
          className: "class",
          beginKeywords: "class interface trait", end: /[:\{(]|$/, excludeEnd: !0,
          illegal: "extends implements", contains: [{
            beginKeywords: "public protected internal private constructor"
          }, e.UNDERSCORE_TITLE_MODE, {
            className: "type", begin: /</, end: />/, excludeBegin: !0,
            excludeEnd: !0, relevance: 0
          }, {
            className: "type", begin: /[,:]\s*/, end: /[<\(,]|$/,
            excludeBegin: !0, returnEnd: !0
          }, l, c]
        }, r, {
          className: "meta", begin: "^#!/usr/bin/env",
          end: "$", illegal: "\n"
        }, o]
      }
    }
})());
hljs.registerLanguage("less", (() => {
 const e = ["a", "abbr", "address", "article", "aside", "audio", "b", "blockquote", "body", "button", "canvas", "caption", "cite", "code", "dd", "del", "details", "dfn", "div", "dl", "dt", "em", "fieldset", "figcaption", "figure", "footer", "form", "h1", "h2", "h3", "h4", "h5", "h6", "header", "hgroup", "html", "i", "iframe", "img", "input", "ins", "kbd", "label", "legend", "li", "main", "mark", "menu", "nav", "object", "ol", "p", "q", "quote", "samp", "section", "span", "strong", "summary", "sup", "table", "tbody", "td", "textarea", "tfoot", "th", "thead", "time", "tr", "ul", "var", "video"], t = ["any-hover", "any-pointer", "aspect-ratio", "color", "color-gamut", "color-index", "device-aspect-ratio", "device-height", "device-width", "display-mode", "forced-colors", "grid", "height", "hover", "inverted-colors", "monochrome", "orientation", "overflow-block", "overflow-inline", "pointer", "prefers-color-scheme", "prefers-contrast", "prefers-reduced-motion", "prefers-reduced-transparency", "resolution", "scan", "scripting", "update", "width", "min-width", "max-width", "min-height", "max-height"], i = ["active", "any-link", "blank", "checked", "current", "default", "defined", "dir", "disabled", "drop", "empty", "enabled", "first", "first-child", "first-of-type", "fullscreen", "future", "focus", "focus-visible", "focus-within", "has", "host", "host-context", "hover", "indeterminate", "in-range", "invalid", "is", "lang", "last-child", "last-of-type", "left", "link", "local-link", "not", "nth-child", "nth-col", "nth-last-child", "nth-last-col", "nth-last-of-type", "nth-of-type", "only-child", "only-of-type", "optional", "out-of-range", "past", "placeholder-shown", "read-only", "read-write", "required", "right", "root", "scope", "target", "target-within", "user-invalid", "valid", "visited", "where"], o = ["after", "backdrop", "before", "cue", "cue-region", "first-letter", "first-line", "grammar-error", "marker", "part", "placeholder", "selection", "slotted", "spelling-error"], n = ["align-content", "align-items", "align-self", "animation", "animation-delay", "animation-direction", "animation-duration", "animation-fill-mode", "animation-iteration-count", "animation-name", "animation-play-state", "animation-timing-function", "auto", "backface-visibility", "background", "background-attachment", "background-clip", "background-color", "background-image", "background-origin", "background-position", "background-repeat", "background-size", "border", "border-bottom", "border-bottom-color", "border-bottom-left-radius", "border-bottom-right-radius", "border-bottom-style", "border-bottom-width", "border-collapse", "border-color", "border-image", "border-image-outset", "border-image-repeat", "border-image-slice", "border-image-source", "border-image-width", "border-left", "border-left-color", "border-left-style", "border-left-width", "border-radius", "border-right", "border-right-color", "border-right-style", "border-right-width", "border-spacing", "border-style", "border-top", "border-top-color", "border-top-left-radius", "border-top-right-radius", "border-top-style", "border-top-width", "border-width", "bottom", "box-decoration-break", "box-shadow", "box-sizing", "break-after", "break-before", "break-inside", "caption-side", "clear", "clip", "clip-path", "color", "column-count", "column-fill", "column-gap", "column-rule", "column-rule-color", "column-rule-style", "column-rule-width", "column-span", "column-width", "columns", "content", "counter-increment", "counter-reset", "cursor", "direction", "display", "empty-cells", "filter", "flex", "flex-basis", "flex-direction", "flex-flow", "flex-grow", "flex-shrink", "flex-wrap", "float", "font", "font-display", "font-family", "font-feature-settings", "font-kerning", "font-language-override", "font-size", "font-size-adjust", "font-smoothing", "font-stretch", "font-style", "font-variant", "font-variant-ligatures", "font-variation-settings", "font-weight", "height", "hyphens", "icon", "image-orientation", "image-rendering", "image-resolution", "ime-mode", "inherit", "initial", "justify-content", "left", "letter-spacing", "line-height", "list-style", "list-style-image", "list-style-position", "list-style-type", "margin", "margin-bottom", "margin-left", "margin-right", "margin-top", "marks", "mask", "max-height", "max-width", "min-height", "min-width", "nav-down", "nav-index", "nav-left", "nav-right", "nav-up", "none", "normal", "object-fit", "object-position", "opacity", "order", "orphans", "outline", "outline-color", "outline-offset", "outline-style", "outline-width", "overflow", "overflow-wrap", "overflow-x", "overflow-y", "padding", "padding-bottom", "padding-left", "padding-right", "padding-top", "page-break-after", "page-break-before", "page-break-inside", "perspective", "perspective-origin", "pointer-events", "position", "quotes", "resize", "right", "src", "tab-size", "table-layout", "text-align", "text-align-last", "text-decoration", "text-decoration-color", "text-decoration-line", "text-decoration-style", "text-indent", "text-overflow", "text-rendering", "text-shadow", "text-transform", "text-underline-position", "top", "transform", "transform-origin", "transform-style", "transition", "transition-delay", "transition-duration", "transition-property", "transition-timing-function", "unicode-bidi", "vertical-align", "visibility", "white-space", "widows", "width", "word-break", "word-spacing", "word-wrap", "z-index"].reverse(), r = i.concat(o)
    ; return a => {
      const s = (e => ({
        IMPORTANT: { className: "meta", begin: "!important" },
        HEXCOLOR: { className: "number", begin: "#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})" },
        ATTRIBUTE_SELECTOR_MODE: {
          className: "selector-attr", begin: /\[/, end: /\]/,
          illegal: "$", contains: [e.APOS_STRING_MODE, e.QUOTE_STRING_MODE]
        }
      }))(a), l = r, d = "([\\w-]+|@\\{[\\w-]+\\})", c = [], g = [], b = e => ({
        className: "string",
        begin: "~?" + e + ".*?" + e
      }), m = (e, t, i) => ({ className: e, begin: t, relevance: i }), u = {
        $pattern: /[a-z-]+/, keyword: "and or not only", attribute: t.join(" ")
      }, p = {
        begin: "\\(", end: "\\)", contains: g, keywords: u, relevance: 0
      }
      ; g.push(a.C_LINE_COMMENT_MODE, a.C_BLOCK_COMMENT_MODE, b("'"), b('"'), a.CSS_NUMBER_MODE, {
        begin: "(url|data-uri)\\(", starts: {
          className: "string", end: "[\\)\\n]",
          excludeEnd: !0
        }
      }, s.HEXCOLOR, p, m("variable", "@@?[\\w-]+", 10), m("variable", "@\\{[\\w-]+\\}"), m("built_in", "~?`[^`]*?`"), {
        className: "attribute", begin: "[\\w-]+\\s*:", end: ":", returnBegin: !0, excludeEnd: !0
      }, s.IMPORTANT); const f = g.concat({ begin: /\{/, end: /\}/, contains: c }), h = {
        beginKeywords: "when", endsWithParent: !0, contains: [{
          beginKeywords: "and not"
        }].concat(g)
      }, w = {
        begin: d + "\\s*:", returnBegin: !0, end: /[;}]/, relevance: 0,
        contains: [{ begin: /-(webkit|moz|ms|o)-/ }, {
          className: "attribute",
          begin: "\\b(" + n.join("|") + ")\\b", end: /(?=:)/, starts: {
            endsWithParent: !0,
            illegal: "[<=$]", relevance: 0, contains: g
          }
        }]
      }, v = {
        className: "keyword",
        begin: "@(import|media|charset|font-face|(-[a-z]+-)?keyframes|supports|document|namespace|page|viewport|host)\\b",
        starts: { end: "[;{}]", keywords: u, returnEnd: !0, contains: g, relevance: 0 }
      }, y = {
        className: "variable", variants: [{ begin: "@[\\w-]+\\s*:", relevance: 15 }, {
          begin: "@[\\w-]+"
        }], starts: { end: "[;}]", returnEnd: !0, contains: f }
      }, k = {
        variants: [{
          begin: "[\\.#:&\\[>]", end: "[;{}]"
        }, { begin: d, end: /\{/ }], returnBegin: !0,
        returnEnd: !0, illegal: "[<='$\"]", relevance: 0,
        contains: [a.C_LINE_COMMENT_MODE, a.C_BLOCK_COMMENT_MODE, h, m("keyword", "all\\b"), m("variable", "@\\{[\\w-]+\\}"), {
          begin: "\\b(" + e.join("|") + ")\\b", className: "selector-tag"
        }, m("selector-tag", d + "%?", 0), m("selector-id", "#" + d), m("selector-class", "\\." + d, 0), m("selector-tag", "&", 0), s.ATTRIBUTE_SELECTOR_MODE, {
          className: "selector-pseudo", begin: ":(" + i.join("|") + ")"
        }, {
          className: "selector-pseudo", begin: "::(" + o.join("|") + ")"
        }, {
          begin: "\\(", end: "\\)",
          contains: f
        }, { begin: "!important" }]
      }, E = {
        begin: `[\\w-]+:(:)?(${l.join("|")})`,
        returnBegin: !0, contains: [k]
      }
        ; return c.push(a.C_LINE_COMMENT_MODE, a.C_BLOCK_COMMENT_MODE, v, y, E, w, k), {
          name: "Less", case_insensitive: !0, illegal: "[=>'/<($\"]", contains: c
        }
    }
})());
hljs.registerLanguage("lua", (() => {
 return e => {
    const t = "\\[=*\\[", a = "\\]=*\\]", n = {
      begin: t, end: a, contains: ["self"]
    }, o = [e.COMMENT("--(?!\\[=*\\[)", "$"), e.COMMENT("--\\[=*\\[", a, {
      contains: [n],
      relevance: 10
    })]; return {
      name: "Lua", keywords: {
        $pattern: e.UNDERSCORE_IDENT_RE,
        literal: "true false nil",
        keyword: "and break do else elseif end for goto if in local not or repeat return then until while",
        built_in: "_G _ENV _VERSION __index __newindex __mode __call __metatable __tostring __len __gc __add __sub __mul __div __mod __pow __concat __unm __eq __lt __le assert collectgarbage dofile error getfenv getmetatable ipairs load loadfile loadstring module next pairs pcall print rawequal rawget rawset require select setfenv setmetatable tonumber tostring type unpack xpcall arg self coroutine resume yield status wrap create running debug getupvalue debug sethook getmetatable gethook setmetatable setlocal traceback setfenv getinfo setupvalue getlocal getregistry getfenv io lines write close flush open output type read stderr stdin input stdout popen tmpfile math log max acos huge ldexp pi cos tanh pow deg tan cosh sinh random randomseed frexp ceil floor rad abs sqrt modf asin min mod fmod log10 atan2 exp sin atan os exit setlocale date getenv difftime remove time clock tmpname rename execute package preload loadlib loaded loaders cpath config path seeall string sub upper len gfind rep find match char dump gmatch reverse byte format gsub lower table setn insert getn foreachi maxn foreach concat sort remove"
      }, contains: o.concat([{
        className: "function", beginKeywords: "function", end: "\\)",
        contains: [e.inherit(e.TITLE_MODE, {
          begin: "([_a-zA-Z]\\w*\\.)*([_a-zA-Z]\\w*:)?[_a-zA-Z]\\w*"
        }), {
          className: "params",
          begin: "\\(", endsWithParent: !0, contains: o
        }].concat(o)
      }, e.C_NUMBER_MODE, e.APOS_STRING_MODE, e.QUOTE_STRING_MODE, {
        className: "string",
        begin: t, end: a, contains: [n], relevance: 5
      }])
    }
  }
})());
hljs.registerLanguage("makefile", (() => {
 return e => {
    const i = {
      className: "variable", variants: [{
        begin: "\\$\\(" + e.UNDERSCORE_IDENT_RE + "\\)",
        contains: [e.BACKSLASH_ESCAPE]
      }, { begin: /\$[@%<?\^\+\*]/ }]
    }, a = {
      className: "string",
      begin: /"/, end: /"/, contains: [e.BACKSLASH_ESCAPE, i]
    }, n = {
      className: "variable",
      begin: /\$\([\w-]+\s/, end: /\)/, keywords: {
        built_in: "subst patsubst strip findstring filter filter-out sort word wordlist firstword lastword dir notdir suffix basename addsuffix addprefix join wildcard realpath abspath error warning shell origin flavor foreach if or and call eval file value"
      }, contains: [i]
    }, s = { begin: "^" + e.UNDERSCORE_IDENT_RE + "\\s*(?=[:+?]?=)" }, r = {
      className: "section", begin: /^[^\s]+:/, end: /$/, contains: [i]
    }; return {
      name: "Makefile", aliases: ["mk", "mak", "make"], keywords: {
        $pattern: /[\w-]+/,
        keyword: "define endef undefine ifdef ifndef ifeq ifneq else endif include -include sinclude override export unexport private vpath"
      }, contains: [e.HASH_COMMENT_MODE, i, a, n, s, {
        className: "meta", begin: /^\.PHONY:/,
        end: /$/, keywords: { $pattern: /[\.\w]+/, "meta-keyword": ".PHONY" }
      }, r]
    }
  }
})());
hljs.registerLanguage("xml", (() => {
 function e(e) {
    return e ? "string" == typeof e ? e : e.source : null
  } function n(e) { return a("(?=", e, ")") }
  function a(...n) { return n.map((n => e(n))).join("") } function s(...n) {
    return "(" + n.map((n => e(n))).join("|") + ")"
  } return e => {
    const t = a(/[A-Z_]/, a("(", /[A-Z0-9_.-]*:/, ")?"), /[A-Z0-9_.-]*/), i = {
      className: "symbol", begin: /&[a-z]+;|&#[0-9]+;|&#x[a-f0-9]+;/
    }, r = {
      begin: /\s/,
      contains: [{ className: "meta-keyword", begin: /#?[a-z_][a-z1-9_-]+/, illegal: /\n/ }]
    }, c = e.inherit(r, { begin: /\(/, end: /\)/ }), l = e.inherit(e.APOS_STRING_MODE, {
      className: "meta-string"
    }), g = e.inherit(e.QUOTE_STRING_MODE, {
      className: "meta-string"
    }), m = {
      endsWithParent: !0, illegal: /</, relevance: 0,
      contains: [{ className: "attr", begin: /[A-Za-z0-9._:-]+/, relevance: 0 }, {
        begin: /=\s*/,
        relevance: 0, contains: [{
          className: "string", endsParent: !0, variants: [{
            begin: /"/,
            end: /"/, contains: [i]
          }, { begin: /'/, end: /'/, contains: [i] }, { begin: /[^\s"'=<>`]+/ }]
        }]
      }]
    }; return {
      name: "HTML, XML",
      aliases: ["html", "xhtml", "rss", "atom", "xjb", "xsd", "xsl", "plist", "wsf", "svg"],
      case_insensitive: !0, contains: [{
        className: "meta", begin: /<![a-z]/, end: />/,
        relevance: 10, contains: [r, g, l, c, {
          begin: /\[/, end: /\]/, contains: [{
            className: "meta",
            begin: /<![a-z]/, end: />/, contains: [r, c, g, l]
          }]
        }]
      }, e.COMMENT(/<!--/, /-->/, {
        relevance: 10
      }), { begin: /<!\[CDATA\[/, end: /\]\]>/, relevance: 10 }, i, {
        className: "meta", begin: /<\?xml/, end: /\?>/, relevance: 10
      }, {
        className: "tag",
        begin: /<style(?=\s|>)/, end: />/, keywords: { name: "style" }, contains: [m], starts: {
          end: /<\/style>/, returnEnd: !0, subLanguage: ["css", "xml"]
        }
      }, {
        className: "tag",
        begin: /<script(?=\s|>)/, end: />/, keywords: { name: "script" }, contains: [m], starts: {
          end: /<\/script>/, returnEnd: !0, subLanguage: ["javascript", "handlebars", "xml"]
        }
      }, {
        className: "tag", begin: /<>|<\/>/
      }, {
        className: "tag",
        begin: a(/</, n(a(t, s(/\/>/, />/, /\s/)))), end: /\/?>/, contains: [{
          className: "name",
          begin: t, relevance: 0, starts: m
        }]
      }, {
        className: "tag", begin: a(/<\//, n(a(t, />/))),
        contains: [{ className: "name", begin: t, relevance: 0 }, {
          begin: />/, relevance: 0,
          endsParent: !0
        }]
      }]
    }
  }
})());
hljs.registerLanguage("markdown", (() => {
 function n(...n) {
    return n.map((n => {
      return (e = n) ? "string" == typeof e ? e : e.source : null; var e;
    })).join("")
  } return e => {
    const a = {
      begin: /<\/?[A-Za-z_]/, end: ">",
      subLanguage: "xml", relevance: 0
    }, i = {
      variants: [{
        begin: /\[.+?\]\[.*?\]/, relevance: 0
      }, {
        begin: /\[.+?\]\(((data|javascript|mailto):|(?:http|ftp)s?:\/\/).*?\)/,
        relevance: 2
      }, {
        begin: n(/\[.+?\]\(/, /[A-Za-z][A-Za-z0-9+.-]*/, /:\/\/.*?\)/),
        relevance: 2
      }, { begin: /\[.+?\]\([./?&#].*?\)/, relevance: 1 }, {
        begin: /\[.+?\]\(.*?\)/, relevance: 0
      }], returnBegin: !0, contains: [{
        className: "string", relevance: 0, begin: "\\[", end: "\\]", excludeBegin: !0,
        returnEnd: !0
      }, {
        className: "link", relevance: 0, begin: "\\]\\(", end: "\\)",
        excludeBegin: !0, excludeEnd: !0
      }, {
        className: "symbol", relevance: 0, begin: "\\]\\[",
        end: "\\]", excludeBegin: !0, excludeEnd: !0
      }]
    }, s = {
      className: "strong", contains: [],
      variants: [{ begin: /_{2}/, end: /_{2}/ }, { begin: /\*{2}/, end: /\*{2}/ }]
    }, c = {
      className: "emphasis", contains: [], variants: [{ begin: /\*(?!\*)/, end: /\*/ }, {
        begin: /_(?!_)/, end: /_/, relevance: 0
      }]
    }; s.contains.push(c), c.contains.push(s)
      ; let t = [a, i]
      ; return s.contains = s.contains.concat(t), c.contains = c.contains.concat(t),
        t = t.concat(s, c), {
          name: "Markdown", aliases: ["md", "mkdown", "mkd"], contains: [{
            className: "section", variants: [{ begin: "^#{1,6}", end: "$", contains: t }, {
              begin: "(?=^.+?\\n[=-]{2,}$)", contains: [{ begin: "^[=-]*$" }, {
                begin: "^", end: "\\n",
                contains: t
              }]
            }]
          }, a, {
            className: "bullet", begin: "^[ \t]*([*+-]|(\\d+\\.))(?=\\s+)",
            end: "\\s+", excludeEnd: !0
          }, s, c, {
            className: "quote", begin: "^>\\s+", contains: t,
            end: "$"
          }, {
            className: "code", variants: [{ begin: "(`{3,})[^`](.|\\n)*?\\1`*[ ]*" }, {
              begin: "(~{3,})[^~](.|\\n)*?\\1~*[ ]*"
            }, { begin: "```", end: "```+[ ]*$" }, {
              begin: "~~~", end: "~~~+[ ]*$"
            }, { begin: "`.+?`" }, {
              begin: "(?=^( {4}|\\t))",
              contains: [{ begin: "^( {4}|\\t)", end: "(\\n)$" }], relevance: 0
            }]
          }, {
            begin: "^[-\\*]{3,}", end: "$"
          }, i, {
            begin: /^\[[^\n]+\]:/, returnBegin: !0, contains: [{
              className: "symbol", begin: /\[/, end: /\]/, excludeBegin: !0, excludeEnd: !0
            }, {
              className: "link", begin: /:\s*/, end: /$/, excludeBegin: !0
            }]
          }]
      }
  }
})());
hljs.registerLanguage("nginx", (() => {
 return e => {
    const n = {
      className: "variable", variants: [{ begin: /\$\d+/ }, { begin: /\$\{/, end: /\}/ }, {
        begin: /[$@]/ + e.UNDERSCORE_IDENT_RE
      }]
    }, a = {
      endsWithParent: !0, keywords: {
        $pattern: "[a-z/_]+",
        literal: "on off yes no true false none blocked debug info notice warn error crit select break last permanent redirect kqueue rtsig epoll poll /dev/poll"
      }, relevance: 0, illegal: "=>", contains: [e.HASH_COMMENT_MODE, {
        className: "string",
        contains: [e.BACKSLASH_ESCAPE, n], variants: [{ begin: /"/, end: /"/ }, {
          begin: /'/, end: /'/
        }]
      }, {
        begin: "([a-z]+):/", end: "\\s", endsWithParent: !0, excludeEnd: !0, contains: [n]
      }, {
        className: "regexp", contains: [e.BACKSLASH_ESCAPE, n], variants: [{
          begin: "\\s\\^",
          end: "\\s|\\{|;", returnEnd: !0
        }, { begin: "~\\*?\\s+", end: "\\s|\\{|;", returnEnd: !0 }, {
          begin: "\\*(\\.[a-z\\-]+)+"
        }, { begin: "([a-z\\-]+\\.)+\\*" }]
      }, {
        className: "number",
        begin: "\\b\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}(:\\d{1,5})?\\b"
      }, {
        className: "number", begin: "\\b\\d+[kKmMgGdshdwy]*\\b", relevance: 0
      }, n]
    }; return {
      name: "Nginx config", aliases: ["nginxconf"], contains: [e.HASH_COMMENT_MODE, {
        begin: e.UNDERSCORE_IDENT_RE + "\\s+\\{", returnBegin: !0, end: /\{/, contains: [{
          className: "section", begin: e.UNDERSCORE_IDENT_RE
        }], relevance: 0
      }, {
        begin: e.UNDERSCORE_IDENT_RE + "\\s", end: ";|\\{", returnBegin: !0, contains: [{
          className: "attribute", begin: e.UNDERSCORE_IDENT_RE, starts: a
        }], relevance: 0
      }],
      illegal: "[^\\s\\}]"
    }
  }
})());
hljs.registerLanguage("objectivec", (() => {
 return e => {
    const n = /[a-zA-Z@][a-zA-Z0-9_]*/, _ = {
      $pattern: n,
      keyword: "@interface @class @protocol @implementation"
    }; return {
      name: "Objective-C", aliases: ["mm", "objc", "obj-c", "obj-c++", "objective-c++"],
      keywords: {
        $pattern: n,
        keyword: "int float while char export sizeof typedef const struct for union unsigned long volatile static bool mutable if do return goto void enum else break extern asm case short default double register explicit signed typename this switch continue wchar_t inline readonly assign readwrite self @synchronized id typeof nonatomic super unichar IBOutlet IBAction strong weak copy in out inout bycopy byref oneway __strong __weak __block __autoreleasing @private @protected @public @try @property @end @throw @catch @finally @autoreleasepool @synthesize @dynamic @selector @optional @required @encode @package @import @defs @compatibility_alias __bridge __bridge_transfer __bridge_retained __bridge_retain __covariant __contravariant __kindof _Nonnull _Nullable _Null_unspecified __FUNCTION__ __PRETTY_FUNCTION__ __attribute__ getter setter retain unsafe_unretained nonnull nullable null_unspecified null_resettable class instancetype NS_DESIGNATED_INITIALIZER NS_UNAVAILABLE NS_REQUIRES_SUPER NS_RETURNS_INNER_POINTER NS_INLINE NS_AVAILABLE NS_DEPRECATED NS_ENUM NS_OPTIONS NS_SWIFT_UNAVAILABLE NS_ASSUME_NONNULL_BEGIN NS_ASSUME_NONNULL_END NS_REFINED_FOR_SWIFT NS_SWIFT_NAME NS_SWIFT_NOTHROW NS_DURING NS_HANDLER NS_ENDHANDLER NS_VALUERETURN NS_VOIDRETURN",
        literal: "false true FALSE TRUE nil YES NO NULL",
        built_in: "BOOL dispatch_once_t dispatch_queue_t dispatch_sync dispatch_async dispatch_once"
      }, illegal: "</", contains: [{
        className: "built_in",
        begin: "\\b(AV|CA|CF|CG|CI|CL|CM|CN|CT|MK|MP|MTK|MTL|NS|SCN|SK|UI|WK|XC)\\w+"
      }, e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE, e.C_NUMBER_MODE, e.QUOTE_STRING_MODE, e.APOS_STRING_MODE, {
        className: "string", variants: [{
          begin: '@"', end: '"', illegal: "\\n",
          contains: [e.BACKSLASH_ESCAPE]
        }]
      }, {
        className: "meta", begin: /#\s*[a-z]+\b/, end: /$/,
        keywords: {
          "meta-keyword": "if else elif endif define undef warning error line pragma ifdef ifndef include"
        }, contains: [{ begin: /\\\n/, relevance: 0 }, e.inherit(e.QUOTE_STRING_MODE, {
          className: "meta-string"
        }), {
          className: "meta-string", begin: /<.*?>/, end: /$/,
          illegal: "\\n"
        }, e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE]
      }, {
        className: "class", begin: "(" + _.keyword.split(" ").join("|") + ")\\b", end: /(\{|$)/,
        excludeEnd: !0, keywords: _, contains: [e.UNDERSCORE_TITLE_MODE]
      }, {
        begin: "\\." + e.UNDERSCORE_IDENT_RE, relevance: 0
      }]
    }
  }
})());
hljs.registerLanguage("perl", (() => {
 function e(e) {
    return e ? "string" == typeof e ? e : e.source : null
  } function n(...n) {
    return n.map((n => e(n))).join("")
  } function t(...n) {
    return "(" + n.map((n => e(n))).join("|") + ")"
  } return e => {
    const r = /[dualxmsipngr]{0,12}/, s = {
      $pattern: /[\w.]+/,
      keyword: "abs accept alarm and atan2 bind binmode bless break caller chdir chmod chomp chop chown chr chroot close closedir connect continue cos crypt dbmclose dbmopen defined delete die do dump each else elsif endgrent endhostent endnetent endprotoent endpwent endservent eof eval exec exists exit exp fcntl fileno flock for foreach fork format formline getc getgrent getgrgid getgrnam gethostbyaddr gethostbyname gethostent getlogin getnetbyaddr getnetbyname getnetent getpeername getpgrp getpriority getprotobyname getprotobynumber getprotoent getpwent getpwnam getpwuid getservbyname getservbyport getservent getsockname getsockopt given glob gmtime goto grep gt hex if index int ioctl join keys kill last lc lcfirst length link listen local localtime log lstat lt ma map mkdir msgctl msgget msgrcv msgsnd my ne next no not oct open opendir or ord our pack package pipe pop pos print printf prototype push q|0 qq quotemeta qw qx rand read readdir readline readlink readpipe recv redo ref rename require reset return reverse rewinddir rindex rmdir say scalar seek seekdir select semctl semget semop send setgrent sethostent setnetent setpgrp setpriority setprotoent setpwent setservent setsockopt shift shmctl shmget shmread shmwrite shutdown sin sleep socket socketpair sort splice split sprintf sqrt srand stat state study sub substr symlink syscall sysopen sysread sysseek system syswrite tell telldir tie tied time times tr truncate uc ucfirst umask undef unless unlink unpack unshift untie until use utime values vec wait waitpid wantarray warn when while write x|0 xor y|0"
    }, i = { className: "subst", begin: "[$@]\\{", end: "\\}", keywords: s }, a = {
      begin: /->\{/,
      end: /\}/
    }, o = {
      variants: [{ begin: /\$\d/ }, {
        begin: n(/[$%@](\^\w\b|#\w+(::\w+)*|\{\w+\}|\w+(::\w*)*)/, "(?![A-Za-z])(?![@$%])")
      }, { begin: /[$%@][^\s\w{]/, relevance: 0 }]
    }, c = [e.BACKSLASH_ESCAPE, i, o], g = [/!/, /\//, /\|/, /\?/, /'/, /"/, /#/], l = (e, t, s = "\\1") => {
      const i = "\\1" === s ? s : n(s, t)
        ; return n(n("(?:", e, ")"), t, /(?:\\.|[^\\\/])*?/, i, /(?:\\.|[^\\\/])*?/, s, r)
    }, d = (e, t, s) => n(n("(?:", e, ")"), t, /(?:\\.|[^\\\/])*?/, s, r), p = [o, e.HASH_COMMENT_MODE, e.COMMENT(/^=\w/, /=cut/, {
      endsWithParent: !0
    }), a, {
      className: "string", contains: c, variants: [{
        begin: "q[qwxr]?\\s*\\(", end: "\\)", relevance: 5
      }, {
        begin: "q[qwxr]?\\s*\\[",
        end: "\\]", relevance: 5
      }, { begin: "q[qwxr]?\\s*\\{", end: "\\}", relevance: 5 }, {
        begin: "q[qwxr]?\\s*\\|", end: "\\|", relevance: 5
      }, {
        begin: "q[qwxr]?\\s*<", end: ">",
        relevance: 5
      }, { begin: "qw\\s+q", end: "q", relevance: 5 }, {
        begin: "'", end: "'",
        contains: [e.BACKSLASH_ESCAPE]
      }, { begin: '"', end: '"' }, {
        begin: "`", end: "`",
        contains: [e.BACKSLASH_ESCAPE]
      }, { begin: /\{\w+\}/, relevance: 0 }, {
        begin: "-?\\w+\\s*=>", relevance: 0
      }]
      }, {
        className: "number",
        begin: "(\\b0[0-7_]+)|(\\b0x[0-9a-fA-F_]+)|(\\b[1-9][0-9_]*(\\.[0-9_]+)?)|[0_]\\b",
        relevance: 0
      }, {
        begin: "(\\/\\/|" + e.RE_STARTERS_RE + "|\\b(split|return|print|reverse|grep)\\b)\\s*",
        keywords: "split return print reverse grep", relevance: 0,
        contains: [e.HASH_COMMENT_MODE, {
          className: "regexp", variants: [{
            begin: l("s|tr|y", t(...g))
          }, { begin: l("s|tr|y", "\\(", "\\)") }, {
            begin: l("s|tr|y", "\\[", "\\]")
          }, { begin: l("s|tr|y", "\\{", "\\}") }], relevance: 2
        }, {
          className: "regexp", variants: [{ begin: /(m|qr)\/\//, relevance: 0 }, {
            begin: d("(?:m|qr)?", /\//, /\//)
          }, { begin: d("m|qr", t(...g), /\1/) }, {
            begin: d("m|qr", /\(/, /\)/)
          }, { begin: d("m|qr", /\[/, /\]/) }, {
            begin: d("m|qr", /\{/, /\}/)
          }]
        }]
      }, {
        className: "function", beginKeywords: "sub",
        end: "(\\s*\\(.*?\\))?[;{]", excludeEnd: !0, relevance: 5, contains: [e.TITLE_MODE]
      }, {
        begin: "-\\w\\b", relevance: 0
      }, {
        begin: "^__DATA__$", end: "^__END__$",
        subLanguage: "mojolicious", contains: [{ begin: "^@@.*", end: "$", className: "comment" }]
      }]; return i.contains = p, a.contains = p, {
        name: "Perl", aliases: ["pl", "pm"], keywords: s,
        contains: p
      }
  }
})());
hljs.registerLanguage("php", (() => {
 return e => {
    const r = {
      className: "variable",
      begin: "\\$+[a-zA-Z_\x7f-\xff][a-zA-Z0-9_\x7f-\xff]*(?![A-Za-z0-9])(?![$])"
    }, t = {
      className: "meta", variants: [{ begin: /<\?php/, relevance: 10 }, { begin: /<\?[=]?/ }, {
        begin: /\?>/
      }]
    }, a = {
      className: "subst", variants: [{ begin: /\$\w+/ }, {
        begin: /\{\$/,
        end: /\}/
      }]
    }, n = e.inherit(e.APOS_STRING_MODE, {
      illegal: null
    }), i = e.inherit(e.QUOTE_STRING_MODE, {
      illegal: null,
      contains: e.QUOTE_STRING_MODE.contains.concat(a)
    }), o = e.END_SAME_AS_BEGIN({
      begin: /<<<[ \t]*(\w+)\n/, end: /[ \t]*(\w+)\b/,
      contains: e.QUOTE_STRING_MODE.contains.concat(a)
    }), l = {
      className: "string",
      contains: [e.BACKSLASH_ESCAPE, t], variants: [e.inherit(n, {
        begin: "b'", end: "'"
      }), e.inherit(i, { begin: 'b"', end: '"' }), i, n, o]
    }, s = {
      className: "number", variants: [{
        begin: "\\b0b[01]+(?:_[01]+)*\\b"
      }, { begin: "\\b0o[0-7]+(?:_[0-7]+)*\\b" }, {
        begin: "\\b0x[\\da-f]+(?:_[\\da-f]+)*\\b"
      }, {
        begin: "(?:\\b\\d+(?:_\\d+)*(\\.(?:\\d+(?:_\\d+)*))?|\\B\\.\\d+)(?:e[+-]?\\d+)?"
      }], relevance: 0
    }, c = {
      keyword: "__CLASS__ __DIR__ __FILE__ __FUNCTION__ __LINE__ __METHOD__ __NAMESPACE__ __TRAIT__ die echo exit include include_once print require require_once array abstract and as binary bool boolean break callable case catch class clone const continue declare default do double else elseif empty enddeclare endfor endforeach endif endswitch endwhile enum eval extends final finally float for foreach from global goto if implements instanceof insteadof int integer interface isset iterable list match|0 mixed new object or private protected public real return string switch throw trait try unset use var void while xor yield",
      literal: "false null true",
      built_in: "Error|0 AppendIterator ArgumentCountError ArithmeticError ArrayIterator ArrayObject AssertionError BadFunctionCallException BadMethodCallException CachingIterator CallbackFilterIterator CompileError Countable DirectoryIterator DivisionByZeroError DomainException EmptyIterator ErrorException Exception FilesystemIterator FilterIterator GlobIterator InfiniteIterator InvalidArgumentException IteratorIterator LengthException LimitIterator LogicException MultipleIterator NoRewindIterator OutOfBoundsException OutOfRangeException OuterIterator OverflowException ParentIterator ParseError RangeException RecursiveArrayIterator RecursiveCachingIterator RecursiveCallbackFilterIterator RecursiveDirectoryIterator RecursiveFilterIterator RecursiveIterator RecursiveIteratorIterator RecursiveRegexIterator RecursiveTreeIterator RegexIterator RuntimeException SeekableIterator SplDoublyLinkedList SplFileInfo SplFileObject SplFixedArray SplHeap SplMaxHeap SplMinHeap SplObjectStorage SplObserver SplObserver SplPriorityQueue SplQueue SplStack SplSubject SplSubject SplTempFileObject TypeError UnderflowException UnexpectedValueException UnhandledMatchError ArrayAccess Closure Generator Iterator IteratorAggregate Serializable Stringable Throwable Traversable WeakReference WeakMap Directory __PHP_Incomplete_Class parent php_user_filter self static stdClass"
    }; return {
      aliases: ["php3", "php4", "php5", "php6", "php7", "php8"],
      case_insensitive: !0, keywords: c,
      contains: [e.HASH_COMMENT_MODE, e.COMMENT("//", "$", {
        contains: [t]
      }), e.COMMENT("/\\*", "\\*/", {
        contains: [{ className: "doctag", begin: "@[A-Za-z]+" }]
      }), e.COMMENT("__halt_compiler.+?;", !1, {
        endsWithParent: !0,
        keywords: "__halt_compiler"
      }), t, { className: "keyword", begin: /\$this\b/ }, r, {
        begin: /(::|->)+[a-zA-Z_\x7f-\xff][a-zA-Z0-9_\x7f-\xff]*/
      }, {
        className: "function",
        relevance: 0, beginKeywords: "fn function", end: /[;{]/, excludeEnd: !0,
        illegal: "[$%\\[]", contains: [{ beginKeywords: "use" }, e.UNDERSCORE_TITLE_MODE, {
          begin: "=>", endsParent: !0
        }, {
          className: "params", begin: "\\(", end: "\\)",
          excludeBegin: !0, excludeEnd: !0, keywords: c,
          contains: ["self", r, e.C_BLOCK_COMMENT_MODE, l, s]
        }]
      }, {
        className: "class", variants: [{
          beginKeywords: "enum", illegal: /[($"]/
        }, {
          beginKeywords: "class interface trait",
          illegal: /[:($"]/
        }], relevance: 0, end: /\{/, excludeEnd: !0, contains: [{
          beginKeywords: "extends implements"
        }, e.UNDERSCORE_TITLE_MODE]
      }, {
        beginKeywords: "namespace", relevance: 0, end: ";", illegal: /[.']/,
        contains: [e.UNDERSCORE_TITLE_MODE]
      }, {
        beginKeywords: "use", relevance: 0, end: ";",
        contains: [e.UNDERSCORE_TITLE_MODE]
      }, l, s]
    }
  }
})());
hljs.registerLanguage("php-template", (() => {
 return n => ({
    name: "PHP template", subLanguage: "xml", contains: [{
      begin: /<\?(php|=)?/, end: /\?>/,
      subLanguage: "php", contains: [{ begin: "/\\*", end: "\\*/", skip: !0 }, {
        begin: 'b"',
        end: '"', skip: !0
      }, { begin: "b'", end: "'", skip: !0 }, n.inherit(n.APOS_STRING_MODE, {
        illegal: null, className: null, contains: null, skip: !0
      }), n.inherit(n.QUOTE_STRING_MODE, {
        illegal: null, className: null, contains: null,
        skip: !0
      })]
    }]
  })
})());
hljs.registerLanguage("plaintext", (() => {
 return t => ({
    name: "Plain text", aliases: ["text", "txt"], disableAutodetect: !0
  })
})());
hljs.registerLanguage("properties", (() => {
 return e => {
    var n = "[ \\t\\f]*", a = n + "[:=]" + n, t = "(" + a + "|[ \\t\\f]+)", r = "([^\\\\\\W:= \\t\\f\\n]|\\\\.)+", s = "([^\\\\:= \\t\\f\\n]|\\\\.)+", i = {
      end: t, relevance: 0, starts: {
        className: "string", end: /$/, relevance: 0, contains: [{
          begin: "\\\\\\\\"
        }, { begin: "\\\\\\n" }]
      }
    }; return {
      name: ".properties",
      case_insensitive: !0, illegal: /\S/, contains: [e.COMMENT("^\\s*[!#]", "$"), {
        returnBegin: !0, variants: [{ begin: r + a, relevance: 1 }, {
          begin: r + "[ \\t\\f]+",
          relevance: 0
        }], contains: [{ className: "attr", begin: r, endsParent: !0, relevance: 0 }],
        starts: i
      }, {
        begin: s + t, returnBegin: !0, relevance: 0, contains: [{
          className: "meta",
          begin: s, endsParent: !0, relevance: 0
        }], starts: i
      }, {
        className: "attr", relevance: 0,
        begin: s + n + "$"
      }]
    }
  }
})());
hljs.registerLanguage("python", (() => {
 return e => {
    const n = {
      $pattern: /[A-Za-z]\w+|__\w+__/,
      keyword: ["and", "as", "assert", "async", "await", "break", "class", "continue", "def", "del", "elif", "else", "except", "finally", "for", "from", "global", "if", "import", "in", "is", "lambda", "nonlocal|10", "not", "or", "pass", "raise", "return", "try", "while", "with", "yield"],
      built_in: ["__import__", "abs", "all", "any", "ascii", "bin", "bool", "breakpoint", "bytearray", "bytes", "callable", "chr", "classmethod", "compile", "complex", "delattr", "dict", "dir", "divmod", "enumerate", "eval", "exec", "filter", "float", "format", "frozenset", "getattr", "globals", "hasattr", "hash", "help", "hex", "id", "input", "int", "isinstance", "issubclass", "iter", "len", "list", "locals", "map", "max", "memoryview", "min", "next", "object", "oct", "open", "ord", "pow", "print", "property", "range", "repr", "reversed", "round", "set", "setattr", "slice", "sorted", "staticmethod", "str", "sum", "super", "tuple", "type", "vars", "zip"],
      literal: ["__debug__", "Ellipsis", "False", "None", "NotImplemented", "True"],
      type: ["Any", "Callable", "Coroutine", "Dict", "List", "Literal", "Generic", "Optional", "Sequence", "Set", "Tuple", "Type", "Union"]
    }, a = { className: "meta", begin: /^(>>>|\.\.\.) / }, i = {
      className: "subst", begin: /\{/,
      end: /\}/, keywords: n, illegal: /#/
    }, s = { begin: /\{\{/, relevance: 0 }, t = {
      className: "string", contains: [e.BACKSLASH_ESCAPE], variants: [{
        begin: /([uU]|[bB]|[rR]|[bB][rR]|[rR][bB])?'''/, end: /'''/,
        contains: [e.BACKSLASH_ESCAPE, a], relevance: 10
      }, {
        begin: /([uU]|[bB]|[rR]|[bB][rR]|[rR][bB])?"""/, end: /"""/,
        contains: [e.BACKSLASH_ESCAPE, a], relevance: 10
      }, {
        begin: /([fF][rR]|[rR][fF]|[fF])'''/, end: /'''/,
        contains: [e.BACKSLASH_ESCAPE, a, s, i]
      }, {
        begin: /([fF][rR]|[rR][fF]|[fF])"""/,
        end: /"""/, contains: [e.BACKSLASH_ESCAPE, a, s, i]
      }, {
        begin: /([uU]|[rR])'/, end: /'/,
        relevance: 10
      }, { begin: /([uU]|[rR])"/, end: /"/, relevance: 10 }, {
        begin: /([bB]|[bB][rR]|[rR][bB])'/, end: /'/
      }, {
        begin: /([bB]|[bB][rR]|[rR][bB])"/,
        end: /"/
      }, {
        begin: /([fF][rR]|[rR][fF]|[fF])'/, end: /'/,
        contains: [e.BACKSLASH_ESCAPE, s, i]
      }, {
        begin: /([fF][rR]|[rR][fF]|[fF])"/, end: /"/,
        contains: [e.BACKSLASH_ESCAPE, s, i]
      }, e.APOS_STRING_MODE, e.QUOTE_STRING_MODE]
    }, r = "[0-9](_?[0-9])*", l = `(\\b(${r}))?\\.(${r})|\\b(${r})\\.`, b = {
      className: "number", relevance: 0, variants: [{
        begin: `(\\b(${r})|(${l}))[eE][+-]?(${r})[jJ]?\\b`
      }, { begin: `(${l})[jJ]?` }, {
        begin: "\\b([1-9](_?[0-9])*|0+(_?0)*)[lLjJ]?\\b"
      }, {
        begin: "\\b0[bB](_?[01])+[lL]?\\b"
      }, { begin: "\\b0[oO](_?[0-7])+[lL]?\\b" }, {
        begin: "\\b0[xX](_?[0-9a-fA-F])+[lL]?\\b"
      }, { begin: `\\b(${r})[jJ]\\b` }]
    }, o = {
      className: "comment",
      begin: (d = /# type:/, ((...e) => e.map((e => (e => e ? "string" == typeof e ? e : e.source : null)(e))).join(""))("(?=", d, ")")),
      end: /$/, keywords: n, contains: [{ begin: /# type:/ }, {
        begin: /#/, end: /\b\B/,
        endsWithParent: !0
      }]
    }, c = {
      className: "params", variants: [{
        className: "",
        begin: /\(\s*\)/, skip: !0
      }, {
        begin: /\(/, end: /\)/, excludeBegin: !0, excludeEnd: !0,
        keywords: n, contains: ["self", a, b, t, e.HASH_COMMENT_MODE]
      }]
    }; var d
      ; return i.contains = [t, b, a], {
        name: "Python", aliases: ["py", "gyp", "ipython"],
        keywords: n, illegal: /(<\/|->|\?)|=>/, contains: [a, b, { begin: /\bself\b/ }, {
          beginKeywords: "if", relevance: 0
        }, t, o, e.HASH_COMMENT_MODE, {
          variants: [{
            className: "function", beginKeywords: "def"
          }, {
            className: "class",
            beginKeywords: "class"
          }], end: /:/, illegal: /[${=;\n,]/,
            contains: [e.UNDERSCORE_TITLE_MODE, c, { begin: /->/, endsWithParent: !0, keywords: n }]
          }, { className: "meta", begin: /^[\t ]*@/, end: /(?=#)|$/, contains: [b, c, t] }]
      }
  }
})());
hljs.registerLanguage("python-repl", (() => {
 return s => ({
    aliases: ["pycon"], contains: [{
      className: "meta", starts: {
        end: / |$/, starts: {
          end: "$",
          subLanguage: "python"
        }
      }, variants: [{ begin: /^>>>(?=[ ]|$)/ }, {
        begin: /^\.\.\.(?=[ ]|$)/
      }]
    }]
  })
})());
hljs.registerLanguage("r", (() => {
 function e(...e) {
    return e.map((e => {
      return (a = e) ? "string" == typeof a ? a : a.source : null; var a;
    })).join("")
  } return a => {
    const n = /(?:(?:[a-zA-Z]|\.[._a-zA-Z])[._a-zA-Z0-9]*)|\.(?!\d)/; return {
      name: "R",
      illegal: /->/, keywords: {
        $pattern: n,
        keyword: "function if in break next repeat else for while",
        literal: "NULL NA TRUE FALSE Inf NaN NA_integer_|10 NA_real_|10 NA_character_|10 NA_complex_|10",
        built_in: "LETTERS letters month.abb month.name pi T F abs acos acosh all any anyNA Arg as.call as.character as.complex as.double as.environment as.integer as.logical as.null.default as.numeric as.raw asin asinh atan atanh attr attributes baseenv browser c call ceiling class Conj cos cosh cospi cummax cummin cumprod cumsum digamma dim dimnames emptyenv exp expression floor forceAndCall gamma gc.time globalenv Im interactive invisible is.array is.atomic is.call is.character is.complex is.double is.environment is.expression is.finite is.function is.infinite is.integer is.language is.list is.logical is.matrix is.na is.name is.nan is.null is.numeric is.object is.pairlist is.raw is.recursive is.single is.symbol lazyLoadDBfetch length lgamma list log max min missing Mod names nargs nzchar oldClass on.exit pos.to.env proc.time prod quote range Re rep retracemem return round seq_along seq_len seq.int sign signif sin sinh sinpi sqrt standardGeneric substitute sum switch tan tanh tanpi tracemem trigamma trunc unclass untracemem UseMethod xtfrm"
      }, compilerExtensions: [(a, n) => {
        if (!a.beforeMatch) return
          ; if (a.starts) throw Error("beforeMatch cannot be used with starts")
            ; const i = Object.assign({}, a); Object.keys(a).forEach((e => {
              delete a[e];
            })), a.begin = e(i.beforeMatch, e("(?=", i.begin, ")")), a.starts = {
              relevance: 0,
              contains: [Object.assign(i, { endsParent: !0 })]
            }, a.relevance = 0, delete i.beforeMatch;
      }], contains: [a.COMMENT(/#'/, /$/, {
        contains: [{
          className: "doctag",
          begin: "@examples", starts: {
            contains: [{ begin: /\n/ }, {
              begin: /#'\s*(?=@[a-zA-Z]+)/,
              endsParent: !0
            }, { begin: /#'/, end: /$/, excludeBegin: !0 }]
          }
        }, {
          className: "doctag",
          begin: "@param", end: /$/, contains: [{
            className: "variable", variants: [{ begin: n }, {
              begin: /`(?:\\.|[^`\\])+`/
            }], endsParent: !0
          }]
        }, {
          className: "doctag",
          begin: /@[a-zA-Z]+/
        }, { className: "meta-keyword", begin: /\\[a-zA-Z]+/ }]
      }), a.HASH_COMMENT_MODE, {
        className: "string", contains: [a.BACKSLASH_ESCAPE],
        variants: [a.END_SAME_AS_BEGIN({
          begin: /[rR]"(-*)\(/, end: /\)(-*)"/
        }), a.END_SAME_AS_BEGIN({
          begin: /[rR]"(-*)\{/, end: /\}(-*)"/
        }), a.END_SAME_AS_BEGIN({
          begin: /[rR]"(-*)\[/, end: /\](-*)"/
        }), a.END_SAME_AS_BEGIN({
          begin: /[rR]'(-*)\(/, end: /\)(-*)'/
        }), a.END_SAME_AS_BEGIN({
          begin: /[rR]'(-*)\{/, end: /\}(-*)'/
        }), a.END_SAME_AS_BEGIN({ begin: /[rR]'(-*)\[/, end: /\](-*)'/ }), {
          begin: '"', end: '"',
          relevance: 0
        }, { begin: "'", end: "'", relevance: 0 }]
      }, {
        className: "number", relevance: 0,
        beforeMatch: /([^a-zA-Z0-9._])/, variants: [{
          match: /0[xX][0-9a-fA-F]+\.[0-9a-fA-F]*[pP][+-]?\d+i?/
        }, {
          match: /0[xX][0-9a-fA-F]+([pP][+-]?\d+)?[Li]?/
        }, {
          match: /(\d+(\.\d*)?|\.\d+)([eE][+-]?\d+)?[Li]?/
        }]
      }, { begin: "%", end: "%" }, {
        begin: e(/[a-zA-Z][a-zA-Z_0-9]*/, "\\s+<-\\s+")
      }, {
        begin: "`", end: "`", contains: [{
          begin: /\\./
        }]
      }]
    }
  }
})());
hljs.registerLanguage("ruby", (() => {
 function e(...e) {
    return e.map((e => {
      return (n = e) ? "string" == typeof n ? n : n.source : null; var n;
    })).join("")
  } return n => {
    const a = "([a-zA-Z_]\\w*[!?=]?|[-+~]@|<<|>>|=~|===?|<=>|[<>]=?|\\*\\*|[-/+%^&*~`|]|\\[\\]=?)", i = {
      keyword: "and then defined module in return redo if BEGIN retry end for self when next until do begin unless END rescue else break undef not super class case require yield alias while ensure elsif or include attr_reader attr_writer attr_accessor __FILE__",
      built_in: "proc lambda", literal: "true false nil"
    }, s = {
      className: "doctag",
      begin: "@[A-Za-z]+"
    }, r = { begin: "#<", end: ">" }, b = [n.COMMENT("#", "$", {
      contains: [s]
    }), n.COMMENT("^=begin", "^=end", {
      contains: [s], relevance: 10
    }), n.COMMENT("^__END__", "\\n$")], c = {
      className: "subst", begin: /#\{/, end: /\}/,
      keywords: i
    }, t = {
      className: "string", contains: [n.BACKSLASH_ESCAPE, c], variants: [{
        begin: /'/, end: /'/
      }, { begin: /"/, end: /"/ }, { begin: /`/, end: /`/ }, {
        begin: /%[qQwWx]?\(/,
        end: /\)/
      }, { begin: /%[qQwWx]?\[/, end: /\]/ }, { begin: /%[qQwWx]?\{/, end: /\}/ }, {
        begin: /%[qQwWx]?</, end: />/
      }, { begin: /%[qQwWx]?\//, end: /\// }, {
        begin: /%[qQwWx]?%/,
        end: /%/
      }, { begin: /%[qQwWx]?-/, end: /-/ }, { begin: /%[qQwWx]?\|/, end: /\|/ }, {
        begin: /\B\?(\\\d{1,3})/
      }, { begin: /\B\?(\\x[A-Fa-f0-9]{1,2})/ }, {
        begin: /\B\?(\\u\{?[A-Fa-f0-9]{1,6}\}?)/
      }, {
        begin: /\B\?(\\M-\\C-|\\M-\\c|\\c\\M-|\\M-|\\C-\\M-)[\x20-\x7e]/
      }, {
        begin: /\B\?\\(c|C-)[\x20-\x7e]/
      }, { begin: /\B\?\\?\S/ }, {
        begin: /<<[-~]?'?(\w+)\n(?:[^\n]*\n)*?\s*\1\b/, returnBegin: !0, contains: [{
          begin: /<<[-~]?'?/
        }, n.END_SAME_AS_BEGIN({
          begin: /(\w+)/, end: /(\w+)/,
          contains: [n.BACKSLASH_ESCAPE, c]
        })]
      }]
    }, g = "[0-9](_?[0-9])*", d = {
      className: "number",
      relevance: 0, variants: [{
        begin: `\\b([1-9](_?[0-9])*|0)(\\.(${g}))?([eE][+-]?(${g})|r)?i?\\b`
      }, {
        begin: "\\b0[dD][0-9](_?[0-9])*r?i?\\b"
      }, {
        begin: "\\b0[bB][0-1](_?[0-1])*r?i?\\b"
      }, { begin: "\\b0[oO][0-7](_?[0-7])*r?i?\\b" }, {
        begin: "\\b0[xX][0-9a-fA-F](_?[0-9a-fA-F])*r?i?\\b"
      }, {
        begin: "\\b0(_?[0-7])+r?i?\\b"
      }]
    }, l = {
      className: "params", begin: "\\(", end: "\\)",
      endsParent: !0, keywords: i
    }, o = [t, {
      className: "class", beginKeywords: "class module",
      end: "$|;", illegal: /=/, contains: [n.inherit(n.TITLE_MODE, {
        begin: "[A-Za-z_]\\w*(::\\w+)*(\\?|!)?"
      }), {
        begin: "<\\s*", contains: [{
          begin: "(" + n.IDENT_RE + "::)?" + n.IDENT_RE, relevance: 0
        }]
      }].concat(b)
    }, {
        className: "function", begin: e(/def\s+/, (_ = a + "\\s*(\\(|;|$)", e("(?=", _, ")"))),
        relevance: 0, keywords: "def", end: "$|;", contains: [n.inherit(n.TITLE_MODE, {
          begin: a
        }), l].concat(b)
      }, { begin: n.IDENT_RE + "::" }, {
        className: "symbol",
        begin: n.UNDERSCORE_IDENT_RE + "(!|\\?)?:", relevance: 0
      }, {
        className: "symbol",
        begin: ":(?!\\s)", contains: [t, { begin: a }], relevance: 0
      }, d, {
        className: "variable",
        begin: "(\\$\\W)|((\\$|@@?)(\\w+))(?=[^@$?])(?![A-Za-z])(?![@$?'])"
      }, {
        className: "params", begin: /\|/, end: /\|/, relevance: 0, keywords: i
      }, {
        begin: "(" + n.RE_STARTERS_RE + "|unless)\\s*", keywords: "unless", contains: [{
          className: "regexp", contains: [n.BACKSLASH_ESCAPE, c], illegal: /\n/, variants: [{
            begin: "/", end: "/[a-z]*"
          }, { begin: /%r\{/, end: /\}[a-z]*/ }, {
            begin: "%r\\(",
            end: "\\)[a-z]*"
          }, { begin: "%r!", end: "![a-z]*" }, { begin: "%r\\[", end: "\\][a-z]*" }]
        }].concat(r, b), relevance: 0
      }].concat(r, b); var _; c.contains = o, l.contains = o
      ; const E = [{ begin: /^\s*=>/, starts: { end: "$", contains: o } }, {
        className: "meta",
        begin: "^([>?]>|[\\w#]+\\(\\w+\\):\\d+:\\d+>|(\\w+-)?\\d+\\.\\d+\\.\\d+(p\\d+)?[^\\d][^>]+>)(?=[ ])",
        starts: { end: "$", contains: o }
      }]; return b.unshift(r), {
        name: "Ruby",
        aliases: ["rb", "gemspec", "podspec", "thor", "irb"], keywords: i, illegal: /\/\*/,
        contains: [n.SHEBANG({ binary: "ruby" })].concat(E).concat(b).concat(o)
      }
  }
})());
hljs.registerLanguage("rust", (() => {
 return e => {
    const n = "([ui](8|16|32|64|128|size)|f(32|64))?", t = "drop i8 i16 i32 i64 i128 isize u8 u16 u32 u64 u128 usize f32 f64 str char bool Box Option Result String Vec Copy Send Sized Sync Drop Fn FnMut FnOnce ToOwned Clone Debug PartialEq PartialOrd Eq Ord AsRef AsMut Into From Default Iterator Extend IntoIterator DoubleEndedIterator ExactSizeIterator SliceConcatExt ToString assert! assert_eq! bitflags! bytes! cfg! col! concat! concat_idents! debug_assert! debug_assert_eq! env! panic! file! format! format_args! include_bin! include_str! line! local_data_key! module_path! option_env! print! println! select! stringify! try! unimplemented! unreachable! vec! write! writeln! macro_rules! assert_ne! debug_assert_ne!"
      ; return {
        name: "Rust", aliases: ["rs"], keywords: {
          $pattern: e.IDENT_RE + "!?",
          keyword: "abstract as async await become box break const continue crate do dyn else enum extern false final fn for if impl in let loop macro match mod move mut override priv pub ref return self Self static struct super trait true try type typeof unsafe unsized use virtual where while yield",
          literal: "true false Some None Ok Err", built_in: t
        }, illegal: "</",
        contains: [e.C_LINE_COMMENT_MODE, e.COMMENT("/\\*", "\\*/", {
          contains: ["self"]
        }), e.inherit(e.QUOTE_STRING_MODE, { begin: /b?"/, illegal: null }), {
          className: "string", variants: [{ begin: /r(#*)"(.|\n)*?"\1(?!#)/ }, {
            begin: /b?'\\?(x\w{2}|u\w{4}|U\w{8}|.)'/
          }]
        }, {
          className: "symbol",
          begin: /'[a-zA-Z_][a-zA-Z0-9_]*/
        }, {
          className: "number", variants: [{
            begin: "\\b0b([01_]+)" + n
          }, { begin: "\\b0o([0-7_]+)" + n }, {
            begin: "\\b0x([A-Fa-f0-9_]+)" + n
          }, {
            begin: "\\b(\\d[\\d_]*(\\.[0-9_]+)?([eE][+-]?[0-9_]+)?)" + n
          }], relevance: 0
        }, {
          className: "function", beginKeywords: "fn", end: "(\\(|<)", excludeEnd: !0,
          contains: [e.UNDERSCORE_TITLE_MODE]
        }, {
          className: "meta", begin: "#!?\\[", end: "\\]",
          contains: [{ className: "meta-string", begin: /"/, end: /"/ }]
        }, {
          className: "class",
          beginKeywords: "type", end: ";", contains: [e.inherit(e.UNDERSCORE_TITLE_MODE, {
            endsParent: !0
          })], illegal: "\\S"
        }, {
          className: "class",
          beginKeywords: "trait enum struct union", end: /\{/,
          contains: [e.inherit(e.UNDERSCORE_TITLE_MODE, { endsParent: !0 })], illegal: "[\\w\\d]"
        }, { begin: e.IDENT_RE + "::", keywords: { built_in: t } }, { begin: "->" }]
      }
  }
})());
hljs.registerLanguage("scss", (() => {
 const e = ["a", "abbr", "address", "article", "aside", "audio", "b", "blockquote", "body", "button", "canvas", "caption", "cite", "code", "dd", "del", "details", "dfn", "div", "dl", "dt", "em", "fieldset", "figcaption", "figure", "footer", "form", "h1", "h2", "h3", "h4", "h5", "h6", "header", "hgroup", "html", "i", "iframe", "img", "input", "ins", "kbd", "label", "legend", "li", "main", "mark", "menu", "nav", "object", "ol", "p", "q", "quote", "samp", "section", "span", "strong", "summary", "sup", "table", "tbody", "td", "textarea", "tfoot", "th", "thead", "time", "tr", "ul", "var", "video"], t = ["any-hover", "any-pointer", "aspect-ratio", "color", "color-gamut", "color-index", "device-aspect-ratio", "device-height", "device-width", "display-mode", "forced-colors", "grid", "height", "hover", "inverted-colors", "monochrome", "orientation", "overflow-block", "overflow-inline", "pointer", "prefers-color-scheme", "prefers-contrast", "prefers-reduced-motion", "prefers-reduced-transparency", "resolution", "scan", "scripting", "update", "width", "min-width", "max-width", "min-height", "max-height"], i = ["active", "any-link", "blank", "checked", "current", "default", "defined", "dir", "disabled", "drop", "empty", "enabled", "first", "first-child", "first-of-type", "fullscreen", "future", "focus", "focus-visible", "focus-within", "has", "host", "host-context", "hover", "indeterminate", "in-range", "invalid", "is", "lang", "last-child", "last-of-type", "left", "link", "local-link", "not", "nth-child", "nth-col", "nth-last-child", "nth-last-col", "nth-last-of-type", "nth-of-type", "only-child", "only-of-type", "optional", "out-of-range", "past", "placeholder-shown", "read-only", "read-write", "required", "right", "root", "scope", "target", "target-within", "user-invalid", "valid", "visited", "where"], o = ["after", "backdrop", "before", "cue", "cue-region", "first-letter", "first-line", "grammar-error", "marker", "part", "placeholder", "selection", "slotted", "spelling-error"], r = ["align-content", "align-items", "align-self", "animation", "animation-delay", "animation-direction", "animation-duration", "animation-fill-mode", "animation-iteration-count", "animation-name", "animation-play-state", "animation-timing-function", "auto", "backface-visibility", "background", "background-attachment", "background-clip", "background-color", "background-image", "background-origin", "background-position", "background-repeat", "background-size", "border", "border-bottom", "border-bottom-color", "border-bottom-left-radius", "border-bottom-right-radius", "border-bottom-style", "border-bottom-width", "border-collapse", "border-color", "border-image", "border-image-outset", "border-image-repeat", "border-image-slice", "border-image-source", "border-image-width", "border-left", "border-left-color", "border-left-style", "border-left-width", "border-radius", "border-right", "border-right-color", "border-right-style", "border-right-width", "border-spacing", "border-style", "border-top", "border-top-color", "border-top-left-radius", "border-top-right-radius", "border-top-style", "border-top-width", "border-width", "bottom", "box-decoration-break", "box-shadow", "box-sizing", "break-after", "break-before", "break-inside", "caption-side", "clear", "clip", "clip-path", "color", "column-count", "column-fill", "column-gap", "column-rule", "column-rule-color", "column-rule-style", "column-rule-width", "column-span", "column-width", "columns", "content", "counter-increment", "counter-reset", "cursor", "direction", "display", "empty-cells", "filter", "flex", "flex-basis", "flex-direction", "flex-flow", "flex-grow", "flex-shrink", "flex-wrap", "float", "font", "font-display", "font-family", "font-feature-settings", "font-kerning", "font-language-override", "font-size", "font-size-adjust", "font-smoothing", "font-stretch", "font-style", "font-variant", "font-variant-ligatures", "font-variation-settings", "font-weight", "height", "hyphens", "icon", "image-orientation", "image-rendering", "image-resolution", "ime-mode", "inherit", "initial", "justify-content", "left", "letter-spacing", "line-height", "list-style", "list-style-image", "list-style-position", "list-style-type", "margin", "margin-bottom", "margin-left", "margin-right", "margin-top", "marks", "mask", "max-height", "max-width", "min-height", "min-width", "nav-down", "nav-index", "nav-left", "nav-right", "nav-up", "none", "normal", "object-fit", "object-position", "opacity", "order", "orphans", "outline", "outline-color", "outline-offset", "outline-style", "outline-width", "overflow", "overflow-wrap", "overflow-x", "overflow-y", "padding", "padding-bottom", "padding-left", "padding-right", "padding-top", "page-break-after", "page-break-before", "page-break-inside", "perspective", "perspective-origin", "pointer-events", "position", "quotes", "resize", "right", "src", "tab-size", "table-layout", "text-align", "text-align-last", "text-decoration", "text-decoration-color", "text-decoration-line", "text-decoration-style", "text-indent", "text-overflow", "text-rendering", "text-shadow", "text-transform", "text-underline-position", "top", "transform", "transform-origin", "transform-style", "transition", "transition-delay", "transition-duration", "transition-property", "transition-timing-function", "unicode-bidi", "vertical-align", "visibility", "white-space", "widows", "width", "word-break", "word-spacing", "word-wrap", "z-index"].reverse()
    ; return a => {
      const n = (e => ({
        IMPORTANT: { className: "meta", begin: "!important" },
        HEXCOLOR: { className: "number", begin: "#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})" },
        ATTRIBUTE_SELECTOR_MODE: {
          className: "selector-attr", begin: /\[/, end: /\]/,
          illegal: "$", contains: [e.APOS_STRING_MODE, e.QUOTE_STRING_MODE]
        }
      }))(a), l = o, s = i, d = "@[a-z-]+", c = {
        className: "variable",
        begin: "(\\$[a-zA-Z-][a-zA-Z0-9_-]*)\\b"
      }; return {
        name: "SCSS", case_insensitive: !0,
        illegal: "[=/|']", contains: [a.C_LINE_COMMENT_MODE, a.C_BLOCK_COMMENT_MODE, {
          className: "selector-id", begin: "#[A-Za-z0-9_-]+", relevance: 0
        }, {
          className: "selector-class", begin: "\\.[A-Za-z0-9_-]+", relevance: 0
        }, n.ATTRIBUTE_SELECTOR_MODE, {
          className: "selector-tag",
          begin: "\\b(" + e.join("|") + ")\\b", relevance: 0
        }, {
          className: "selector-pseudo",
          begin: ":(" + s.join("|") + ")"
        }, {
          className: "selector-pseudo",
          begin: "::(" + l.join("|") + ")"
        }, c, {
          begin: /\(/, end: /\)/, contains: [a.CSS_NUMBER_MODE]
        }, { className: "attribute", begin: "\\b(" + r.join("|") + ")\\b" }, {
          begin: "\\b(whitespace|wait|w-resize|visible|vertical-text|vertical-ideographic|uppercase|upper-roman|upper-alpha|underline|transparent|top|thin|thick|text|text-top|text-bottom|tb-rl|table-header-group|table-footer-group|sw-resize|super|strict|static|square|solid|small-caps|separate|se-resize|scroll|s-resize|rtl|row-resize|ridge|right|repeat|repeat-y|repeat-x|relative|progress|pointer|overline|outside|outset|oblique|nowrap|not-allowed|normal|none|nw-resize|no-repeat|no-drop|newspaper|ne-resize|n-resize|move|middle|medium|ltr|lr-tb|lowercase|lower-roman|lower-alpha|loose|list-item|line|line-through|line-edge|lighter|left|keep-all|justify|italic|inter-word|inter-ideograph|inside|inset|inline|inline-block|inherit|inactive|ideograph-space|ideograph-parenthesis|ideograph-numeric|ideograph-alpha|horizontal|hidden|help|hand|groove|fixed|ellipsis|e-resize|double|dotted|distribute|distribute-space|distribute-letter|distribute-all-lines|disc|disabled|default|decimal|dashed|crosshair|collapse|col-resize|circle|char|center|capitalize|break-word|break-all|bottom|both|bolder|bold|block|bidi-override|below|baseline|auto|always|all-scroll|absolute|table|table-cell)\\b"
        }, {
          begin: ":", end: ";",
          contains: [c, n.HEXCOLOR, a.CSS_NUMBER_MODE, a.QUOTE_STRING_MODE, a.APOS_STRING_MODE, n.IMPORTANT]
        }, { begin: "@(page|font-face)", lexemes: d, keywords: "@page @font-face" }, {
          begin: "@",
          end: "[{;]", returnBegin: !0, keywords: {
            $pattern: /[a-z-]+/,
            keyword: "and or not only", attribute: t.join(" ")
          }, contains: [{
            begin: d,
            className: "keyword"
          }, {
            begin: /[a-z-]+(?=:)/, className: "attribute"
          }, c, a.QUOTE_STRING_MODE, a.APOS_STRING_MODE, n.HEXCOLOR, a.CSS_NUMBER_MODE]
        }]
      }
    }
})());
hljs.registerLanguage("shell", (() => {
 return s => ({
    name: "Shell Session", aliases: ["console"], contains: [{
      className: "meta",
      begin: /^\s{0,3}[/~\w\d[\]()@-]*[>%$#]/, starts: {
        end: /[^\\](?=\s*$)/,
        subLanguage: "bash"
      }
    }]
  })
})());
hljs.registerLanguage("sql", (() => {
 function e(e) {
    return e ? "string" == typeof e ? e : e.source : null
  } function r(...r) {
    return r.map((r => e(r))).join("")
  } function t(...r) {
    return "(" + r.map((r => e(r))).join("|") + ")"
  } return e => {
    const n = e.COMMENT("--", "$"), a = ["true", "false", "unknown"], i = ["bigint", "binary", "blob", "boolean", "char", "character", "clob", "date", "dec", "decfloat", "decimal", "float", "int", "integer", "interval", "nchar", "nclob", "national", "numeric", "real", "row", "smallint", "time", "timestamp", "varchar", "varying", "varbinary"], s = ["abs", "acos", "array_agg", "asin", "atan", "avg", "cast", "ceil", "ceiling", "coalesce", "corr", "cos", "cosh", "count", "covar_pop", "covar_samp", "cume_dist", "dense_rank", "deref", "element", "exp", "extract", "first_value", "floor", "json_array", "json_arrayagg", "json_exists", "json_object", "json_objectagg", "json_query", "json_table", "json_table_primitive", "json_value", "lag", "last_value", "lead", "listagg", "ln", "log", "log10", "lower", "max", "min", "mod", "nth_value", "ntile", "nullif", "percent_rank", "percentile_cont", "percentile_disc", "position", "position_regex", "power", "rank", "regr_avgx", "regr_avgy", "regr_count", "regr_intercept", "regr_r2", "regr_slope", "regr_sxx", "regr_sxy", "regr_syy", "row_number", "sin", "sinh", "sqrt", "stddev_pop", "stddev_samp", "substring", "substring_regex", "sum", "tan", "tanh", "translate", "translate_regex", "treat", "trim", "trim_array", "unnest", "upper", "value_of", "var_pop", "var_samp", "width_bucket"], o = ["create table", "insert into", "primary key", "foreign key", "not null", "alter table", "add constraint", "grouping sets", "on overflow", "character set", "respect nulls", "ignore nulls", "nulls first", "nulls last", "depth first", "breadth first"], c = s, l = ["abs", "acos", "all", "allocate", "alter", "and", "any", "are", "array", "array_agg", "array_max_cardinality", "as", "asensitive", "asin", "asymmetric", "at", "atan", "atomic", "authorization", "avg", "begin", "begin_frame", "begin_partition", "between", "bigint", "binary", "blob", "boolean", "both", "by", "call", "called", "cardinality", "cascaded", "case", "cast", "ceil", "ceiling", "char", "char_length", "character", "character_length", "check", "classifier", "clob", "close", "coalesce", "collate", "collect", "column", "commit", "condition", "connect", "constraint", "contains", "convert", "copy", "corr", "corresponding", "cos", "cosh", "count", "covar_pop", "covar_samp", "create", "cross", "cube", "cume_dist", "current", "current_catalog", "current_date", "current_default_transform_group", "current_path", "current_role", "current_row", "current_schema", "current_time", "current_timestamp", "current_path", "current_role", "current_transform_group_for_type", "current_user", "cursor", "cycle", "date", "day", "deallocate", "dec", "decimal", "decfloat", "declare", "default", "define", "delete", "dense_rank", "deref", "describe", "deterministic", "disconnect", "distinct", "double", "drop", "dynamic", "each", "element", "else", "empty", "end", "end_frame", "end_partition", "end-exec", "equals", "escape", "every", "except", "exec", "execute", "exists", "exp", "external", "extract", "false", "fetch", "filter", "first_value", "float", "floor", "for", "foreign", "frame_row", "free", "from", "full", "function", "fusion", "get", "global", "grant", "group", "grouping", "groups", "having", "hold", "hour", "identity", "in", "indicator", "initial", "inner", "inout", "insensitive", "insert", "int", "integer", "intersect", "intersection", "interval", "into", "is", "join", "json_array", "json_arrayagg", "json_exists", "json_object", "json_objectagg", "json_query", "json_table", "json_table_primitive", "json_value", "lag", "language", "large", "last_value", "lateral", "lead", "leading", "left", "like", "like_regex", "listagg", "ln", "local", "localtime", "localtimestamp", "log", "log10", "lower", "match", "match_number", "match_recognize", "matches", "max", "member", "merge", "method", "min", "minute", "mod", "modifies", "module", "month", "multiset", "national", "natural", "nchar", "nclob", "new", "no", "none", "normalize", "not", "nth_value", "ntile", "null", "nullif", "numeric", "octet_length", "occurrences_regex", "of", "offset", "old", "omit", "on", "one", "only", "open", "or", "order", "out", "outer", "over", "overlaps", "overlay", "parameter", "partition", "pattern", "per", "percent", "percent_rank", "percentile_cont", "percentile_disc", "period", "portion", "position", "position_regex", "power", "precedes", "precision", "prepare", "primary", "procedure", "ptf", "range", "rank", "reads", "real", "recursive", "ref", "references", "referencing", "regr_avgx", "regr_avgy", "regr_count", "regr_intercept", "regr_r2", "regr_slope", "regr_sxx", "regr_sxy", "regr_syy", "release", "result", "return", "returns", "revoke", "right", "rollback", "rollup", "row", "row_number", "rows", "running", "savepoint", "scope", "scroll", "search", "second", "seek", "select", "sensitive", "session_user", "set", "show", "similar", "sin", "sinh", "skip", "smallint", "some", "specific", "specifictype", "sql", "sqlexception", "sqlstate", "sqlwarning", "sqrt", "start", "static", "stddev_pop", "stddev_samp", "submultiset", "subset", "substring", "substring_regex", "succeeds", "sum", "symmetric", "system", "system_time", "system_user", "table", "tablesample", "tan", "tanh", "then", "time", "timestamp", "timezone_hour", "timezone_minute", "to", "trailing", "translate", "translate_regex", "translation", "treat", "trigger", "trim", "trim_array", "true", "truncate", "uescape", "union", "unique", "unknown", "unnest", "update   ", "upper", "user", "using", "value", "values", "value_of", "var_pop", "var_samp", "varbinary", "varchar", "varying", "versioning", "when", "whenever", "where", "width_bucket", "window", "with", "within", "without", "year", "add", "asc", "collation", "desc", "final", "first", "last", "view"].filter((e => !s.includes(e))), u = {
      begin: r(/\b/, t(...c), /\s*\(/), keywords: { built_in: c }
    }; return {
      name: "SQL",
      case_insensitive: !0, illegal: /[{}]|<\//, keywords: {
        $pattern: /\b[\w\.]+/,
        keyword: ((e, { exceptions: r, when: t } = {}) => {
          const n = t
          ; return r = r || [], e.map((e => e.match(/\|\d+$/) || r.includes(e) ? e : n(e) ? e + "|0" : e))
        })(l, { when: e => e.length < 3 }), literal: a, type: i,
        built_in: ["current_catalog", "current_date", "current_default_transform_group", "current_path", "current_role", "current_schema", "current_transform_group_for_type", "current_user", "session_user", "system_time", "system_user", "current_time", "localtime", "current_timestamp", "localtimestamp"]
      }, contains: [{
        begin: t(...o), keywords: {
          $pattern: /[\w\.]+/, keyword: l.concat(o),
          literal: a, type: i
        }
      }, {
        className: "type",
        begin: t("double precision", "large object", "with timezone", "without timezone")
      }, u, { className: "variable", begin: /@[a-z0-9]+/ }, {
        className: "string", variants: [{
          begin: /'/, end: /'/, contains: [{ begin: /''/ }]
        }]
      }, {
        begin: /"/, end: /"/, contains: [{
          begin: /""/
        }]
      }, e.C_NUMBER_MODE, e.C_BLOCK_COMMENT_MODE, n, {
        className: "operator",
        begin: /[-+*/=%^~]|&&?|\|\|?|!=?|<(?:=>?|<|>)?|>[>=]?/, relevance: 0
      }]
    }
  }
})());
hljs.registerLanguage("swift", (() => {
 function e(e) {
    return e ? "string" == typeof e ? e : e.source : null
  } function n(e) { return a("(?=", e, ")") }
  function a(...n) { return n.map((n => e(n))).join("") } function t(...n) {
    return "(" + n.map((n => e(n))).join("|") + ")"
  }
  const i = e => a(/\b/, e, /\w$/.test(e) ? /\b/ : /\B/), s = ["Protocol", "Type"].map(i), u = ["init", "self"].map(i), c = ["Any", "Self"], r = ["associatedtype", "async", "await", /as\?/, /as!/, "as", "break", "case", "catch", "class", "continue", "convenience", "default", "defer", "deinit", "didSet", "do", "dynamic", "else", "enum", "extension", "fallthrough", /fileprivate\(set\)/, "fileprivate", "final", "for", "func", "get", "guard", "if", "import", "indirect", "infix", /init\?/, /init!/, "inout", /internal\(set\)/, "internal", "in", "is", "lazy", "let", "mutating", "nonmutating", /open\(set\)/, "open", "operator", "optional", "override", "postfix", "precedencegroup", "prefix", /private\(set\)/, "private", "protocol", /public\(set\)/, "public", "repeat", "required", "rethrows", "return", "set", "some", "static", "struct", "subscript", "super", "switch", "throws", "throw", /try\?/, /try!/, "try", "typealias", /unowned\(safe\)/, /unowned\(unsafe\)/, "unowned", "var", "weak", "where", "while", "willSet"], o = ["false", "nil", "true"], l = ["assignment", "associativity", "higherThan", "left", "lowerThan", "none", "right"], m = ["#colorLiteral", "#column", "#dsohandle", "#else", "#elseif", "#endif", "#error", "#file", "#fileID", "#fileLiteral", "#filePath", "#function", "#if", "#imageLiteral", "#keyPath", "#line", "#selector", "#sourceLocation", "#warn_unqualified_access", "#warning"], d = ["abs", "all", "any", "assert", "assertionFailure", "debugPrint", "dump", "fatalError", "getVaList", "isKnownUniquelyReferenced", "max", "min", "numericCast", "pointwiseMax", "pointwiseMin", "precondition", "preconditionFailure", "print", "readLine", "repeatElement", "sequence", "stride", "swap", "swift_unboxFromSwiftValueWithType", "transcode", "type", "unsafeBitCast", "unsafeDowncast", "withExtendedLifetime", "withUnsafeMutablePointer", "withUnsafePointer", "withVaList", "withoutActuallyEscaping", "zip"], p = t(/[/=\-+!*%<>&|^~?]/, /[\u00A1-\u00A7]/, /[\u00A9\u00AB]/, /[\u00AC\u00AE]/, /[\u00B0\u00B1]/, /[\u00B6\u00BB\u00BF\u00D7\u00F7]/, /[\u2016-\u2017]/, /[\u2020-\u2027]/, /[\u2030-\u203E]/, /[\u2041-\u2053]/, /[\u2055-\u205E]/, /[\u2190-\u23FF]/, /[\u2500-\u2775]/, /[\u2794-\u2BFF]/, /[\u2E00-\u2E7F]/, /[\u3001-\u3003]/, /[\u3008-\u3020]/, /[\u3030]/), F = t(p, /[\u0300-\u036F]/, /[\u1DC0-\u1DFF]/, /[\u20D0-\u20FF]/, /[\uFE00-\uFE0F]/, /[\uFE20-\uFE2F]/), b = a(p, F, "*"), h = t(/[a-zA-Z_]/, /[\u00A8\u00AA\u00AD\u00AF\u00B2-\u00B5\u00B7-\u00BA]/, /[\u00BC-\u00BE\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u00FF]/, /[\u0100-\u02FF\u0370-\u167F\u1681-\u180D\u180F-\u1DBF]/, /[\u1E00-\u1FFF]/, /[\u200B-\u200D\u202A-\u202E\u203F-\u2040\u2054\u2060-\u206F]/, /[\u2070-\u20CF\u2100-\u218F\u2460-\u24FF\u2776-\u2793]/, /[\u2C00-\u2DFF\u2E80-\u2FFF]/, /[\u3004-\u3007\u3021-\u302F\u3031-\u303F\u3040-\uD7FF]/, /[\uF900-\uFD3D\uFD40-\uFDCF\uFDF0-\uFE1F\uFE30-\uFE44]/, /[\uFE47-\uFEFE\uFF00-\uFFFD]/), f = t(h, /\d/, /[\u0300-\u036F\u1DC0-\u1DFF\u20D0-\u20FF\uFE20-\uFE2F]/), w = a(h, f, "*"), y = a(/[A-Z]/, f, "*"), g = ["autoclosure", a(/convention\(/, t("swift", "block", "c"), /\)/), "discardableResult", "dynamicCallable", "dynamicMemberLookup", "escaping", "frozen", "GKInspectable", "IBAction", "IBDesignable", "IBInspectable", "IBOutlet", "IBSegueAction", "inlinable", "main", "nonobjc", "NSApplicationMain", "NSCopying", "NSManaged", a(/objc\(/, w, /\)/), "objc", "objcMembers", "propertyWrapper", "requires_stored_property_inits", "testable", "UIApplicationMain", "unknown", "usableFromInline"], E = ["iOS", "iOSApplicationExtension", "macOS", "macOSApplicationExtension", "macCatalyst", "macCatalystApplicationExtension", "watchOS", "watchOSApplicationExtension", "tvOS", "tvOSApplicationExtension", "swift"]
    ; return e => {
      const p = { match: /\s+/, relevance: 0 }, h = e.COMMENT("/\\*", "\\*/", {
        contains: ["self"]
      }), v = [e.C_LINE_COMMENT_MODE, h], N = {
        className: "keyword",
        begin: a(/\./, n(t(...s, ...u))), end: t(...s, ...u), excludeBegin: !0
      }, A = {
        match: a(/\./, t(...r)), relevance: 0
      }, C = r.filter((e => "string" == typeof e)).concat(["_|0"]), _ = {
        variants: [{
          className: "keyword",
          match: t(...r.filter((e => "string" != typeof e)).concat(c).map(i), ...u)
        }]
      }, D = {
        $pattern: t(/\b\w+/, /#\w+/), keyword: C.concat(m), literal: o
      }, B = [N, A, _], k = [{
        match: a(/\./, t(...d)), relevance: 0
      }, {
        className: "built_in",
        match: a(/\b/, t(...d), /(?=\()/)
      }], M = { match: /->/, relevance: 0 }, S = [M, {
        className: "operator", relevance: 0, variants: [{ match: b }, { match: `\\.(\\.|${F})+` }]
      }], x = "([0-9a-fA-F]_*)+", I = {
        className: "number", relevance: 0, variants: [{
          match: "\\b(([0-9]_*)+)(\\.(([0-9]_*)+))?([eE][+-]?(([0-9]_*)+))?\\b"
        }, {
          match: `\\b0x(${x})(\\.(${x}))?([pP][+-]?(([0-9]_*)+))?\\b`
        }, {
          match: /\b0o([0-7]_*)+\b/
        }, { match: /\b0b([01]_*)+\b/ }]
      }, O = (e = "") => ({
        className: "subst", variants: [{ match: a(/\\/, e, /[0\\tnr"']/) }, {
          match: a(/\\/, e, /u\{[0-9a-fA-F]{1,8}\}/)
        }]
      }), T = (e = "") => ({
        className: "subst",
        match: a(/\\/, e, /[\t ]*(?:[\r\n]|\r\n)/)
      }), L = (e = "") => ({
        className: "subst",
        label: "interpol", begin: a(/\\/, e, /\(/), end: /\)/
      }), P = (e = "") => ({
        begin: a(e, /"""/),
        end: a(/"""/, e), contains: [O(e), T(e), L(e)]
      }), $ = (e = "") => ({
        begin: a(e, /"/),
        end: a(/"/, e), contains: [O(e), L(e)]
      }), K = {
        className: "string",
        variants: [P(), P("#"), P("##"), P("###"), $(), $("#"), $("##"), $("###")]
      }, j = {
        match: a(/`/, w, /`/)
      }, z = [j, { className: "variable", match: /\$\d+/ }, {
        className: "variable", match: `\\$${f}+`
      }], q = [{
        match: /(@|#)available/,
        className: "keyword", starts: {
          contains: [{
            begin: /\(/, end: /\)/, keywords: E,
            contains: [...S, I, K]
          }]
        }
      }, { className: "keyword", match: a(/@/, t(...g)) }, {
        className: "meta", match: a(/@/, w)
      }], U = {
        match: n(/\b[A-Z]/), relevance: 0, contains: [{
          className: "type",
          match: a(/(AV|CA|CF|CG|CI|CL|CM|CN|CT|MK|MP|MTK|MTL|NS|SCN|SK|UI|WK|XC)/, f, "+")
        }, { className: "type", match: y, relevance: 0 }, { match: /[?!]+/, relevance: 0 }, {
          match: /\.\.\./, relevance: 0
        }, { match: a(/\s+&\s+/, n(y)), relevance: 0 }]
      }, Z = {
        begin: /</, end: />/, keywords: D, contains: [...v, ...B, ...q, M, U]
      }; U.contains.push(Z)
        ; const G = {
          begin: /\(/, end: /\)/, relevance: 0, keywords: D, contains: ["self", {
            match: a(w, /\s*:/), keywords: "_|0", relevance: 0
          }, ...v, ...B, ...k, ...S, I, K, ...z, ...q, U]
        }, H = {
          beginKeywords: "func", contains: [{
            className: "title", match: t(j.match, w, b), endsParent: !0, relevance: 0
          }, p]
        }, R = {
          begin: /</, end: />/, contains: [...v, U]
        }, V = {
          begin: /\(/, end: /\)/, keywords: D,
          contains: [{
            begin: t(n(a(w, /\s*:/)), n(a(w, /\s+/, w, /\s*:/))), end: /:/, relevance: 0,
            contains: [{ className: "keyword", match: /\b_\b/ }, { className: "params", match: w }]
          }, ...v, ...B, ...S, I, K, ...q, U, G], endsParent: !0, illegal: /["']/
        }, W = {
          className: "function", match: n(/\bfunc\b/), contains: [H, R, V, p], illegal: [/\[/, /%/]
        }, X = {
          className: "function", match: /\b(subscript|init[?!]?)\s*(?=[<(])/, keywords: {
            keyword: "subscript init init? init!", $pattern: /\w+[?!]?/
          }, contains: [R, V, p],
          illegal: /\[|%/
        }, J = {
          beginKeywords: "operator", end: e.MATCH_NOTHING_RE, contains: [{
            className: "title", match: b, endsParent: !0, relevance: 0
          }]
        }, Q = {
          beginKeywords: "precedencegroup", end: e.MATCH_NOTHING_RE, contains: [{
            className: "title", match: y, relevance: 0
          }, {
            begin: /{/, end: /}/, relevance: 0,
            endsParent: !0, keywords: [...l, ...o], contains: [U]
          }]
        }; for (const e of K.variants) {
          const n = e.contains.find((e => "interpol" === e.label)); n.keywords = D
            ; const a = [...B, ...k, ...S, I, K, ...z]; n.contains = [...a, {
              begin: /\(/, end: /\)/,
              contains: ["self", ...a]
            }];
        } return {
          name: "Swift", keywords: D, contains: [...v, W, X, {
            className: "class", beginKeywords: "struct protocol class extension enum",
            end: "\\{", excludeEnd: !0, keywords: D, contains: [e.inherit(e.TITLE_MODE, {
              begin: /[A-Za-z$_][\u00C0-\u02B80-9A-Za-z$_]*/
            }), ...B]
          }, J, Q, {
            beginKeywords: "import", end: /$/, contains: [...v], relevance: 0
          }, ...B, ...k, ...S, I, K, ...z, ...q, U, G]
        }
    }
})());
hljs.registerLanguage("typescript", (() => {
 const e = "[A-Za-z$_][0-9A-Za-z$_]*", n = ["as", "in", "of", "if", "for", "while", "finally", "var", "new", "function", "do", "return", "void", "else", "break", "catch", "instanceof", "with", "throw", "case", "default", "try", "switch", "continue", "typeof", "delete", "let", "yield", "const", "class", "debugger", "async", "await", "static", "import", "from", "export", "extends"], a = ["true", "false", "null", "undefined", "NaN", "Infinity"], s = [].concat(["setInterval", "setTimeout", "clearInterval", "clearTimeout", "require", "exports", "eval", "isFinite", "isNaN", "parseFloat", "parseInt", "decodeURI", "decodeURIComponent", "encodeURI", "encodeURIComponent", "escape", "unescape"], ["arguments", "this", "super", "console", "window", "document", "localStorage", "module", "global"], ["Intl", "DataView", "Number", "Math", "Date", "String", "RegExp", "Object", "Function", "Boolean", "Error", "Symbol", "Set", "Map", "WeakSet", "WeakMap", "Proxy", "Reflect", "JSON", "Promise", "Float64Array", "Int16Array", "Int32Array", "Int8Array", "Uint16Array", "Uint32Array", "Float32Array", "Array", "Uint8Array", "Uint8ClampedArray", "ArrayBuffer", "BigInt64Array", "BigUint64Array", "BigInt"], ["EvalError", "InternalError", "RangeError", "ReferenceError", "SyntaxError", "TypeError", "URIError"])
    ; function t(e) { return r("(?=", e, ")") } function r(...e) {
      return e.map((e => {
        return (n = e) ? "string" == typeof n ? n : n.source : null; var n;
      })).join("")
    } return i => {
      const c = {
        $pattern: e,
        keyword: n.concat(["type", "namespace", "typedef", "interface", "public", "private", "protected", "implements", "declare", "abstract", "readonly"]),
        literal: a,
        built_in: s.concat(["any", "void", "number", "boolean", "string", "object", "never", "enum"])
      }, o = { className: "meta", begin: "@[A-Za-z$_][0-9A-Za-z$_]*" }, l = (e, n, a) => {
        const s = e.contains.findIndex((e => e.label === n))
          ; if (-1 === s) throw Error("can not find mode to replace"); e.contains.splice(s, 1, a);
      }, b = (i => {
        const c = e, o = {
          begin: /<[A-Za-z0-9\\._:-]+/,
          end: /\/[A-Za-z0-9\\._:-]+>|\/>/, isTrulyOpeningTag: (e, n) => {
            const a = e[0].length + e.index, s = e.input[a]; "<" !== s ? ">" === s && (((e, { after: n }) => {
              const a = "</" + e[0].slice(1); return -1 !== e.input.indexOf(a, n)
            })(e, {
              after: a
            }) || n.ignoreMatch()) : n.ignoreMatch();
          }
        }, l = {
          $pattern: e, keyword: n, literal: a,
          built_in: s
        }, b = "\\.([0-9](_?[0-9])*)", d = "0|[1-9](_?[0-9])*|0[0-7]*[89][0-9]*", g = {
          className: "number", variants: [{
            begin: `(\\b(${d})((${b})|\\.)?|(${b}))[eE][+-]?([0-9](_?[0-9])*)\\b`
          }, {
            begin: `\\b(${d})\\b((${b})\\b|\\.)?|(${b})\\b`
          }, {
            begin: "\\b(0|[1-9](_?[0-9])*)n\\b"
          }, {
            begin: "\\b0[xX][0-9a-fA-F](_?[0-9a-fA-F])*n?\\b"
          }, {
            begin: "\\b0[bB][0-1](_?[0-1])*n?\\b"
          }, { begin: "\\b0[oO][0-7](_?[0-7])*n?\\b" }, {
            begin: "\\b0[0-7]+n?\\b"
          }], relevance: 0
        }, u = {
          className: "subst", begin: "\\$\\{",
          end: "\\}", keywords: l, contains: []
        }, E = {
          begin: "html`", end: "", starts: {
            end: "`",
            returnEnd: !1, contains: [i.BACKSLASH_ESCAPE, u], subLanguage: "xml"
          }
        }, m = {
          begin: "css`", end: "", starts: {
            end: "`", returnEnd: !1,
            contains: [i.BACKSLASH_ESCAPE, u], subLanguage: "css"
          }
        }, y = {
          className: "string",
          begin: "`", end: "`", contains: [i.BACKSLASH_ESCAPE, u]
        }, _ = {
          className: "comment",
          variants: [i.COMMENT(/\/\*\*(?!\/)/, "\\*/", {
            relevance: 0, contains: [{
              className: "doctag", begin: "@[A-Za-z]+", contains: [{
                className: "type", begin: "\\{",
                end: "\\}", relevance: 0
              }, {
                className: "variable", begin: c + "(?=\\s*(-)|$)",
                endsParent: !0, relevance: 0
              }, { begin: /(?=[^\n])\s/, relevance: 0 }]
            }]
          }), i.C_BLOCK_COMMENT_MODE, i.C_LINE_COMMENT_MODE]
        }, p = [i.APOS_STRING_MODE, i.QUOTE_STRING_MODE, E, m, y, g, i.REGEXP_MODE]
        ; u.contains = p.concat({
          begin: /\{/, end: /\}/, keywords: l, contains: ["self"].concat(p)
        }); const N = [].concat(_, u.contains), f = N.concat([{
          begin: /\(/, end: /\)/, keywords: l,
          contains: ["self"].concat(N)
        }]), A = {
          className: "params", begin: /\(/, end: /\)/,
          excludeBegin: !0, excludeEnd: !0, keywords: l, contains: f
        }; return {
          name: "Javascript",
          aliases: ["js", "jsx", "mjs", "cjs"], keywords: l, exports: { PARAMS_CONTAINS: f },
          illegal: /#(?![$_A-z])/, contains: [i.SHEBANG({
            label: "shebang", binary: "node",
            relevance: 5
          }), {
            label: "use_strict", className: "meta", relevance: 10,
            begin: /^\s*['"]use (strict|asm)['"]/
          }, i.APOS_STRING_MODE, i.QUOTE_STRING_MODE, E, m, y, _, g, {
            begin: r(/[{,\n]\s*/, t(r(/(((\/\/.*$)|(\/\*(\*[^/]|[^*])*\*\/))\s*)*/, c + "\\s*:"))),
            relevance: 0, contains: [{ className: "attr", begin: c + t("\\s*:"), relevance: 0 }]
          }, {
            begin: "(" + i.RE_STARTERS_RE + "|\\b(case|return|throw)\\b)\\s*",
            keywords: "return throw case", contains: [_, i.REGEXP_MODE, {
              className: "function",
              begin: "(\\([^()]*(\\([^()]*(\\([^()]*\\)[^()]*)*\\)[^()]*)*\\)|" + i.UNDERSCORE_IDENT_RE + ")\\s*=>",
              returnBegin: !0, end: "\\s*=>", contains: [{
                className: "params", variants: [{
                  begin: i.UNDERSCORE_IDENT_RE, relevance: 0
                }, {
                  className: null, begin: /\(\s*\)/, skip: !0
                }, { begin: /\(/, end: /\)/, excludeBegin: !0, excludeEnd: !0, keywords: l, contains: f }]
              }]
            }, { begin: /,/, relevance: 0 }, { className: "", begin: /\s/, end: /\s*/, skip: !0 }, {
                variants: [{ begin: "<>", end: "</>" }, {
                  begin: o.begin, "on:begin": o.isTrulyOpeningTag,
                  end: o.end
                }], subLanguage: "xml", contains: [{
                  begin: o.begin, end: o.end, skip: !0,
                  contains: ["self"]
                }]
              }], relevance: 0
          }, {
            className: "function",
            beginKeywords: "function", end: /[{;]/, excludeEnd: !0, keywords: l,
            contains: ["self", i.inherit(i.TITLE_MODE, { begin: c }), A], illegal: /%/
          }, {
            beginKeywords: "while if switch catch for"
          }, {
            className: "function",
            begin: i.UNDERSCORE_IDENT_RE + "\\([^()]*(\\([^()]*(\\([^()]*\\)[^()]*)*\\)[^()]*)*\\)\\s*\\{",
            returnBegin: !0, contains: [A, i.inherit(i.TITLE_MODE, { begin: c })]
          }, {
            variants: [{
              begin: "\\." + c
            }, { begin: "\\$" + c }], relevance: 0
          }, {
            className: "class",
            beginKeywords: "class", end: /[{;=]/, excludeEnd: !0, illegal: /[:"[\]]/, contains: [{
              beginKeywords: "extends"
            }, i.UNDERSCORE_TITLE_MODE]
          }, {
            begin: /\b(?=constructor)/,
            end: /[{;]/, excludeEnd: !0, contains: [i.inherit(i.TITLE_MODE, { begin: c }), "self", A]
          }, {
            begin: "(get|set)\\s+(?=" + c + "\\()", end: /\{/, keywords: "get set",
            contains: [i.inherit(i.TITLE_MODE, { begin: c }), { begin: /\(\)/ }, A]
          }, { begin: /\$[(.]/ }]
        }
      })(i)
        ; return Object.assign(b.keywords, c), b.exports.PARAMS_CONTAINS.push(o), b.contains = b.contains.concat([o, {
          beginKeywords: "namespace", end: /\{/, excludeEnd: !0
        }, {
          beginKeywords: "interface",
            end: /\{/, excludeEnd: !0, keywords: "interface extends"
          }]), l(b, "shebang", i.SHEBANG()), l(b, "use_strict", {
            className: "meta", relevance: 10,
            begin: /^\s*['"]use strict['"]/
          }), b.contains.find((e => "function" === e.className)).relevance = 0, Object.assign(b, {
            name: "TypeScript", aliases: ["ts", "tsx"]
          }), b
    }
})());
hljs.registerLanguage("vbnet", (() => {
 function e(e) {
    return e ? "string" == typeof e ? e : e.source : null
  } function n(...n) {
    return n.map((n => e(n))).join("")
  } function t(...n) {
    return "(" + n.map((n => e(n))).join("|") + ")"
  } return e => {
    const a = /\d{1,2}\/\d{1,2}\/\d{4}/, i = /\d{4}-\d{1,2}-\d{1,2}/, s = /(\d|1[012])(:\d+){0,2} *(AM|PM)/, r = /\d{1,2}(:\d{1,2}){1,2}/, o = {
      className: "literal", variants: [{ begin: n(/# */, t(i, a), / *#/) }, {
        begin: n(/# */, r, / *#/)
      }, { begin: n(/# */, s, / *#/) }, {
        begin: n(/# */, t(i, a), / +/, t(s, r), / *#/)
      }]
    }, l = e.COMMENT(/'''/, /$/, {
      contains: [{
        className: "doctag", begin: /<\/?/, end: />/
      }]
    }), c = e.COMMENT(null, /$/, {
      variants: [{
        begin: /'/
      }, { begin: /([\t ]|^)REM(?=\s)/ }]
    }); return {
      name: "Visual Basic .NET",
      aliases: ["vb"], case_insensitive: !0, classNameAliases: { label: "symbol" }, keywords: {
        keyword: "addhandler alias aggregate ansi as async assembly auto binary by byref byval call case catch class compare const continue custom declare default delegate dim distinct do each equals else elseif end enum erase error event exit explicit finally for friend from function get global goto group handles if implements imports in inherits interface into iterator join key let lib loop me mid module mustinherit mustoverride mybase myclass namespace narrowing new next notinheritable notoverridable of off on operator option optional order overloads overridable overrides paramarray partial preserve private property protected public raiseevent readonly redim removehandler resume return select set shadows shared skip static step stop structure strict sub synclock take text then throw to try unicode until using when where while widening with withevents writeonly yield",
        built_in: "addressof and andalso await directcast gettype getxmlnamespace is isfalse isnot istrue like mod nameof new not or orelse trycast typeof xor cbool cbyte cchar cdate cdbl cdec cint clng cobj csbyte cshort csng cstr cuint culng cushort",
        type: "boolean byte char date decimal double integer long object sbyte short single string uinteger ulong ushort",
        literal: "true false nothing"
      },
      illegal: "//|\\{|\\}|endif|gosub|variant|wend|^\\$ ", contains: [{
        className: "string", begin: /"(""|[^/n])"C\b/
      }, {
        className: "string", begin: /"/,
        end: /"/, illegal: /\n/, contains: [{ begin: /""/ }]
      }, o, {
        className: "number", relevance: 0,
        variants: [{
          begin: /\b\d[\d_]*((\.[\d_]+(E[+-]?[\d_]+)?)|(E[+-]?[\d_]+))[RFD@!#]?/
        }, { begin: /\b\d[\d_]*((U?[SIL])|[%&])?/ }, { begin: /&H[\dA-F_]+((U?[SIL])|[%&])?/ }, {
          begin: /&O[0-7_]+((U?[SIL])|[%&])?/
        }, { begin: /&B[01_]+((U?[SIL])|[%&])?/ }]
      }, {
        className: "label", begin: /^\w+:/
      }, l, c, {
        className: "meta",
        begin: /[\t ]*#(const|disable|else|elseif|enable|end|externalsource|if|region)\b/,
        end: /$/, keywords: {
          "meta-keyword": "const disable else elseif enable end externalsource if region then"
        }, contains: [c]
      }]
    }
  }
})());
hljs.registerLanguage("yaml", (() => {
 return e => {
    var n = "true false yes no null", a = "[\\w#;/?:@&=+$,.~*'()[\\]]+", s = {
      className: "string", relevance: 0, variants: [{ begin: /'/, end: /'/ }, {
        begin: /"/, end: /"/
      }, { begin: /\S+/ }], contains: [e.BACKSLASH_ESCAPE, {
        className: "template-variable",
        variants: [{ begin: /\{\{/, end: /\}\}/ }, { begin: /%\{/, end: /\}/ }]
      }]
    }, i = e.inherit(s, {
      variants: [{ begin: /'/, end: /'/ }, { begin: /"/, end: /"/ }, { begin: /[^\s,{}[\]]+/ }]
    }), l = {
      end: ",", endsWithParent: !0, excludeEnd: !0, keywords: n, relevance: 0
    }, t = {
      begin: /\{/,
      end: /\}/, contains: [l], illegal: "\\n", relevance: 0
    }, g = {
      begin: "\\[", end: "\\]",
      contains: [l], illegal: "\\n", relevance: 0
    }, b = [{
      className: "attr", variants: [{
        begin: "\\w[\\w :\\/.-]*:(?=[ \t]|$)"
      }, { begin: '"\\w[\\w :\\/.-]*":(?=[ \t]|$)' }, {
        begin: "'\\w[\\w :\\/.-]*':(?=[ \t]|$)"
      }]
    }, {
      className: "meta", begin: "^---\\s*$",
      relevance: 10
    }, {
      className: "string",
      begin: "[\\|>]([1-9]?[+-])?[ ]*\\n( +)[^ ][^\\n]*\\n(\\2[^\\n]+\\n?)*"
    }, {
      begin: "<%[%=-]?", end: "[%-]?%>", subLanguage: "ruby", excludeBegin: !0, excludeEnd: !0,
      relevance: 0
    }, { className: "type", begin: "!\\w+!" + a }, {
      className: "type",
      begin: "!<" + a + ">"
    }, { className: "type", begin: "!" + a }, {
      className: "type", begin: "!!" + a
    }, { className: "meta", begin: "&" + e.UNDERSCORE_IDENT_RE + "$" }, {
      className: "meta",
      begin: "\\*" + e.UNDERSCORE_IDENT_RE + "$"
    }, {
      className: "bullet", begin: "-(?=[ ]|$)",
      relevance: 0
    }, e.HASH_COMMENT_MODE, { beginKeywords: n, keywords: { literal: n } }, {
      className: "number",
      begin: "\\b[0-9]{4}(-[0-9][0-9]){0,2}([Tt \\t][0-9][0-9]?(:[0-9][0-9]){2})?(\\.[0-9]*)?([ \\t])*(Z|[-+][0-9][0-9]?(:[0-9][0-9])?)?\\b"
    }, { className: "number", begin: e.C_NUMBER_RE + "\\b", relevance: 0 }, t, g, s], r = [...b]
      ; return r.pop(), r.push(i), l.contains = r, {
        name: "YAML", case_insensitive: !0,
        aliases: ["yml"], contains: b
      }
  }
})());

// enable highlight
hljs.highlightAll();

/*!
 * Chart.js v2.9.4
 * https://www.chartjs.org
 * (c) 2020 Chart.js Contributors
 * Released under the MIT License
 */
!function(t,e){"object"==typeof exports&&"undefined"!=typeof module?module.exports=e(function(){try{return require("moment")}catch(t){}}()):"function"==typeof define&&define.amd?define(["require"],(function(t){return e(function(){try{return t("moment")}catch(t){}}())})):(t=t||self).Chart=e(t.moment);}(undefined,(function(t){t=t&&t.hasOwnProperty("default")?t.default:t;var e={aliceblue:[240,248,255],antiquewhite:[250,235,215],aqua:[0,255,255],aquamarine:[127,255,212],azure:[240,255,255],beige:[245,245,220],bisque:[255,228,196],black:[0,0,0],blanchedalmond:[255,235,205],blue:[0,0,255],blueviolet:[138,43,226],brown:[165,42,42],burlywood:[222,184,135],cadetblue:[95,158,160],chartreuse:[127,255,0],chocolate:[210,105,30],coral:[255,127,80],cornflowerblue:[100,149,237],cornsilk:[255,248,220],crimson:[220,20,60],cyan:[0,255,255],darkblue:[0,0,139],darkcyan:[0,139,139],darkgoldenrod:[184,134,11],darkgray:[169,169,169],darkgreen:[0,100,0],darkgrey:[169,169,169],darkkhaki:[189,183,107],darkmagenta:[139,0,139],darkolivegreen:[85,107,47],darkorange:[255,140,0],darkorchid:[153,50,204],darkred:[139,0,0],darksalmon:[233,150,122],darkseagreen:[143,188,143],darkslateblue:[72,61,139],darkslategray:[47,79,79],darkslategrey:[47,79,79],darkturquoise:[0,206,209],darkviolet:[148,0,211],deeppink:[255,20,147],deepskyblue:[0,191,255],dimgray:[105,105,105],dimgrey:[105,105,105],dodgerblue:[30,144,255],firebrick:[178,34,34],floralwhite:[255,250,240],forestgreen:[34,139,34],fuchsia:[255,0,255],gainsboro:[220,220,220],ghostwhite:[248,248,255],gold:[255,215,0],goldenrod:[218,165,32],gray:[128,128,128],green:[0,128,0],greenyellow:[173,255,47],grey:[128,128,128],honeydew:[240,255,240],hotpink:[255,105,180],indianred:[205,92,92],indigo:[75,0,130],ivory:[255,255,240],khaki:[240,230,140],lavender:[230,230,250],lavenderblush:[255,240,245],lawngreen:[124,252,0],lemonchiffon:[255,250,205],lightblue:[173,216,230],lightcoral:[240,128,128],lightcyan:[224,255,255],lightgoldenrodyellow:[250,250,210],lightgray:[211,211,211],lightgreen:[144,238,144],lightgrey:[211,211,211],lightpink:[255,182,193],lightsalmon:[255,160,122],lightseagreen:[32,178,170],lightskyblue:[135,206,250],lightslategray:[119,136,153],lightslategrey:[119,136,153],lightsteelblue:[176,196,222],lightyellow:[255,255,224],lime:[0,255,0],limegreen:[50,205,50],linen:[250,240,230],magenta:[255,0,255],maroon:[128,0,0],mediumaquamarine:[102,205,170],mediumblue:[0,0,205],mediumorchid:[186,85,211],mediumpurple:[147,112,219],mediumseagreen:[60,179,113],mediumslateblue:[123,104,238],mediumspringgreen:[0,250,154],mediumturquoise:[72,209,204],mediumvioletred:[199,21,133],midnightblue:[25,25,112],mintcream:[245,255,250],mistyrose:[255,228,225],moccasin:[255,228,181],navajowhite:[255,222,173],navy:[0,0,128],oldlace:[253,245,230],olive:[128,128,0],olivedrab:[107,142,35],orange:[255,165,0],orangered:[255,69,0],orchid:[218,112,214],palegoldenrod:[238,232,170],palegreen:[152,251,152],paleturquoise:[175,238,238],palevioletred:[219,112,147],papayawhip:[255,239,213],peachpuff:[255,218,185],peru:[205,133,63],pink:[255,192,203],plum:[221,160,221],powderblue:[176,224,230],purple:[128,0,128],rebeccapurple:[102,51,153],red:[255,0,0],rosybrown:[188,143,143],royalblue:[65,105,225],saddlebrown:[139,69,19],salmon:[250,128,114],sandybrown:[244,164,96],seagreen:[46,139,87],seashell:[255,245,238],sienna:[160,82,45],silver:[192,192,192],skyblue:[135,206,235],slateblue:[106,90,205],slategray:[112,128,144],slategrey:[112,128,144],snow:[255,250,250],springgreen:[0,255,127],steelblue:[70,130,180],tan:[210,180,140],teal:[0,128,128],thistle:[216,191,216],tomato:[255,99,71],turquoise:[64,224,208],violet:[238,130,238],wheat:[245,222,179],white:[255,255,255],whitesmoke:[245,245,245],yellow:[255,255,0],yellowgreen:[154,205,50]},n=function(t,e){return t(e={exports:{}},e.exports),e.exports}((function(t){var n={};for(var i in e)e.hasOwnProperty(i)&&(n[e[i]]=i);var a=t.exports={rgb:{channels:3,labels:"rgb"},hsl:{channels:3,labels:"hsl"},hsv:{channels:3,labels:"hsv"},hwb:{channels:3,labels:"hwb"},cmyk:{channels:4,labels:"cmyk"},xyz:{channels:3,labels:"xyz"},lab:{channels:3,labels:"lab"},lch:{channels:3,labels:"lch"},hex:{channels:1,labels:["hex"]},keyword:{channels:1,labels:["keyword"]},ansi16:{channels:1,labels:["ansi16"]},ansi256:{channels:1,labels:["ansi256"]},hcg:{channels:3,labels:["h","c","g"]},apple:{channels:3,labels:["r16","g16","b16"]},gray:{channels:1,labels:["gray"]}};for(var r in a)if(a.hasOwnProperty(r)){if(!("channels"in a[r]))throw new Error("missing channels property: "+r);if(!("labels"in a[r]))throw new Error("missing channel labels property: "+r);if(a[r].labels.length!==a[r].channels)throw new Error("channel and label counts mismatch: "+r);var o=a[r].channels,s=a[r].labels;delete a[r].channels,delete a[r].labels,Object.defineProperty(a[r],"channels",{value:o}),Object.defineProperty(a[r],"labels",{value:s});}a.rgb.hsl=function(t){var e,n,i=t[0]/255,a=t[1]/255,r=t[2]/255,o=Math.min(i,a,r),s=Math.max(i,a,r),l=s-o;return s===o?e=0:i===s?e=(a-r)/l:a===s?e=2+(r-i)/l:r===s&&(e=4+(i-a)/l),(e=Math.min(60*e,360))<0&&(e+=360),n=(o+s)/2,[e,100*(s===o?0:n<=.5?l/(s+o):l/(2-s-o)),100*n]},a.rgb.hsv=function(t){var e,n,i,a,r,o=t[0]/255,s=t[1]/255,l=t[2]/255,u=Math.max(o,s,l),d=u-Math.min(o,s,l),h=function(t){return (u-t)/6/d+.5};return 0===d?a=r=0:(r=d/u,e=h(o),n=h(s),i=h(l),o===u?a=i-n:s===u?a=1/3+e-i:l===u&&(a=2/3+n-e),a<0?a+=1:a>1&&(a-=1)),[360*a,100*r,100*u]},a.rgb.hwb=function(t){var e=t[0],n=t[1],i=t[2];return [a.rgb.hsl(t)[0],100*(1/255*Math.min(e,Math.min(n,i))),100*(i=1-1/255*Math.max(e,Math.max(n,i)))]},a.rgb.cmyk=function(t){var e,n=t[0]/255,i=t[1]/255,a=t[2]/255;return [100*((1-n-(e=Math.min(1-n,1-i,1-a)))/(1-e)||0),100*((1-i-e)/(1-e)||0),100*((1-a-e)/(1-e)||0),100*e]},a.rgb.keyword=function(t){var i=n[t];if(i)return i;var a,r,o,s=1/0;for(var l in e)if(e.hasOwnProperty(l)){var u=e[l],d=(r=t,o=u,Math.pow(r[0]-o[0],2)+Math.pow(r[1]-o[1],2)+Math.pow(r[2]-o[2],2));d<s&&(s=d,a=l);}return a},a.keyword.rgb=function(t){return e[t]},a.rgb.xyz=function(t){var e=t[0]/255,n=t[1]/255,i=t[2]/255;return [100*(.4124*(e=e>.04045?Math.pow((e+.055)/1.055,2.4):e/12.92)+.3576*(n=n>.04045?Math.pow((n+.055)/1.055,2.4):n/12.92)+.1805*(i=i>.04045?Math.pow((i+.055)/1.055,2.4):i/12.92)),100*(.2126*e+.7152*n+.0722*i),100*(.0193*e+.1192*n+.9505*i)]},a.rgb.lab=function(t){var e=a.rgb.xyz(t),n=e[0],i=e[1],r=e[2];return i/=100,r/=108.883,n=(n/=95.047)>.008856?Math.pow(n,1/3):7.787*n+16/116,[116*(i=i>.008856?Math.pow(i,1/3):7.787*i+16/116)-16,500*(n-i),200*(i-(r=r>.008856?Math.pow(r,1/3):7.787*r+16/116))]},a.hsl.rgb=function(t){var e,n,i,a,r,o=t[0]/360,s=t[1]/100,l=t[2]/100;if(0===s)return [r=255*l,r,r];e=2*l-(n=l<.5?l*(1+s):l+s-l*s),a=[0,0,0];for(var u=0;u<3;u++)(i=o+1/3*-(u-1))<0&&i++,i>1&&i--,r=6*i<1?e+6*(n-e)*i:2*i<1?n:3*i<2?e+(n-e)*(2/3-i)*6:e,a[u]=255*r;return a},a.hsl.hsv=function(t){var e=t[0],n=t[1]/100,i=t[2]/100,a=n,r=Math.max(i,.01);return n*=(i*=2)<=1?i:2-i,a*=r<=1?r:2-r,[e,100*(0===i?2*a/(r+a):2*n/(i+n)),100*((i+n)/2)]},a.hsv.rgb=function(t){var e=t[0]/60,n=t[1]/100,i=t[2]/100,a=Math.floor(e)%6,r=e-Math.floor(e),o=255*i*(1-n),s=255*i*(1-n*r),l=255*i*(1-n*(1-r));switch(i*=255,a){case 0:return [i,l,o];case 1:return [s,i,o];case 2:return [o,i,l];case 3:return [o,s,i];case 4:return [l,o,i];case 5:return [i,o,s]}},a.hsv.hsl=function(t){var e,n,i,a=t[0],r=t[1]/100,o=t[2]/100,s=Math.max(o,.01);return i=(2-r)*o,n=r*s,[a,100*(n=(n/=(e=(2-r)*s)<=1?e:2-e)||0),100*(i/=2)]},a.hwb.rgb=function(t){var e,n,i,a,r,o,s,l=t[0]/360,u=t[1]/100,d=t[2]/100,h=u+d;switch(h>1&&(u/=h,d/=h),i=6*l-(e=Math.floor(6*l)),0!=(1&e)&&(i=1-i),a=u+i*((n=1-d)-u),e){default:case 6:case 0:r=n,o=a,s=u;break;case 1:r=a,o=n,s=u;break;case 2:r=u,o=n,s=a;break;case 3:r=u,o=a,s=n;break;case 4:r=a,o=u,s=n;break;case 5:r=n,o=u,s=a;}return [255*r,255*o,255*s]},a.cmyk.rgb=function(t){var e=t[0]/100,n=t[1]/100,i=t[2]/100,a=t[3]/100;return [255*(1-Math.min(1,e*(1-a)+a)),255*(1-Math.min(1,n*(1-a)+a)),255*(1-Math.min(1,i*(1-a)+a))]},a.xyz.rgb=function(t){var e,n,i,a=t[0]/100,r=t[1]/100,o=t[2]/100;return n=-.9689*a+1.8758*r+.0415*o,i=.0557*a+-.204*r+1.057*o,e=(e=3.2406*a+-1.5372*r+-.4986*o)>.0031308?1.055*Math.pow(e,1/2.4)-.055:12.92*e,n=n>.0031308?1.055*Math.pow(n,1/2.4)-.055:12.92*n,i=i>.0031308?1.055*Math.pow(i,1/2.4)-.055:12.92*i,[255*(e=Math.min(Math.max(0,e),1)),255*(n=Math.min(Math.max(0,n),1)),255*(i=Math.min(Math.max(0,i),1))]},a.xyz.lab=function(t){var e=t[0],n=t[1],i=t[2];return n/=100,i/=108.883,e=(e/=95.047)>.008856?Math.pow(e,1/3):7.787*e+16/116,[116*(n=n>.008856?Math.pow(n,1/3):7.787*n+16/116)-16,500*(e-n),200*(n-(i=i>.008856?Math.pow(i,1/3):7.787*i+16/116))]},a.lab.xyz=function(t){var e,n,i,a=t[0];e=t[1]/500+(n=(a+16)/116),i=n-t[2]/200;var r=Math.pow(n,3),o=Math.pow(e,3),s=Math.pow(i,3);return n=r>.008856?r:(n-16/116)/7.787,e=o>.008856?o:(e-16/116)/7.787,i=s>.008856?s:(i-16/116)/7.787,[e*=95.047,n*=100,i*=108.883]},a.lab.lch=function(t){var e,n=t[0],i=t[1],a=t[2];return (e=360*Math.atan2(a,i)/2/Math.PI)<0&&(e+=360),[n,Math.sqrt(i*i+a*a),e]},a.lch.lab=function(t){var e,n=t[0],i=t[1];return e=t[2]/360*2*Math.PI,[n,i*Math.cos(e),i*Math.sin(e)]},a.rgb.ansi16=function(t){var e=t[0],n=t[1],i=t[2],r=1 in arguments?arguments[1]:a.rgb.hsv(t)[2];if(0===(r=Math.round(r/50)))return 30;var o=30+(Math.round(i/255)<<2|Math.round(n/255)<<1|Math.round(e/255));return 2===r&&(o+=60),o},a.hsv.ansi16=function(t){return a.rgb.ansi16(a.hsv.rgb(t),t[2])},a.rgb.ansi256=function(t){var e=t[0],n=t[1],i=t[2];return e===n&&n===i?e<8?16:e>248?231:Math.round((e-8)/247*24)+232:16+36*Math.round(e/255*5)+6*Math.round(n/255*5)+Math.round(i/255*5)},a.ansi16.rgb=function(t){var e=t%10;if(0===e||7===e)return t>50&&(e+=3.5),[e=e/10.5*255,e,e];var n=.5*(1+~~(t>50));return [(1&e)*n*255,(e>>1&1)*n*255,(e>>2&1)*n*255]},a.ansi256.rgb=function(t){if(t>=232){var e=10*(t-232)+8;return [e,e,e]}var n;return t-=16,[Math.floor(t/36)/5*255,Math.floor((n=t%36)/6)/5*255,n%6/5*255]},a.rgb.hex=function(t){var e=(((255&Math.round(t[0]))<<16)+((255&Math.round(t[1]))<<8)+(255&Math.round(t[2]))).toString(16).toUpperCase();return "000000".substring(e.length)+e},a.hex.rgb=function(t){var e=t.toString(16).match(/[a-f0-9]{6}|[a-f0-9]{3}/i);if(!e)return [0,0,0];var n=e[0];3===e[0].length&&(n=n.split("").map((function(t){return t+t})).join(""));var i=parseInt(n,16);return [i>>16&255,i>>8&255,255&i]},a.rgb.hcg=function(t){var e,n=t[0]/255,i=t[1]/255,a=t[2]/255,r=Math.max(Math.max(n,i),a),o=Math.min(Math.min(n,i),a),s=r-o;return e=s<=0?0:r===n?(i-a)/s%6:r===i?2+(a-n)/s:4+(n-i)/s+4,e/=6,[360*(e%=1),100*s,100*(s<1?o/(1-s):0)]},a.hsl.hcg=function(t){var e=t[1]/100,n=t[2]/100,i=1,a=0;return (i=n<.5?2*e*n:2*e*(1-n))<1&&(a=(n-.5*i)/(1-i)),[t[0],100*i,100*a]},a.hsv.hcg=function(t){var e=t[1]/100,n=t[2]/100,i=e*n,a=0;return i<1&&(a=(n-i)/(1-i)),[t[0],100*i,100*a]},a.hcg.rgb=function(t){var e=t[0]/360,n=t[1]/100,i=t[2]/100;if(0===n)return [255*i,255*i,255*i];var a,r=[0,0,0],o=e%1*6,s=o%1,l=1-s;switch(Math.floor(o)){case 0:r[0]=1,r[1]=s,r[2]=0;break;case 1:r[0]=l,r[1]=1,r[2]=0;break;case 2:r[0]=0,r[1]=1,r[2]=s;break;case 3:r[0]=0,r[1]=l,r[2]=1;break;case 4:r[0]=s,r[1]=0,r[2]=1;break;default:r[0]=1,r[1]=0,r[2]=l;}return a=(1-n)*i,[255*(n*r[0]+a),255*(n*r[1]+a),255*(n*r[2]+a)]},a.hcg.hsv=function(t){var e=t[1]/100,n=e+t[2]/100*(1-e),i=0;return n>0&&(i=e/n),[t[0],100*i,100*n]},a.hcg.hsl=function(t){var e=t[1]/100,n=t[2]/100*(1-e)+.5*e,i=0;return n>0&&n<.5?i=e/(2*n):n>=.5&&n<1&&(i=e/(2*(1-n))),[t[0],100*i,100*n]},a.hcg.hwb=function(t){var e=t[1]/100,n=e+t[2]/100*(1-e);return [t[0],100*(n-e),100*(1-n)]},a.hwb.hcg=function(t){var e=t[1]/100,n=1-t[2]/100,i=n-e,a=0;return i<1&&(a=(n-i)/(1-i)),[t[0],100*i,100*a]},a.apple.rgb=function(t){return [t[0]/65535*255,t[1]/65535*255,t[2]/65535*255]},a.rgb.apple=function(t){return [t[0]/255*65535,t[1]/255*65535,t[2]/255*65535]},a.gray.rgb=function(t){return [t[0]/100*255,t[0]/100*255,t[0]/100*255]},a.gray.hsl=a.gray.hsv=function(t){return [0,0,t[0]]},a.gray.hwb=function(t){return [0,100,t[0]]},a.gray.cmyk=function(t){return [0,0,0,t[0]]},a.gray.lab=function(t){return [t[0],0,0]},a.gray.hex=function(t){var e=255&Math.round(t[0]/100*255),n=((e<<16)+(e<<8)+e).toString(16).toUpperCase();return "000000".substring(n.length)+n},a.rgb.gray=function(t){return [(t[0]+t[1]+t[2])/3/255*100]};}));n.rgb,n.hsl,n.hsv,n.hwb,n.cmyk,n.xyz,n.lab,n.lch,n.hex,n.keyword,n.ansi16,n.ansi256,n.hcg,n.apple,n.gray;function i(t){var e=function(){for(var t={},e=Object.keys(n),i=e.length,a=0;a<i;a++)t[e[a]]={distance:-1,parent:null};return t}(),i=[t];for(e[t].distance=0;i.length;)for(var a=i.pop(),r=Object.keys(n[a]),o=r.length,s=0;s<o;s++){var l=r[s],u=e[l];-1===u.distance&&(u.distance=e[a].distance+1,u.parent=a,i.unshift(l));}return e}function a(t,e){return function(n){return e(t(n))}}function r(t,e){for(var i=[e[t].parent,t],r=n[e[t].parent][t],o=e[t].parent;e[o].parent;)i.unshift(e[o].parent),r=a(n[e[o].parent][o],r),o=e[o].parent;return r.conversion=i,r}var o={};Object.keys(n).forEach((function(t){o[t]={},Object.defineProperty(o[t],"channels",{value:n[t].channels}),Object.defineProperty(o[t],"labels",{value:n[t].labels});var e=function(t){for(var e=i(t),n={},a=Object.keys(e),o=a.length,s=0;s<o;s++){var l=a[s];null!==e[l].parent&&(n[l]=r(l,e));}return n}(t);Object.keys(e).forEach((function(n){var i=e[n];o[t][n]=function(t){var e=function(e){if(null==e)return e;arguments.length>1&&(e=Array.prototype.slice.call(arguments));var n=t(e);if("object"==typeof n)for(var i=n.length,a=0;a<i;a++)n[a]=Math.round(n[a]);return n};return "conversion"in t&&(e.conversion=t.conversion),e}(i),o[t][n].raw=function(t){var e=function(e){return null==e?e:(arguments.length>1&&(e=Array.prototype.slice.call(arguments)),t(e))};return "conversion"in t&&(e.conversion=t.conversion),e}(i);}));}));var s=o,l={aliceblue:[240,248,255],antiquewhite:[250,235,215],aqua:[0,255,255],aquamarine:[127,255,212],azure:[240,255,255],beige:[245,245,220],bisque:[255,228,196],black:[0,0,0],blanchedalmond:[255,235,205],blue:[0,0,255],blueviolet:[138,43,226],brown:[165,42,42],burlywood:[222,184,135],cadetblue:[95,158,160],chartreuse:[127,255,0],chocolate:[210,105,30],coral:[255,127,80],cornflowerblue:[100,149,237],cornsilk:[255,248,220],crimson:[220,20,60],cyan:[0,255,255],darkblue:[0,0,139],darkcyan:[0,139,139],darkgoldenrod:[184,134,11],darkgray:[169,169,169],darkgreen:[0,100,0],darkgrey:[169,169,169],darkkhaki:[189,183,107],darkmagenta:[139,0,139],darkolivegreen:[85,107,47],darkorange:[255,140,0],darkorchid:[153,50,204],darkred:[139,0,0],darksalmon:[233,150,122],darkseagreen:[143,188,143],darkslateblue:[72,61,139],darkslategray:[47,79,79],darkslategrey:[47,79,79],darkturquoise:[0,206,209],darkviolet:[148,0,211],deeppink:[255,20,147],deepskyblue:[0,191,255],dimgray:[105,105,105],dimgrey:[105,105,105],dodgerblue:[30,144,255],firebrick:[178,34,34],floralwhite:[255,250,240],forestgreen:[34,139,34],fuchsia:[255,0,255],gainsboro:[220,220,220],ghostwhite:[248,248,255],gold:[255,215,0],goldenrod:[218,165,32],gray:[128,128,128],green:[0,128,0],greenyellow:[173,255,47],grey:[128,128,128],honeydew:[240,255,240],hotpink:[255,105,180],indianred:[205,92,92],indigo:[75,0,130],ivory:[255,255,240],khaki:[240,230,140],lavender:[230,230,250],lavenderblush:[255,240,245],lawngreen:[124,252,0],lemonchiffon:[255,250,205],lightblue:[173,216,230],lightcoral:[240,128,128],lightcyan:[224,255,255],lightgoldenrodyellow:[250,250,210],lightgray:[211,211,211],lightgreen:[144,238,144],lightgrey:[211,211,211],lightpink:[255,182,193],lightsalmon:[255,160,122],lightseagreen:[32,178,170],lightskyblue:[135,206,250],lightslategray:[119,136,153],lightslategrey:[119,136,153],lightsteelblue:[176,196,222],lightyellow:[255,255,224],lime:[0,255,0],limegreen:[50,205,50],linen:[250,240,230],magenta:[255,0,255],maroon:[128,0,0],mediumaquamarine:[102,205,170],mediumblue:[0,0,205],mediumorchid:[186,85,211],mediumpurple:[147,112,219],mediumseagreen:[60,179,113],mediumslateblue:[123,104,238],mediumspringgreen:[0,250,154],mediumturquoise:[72,209,204],mediumvioletred:[199,21,133],midnightblue:[25,25,112],mintcream:[245,255,250],mistyrose:[255,228,225],moccasin:[255,228,181],navajowhite:[255,222,173],navy:[0,0,128],oldlace:[253,245,230],olive:[128,128,0],olivedrab:[107,142,35],orange:[255,165,0],orangered:[255,69,0],orchid:[218,112,214],palegoldenrod:[238,232,170],palegreen:[152,251,152],paleturquoise:[175,238,238],palevioletred:[219,112,147],papayawhip:[255,239,213],peachpuff:[255,218,185],peru:[205,133,63],pink:[255,192,203],plum:[221,160,221],powderblue:[176,224,230],purple:[128,0,128],rebeccapurple:[102,51,153],red:[255,0,0],rosybrown:[188,143,143],royalblue:[65,105,225],saddlebrown:[139,69,19],salmon:[250,128,114],sandybrown:[244,164,96],seagreen:[46,139,87],seashell:[255,245,238],sienna:[160,82,45],silver:[192,192,192],skyblue:[135,206,235],slateblue:[106,90,205],slategray:[112,128,144],slategrey:[112,128,144],snow:[255,250,250],springgreen:[0,255,127],steelblue:[70,130,180],tan:[210,180,140],teal:[0,128,128],thistle:[216,191,216],tomato:[255,99,71],turquoise:[64,224,208],violet:[238,130,238],wheat:[245,222,179],white:[255,255,255],whitesmoke:[245,245,245],yellow:[255,255,0],yellowgreen:[154,205,50]},u={getRgba:d,getHsla:h,getRgb:function(t){var e=d(t);return e&&e.slice(0,3)},getHsl:function(t){var e=h(t);return e&&e.slice(0,3)},getHwb:c,getAlpha:function(t){var e=d(t);if(e)return e[3];if(e=h(t))return e[3];if(e=c(t))return e[3]},hexString:function(t,e){e=void 0!==e&&3===t.length?e:t[3];return "#"+v(t[0])+v(t[1])+v(t[2])+(e>=0&&e<1?v(Math.round(255*e)):"")},rgbString:function(t,e){if(e<1||t[3]&&t[3]<1)return f(t,e);return "rgb("+t[0]+", "+t[1]+", "+t[2]+")"},rgbaString:f,percentString:function(t,e){if(e<1||t[3]&&t[3]<1)return g(t,e);var n=Math.round(t[0]/255*100),i=Math.round(t[1]/255*100),a=Math.round(t[2]/255*100);return "rgb("+n+"%, "+i+"%, "+a+"%)"},percentaString:g,hslString:function(t,e){if(e<1||t[3]&&t[3]<1)return p(t,e);return "hsl("+t[0]+", "+t[1]+"%, "+t[2]+"%)"},hslaString:p,hwbString:function(t,e){void 0===e&&(e=void 0!==t[3]?t[3]:1);return "hwb("+t[0]+", "+t[1]+"%, "+t[2]+"%"+(void 0!==e&&1!==e?", "+e:"")+")"},keyword:function(t){return b[t.slice(0,3)]}};function d(t){if(t){var e=[0,0,0],n=1,i=t.match(/^#([a-fA-F0-9]{3,4})$/i),a="";if(i){a=(i=i[1])[3];for(var r=0;r<e.length;r++)e[r]=parseInt(i[r]+i[r],16);a&&(n=Math.round(parseInt(a+a,16)/255*100)/100);}else if(i=t.match(/^#([a-fA-F0-9]{6}([a-fA-F0-9]{2})?)$/i)){a=i[2],i=i[1];for(r=0;r<e.length;r++)e[r]=parseInt(i.slice(2*r,2*r+2),16);a&&(n=Math.round(parseInt(a,16)/255*100)/100);}else if(i=t.match(/^rgba?\(\s*([+-]?\d+)\s*,\s*([+-]?\d+)\s*,\s*([+-]?\d+)\s*(?:,\s*([+-]?[\d\.]+)\s*)?\)$/i)){for(r=0;r<e.length;r++)e[r]=parseInt(i[r+1]);n=parseFloat(i[4]);}else if(i=t.match(/^rgba?\(\s*([+-]?[\d\.]+)\%\s*,\s*([+-]?[\d\.]+)\%\s*,\s*([+-]?[\d\.]+)\%\s*(?:,\s*([+-]?[\d\.]+)\s*)?\)$/i)){for(r=0;r<e.length;r++)e[r]=Math.round(2.55*parseFloat(i[r+1]));n=parseFloat(i[4]);}else if(i=t.match(/(\w+)/)){if("transparent"==i[1])return [0,0,0,0];if(!(e=l[i[1]]))return}for(r=0;r<e.length;r++)e[r]=m(e[r],0,255);return n=n||0==n?m(n,0,1):1,e[3]=n,e}}function h(t){if(t){var e=t.match(/^hsla?\(\s*([+-]?\d+)(?:deg)?\s*,\s*([+-]?[\d\.]+)%\s*,\s*([+-]?[\d\.]+)%\s*(?:,\s*([+-]?[\d\.]+)\s*)?\)/);if(e){var n=parseFloat(e[4]);return [m(parseInt(e[1]),0,360),m(parseFloat(e[2]),0,100),m(parseFloat(e[3]),0,100),m(isNaN(n)?1:n,0,1)]}}}function c(t){if(t){var e=t.match(/^hwb\(\s*([+-]?\d+)(?:deg)?\s*,\s*([+-]?[\d\.]+)%\s*,\s*([+-]?[\d\.]+)%\s*(?:,\s*([+-]?[\d\.]+)\s*)?\)/);if(e){var n=parseFloat(e[4]);return [m(parseInt(e[1]),0,360),m(parseFloat(e[2]),0,100),m(parseFloat(e[3]),0,100),m(isNaN(n)?1:n,0,1)]}}}function f(t,e){return void 0===e&&(e=void 0!==t[3]?t[3]:1),"rgba("+t[0]+", "+t[1]+", "+t[2]+", "+e+")"}function g(t,e){return "rgba("+Math.round(t[0]/255*100)+"%, "+Math.round(t[1]/255*100)+"%, "+Math.round(t[2]/255*100)+"%, "+(e||t[3]||1)+")"}function p(t,e){return void 0===e&&(e=void 0!==t[3]?t[3]:1),"hsla("+t[0]+", "+t[1]+"%, "+t[2]+"%, "+e+")"}function m(t,e,n){return Math.min(Math.max(e,t),n)}function v(t){var e=t.toString(16).toUpperCase();return e.length<2?"0"+e:e}var b={};for(var x in l)b[l[x]]=x;var y=function(t){return t instanceof y?t:this instanceof y?(this.valid=!1,this.values={rgb:[0,0,0],hsl:[0,0,0],hsv:[0,0,0],hwb:[0,0,0],cmyk:[0,0,0,0],alpha:1},void("string"==typeof t?(e=u.getRgba(t))?this.setValues("rgb",e):(e=u.getHsla(t))?this.setValues("hsl",e):(e=u.getHwb(t))&&this.setValues("hwb",e):"object"==typeof t&&(void 0!==(e=t).r||void 0!==e.red?this.setValues("rgb",e):void 0!==e.l||void 0!==e.lightness?this.setValues("hsl",e):void 0!==e.v||void 0!==e.value?this.setValues("hsv",e):void 0!==e.w||void 0!==e.whiteness?this.setValues("hwb",e):void 0===e.c&&void 0===e.cyan||this.setValues("cmyk",e)))):new y(t);var e;};y.prototype={isValid:function(){return this.valid},rgb:function(){return this.setSpace("rgb",arguments)},hsl:function(){return this.setSpace("hsl",arguments)},hsv:function(){return this.setSpace("hsv",arguments)},hwb:function(){return this.setSpace("hwb",arguments)},cmyk:function(){return this.setSpace("cmyk",arguments)},rgbArray:function(){return this.values.rgb},hslArray:function(){return this.values.hsl},hsvArray:function(){return this.values.hsv},hwbArray:function(){var t=this.values;return 1!==t.alpha?t.hwb.concat([t.alpha]):t.hwb},cmykArray:function(){return this.values.cmyk},rgbaArray:function(){var t=this.values;return t.rgb.concat([t.alpha])},hslaArray:function(){var t=this.values;return t.hsl.concat([t.alpha])},alpha:function(t){return void 0===t?this.values.alpha:(this.setValues("alpha",t),this)},red:function(t){return this.setChannel("rgb",0,t)},green:function(t){return this.setChannel("rgb",1,t)},blue:function(t){return this.setChannel("rgb",2,t)},hue:function(t){return t&&(t=(t%=360)<0?360+t:t),this.setChannel("hsl",0,t)},saturation:function(t){return this.setChannel("hsl",1,t)},lightness:function(t){return this.setChannel("hsl",2,t)},saturationv:function(t){return this.setChannel("hsv",1,t)},whiteness:function(t){return this.setChannel("hwb",1,t)},blackness:function(t){return this.setChannel("hwb",2,t)},value:function(t){return this.setChannel("hsv",2,t)},cyan:function(t){return this.setChannel("cmyk",0,t)},magenta:function(t){return this.setChannel("cmyk",1,t)},yellow:function(t){return this.setChannel("cmyk",2,t)},black:function(t){return this.setChannel("cmyk",3,t)},hexString:function(){return u.hexString(this.values.rgb)},rgbString:function(){return u.rgbString(this.values.rgb,this.values.alpha)},rgbaString:function(){return u.rgbaString(this.values.rgb,this.values.alpha)},percentString:function(){return u.percentString(this.values.rgb,this.values.alpha)},hslString:function(){return u.hslString(this.values.hsl,this.values.alpha)},hslaString:function(){return u.hslaString(this.values.hsl,this.values.alpha)},hwbString:function(){return u.hwbString(this.values.hwb,this.values.alpha)},keyword:function(){return u.keyword(this.values.rgb,this.values.alpha)},rgbNumber:function(){var t=this.values.rgb;return t[0]<<16|t[1]<<8|t[2]},luminosity:function(){for(var t=this.values.rgb,e=[],n=0;n<t.length;n++){var i=t[n]/255;e[n]=i<=.03928?i/12.92:Math.pow((i+.055)/1.055,2.4);}return .2126*e[0]+.7152*e[1]+.0722*e[2]},contrast:function(t){var e=this.luminosity(),n=t.luminosity();return e>n?(e+.05)/(n+.05):(n+.05)/(e+.05)},level:function(t){var e=this.contrast(t);return e>=7.1?"AAA":e>=4.5?"AA":""},dark:function(){var t=this.values.rgb;return (299*t[0]+587*t[1]+114*t[2])/1e3<128},light:function(){return !this.dark()},negate:function(){for(var t=[],e=0;e<3;e++)t[e]=255-this.values.rgb[e];return this.setValues("rgb",t),this},lighten:function(t){var e=this.values.hsl;return e[2]+=e[2]*t,this.setValues("hsl",e),this},darken:function(t){var e=this.values.hsl;return e[2]-=e[2]*t,this.setValues("hsl",e),this},saturate:function(t){var e=this.values.hsl;return e[1]+=e[1]*t,this.setValues("hsl",e),this},desaturate:function(t){var e=this.values.hsl;return e[1]-=e[1]*t,this.setValues("hsl",e),this},whiten:function(t){var e=this.values.hwb;return e[1]+=e[1]*t,this.setValues("hwb",e),this},blacken:function(t){var e=this.values.hwb;return e[2]+=e[2]*t,this.setValues("hwb",e),this},greyscale:function(){var t=this.values.rgb,e=.3*t[0]+.59*t[1]+.11*t[2];return this.setValues("rgb",[e,e,e]),this},clearer:function(t){var e=this.values.alpha;return this.setValues("alpha",e-e*t),this},opaquer:function(t){var e=this.values.alpha;return this.setValues("alpha",e+e*t),this},rotate:function(t){var e=this.values.hsl,n=(e[0]+t)%360;return e[0]=n<0?360+n:n,this.setValues("hsl",e),this},mix:function(t,e){var n=t,i=void 0===e?.5:e,a=2*i-1,r=this.alpha()-n.alpha(),o=((a*r==-1?a:(a+r)/(1+a*r))+1)/2,s=1-o;return this.rgb(o*this.red()+s*n.red(),o*this.green()+s*n.green(),o*this.blue()+s*n.blue()).alpha(this.alpha()*i+n.alpha()*(1-i))},toJSON:function(){return this.rgb()},clone:function(){var t,e,n=new y,i=this.values,a=n.values;for(var r in i)i.hasOwnProperty(r)&&(t=i[r],"[object Array]"===(e={}.toString.call(t))?a[r]=t.slice(0):"[object Number]"===e?a[r]=t:console.error("unexpected color value:",t));return n}},y.prototype.spaces={rgb:["red","green","blue"],hsl:["hue","saturation","lightness"],hsv:["hue","saturation","value"],hwb:["hue","whiteness","blackness"],cmyk:["cyan","magenta","yellow","black"]},y.prototype.maxes={rgb:[255,255,255],hsl:[360,100,100],hsv:[360,100,100],hwb:[360,100,100],cmyk:[100,100,100,100]},y.prototype.getValues=function(t){for(var e=this.values,n={},i=0;i<t.length;i++)n[t.charAt(i)]=e[t][i];return 1!==e.alpha&&(n.a=e.alpha),n},y.prototype.setValues=function(t,e){var n,i,a=this.values,r=this.spaces,o=this.maxes,l=1;if(this.valid=!0,"alpha"===t)l=e;else if(e.length)a[t]=e.slice(0,t.length),l=e[t.length];else if(void 0!==e[t.charAt(0)]){for(n=0;n<t.length;n++)a[t][n]=e[t.charAt(n)];l=e.a;}else if(void 0!==e[r[t][0]]){var u=r[t];for(n=0;n<t.length;n++)a[t][n]=e[u[n]];l=e.alpha;}if(a.alpha=Math.max(0,Math.min(1,void 0===l?a.alpha:l)),"alpha"===t)return !1;for(n=0;n<t.length;n++)i=Math.max(0,Math.min(o[t][n],a[t][n])),a[t][n]=Math.round(i);for(var d in r)d!==t&&(a[d]=s[t][d](a[t]));return !0},y.prototype.setSpace=function(t,e){var n=e[0];return void 0===n?this.getValues(t):("number"==typeof n&&(n=Array.prototype.slice.call(e)),this.setValues(t,n),this)},y.prototype.setChannel=function(t,e,n){var i=this.values[t];return void 0===n?i[e]:n===i[e]?this:(i[e]=n,this.setValues(t,i),this)},"undefined"!=typeof window&&(window.Color=y);var _=y;function k(t){return -1===["__proto__","prototype","constructor"].indexOf(t)}var w,M={noop:function(){},uid:(w=0,function(){return w++}),isNullOrUndef:function(t){return null==t},isArray:function(t){if(Array.isArray&&Array.isArray(t))return !0;var e=Object.prototype.toString.call(t);return "[object"===e.substr(0,7)&&"Array]"===e.substr(-6)},isObject:function(t){return null!==t&&"[object Object]"===Object.prototype.toString.call(t)},isFinite:function(t){return ("number"==typeof t||t instanceof Number)&&isFinite(t)},valueOrDefault:function(t,e){return void 0===t?e:t},valueAtIndexOrDefault:function(t,e,n){return M.valueOrDefault(M.isArray(t)?t[e]:t,n)},callback:function(t,e,n){if(t&&"function"==typeof t.call)return t.apply(n,e)},each:function(t,e,n,i){var a,r,o;if(M.isArray(t))if(r=t.length,i)for(a=r-1;a>=0;a--)e.call(n,t[a],a);else for(a=0;a<r;a++)e.call(n,t[a],a);else if(M.isObject(t))for(r=(o=Object.keys(t)).length,a=0;a<r;a++)e.call(n,t[o[a]],o[a]);},arrayEquals:function(t,e){var n,i,a,r;if(!t||!e||t.length!==e.length)return !1;for(n=0,i=t.length;n<i;++n)if(a=t[n],r=e[n],a instanceof Array&&r instanceof Array){if(!M.arrayEquals(a,r))return !1}else if(a!==r)return !1;return !0},clone:function(t){if(M.isArray(t))return t.map(M.clone);if(M.isObject(t)){for(var e=Object.create(t),n=Object.keys(t),i=n.length,a=0;a<i;++a)e[n[a]]=M.clone(t[n[a]]);return e}return t},_merger:function(t,e,n,i){if(k(t)){var a=e[t],r=n[t];M.isObject(a)&&M.isObject(r)?M.merge(a,r,i):e[t]=M.clone(r);}},_mergerIf:function(t,e,n){if(k(t)){var i=e[t],a=n[t];M.isObject(i)&&M.isObject(a)?M.mergeIf(i,a):e.hasOwnProperty(t)||(e[t]=M.clone(a));}},merge:function(t,e,n){var i,a,r,o,s,l=M.isArray(e)?e:[e],u=l.length;if(!M.isObject(t))return t;for(i=(n=n||{}).merger||M._merger,a=0;a<u;++a)if(e=l[a],M.isObject(e))for(s=0,o=(r=Object.keys(e)).length;s<o;++s)i(r[s],t,e,n);return t},mergeIf:function(t,e){return M.merge(t,e,{merger:M._mergerIf})},extend:Object.assign||function(t){return M.merge(t,[].slice.call(arguments,1),{merger:function(t,e,n){e[t]=n[t];}})},inherits:function(t){var e=this,n=t&&t.hasOwnProperty("constructor")?t.constructor:function(){return e.apply(this,arguments)},i=function(){this.constructor=n;};return i.prototype=e.prototype,n.prototype=new i,n.extend=M.inherits,t&&M.extend(n.prototype,t),n.__super__=e.prototype,n},_deprecated:function(t,e,n,i){void 0!==e&&console.warn(t+': "'+n+'" is deprecated. Please use "'+i+'" instead');}},S=M;M.callCallback=M.callback,M.indexOf=function(t,e,n){return Array.prototype.indexOf.call(t,e,n)},M.getValueOrDefault=M.valueOrDefault,M.getValueAtIndexOrDefault=M.valueAtIndexOrDefault;var C={linear:function(t){return t},easeInQuad:function(t){return t*t},easeOutQuad:function(t){return -t*(t-2)},easeInOutQuad:function(t){return (t/=.5)<1?.5*t*t:-.5*(--t*(t-2)-1)},easeInCubic:function(t){return t*t*t},easeOutCubic:function(t){return (t-=1)*t*t+1},easeInOutCubic:function(t){return (t/=.5)<1?.5*t*t*t:.5*((t-=2)*t*t+2)},easeInQuart:function(t){return t*t*t*t},easeOutQuart:function(t){return -((t-=1)*t*t*t-1)},easeInOutQuart:function(t){return (t/=.5)<1?.5*t*t*t*t:-.5*((t-=2)*t*t*t-2)},easeInQuint:function(t){return t*t*t*t*t},easeOutQuint:function(t){return (t-=1)*t*t*t*t+1},easeInOutQuint:function(t){return (t/=.5)<1?.5*t*t*t*t*t:.5*((t-=2)*t*t*t*t+2)},easeInSine:function(t){return 1-Math.cos(t*(Math.PI/2))},easeOutSine:function(t){return Math.sin(t*(Math.PI/2))},easeInOutSine:function(t){return -.5*(Math.cos(Math.PI*t)-1)},easeInExpo:function(t){return 0===t?0:Math.pow(2,10*(t-1))},easeOutExpo:function(t){return 1===t?1:1-Math.pow(2,-10*t)},easeInOutExpo:function(t){return 0===t?0:1===t?1:(t/=.5)<1?.5*Math.pow(2,10*(t-1)):.5*(2-Math.pow(2,-10*--t))},easeInCirc:function(t){return t>=1?t:-(Math.sqrt(1-t*t)-1)},easeOutCirc:function(t){return Math.sqrt(1-(t-=1)*t)},easeInOutCirc:function(t){return (t/=.5)<1?-.5*(Math.sqrt(1-t*t)-1):.5*(Math.sqrt(1-(t-=2)*t)+1)},easeInElastic:function(t){var e=1.70158,n=0,i=1;return 0===t?0:1===t?1:(n||(n=.3),e=n/(2*Math.PI)*Math.asin(1/i),-i*Math.pow(2,10*(t-=1))*Math.sin((t-e)*(2*Math.PI)/n))},easeOutElastic:function(t){var e=1.70158,n=0,i=1;return 0===t?0:1===t?1:(n||(n=.3),e=n/(2*Math.PI)*Math.asin(1/i),i*Math.pow(2,-10*t)*Math.sin((t-e)*(2*Math.PI)/n)+1)},easeInOutElastic:function(t){var e=1.70158,n=0,i=1;return 0===t?0:2==(t/=.5)?1:(n||(n=.45),e=n/(2*Math.PI)*Math.asin(1/i),t<1?i*Math.pow(2,10*(t-=1))*Math.sin((t-e)*(2*Math.PI)/n)*-.5:i*Math.pow(2,-10*(t-=1))*Math.sin((t-e)*(2*Math.PI)/n)*.5+1)},easeInBack:function(t){var e=1.70158;return t*t*((e+1)*t-e)},easeOutBack:function(t){var e=1.70158;return (t-=1)*t*((e+1)*t+e)+1},easeInOutBack:function(t){var e=1.70158;return (t/=.5)<1?t*t*((1+(e*=1.525))*t-e)*.5:.5*((t-=2)*t*((1+(e*=1.525))*t+e)+2)},easeInBounce:function(t){return 1-C.easeOutBounce(1-t)},easeOutBounce:function(t){return t<1/2.75?7.5625*t*t:t<2/2.75?7.5625*(t-=1.5/2.75)*t+.75:t<2.5/2.75?7.5625*(t-=2.25/2.75)*t+.9375:7.5625*(t-=2.625/2.75)*t+.984375},easeInOutBounce:function(t){return t<.5?.5*C.easeInBounce(2*t):.5*C.easeOutBounce(2*t-1)+.5}},P={effects:C};S.easingEffects=C;var A=Math.PI,D=A/180,T=2*A,I=A/2,F=A/4,O=2*A/3,L={clear:function(t){t.ctx.clearRect(0,0,t.width,t.height);},roundedRect:function(t,e,n,i,a,r){if(r){var o=Math.min(r,a/2,i/2),s=e+o,l=n+o,u=e+i-o,d=n+a-o;t.moveTo(e,l),s<u&&l<d?(t.arc(s,l,o,-A,-I),t.arc(u,l,o,-I,0),t.arc(u,d,o,0,I),t.arc(s,d,o,I,A)):s<u?(t.moveTo(s,n),t.arc(u,l,o,-I,I),t.arc(s,l,o,I,A+I)):l<d?(t.arc(s,l,o,-A,0),t.arc(s,d,o,0,A)):t.arc(s,l,o,-A,A),t.closePath(),t.moveTo(e,n);}else t.rect(e,n,i,a);},drawPoint:function(t,e,n,i,a,r){var o,s,l,u,d,h=(r||0)*D;if(e&&"object"==typeof e&&("[object HTMLImageElement]"===(o=e.toString())||"[object HTMLCanvasElement]"===o))return t.save(),t.translate(i,a),t.rotate(h),t.drawImage(e,-e.width/2,-e.height/2,e.width,e.height),void t.restore();if(!(isNaN(n)||n<=0)){switch(t.beginPath(),e){default:t.arc(i,a,n,0,T),t.closePath();break;case"triangle":t.moveTo(i+Math.sin(h)*n,a-Math.cos(h)*n),h+=O,t.lineTo(i+Math.sin(h)*n,a-Math.cos(h)*n),h+=O,t.lineTo(i+Math.sin(h)*n,a-Math.cos(h)*n),t.closePath();break;case"rectRounded":u=n-(d=.516*n),s=Math.cos(h+F)*u,l=Math.sin(h+F)*u,t.arc(i-s,a-l,d,h-A,h-I),t.arc(i+l,a-s,d,h-I,h),t.arc(i+s,a+l,d,h,h+I),t.arc(i-l,a+s,d,h+I,h+A),t.closePath();break;case"rect":if(!r){u=Math.SQRT1_2*n,t.rect(i-u,a-u,2*u,2*u);break}h+=F;case"rectRot":s=Math.cos(h)*n,l=Math.sin(h)*n,t.moveTo(i-s,a-l),t.lineTo(i+l,a-s),t.lineTo(i+s,a+l),t.lineTo(i-l,a+s),t.closePath();break;case"crossRot":h+=F;case"cross":s=Math.cos(h)*n,l=Math.sin(h)*n,t.moveTo(i-s,a-l),t.lineTo(i+s,a+l),t.moveTo(i+l,a-s),t.lineTo(i-l,a+s);break;case"star":s=Math.cos(h)*n,l=Math.sin(h)*n,t.moveTo(i-s,a-l),t.lineTo(i+s,a+l),t.moveTo(i+l,a-s),t.lineTo(i-l,a+s),h+=F,s=Math.cos(h)*n,l=Math.sin(h)*n,t.moveTo(i-s,a-l),t.lineTo(i+s,a+l),t.moveTo(i+l,a-s),t.lineTo(i-l,a+s);break;case"line":s=Math.cos(h)*n,l=Math.sin(h)*n,t.moveTo(i-s,a-l),t.lineTo(i+s,a+l);break;case"dash":t.moveTo(i,a),t.lineTo(i+Math.cos(h)*n,a+Math.sin(h)*n);}t.fill(),t.stroke();}},_isPointInArea:function(t,e){return t.x>e.left-1e-6&&t.x<e.right+1e-6&&t.y>e.top-1e-6&&t.y<e.bottom+1e-6},clipArea:function(t,e){t.save(),t.beginPath(),t.rect(e.left,e.top,e.right-e.left,e.bottom-e.top),t.clip();},unclipArea:function(t){t.restore();},lineTo:function(t,e,n,i){var a=n.steppedLine;if(a){if("middle"===a){var r=(e.x+n.x)/2;t.lineTo(r,i?n.y:e.y),t.lineTo(r,i?e.y:n.y);}else "after"===a&&!i||"after"!==a&&i?t.lineTo(e.x,n.y):t.lineTo(n.x,e.y);t.lineTo(n.x,n.y);}else n.tension?t.bezierCurveTo(i?e.controlPointPreviousX:e.controlPointNextX,i?e.controlPointPreviousY:e.controlPointNextY,i?n.controlPointNextX:n.controlPointPreviousX,i?n.controlPointNextY:n.controlPointPreviousY,n.x,n.y):t.lineTo(n.x,n.y);}},R=L;S.clear=L.clear,S.drawRoundedRectangle=function(t){t.beginPath(),L.roundedRect.apply(L,arguments);};var z={_set:function(t,e){return S.merge(this[t]||(this[t]={}),e)}};z._set("global",{defaultColor:"rgba(0,0,0,0.1)",defaultFontColor:"#666",defaultFontFamily:"'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",defaultFontSize:12,defaultFontStyle:"normal",defaultLineHeight:1.2,showLines:!0});var N=z,B=S.valueOrDefault;var E={toLineHeight:function(t,e){var n=(""+t).match(/^(normal|(\d+(?:\.\d+)?)(px|em|%)?)$/);if(!n||"normal"===n[1])return 1.2*e;switch(t=+n[2],n[3]){case"px":return t;case"%":t/=100;}return e*t},toPadding:function(t){var e,n,i,a;return S.isObject(t)?(e=+t.top||0,n=+t.right||0,i=+t.bottom||0,a=+t.left||0):e=n=i=a=+t||0,{top:e,right:n,bottom:i,left:a,height:e+i,width:a+n}},_parseFont:function(t){var e=N.global,n=B(t.fontSize,e.defaultFontSize),i={family:B(t.fontFamily,e.defaultFontFamily),lineHeight:S.options.toLineHeight(B(t.lineHeight,e.defaultLineHeight),n),size:n,style:B(t.fontStyle,e.defaultFontStyle),weight:null,string:""};return i.string=function(t){return !t||S.isNullOrUndef(t.size)||S.isNullOrUndef(t.family)?null:(t.style?t.style+" ":"")+(t.weight?t.weight+" ":"")+t.size+"px "+t.family}(i),i},resolve:function(t,e,n,i){var a,r,o,s=!0;for(a=0,r=t.length;a<r;++a)if(void 0!==(o=t[a])&&(void 0!==e&&"function"==typeof o&&(o=o(e),s=!1),void 0!==n&&S.isArray(o)&&(o=o[n],s=!1),void 0!==o))return i&&!s&&(i.cacheable=!1),o}},W={_factorize:function(t){var e,n=[],i=Math.sqrt(t);for(e=1;e<i;e++)t%e==0&&(n.push(e),n.push(t/e));return i===(0|i)&&n.push(i),n.sort((function(t,e){return t-e})).pop(),n},log10:Math.log10||function(t){var e=Math.log(t)*Math.LOG10E,n=Math.round(e);return t===Math.pow(10,n)?n:e}},V=W;S.log10=W.log10;var H=S,j=P,q=R,U=E,Y=V,G={getRtlAdapter:function(t,e,n){return t?function(t,e){return {x:function(n){return t+t+e-n},setWidth:function(t){e=t;},textAlign:function(t){return "center"===t?t:"right"===t?"left":"right"},xPlus:function(t,e){return t-e},leftForLtr:function(t,e){return t-e}}}(e,n):{x:function(t){return t},setWidth:function(t){},textAlign:function(t){return t},xPlus:function(t,e){return t+e},leftForLtr:function(t,e){return t}}},overrideTextDirection:function(t,e){var n,i;"ltr"!==e&&"rtl"!==e||(i=[(n=t.canvas.style).getPropertyValue("direction"),n.getPropertyPriority("direction")],n.setProperty("direction",e,"important"),t.prevTextDirection=i);},restoreTextDirection:function(t){var e=t.prevTextDirection;void 0!==e&&(delete t.prevTextDirection,t.canvas.style.setProperty("direction",e[0],e[1]));}};H.easing=j,H.canvas=q,H.options=U,H.math=Y,H.rtl=G;var X=function(t){H.extend(this,t),this.initialize.apply(this,arguments);};H.extend(X.prototype,{_type:void 0,initialize:function(){this.hidden=!1;},pivot:function(){var t=this;return t._view||(t._view=H.extend({},t._model)),t._start={},t},transition:function(t){var e=this,n=e._model,i=e._start,a=e._view;return n&&1!==t?(a||(a=e._view={}),i||(i=e._start={}),function(t,e,n,i){var a,r,o,s,l,u,d,h,c,f=Object.keys(n);for(a=0,r=f.length;a<r;++a)if(u=n[o=f[a]],e.hasOwnProperty(o)||(e[o]=u),(s=e[o])!==u&&"_"!==o[0]){if(t.hasOwnProperty(o)||(t[o]=s),(d=typeof u)===typeof(l=t[o]))if("string"===d){if((h=_(l)).valid&&(c=_(u)).valid){e[o]=c.mix(h,i).rgbString();continue}}else if(H.isFinite(l)&&H.isFinite(u)){e[o]=l+(u-l)*i;continue}e[o]=u;}}(i,a,n,t),e):(e._view=H.extend({},n),e._start=null,e)},tooltipPosition:function(){return {x:this._model.x,y:this._model.y}},hasValue:function(){return H.isNumber(this._model.x)&&H.isNumber(this._model.y)}}),X.extend=H.inherits;var K=X,Z=K.extend({chart:null,currentStep:0,numSteps:60,easing:"",render:null,onAnimationProgress:null,onAnimationComplete:null}),$=Z;Object.defineProperty(Z.prototype,"animationObject",{get:function(){return this}}),Object.defineProperty(Z.prototype,"chartInstance",{get:function(){return this.chart},set:function(t){this.chart=t;}}),N._set("global",{animation:{duration:1e3,easing:"easeOutQuart",onProgress:H.noop,onComplete:H.noop}});var J={animations:[],request:null,addAnimation:function(t,e,n,i){var a,r,o=this.animations;for(e.chart=t,e.startTime=Date.now(),e.duration=n,i||(t.animating=!0),a=0,r=o.length;a<r;++a)if(o[a].chart===t)return void(o[a]=e);o.push(e),1===o.length&&this.requestAnimationFrame();},cancelAnimation:function(t){var e=H.findIndex(this.animations,(function(e){return e.chart===t}));-1!==e&&(this.animations.splice(e,1),t.animating=!1);},requestAnimationFrame:function(){var t=this;null===t.request&&(t.request=H.requestAnimFrame.call(window,(function(){t.request=null,t.startDigest();})));},startDigest:function(){this.advance(),this.animations.length>0&&this.requestAnimationFrame();},advance:function(){for(var t,e,n,i,a=this.animations,r=0;r<a.length;)e=(t=a[r]).chart,n=t.numSteps,i=Math.floor((Date.now()-t.startTime)/t.duration*n)+1,t.currentStep=Math.min(i,n),H.callback(t.render,[e,t],e),H.callback(t.onAnimationProgress,[t],e),t.currentStep>=n?(H.callback(t.onAnimationComplete,[t],e),e.animating=!1,a.splice(r,1)):++r;}},Q=H.options.resolve,tt=["push","pop","shift","splice","unshift"];function et(t,e){var n=t._chartjs;if(n){var i=n.listeners,a=i.indexOf(e);-1!==a&&i.splice(a,1),i.length>0||(tt.forEach((function(e){delete t[e];})),delete t._chartjs);}}var nt=function(t,e){this.initialize(t,e);};H.extend(nt.prototype,{datasetElementType:null,dataElementType:null,_datasetElementOptions:["backgroundColor","borderCapStyle","borderColor","borderDash","borderDashOffset","borderJoinStyle","borderWidth"],_dataElementOptions:["backgroundColor","borderColor","borderWidth","pointStyle"],initialize:function(t,e){var n=this;n.chart=t,n.index=e,n.linkScales(),n.addElements(),n._type=n.getMeta().type;},updateIndex:function(t){this.index=t;},linkScales:function(){var t=this.getMeta(),e=this.chart,n=e.scales,i=this.getDataset(),a=e.options.scales;null!==t.xAxisID&&t.xAxisID in n&&!i.xAxisID||(t.xAxisID=i.xAxisID||a.xAxes[0].id),null!==t.yAxisID&&t.yAxisID in n&&!i.yAxisID||(t.yAxisID=i.yAxisID||a.yAxes[0].id);},getDataset:function(){return this.chart.data.datasets[this.index]},getMeta:function(){return this.chart.getDatasetMeta(this.index)},getScaleForId:function(t){return this.chart.scales[t]},_getValueScaleId:function(){return this.getMeta().yAxisID},_getIndexScaleId:function(){return this.getMeta().xAxisID},_getValueScale:function(){return this.getScaleForId(this._getValueScaleId())},_getIndexScale:function(){return this.getScaleForId(this._getIndexScaleId())},reset:function(){this._update(!0);},destroy:function(){this._data&&et(this._data,this);},createMetaDataset:function(){var t=this.datasetElementType;return t&&new t({_chart:this.chart,_datasetIndex:this.index})},createMetaData:function(t){var e=this.dataElementType;return e&&new e({_chart:this.chart,_datasetIndex:this.index,_index:t})},addElements:function(){var t,e,n=this.getMeta(),i=this.getDataset().data||[],a=n.data;for(t=0,e=i.length;t<e;++t)a[t]=a[t]||this.createMetaData(t);n.dataset=n.dataset||this.createMetaDataset();},addElementAndReset:function(t){var e=this.createMetaData(t);this.getMeta().data.splice(t,0,e),this.updateElement(e,t,!0);},buildOrUpdateElements:function(){var t,e,n=this,i=n.getDataset(),a=i.data||(i.data=[]);n._data!==a&&(n._data&&et(n._data,n),a&&Object.isExtensible(a)&&(e=n,(t=a)._chartjs?t._chartjs.listeners.push(e):(Object.defineProperty(t,"_chartjs",{configurable:!0,enumerable:!1,value:{listeners:[e]}}),tt.forEach((function(e){var n="onData"+e.charAt(0).toUpperCase()+e.slice(1),i=t[e];Object.defineProperty(t,e,{configurable:!0,enumerable:!1,value:function(){var e=Array.prototype.slice.call(arguments),a=i.apply(this,e);return H.each(t._chartjs.listeners,(function(t){"function"==typeof t[n]&&t[n].apply(t,e);})),a}});})))),n._data=a),n.resyncElements();},_configure:function(){this._config=H.merge(Object.create(null),[this.chart.options.datasets[this._type],this.getDataset()],{merger:function(t,e,n){"_meta"!==t&&"data"!==t&&H._merger(t,e,n);}});},_update:function(t){this._configure(),this._cachedDataOpts=null,this.update(t);},update:H.noop,transition:function(t){for(var e=this.getMeta(),n=e.data||[],i=n.length,a=0;a<i;++a)n[a].transition(t);e.dataset&&e.dataset.transition(t);},draw:function(){var t=this.getMeta(),e=t.data||[],n=e.length,i=0;for(t.dataset&&t.dataset.draw();i<n;++i)e[i].draw();},getStyle:function(t){var e,n=this.getMeta(),i=n.dataset;return this._configure(),i&&void 0===t?e=this._resolveDatasetElementOptions(i||{}):(t=t||0,e=this._resolveDataElementOptions(n.data[t]||{},t)),!1!==e.fill&&null!==e.fill||(e.backgroundColor=e.borderColor),e},_resolveDatasetElementOptions:function(t,e){var n,i,a,r,o=this,s=o.chart,l=o._config,u=t.custom||{},d=s.options.elements[o.datasetElementType.prototype._type]||{},h=o._datasetElementOptions,c={},f={chart:s,dataset:o.getDataset(),datasetIndex:o.index,hover:e};for(n=0,i=h.length;n<i;++n)a=h[n],r=e?"hover"+a.charAt(0).toUpperCase()+a.slice(1):a,c[a]=Q([u[r],l[r],d[r]],f);return c},_resolveDataElementOptions:function(t,e){var n=this,i=t&&t.custom,a=n._cachedDataOpts;if(a&&!i)return a;var r,o,s,l,u=n.chart,d=n._config,h=u.options.elements[n.dataElementType.prototype._type]||{},c=n._dataElementOptions,f={},g={chart:u,dataIndex:e,dataset:n.getDataset(),datasetIndex:n.index},p={cacheable:!i};if(i=i||{},H.isArray(c))for(o=0,s=c.length;o<s;++o)f[l=c[o]]=Q([i[l],d[l],h[l]],g,e,p);else for(o=0,s=(r=Object.keys(c)).length;o<s;++o)f[l=r[o]]=Q([i[l],d[c[l]],d[l],h[l]],g,e,p);return p.cacheable&&(n._cachedDataOpts=Object.freeze(f)),f},removeHoverStyle:function(t){H.merge(t._model,t.$previousStyle||{}),delete t.$previousStyle;},setHoverStyle:function(t){var e=this.chart.data.datasets[t._datasetIndex],n=t._index,i=t.custom||{},a=t._model,r=H.getHoverColor;t.$previousStyle={backgroundColor:a.backgroundColor,borderColor:a.borderColor,borderWidth:a.borderWidth},a.backgroundColor=Q([i.hoverBackgroundColor,e.hoverBackgroundColor,r(a.backgroundColor)],void 0,n),a.borderColor=Q([i.hoverBorderColor,e.hoverBorderColor,r(a.borderColor)],void 0,n),a.borderWidth=Q([i.hoverBorderWidth,e.hoverBorderWidth,a.borderWidth],void 0,n);},_removeDatasetHoverStyle:function(){var t=this.getMeta().dataset;t&&this.removeHoverStyle(t);},_setDatasetHoverStyle:function(){var t,e,n,i,a,r,o=this.getMeta().dataset,s={};if(o){for(r=o._model,a=this._resolveDatasetElementOptions(o,!0),t=0,e=(i=Object.keys(a)).length;t<e;++t)s[n=i[t]]=r[n],r[n]=a[n];o.$previousStyle=s;}},resyncElements:function(){var t=this.getMeta(),e=this.getDataset().data,n=t.data.length,i=e.length;i<n?t.data.splice(i,n-i):i>n&&this.insertElements(n,i-n);},insertElements:function(t,e){for(var n=0;n<e;++n)this.addElementAndReset(t+n);},onDataPush:function(){var t=arguments.length;this.insertElements(this.getDataset().data.length-t,t);},onDataPop:function(){this.getMeta().data.pop();},onDataShift:function(){this.getMeta().data.shift();},onDataSplice:function(t,e){this.getMeta().data.splice(t,e),this.insertElements(t,arguments.length-2);},onDataUnshift:function(){this.insertElements(0,arguments.length);}}),nt.extend=H.inherits;var it=nt,at=2*Math.PI;function rt(t,e){var n=e.startAngle,i=e.endAngle,a=e.pixelMargin,r=a/e.outerRadius,o=e.x,s=e.y;t.beginPath(),t.arc(o,s,e.outerRadius,n-r,i+r),e.innerRadius>a?(r=a/e.innerRadius,t.arc(o,s,e.innerRadius-a,i+r,n-r,!0)):t.arc(o,s,a,i+Math.PI/2,n-Math.PI/2),t.closePath(),t.clip();}function ot(t,e,n){var i="inner"===e.borderAlign;i?(t.lineWidth=2*e.borderWidth,t.lineJoin="round"):(t.lineWidth=e.borderWidth,t.lineJoin="bevel"),n.fullCircles&&function(t,e,n,i){var a,r=n.endAngle;for(i&&(n.endAngle=n.startAngle+at,rt(t,n),n.endAngle=r,n.endAngle===n.startAngle&&n.fullCircles&&(n.endAngle+=at,n.fullCircles--)),t.beginPath(),t.arc(n.x,n.y,n.innerRadius,n.startAngle+at,n.startAngle,!0),a=0;a<n.fullCircles;++a)t.stroke();for(t.beginPath(),t.arc(n.x,n.y,e.outerRadius,n.startAngle,n.startAngle+at),a=0;a<n.fullCircles;++a)t.stroke();}(t,e,n,i),i&&rt(t,n),t.beginPath(),t.arc(n.x,n.y,e.outerRadius,n.startAngle,n.endAngle),t.arc(n.x,n.y,n.innerRadius,n.endAngle,n.startAngle,!0),t.closePath(),t.stroke();}N._set("global",{elements:{arc:{backgroundColor:N.global.defaultColor,borderColor:"#fff",borderWidth:2,borderAlign:"center"}}});var st=K.extend({_type:"arc",inLabelRange:function(t){var e=this._view;return !!e&&Math.pow(t-e.x,2)<Math.pow(e.radius+e.hoverRadius,2)},inRange:function(t,e){var n=this._view;if(n){for(var i=H.getAngleFromPoint(n,{x:t,y:e}),a=i.angle,r=i.distance,o=n.startAngle,s=n.endAngle;s<o;)s+=at;for(;a>s;)a-=at;for(;a<o;)a+=at;var l=a>=o&&a<=s,u=r>=n.innerRadius&&r<=n.outerRadius;return l&&u}return !1},getCenterPoint:function(){var t=this._view,e=(t.startAngle+t.endAngle)/2,n=(t.innerRadius+t.outerRadius)/2;return {x:t.x+Math.cos(e)*n,y:t.y+Math.sin(e)*n}},getArea:function(){var t=this._view;return Math.PI*((t.endAngle-t.startAngle)/(2*Math.PI))*(Math.pow(t.outerRadius,2)-Math.pow(t.innerRadius,2))},tooltipPosition:function(){var t=this._view,e=t.startAngle+(t.endAngle-t.startAngle)/2,n=(t.outerRadius-t.innerRadius)/2+t.innerRadius;return {x:t.x+Math.cos(e)*n,y:t.y+Math.sin(e)*n}},draw:function(){var t,e=this._chart.ctx,n=this._view,i="inner"===n.borderAlign?.33:0,a={x:n.x,y:n.y,innerRadius:n.innerRadius,outerRadius:Math.max(n.outerRadius-i,0),pixelMargin:i,startAngle:n.startAngle,endAngle:n.endAngle,fullCircles:Math.floor(n.circumference/at)};if(e.save(),e.fillStyle=n.backgroundColor,e.strokeStyle=n.borderColor,a.fullCircles){for(a.endAngle=a.startAngle+at,e.beginPath(),e.arc(a.x,a.y,a.outerRadius,a.startAngle,a.endAngle),e.arc(a.x,a.y,a.innerRadius,a.endAngle,a.startAngle,!0),e.closePath(),t=0;t<a.fullCircles;++t)e.fill();a.endAngle=a.startAngle+n.circumference%at;}e.beginPath(),e.arc(a.x,a.y,a.outerRadius,a.startAngle,a.endAngle),e.arc(a.x,a.y,a.innerRadius,a.endAngle,a.startAngle,!0),e.closePath(),e.fill(),n.borderWidth&&ot(e,n,a),e.restore();}}),lt=H.valueOrDefault,ut=N.global.defaultColor;N._set("global",{elements:{line:{tension:.4,backgroundColor:ut,borderWidth:3,borderColor:ut,borderCapStyle:"butt",borderDash:[],borderDashOffset:0,borderJoinStyle:"miter",capBezierPoints:!0,fill:!0}}});var dt=K.extend({_type:"line",draw:function(){var t,e,n,i=this,a=i._view,r=i._chart.ctx,o=a.spanGaps,s=i._children.slice(),l=N.global,u=l.elements.line,d=-1,h=i._loop;if(s.length){if(i._loop){for(t=0;t<s.length;++t)if(e=H.previousItem(s,t),!s[t]._view.skip&&e._view.skip){s=s.slice(t).concat(s.slice(0,t)),h=o;break}h&&s.push(s[0]);}for(r.save(),r.lineCap=a.borderCapStyle||u.borderCapStyle,r.setLineDash&&r.setLineDash(a.borderDash||u.borderDash),r.lineDashOffset=lt(a.borderDashOffset,u.borderDashOffset),r.lineJoin=a.borderJoinStyle||u.borderJoinStyle,r.lineWidth=lt(a.borderWidth,u.borderWidth),r.strokeStyle=a.borderColor||l.defaultColor,r.beginPath(),(n=s[0]._view).skip||(r.moveTo(n.x,n.y),d=0),t=1;t<s.length;++t)n=s[t]._view,e=-1===d?H.previousItem(s,t):s[d],n.skip||(d!==t-1&&!o||-1===d?r.moveTo(n.x,n.y):H.canvas.lineTo(r,e._view,n),d=t);h&&r.closePath(),r.stroke(),r.restore();}}}),ht=H.valueOrDefault,ct=N.global.defaultColor;function ft(t){var e=this._view;return !!e&&Math.abs(t-e.x)<e.radius+e.hitRadius}N._set("global",{elements:{point:{radius:3,pointStyle:"circle",backgroundColor:ct,borderColor:ct,borderWidth:1,hitRadius:1,hoverRadius:4,hoverBorderWidth:1}}});var gt=K.extend({_type:"point",inRange:function(t,e){var n=this._view;return !!n&&Math.pow(t-n.x,2)+Math.pow(e-n.y,2)<Math.pow(n.hitRadius+n.radius,2)},inLabelRange:ft,inXRange:ft,inYRange:function(t){var e=this._view;return !!e&&Math.abs(t-e.y)<e.radius+e.hitRadius},getCenterPoint:function(){var t=this._view;return {x:t.x,y:t.y}},getArea:function(){return Math.PI*Math.pow(this._view.radius,2)},tooltipPosition:function(){var t=this._view;return {x:t.x,y:t.y,padding:t.radius+t.borderWidth}},draw:function(t){var e=this._view,n=this._chart.ctx,i=e.pointStyle,a=e.rotation,r=e.radius,o=e.x,s=e.y,l=N.global,u=l.defaultColor;e.skip||(void 0===t||H.canvas._isPointInArea(e,t))&&(n.strokeStyle=e.borderColor||u,n.lineWidth=ht(e.borderWidth,l.elements.point.borderWidth),n.fillStyle=e.backgroundColor||u,H.canvas.drawPoint(n,i,r,o,s,a));}}),pt=N.global.defaultColor;function mt(t){return t&&void 0!==t.width}function vt(t){var e,n,i,a,r;return mt(t)?(r=t.width/2,e=t.x-r,n=t.x+r,i=Math.min(t.y,t.base),a=Math.max(t.y,t.base)):(r=t.height/2,e=Math.min(t.x,t.base),n=Math.max(t.x,t.base),i=t.y-r,a=t.y+r),{left:e,top:i,right:n,bottom:a}}function bt(t,e,n){return t===e?n:t===n?e:t}function xt(t,e,n){var i,a,r,o,s=t.borderWidth,l=function(t){var e=t.borderSkipped,n={};return e?(t.horizontal?t.base>t.x&&(e=bt(e,"left","right")):t.base<t.y&&(e=bt(e,"bottom","top")),n[e]=!0,n):n}(t);return H.isObject(s)?(i=+s.top||0,a=+s.right||0,r=+s.bottom||0,o=+s.left||0):i=a=r=o=+s||0,{t:l.top||i<0?0:i>n?n:i,r:l.right||a<0?0:a>e?e:a,b:l.bottom||r<0?0:r>n?n:r,l:l.left||o<0?0:o>e?e:o}}function yt(t,e,n){var i=null===e,a=null===n,r=!(!t||i&&a)&&vt(t);return r&&(i||e>=r.left&&e<=r.right)&&(a||n>=r.top&&n<=r.bottom)}N._set("global",{elements:{rectangle:{backgroundColor:pt,borderColor:pt,borderSkipped:"bottom",borderWidth:0}}});var _t=K.extend({_type:"rectangle",draw:function(){var t=this._chart.ctx,e=this._view,n=function(t){var e=vt(t),n=e.right-e.left,i=e.bottom-e.top,a=xt(t,n/2,i/2);return {outer:{x:e.left,y:e.top,w:n,h:i},inner:{x:e.left+a.l,y:e.top+a.t,w:n-a.l-a.r,h:i-a.t-a.b}}}(e),i=n.outer,a=n.inner;t.fillStyle=e.backgroundColor,t.fillRect(i.x,i.y,i.w,i.h),i.w===a.w&&i.h===a.h||(t.save(),t.beginPath(),t.rect(i.x,i.y,i.w,i.h),t.clip(),t.fillStyle=e.borderColor,t.rect(a.x,a.y,a.w,a.h),t.fill("evenodd"),t.restore());},height:function(){var t=this._view;return t.base-t.y},inRange:function(t,e){return yt(this._view,t,e)},inLabelRange:function(t,e){var n=this._view;return mt(n)?yt(n,t,null):yt(n,null,e)},inXRange:function(t){return yt(this._view,t,null)},inYRange:function(t){return yt(this._view,null,t)},getCenterPoint:function(){var t,e,n=this._view;return mt(n)?(t=n.x,e=(n.y+n.base)/2):(t=(n.x+n.base)/2,e=n.y),{x:t,y:e}},getArea:function(){var t=this._view;return mt(t)?t.width*Math.abs(t.y-t.base):t.height*Math.abs(t.x-t.base)},tooltipPosition:function(){var t=this._view;return {x:t.x,y:t.y}}}),kt={},wt=st,Mt=dt,St=gt,Ct=_t;kt.Arc=wt,kt.Line=Mt,kt.Point=St,kt.Rectangle=Ct;var Pt=H._deprecated,At=H.valueOrDefault;function Dt(t,e,n){var i,a,r=n.barThickness,o=e.stackCount,s=e.pixels[t],l=H.isNullOrUndef(r)?function(t,e){var n,i,a,r,o=t._length;for(a=1,r=e.length;a<r;++a)o=Math.min(o,Math.abs(e[a]-e[a-1]));for(a=0,r=t.getTicks().length;a<r;++a)i=t.getPixelForTick(a),o=a>0?Math.min(o,Math.abs(i-n)):o,n=i;return o}(e.scale,e.pixels):-1;return H.isNullOrUndef(r)?(i=l*n.categoryPercentage,a=n.barPercentage):(i=r*o,a=1),{chunk:i/o,ratio:a,start:s-i/2}}N._set("bar",{hover:{mode:"label"},scales:{xAxes:[{type:"category",offset:!0,gridLines:{offsetGridLines:!0}}],yAxes:[{type:"linear"}]}}),N._set("global",{datasets:{bar:{categoryPercentage:.8,barPercentage:.9}}});var Tt=it.extend({dataElementType:kt.Rectangle,_dataElementOptions:["backgroundColor","borderColor","borderSkipped","borderWidth","barPercentage","barThickness","categoryPercentage","maxBarThickness","minBarLength"],initialize:function(){var t,e,n=this;it.prototype.initialize.apply(n,arguments),(t=n.getMeta()).stack=n.getDataset().stack,t.bar=!0,e=n._getIndexScale().options,Pt("bar chart",e.barPercentage,"scales.[x/y]Axes.barPercentage","dataset.barPercentage"),Pt("bar chart",e.barThickness,"scales.[x/y]Axes.barThickness","dataset.barThickness"),Pt("bar chart",e.categoryPercentage,"scales.[x/y]Axes.categoryPercentage","dataset.categoryPercentage"),Pt("bar chart",n._getValueScale().options.minBarLength,"scales.[x/y]Axes.minBarLength","dataset.minBarLength"),Pt("bar chart",e.maxBarThickness,"scales.[x/y]Axes.maxBarThickness","dataset.maxBarThickness");},update:function(t){var e,n,i=this.getMeta().data;for(this._ruler=this.getRuler(),e=0,n=i.length;e<n;++e)this.updateElement(i[e],e,t);},updateElement:function(t,e,n){var i=this,a=i.getMeta(),r=i.getDataset(),o=i._resolveDataElementOptions(t,e);t._xScale=i.getScaleForId(a.xAxisID),t._yScale=i.getScaleForId(a.yAxisID),t._datasetIndex=i.index,t._index=e,t._model={backgroundColor:o.backgroundColor,borderColor:o.borderColor,borderSkipped:o.borderSkipped,borderWidth:o.borderWidth,datasetLabel:r.label,label:i.chart.data.labels[e]},H.isArray(r.data[e])&&(t._model.borderSkipped=null),i._updateElementGeometry(t,e,n,o),t.pivot();},_updateElementGeometry:function(t,e,n,i){var a=this,r=t._model,o=a._getValueScale(),s=o.getBasePixel(),l=o.isHorizontal(),u=a._ruler||a.getRuler(),d=a.calculateBarValuePixels(a.index,e,i),h=a.calculateBarIndexPixels(a.index,e,u,i);r.horizontal=l,r.base=n?s:d.base,r.x=l?n?s:d.head:h.center,r.y=l?h.center:n?s:d.head,r.height=l?h.size:void 0,r.width=l?void 0:h.size;},_getStacks:function(t){var e,n,i=this._getIndexScale(),a=i._getMatchingVisibleMetas(this._type),r=i.options.stacked,o=a.length,s=[];for(e=0;e<o&&(n=a[e],(!1===r||-1===s.indexOf(n.stack)||void 0===r&&void 0===n.stack)&&s.push(n.stack),n.index!==t);++e);return s},getStackCount:function(){return this._getStacks().length},getStackIndex:function(t,e){var n=this._getStacks(t),i=void 0!==e?n.indexOf(e):-1;return -1===i?n.length-1:i},getRuler:function(){var t,e,n=this._getIndexScale(),i=[];for(t=0,e=this.getMeta().data.length;t<e;++t)i.push(n.getPixelForValue(null,t,this.index));return {pixels:i,start:n._startPixel,end:n._endPixel,stackCount:this.getStackCount(),scale:n}},calculateBarValuePixels:function(t,e,n){var i,a,r,o,s,l,u,d=this.chart,h=this._getValueScale(),c=h.isHorizontal(),f=d.data.datasets,g=h._getMatchingVisibleMetas(this._type),p=h._parseValue(f[t].data[e]),m=n.minBarLength,v=h.options.stacked,b=this.getMeta().stack,x=void 0===p.start?0:p.max>=0&&p.min>=0?p.min:p.max,y=void 0===p.start?p.end:p.max>=0&&p.min>=0?p.max-p.min:p.min-p.max,_=g.length;if(v||void 0===v&&void 0!==b)for(i=0;i<_&&(a=g[i]).index!==t;++i)a.stack===b&&(r=void 0===(u=h._parseValue(f[a.index].data[e])).start?u.end:u.min>=0&&u.max>=0?u.max:u.min,(p.min<0&&r<0||p.max>=0&&r>0)&&(x+=r));return o=h.getPixelForValue(x),l=(s=h.getPixelForValue(x+y))-o,void 0!==m&&Math.abs(l)<m&&(l=m,s=y>=0&&!c||y<0&&c?o-m:o+m),{size:l,base:o,head:s,center:s+l/2}},calculateBarIndexPixels:function(t,e,n,i){var a="flex"===i.barThickness?function(t,e,n){var i,a=e.pixels,r=a[t],o=t>0?a[t-1]:null,s=t<a.length-1?a[t+1]:null,l=n.categoryPercentage;return null===o&&(o=r-(null===s?e.end-e.start:s-r)),null===s&&(s=r+r-o),i=r-(r-Math.min(o,s))/2*l,{chunk:Math.abs(s-o)/2*l/e.stackCount,ratio:n.barPercentage,start:i}}(e,n,i):Dt(e,n,i),r=this.getStackIndex(t,this.getMeta().stack),o=a.start+a.chunk*r+a.chunk/2,s=Math.min(At(i.maxBarThickness,1/0),a.chunk*a.ratio);return {base:o-s/2,head:o+s/2,center:o,size:s}},draw:function(){var t=this.chart,e=this._getValueScale(),n=this.getMeta().data,i=this.getDataset(),a=n.length,r=0;for(H.canvas.clipArea(t.ctx,t.chartArea);r<a;++r){var o=e._parseValue(i.data[r]);isNaN(o.min)||isNaN(o.max)||n[r].draw();}H.canvas.unclipArea(t.ctx);},_resolveDataElementOptions:function(){var t=this,e=H.extend({},it.prototype._resolveDataElementOptions.apply(t,arguments)),n=t._getIndexScale().options,i=t._getValueScale().options;return e.barPercentage=At(n.barPercentage,e.barPercentage),e.barThickness=At(n.barThickness,e.barThickness),e.categoryPercentage=At(n.categoryPercentage,e.categoryPercentage),e.maxBarThickness=At(n.maxBarThickness,e.maxBarThickness),e.minBarLength=At(i.minBarLength,e.minBarLength),e}}),It=H.valueOrDefault,Ft=H.options.resolve;N._set("bubble",{hover:{mode:"single"},scales:{xAxes:[{type:"linear",position:"bottom",id:"x-axis-0"}],yAxes:[{type:"linear",position:"left",id:"y-axis-0"}]},tooltips:{callbacks:{title:function(){return ""},label:function(t,e){var n=e.datasets[t.datasetIndex].label||"",i=e.datasets[t.datasetIndex].data[t.index];return n+": ("+t.xLabel+", "+t.yLabel+", "+i.r+")"}}}});var Ot=it.extend({dataElementType:kt.Point,_dataElementOptions:["backgroundColor","borderColor","borderWidth","hoverBackgroundColor","hoverBorderColor","hoverBorderWidth","hoverRadius","hitRadius","pointStyle","rotation"],update:function(t){var e=this,n=e.getMeta().data;H.each(n,(function(n,i){e.updateElement(n,i,t);}));},updateElement:function(t,e,n){var i=this,a=i.getMeta(),r=t.custom||{},o=i.getScaleForId(a.xAxisID),s=i.getScaleForId(a.yAxisID),l=i._resolveDataElementOptions(t,e),u=i.getDataset().data[e],d=i.index,h=n?o.getPixelForDecimal(.5):o.getPixelForValue("object"==typeof u?u:NaN,e,d),c=n?s.getBasePixel():s.getPixelForValue(u,e,d);t._xScale=o,t._yScale=s,t._options=l,t._datasetIndex=d,t._index=e,t._model={backgroundColor:l.backgroundColor,borderColor:l.borderColor,borderWidth:l.borderWidth,hitRadius:l.hitRadius,pointStyle:l.pointStyle,rotation:l.rotation,radius:n?0:l.radius,skip:r.skip||isNaN(h)||isNaN(c),x:h,y:c},t.pivot();},setHoverStyle:function(t){var e=t._model,n=t._options,i=H.getHoverColor;t.$previousStyle={backgroundColor:e.backgroundColor,borderColor:e.borderColor,borderWidth:e.borderWidth,radius:e.radius},e.backgroundColor=It(n.hoverBackgroundColor,i(n.backgroundColor)),e.borderColor=It(n.hoverBorderColor,i(n.borderColor)),e.borderWidth=It(n.hoverBorderWidth,n.borderWidth),e.radius=n.radius+n.hoverRadius;},_resolveDataElementOptions:function(t,e){var n=this,i=n.chart,a=n.getDataset(),r=t.custom||{},o=a.data[e]||{},s=it.prototype._resolveDataElementOptions.apply(n,arguments),l={chart:i,dataIndex:e,dataset:a,datasetIndex:n.index};return n._cachedDataOpts===s&&(s=H.extend({},s)),s.radius=Ft([r.radius,o.r,n._config.radius,i.options.elements.point.radius],l,e),s}}),Lt=H.valueOrDefault,Rt=Math.PI,zt=2*Rt,Nt=Rt/2;N._set("doughnut",{animation:{animateRotate:!0,animateScale:!1},hover:{mode:"single"},legendCallback:function(t){var e,n,i,a=document.createElement("ul"),r=t.data,o=r.datasets,s=r.labels;if(a.setAttribute("class",t.id+"-legend"),o.length)for(e=0,n=o[0].data.length;e<n;++e)(i=a.appendChild(document.createElement("li"))).appendChild(document.createElement("span")).style.backgroundColor=o[0].backgroundColor[e],s[e]&&i.appendChild(document.createTextNode(s[e]));return a.outerHTML},legend:{labels:{generateLabels:function(t){var e=t.data;return e.labels.length&&e.datasets.length?e.labels.map((function(n,i){var a=t.getDatasetMeta(0),r=a.controller.getStyle(i);return {text:n,fillStyle:r.backgroundColor,strokeStyle:r.borderColor,lineWidth:r.borderWidth,hidden:isNaN(e.datasets[0].data[i])||a.data[i].hidden,index:i}})):[]}},onClick:function(t,e){var n,i,a,r=e.index,o=this.chart;for(n=0,i=(o.data.datasets||[]).length;n<i;++n)(a=o.getDatasetMeta(n)).data[r]&&(a.data[r].hidden=!a.data[r].hidden);o.update();}},cutoutPercentage:50,rotation:-Nt,circumference:zt,tooltips:{callbacks:{title:function(){return ""},label:function(t,e){var n=e.labels[t.index],i=": "+e.datasets[t.datasetIndex].data[t.index];return H.isArray(n)?(n=n.slice())[0]+=i:n+=i,n}}}});var Bt=it.extend({dataElementType:kt.Arc,linkScales:H.noop,_dataElementOptions:["backgroundColor","borderColor","borderWidth","borderAlign","hoverBackgroundColor","hoverBorderColor","hoverBorderWidth"],getRingIndex:function(t){for(var e=0,n=0;n<t;++n)this.chart.isDatasetVisible(n)&&++e;return e},update:function(t){var e,n,i,a,r=this,o=r.chart,s=o.chartArea,l=o.options,u=1,d=1,h=0,c=0,f=r.getMeta(),g=f.data,p=l.cutoutPercentage/100||0,m=l.circumference,v=r._getRingWeight(r.index);if(m<zt){var b=l.rotation%zt,x=(b+=b>=Rt?-zt:b<-Rt?zt:0)+m,y=Math.cos(b),_=Math.sin(b),k=Math.cos(x),w=Math.sin(x),M=b<=0&&x>=0||x>=zt,S=b<=Nt&&x>=Nt||x>=zt+Nt,C=b<=-Nt&&x>=-Nt||x>=Rt+Nt,P=b===-Rt||x>=Rt?-1:Math.min(y,y*p,k,k*p),A=C?-1:Math.min(_,_*p,w,w*p),D=M?1:Math.max(y,y*p,k,k*p),T=S?1:Math.max(_,_*p,w,w*p);u=(D-P)/2,d=(T-A)/2,h=-(D+P)/2,c=-(T+A)/2;}for(i=0,a=g.length;i<a;++i)g[i]._options=r._resolveDataElementOptions(g[i],i);for(o.borderWidth=r.getMaxBorderWidth(),e=(s.right-s.left-o.borderWidth)/u,n=(s.bottom-s.top-o.borderWidth)/d,o.outerRadius=Math.max(Math.min(e,n)/2,0),o.innerRadius=Math.max(o.outerRadius*p,0),o.radiusLength=(o.outerRadius-o.innerRadius)/(r._getVisibleDatasetWeightTotal()||1),o.offsetX=h*o.outerRadius,o.offsetY=c*o.outerRadius,f.total=r.calculateTotal(),r.outerRadius=o.outerRadius-o.radiusLength*r._getRingWeightOffset(r.index),r.innerRadius=Math.max(r.outerRadius-o.radiusLength*v,0),i=0,a=g.length;i<a;++i)r.updateElement(g[i],i,t);},updateElement:function(t,e,n){var i=this,a=i.chart,r=a.chartArea,o=a.options,s=o.animation,l=(r.left+r.right)/2,u=(r.top+r.bottom)/2,d=o.rotation,h=o.rotation,c=i.getDataset(),f=n&&s.animateRotate?0:t.hidden?0:i.calculateCircumference(c.data[e])*(o.circumference/zt),g=n&&s.animateScale?0:i.innerRadius,p=n&&s.animateScale?0:i.outerRadius,m=t._options||{};H.extend(t,{_datasetIndex:i.index,_index:e,_model:{backgroundColor:m.backgroundColor,borderColor:m.borderColor,borderWidth:m.borderWidth,borderAlign:m.borderAlign,x:l+a.offsetX,y:u+a.offsetY,startAngle:d,endAngle:h,circumference:f,outerRadius:p,innerRadius:g,label:H.valueAtIndexOrDefault(c.label,e,a.data.labels[e])}});var v=t._model;n&&s.animateRotate||(v.startAngle=0===e?o.rotation:i.getMeta().data[e-1]._model.endAngle,v.endAngle=v.startAngle+v.circumference),t.pivot();},calculateTotal:function(){var t,e=this.getDataset(),n=this.getMeta(),i=0;return H.each(n.data,(function(n,a){t=e.data[a],isNaN(t)||n.hidden||(i+=Math.abs(t));})),i},calculateCircumference:function(t){var e=this.getMeta().total;return e>0&&!isNaN(t)?zt*(Math.abs(t)/e):0},getMaxBorderWidth:function(t){var e,n,i,a,r,o,s,l,u=0,d=this.chart;if(!t)for(e=0,n=d.data.datasets.length;e<n;++e)if(d.isDatasetVisible(e)){t=(i=d.getDatasetMeta(e)).data,e!==this.index&&(r=i.controller);break}if(!t)return 0;for(e=0,n=t.length;e<n;++e)a=t[e],r?(r._configure(),o=r._resolveDataElementOptions(a,e)):o=a._options,"inner"!==o.borderAlign&&(s=o.borderWidth,u=(l=o.hoverBorderWidth)>(u=s>u?s:u)?l:u);return u},setHoverStyle:function(t){var e=t._model,n=t._options,i=H.getHoverColor;t.$previousStyle={backgroundColor:e.backgroundColor,borderColor:e.borderColor,borderWidth:e.borderWidth},e.backgroundColor=Lt(n.hoverBackgroundColor,i(n.backgroundColor)),e.borderColor=Lt(n.hoverBorderColor,i(n.borderColor)),e.borderWidth=Lt(n.hoverBorderWidth,n.borderWidth);},_getRingWeightOffset:function(t){for(var e=0,n=0;n<t;++n)this.chart.isDatasetVisible(n)&&(e+=this._getRingWeight(n));return e},_getRingWeight:function(t){return Math.max(Lt(this.chart.data.datasets[t].weight,1),0)},_getVisibleDatasetWeightTotal:function(){return this._getRingWeightOffset(this.chart.data.datasets.length)}});N._set("horizontalBar",{hover:{mode:"index",axis:"y"},scales:{xAxes:[{type:"linear",position:"bottom"}],yAxes:[{type:"category",position:"left",offset:!0,gridLines:{offsetGridLines:!0}}]},elements:{rectangle:{borderSkipped:"left"}},tooltips:{mode:"index",axis:"y"}}),N._set("global",{datasets:{horizontalBar:{categoryPercentage:.8,barPercentage:.9}}});var Et=Tt.extend({_getValueScaleId:function(){return this.getMeta().xAxisID},_getIndexScaleId:function(){return this.getMeta().yAxisID}}),Wt=H.valueOrDefault,Vt=H.options.resolve,Ht=H.canvas._isPointInArea;function jt(t,e){var n=t&&t.options.ticks||{},i=n.reverse,a=void 0===n.min?e:0,r=void 0===n.max?e:0;return {start:i?r:a,end:i?a:r}}function qt(t,e,n){var i=n/2,a=jt(t,i),r=jt(e,i);return {top:r.end,right:a.end,bottom:r.start,left:a.start}}function Ut(t){var e,n,i,a;return H.isObject(t)?(e=t.top,n=t.right,i=t.bottom,a=t.left):e=n=i=a=t,{top:e,right:n,bottom:i,left:a}}N._set("line",{showLines:!0,spanGaps:!1,hover:{mode:"label"},scales:{xAxes:[{type:"category",id:"x-axis-0"}],yAxes:[{type:"linear",id:"y-axis-0"}]}});var Yt=it.extend({datasetElementType:kt.Line,dataElementType:kt.Point,_datasetElementOptions:["backgroundColor","borderCapStyle","borderColor","borderDash","borderDashOffset","borderJoinStyle","borderWidth","cubicInterpolationMode","fill"],_dataElementOptions:{backgroundColor:"pointBackgroundColor",borderColor:"pointBorderColor",borderWidth:"pointBorderWidth",hitRadius:"pointHitRadius",hoverBackgroundColor:"pointHoverBackgroundColor",hoverBorderColor:"pointHoverBorderColor",hoverBorderWidth:"pointHoverBorderWidth",hoverRadius:"pointHoverRadius",pointStyle:"pointStyle",radius:"pointRadius",rotation:"pointRotation"},update:function(t){var e,n,i=this,a=i.getMeta(),r=a.dataset,o=a.data||[],s=i.chart.options,l=i._config,u=i._showLine=Wt(l.showLine,s.showLines);for(i._xScale=i.getScaleForId(a.xAxisID),i._yScale=i.getScaleForId(a.yAxisID),u&&(void 0!==l.tension&&void 0===l.lineTension&&(l.lineTension=l.tension),r._scale=i._yScale,r._datasetIndex=i.index,r._children=o,r._model=i._resolveDatasetElementOptions(r),r.pivot()),e=0,n=o.length;e<n;++e)i.updateElement(o[e],e,t);for(u&&0!==r._model.tension&&i.updateBezierControlPoints(),e=0,n=o.length;e<n;++e)o[e].pivot();},updateElement:function(t,e,n){var i,a,r=this,o=r.getMeta(),s=t.custom||{},l=r.getDataset(),u=r.index,d=l.data[e],h=r._xScale,c=r._yScale,f=o.dataset._model,g=r._resolveDataElementOptions(t,e);i=h.getPixelForValue("object"==typeof d?d:NaN,e,u),a=n?c.getBasePixel():r.calculatePointY(d,e,u),t._xScale=h,t._yScale=c,t._options=g,t._datasetIndex=u,t._index=e,t._model={x:i,y:a,skip:s.skip||isNaN(i)||isNaN(a),radius:g.radius,pointStyle:g.pointStyle,rotation:g.rotation,backgroundColor:g.backgroundColor,borderColor:g.borderColor,borderWidth:g.borderWidth,tension:Wt(s.tension,f?f.tension:0),steppedLine:!!f&&f.steppedLine,hitRadius:g.hitRadius};},_resolveDatasetElementOptions:function(t){var e=this,n=e._config,i=t.custom||{},a=e.chart.options,r=a.elements.line,o=it.prototype._resolveDatasetElementOptions.apply(e,arguments);return o.spanGaps=Wt(n.spanGaps,a.spanGaps),o.tension=Wt(n.lineTension,r.tension),o.steppedLine=Vt([i.steppedLine,n.steppedLine,r.stepped]),o.clip=Ut(Wt(n.clip,qt(e._xScale,e._yScale,o.borderWidth))),o},calculatePointY:function(t,e,n){var i,a,r,o,s,l,u,d=this.chart,h=this._yScale,c=0,f=0;if(h.options.stacked){for(s=+h.getRightValue(t),u=(l=d._getSortedVisibleDatasetMetas()).length,i=0;i<u&&(r=l[i]).index!==n;++i)a=d.data.datasets[r.index],"line"===r.type&&r.yAxisID===h.id&&((o=+h.getRightValue(a.data[e]))<0?f+=o||0:c+=o||0);return s<0?h.getPixelForValue(f+s):h.getPixelForValue(c+s)}return h.getPixelForValue(t)},updateBezierControlPoints:function(){var t,e,n,i,a=this.chart,r=this.getMeta(),o=r.dataset._model,s=a.chartArea,l=r.data||[];function u(t,e,n){return Math.max(Math.min(t,n),e)}if(o.spanGaps&&(l=l.filter((function(t){return !t._model.skip}))),"monotone"===o.cubicInterpolationMode)H.splineCurveMonotone(l);else for(t=0,e=l.length;t<e;++t)n=l[t]._model,i=H.splineCurve(H.previousItem(l,t)._model,n,H.nextItem(l,t)._model,o.tension),n.controlPointPreviousX=i.previous.x,n.controlPointPreviousY=i.previous.y,n.controlPointNextX=i.next.x,n.controlPointNextY=i.next.y;if(a.options.elements.line.capBezierPoints)for(t=0,e=l.length;t<e;++t)n=l[t]._model,Ht(n,s)&&(t>0&&Ht(l[t-1]._model,s)&&(n.controlPointPreviousX=u(n.controlPointPreviousX,s.left,s.right),n.controlPointPreviousY=u(n.controlPointPreviousY,s.top,s.bottom)),t<l.length-1&&Ht(l[t+1]._model,s)&&(n.controlPointNextX=u(n.controlPointNextX,s.left,s.right),n.controlPointNextY=u(n.controlPointNextY,s.top,s.bottom)));},draw:function(){var t,e=this.chart,n=this.getMeta(),i=n.data||[],a=e.chartArea,r=e.canvas,o=0,s=i.length;for(this._showLine&&(t=n.dataset._model.clip,H.canvas.clipArea(e.ctx,{left:!1===t.left?0:a.left-t.left,right:!1===t.right?r.width:a.right+t.right,top:!1===t.top?0:a.top-t.top,bottom:!1===t.bottom?r.height:a.bottom+t.bottom}),n.dataset.draw(),H.canvas.unclipArea(e.ctx));o<s;++o)i[o].draw(a);},setHoverStyle:function(t){var e=t._model,n=t._options,i=H.getHoverColor;t.$previousStyle={backgroundColor:e.backgroundColor,borderColor:e.borderColor,borderWidth:e.borderWidth,radius:e.radius},e.backgroundColor=Wt(n.hoverBackgroundColor,i(n.backgroundColor)),e.borderColor=Wt(n.hoverBorderColor,i(n.borderColor)),e.borderWidth=Wt(n.hoverBorderWidth,n.borderWidth),e.radius=Wt(n.hoverRadius,n.radius);}}),Gt=H.options.resolve;N._set("polarArea",{scale:{type:"radialLinear",angleLines:{display:!1},gridLines:{circular:!0},pointLabels:{display:!1},ticks:{beginAtZero:!0}},animation:{animateRotate:!0,animateScale:!0},startAngle:-.5*Math.PI,legendCallback:function(t){var e,n,i,a=document.createElement("ul"),r=t.data,o=r.datasets,s=r.labels;if(a.setAttribute("class",t.id+"-legend"),o.length)for(e=0,n=o[0].data.length;e<n;++e)(i=a.appendChild(document.createElement("li"))).appendChild(document.createElement("span")).style.backgroundColor=o[0].backgroundColor[e],s[e]&&i.appendChild(document.createTextNode(s[e]));return a.outerHTML},legend:{labels:{generateLabels:function(t){var e=t.data;return e.labels.length&&e.datasets.length?e.labels.map((function(n,i){var a=t.getDatasetMeta(0),r=a.controller.getStyle(i);return {text:n,fillStyle:r.backgroundColor,strokeStyle:r.borderColor,lineWidth:r.borderWidth,hidden:isNaN(e.datasets[0].data[i])||a.data[i].hidden,index:i}})):[]}},onClick:function(t,e){var n,i,a,r=e.index,o=this.chart;for(n=0,i=(o.data.datasets||[]).length;n<i;++n)(a=o.getDatasetMeta(n)).data[r].hidden=!a.data[r].hidden;o.update();}},tooltips:{callbacks:{title:function(){return ""},label:function(t,e){return e.labels[t.index]+": "+t.yLabel}}}});var Xt=it.extend({dataElementType:kt.Arc,linkScales:H.noop,_dataElementOptions:["backgroundColor","borderColor","borderWidth","borderAlign","hoverBackgroundColor","hoverBorderColor","hoverBorderWidth"],_getIndexScaleId:function(){return this.chart.scale.id},_getValueScaleId:function(){return this.chart.scale.id},update:function(t){var e,n,i,a=this,r=a.getDataset(),o=a.getMeta(),s=a.chart.options.startAngle||0,l=a._starts=[],u=a._angles=[],d=o.data;for(a._updateRadius(),o.count=a.countVisibleElements(),e=0,n=r.data.length;e<n;e++)l[e]=s,i=a._computeAngle(e),u[e]=i,s+=i;for(e=0,n=d.length;e<n;++e)d[e]._options=a._resolveDataElementOptions(d[e],e),a.updateElement(d[e],e,t);},_updateRadius:function(){var t=this,e=t.chart,n=e.chartArea,i=e.options,a=Math.min(n.right-n.left,n.bottom-n.top);e.outerRadius=Math.max(a/2,0),e.innerRadius=Math.max(i.cutoutPercentage?e.outerRadius/100*i.cutoutPercentage:1,0),e.radiusLength=(e.outerRadius-e.innerRadius)/e.getVisibleDatasetCount(),t.outerRadius=e.outerRadius-e.radiusLength*t.index,t.innerRadius=t.outerRadius-e.radiusLength;},updateElement:function(t,e,n){var i=this,a=i.chart,r=i.getDataset(),o=a.options,s=o.animation,l=a.scale,u=a.data.labels,d=l.xCenter,h=l.yCenter,c=o.startAngle,f=t.hidden?0:l.getDistanceFromCenterForValue(r.data[e]),g=i._starts[e],p=g+(t.hidden?0:i._angles[e]),m=s.animateScale?0:l.getDistanceFromCenterForValue(r.data[e]),v=t._options||{};H.extend(t,{_datasetIndex:i.index,_index:e,_scale:l,_model:{backgroundColor:v.backgroundColor,borderColor:v.borderColor,borderWidth:v.borderWidth,borderAlign:v.borderAlign,x:d,y:h,innerRadius:0,outerRadius:n?m:f,startAngle:n&&s.animateRotate?c:g,endAngle:n&&s.animateRotate?c:p,label:H.valueAtIndexOrDefault(u,e,u[e])}}),t.pivot();},countVisibleElements:function(){var t=this.getDataset(),e=this.getMeta(),n=0;return H.each(e.data,(function(e,i){isNaN(t.data[i])||e.hidden||n++;})),n},setHoverStyle:function(t){var e=t._model,n=t._options,i=H.getHoverColor,a=H.valueOrDefault;t.$previousStyle={backgroundColor:e.backgroundColor,borderColor:e.borderColor,borderWidth:e.borderWidth},e.backgroundColor=a(n.hoverBackgroundColor,i(n.backgroundColor)),e.borderColor=a(n.hoverBorderColor,i(n.borderColor)),e.borderWidth=a(n.hoverBorderWidth,n.borderWidth);},_computeAngle:function(t){var e=this,n=this.getMeta().count,i=e.getDataset(),a=e.getMeta();if(isNaN(i.data[t])||a.data[t].hidden)return 0;var r={chart:e.chart,dataIndex:t,dataset:i,datasetIndex:e.index};return Gt([e.chart.options.elements.arc.angle,2*Math.PI/n],r,t)}});N._set("pie",H.clone(N.doughnut)),N._set("pie",{cutoutPercentage:0});var Kt=Bt,Zt=H.valueOrDefault;N._set("radar",{spanGaps:!1,scale:{type:"radialLinear"},elements:{line:{fill:"start",tension:0}}});var $t=it.extend({datasetElementType:kt.Line,dataElementType:kt.Point,linkScales:H.noop,_datasetElementOptions:["backgroundColor","borderWidth","borderColor","borderCapStyle","borderDash","borderDashOffset","borderJoinStyle","fill"],_dataElementOptions:{backgroundColor:"pointBackgroundColor",borderColor:"pointBorderColor",borderWidth:"pointBorderWidth",hitRadius:"pointHitRadius",hoverBackgroundColor:"pointHoverBackgroundColor",hoverBorderColor:"pointHoverBorderColor",hoverBorderWidth:"pointHoverBorderWidth",hoverRadius:"pointHoverRadius",pointStyle:"pointStyle",radius:"pointRadius",rotation:"pointRotation"},_getIndexScaleId:function(){return this.chart.scale.id},_getValueScaleId:function(){return this.chart.scale.id},update:function(t){var e,n,i=this,a=i.getMeta(),r=a.dataset,o=a.data||[],s=i.chart.scale,l=i._config;for(void 0!==l.tension&&void 0===l.lineTension&&(l.lineTension=l.tension),r._scale=s,r._datasetIndex=i.index,r._children=o,r._loop=!0,r._model=i._resolveDatasetElementOptions(r),r.pivot(),e=0,n=o.length;e<n;++e)i.updateElement(o[e],e,t);for(i.updateBezierControlPoints(),e=0,n=o.length;e<n;++e)o[e].pivot();},updateElement:function(t,e,n){var i=this,a=t.custom||{},r=i.getDataset(),o=i.chart.scale,s=o.getPointPositionForValue(e,r.data[e]),l=i._resolveDataElementOptions(t,e),u=i.getMeta().dataset._model,d=n?o.xCenter:s.x,h=n?o.yCenter:s.y;t._scale=o,t._options=l,t._datasetIndex=i.index,t._index=e,t._model={x:d,y:h,skip:a.skip||isNaN(d)||isNaN(h),radius:l.radius,pointStyle:l.pointStyle,rotation:l.rotation,backgroundColor:l.backgroundColor,borderColor:l.borderColor,borderWidth:l.borderWidth,tension:Zt(a.tension,u?u.tension:0),hitRadius:l.hitRadius};},_resolveDatasetElementOptions:function(){var t=this,e=t._config,n=t.chart.options,i=it.prototype._resolveDatasetElementOptions.apply(t,arguments);return i.spanGaps=Zt(e.spanGaps,n.spanGaps),i.tension=Zt(e.lineTension,n.elements.line.tension),i},updateBezierControlPoints:function(){var t,e,n,i,a=this.getMeta(),r=this.chart.chartArea,o=a.data||[];function s(t,e,n){return Math.max(Math.min(t,n),e)}for(a.dataset._model.spanGaps&&(o=o.filter((function(t){return !t._model.skip}))),t=0,e=o.length;t<e;++t)n=o[t]._model,i=H.splineCurve(H.previousItem(o,t,!0)._model,n,H.nextItem(o,t,!0)._model,n.tension),n.controlPointPreviousX=s(i.previous.x,r.left,r.right),n.controlPointPreviousY=s(i.previous.y,r.top,r.bottom),n.controlPointNextX=s(i.next.x,r.left,r.right),n.controlPointNextY=s(i.next.y,r.top,r.bottom);},setHoverStyle:function(t){var e=t._model,n=t._options,i=H.getHoverColor;t.$previousStyle={backgroundColor:e.backgroundColor,borderColor:e.borderColor,borderWidth:e.borderWidth,radius:e.radius},e.backgroundColor=Zt(n.hoverBackgroundColor,i(n.backgroundColor)),e.borderColor=Zt(n.hoverBorderColor,i(n.borderColor)),e.borderWidth=Zt(n.hoverBorderWidth,n.borderWidth),e.radius=Zt(n.hoverRadius,n.radius);}});N._set("scatter",{hover:{mode:"single"},scales:{xAxes:[{id:"x-axis-1",type:"linear",position:"bottom"}],yAxes:[{id:"y-axis-1",type:"linear",position:"left"}]},tooltips:{callbacks:{title:function(){return ""},label:function(t){return "("+t.xLabel+", "+t.yLabel+")"}}}}),N._set("global",{datasets:{scatter:{showLine:!1}}});var Jt={bar:Tt,bubble:Ot,doughnut:Bt,horizontalBar:Et,line:Yt,polarArea:Xt,pie:Kt,radar:$t,scatter:Yt};function Qt(t,e){return t.native?{x:t.x,y:t.y}:H.getRelativePosition(t,e)}function te(t,e){var n,i,a,r,o,s,l=t._getSortedVisibleDatasetMetas();for(i=0,r=l.length;i<r;++i)for(a=0,o=(n=l[i].data).length;a<o;++a)(s=n[a])._view.skip||e(s);}function ee(t,e){var n=[];return te(t,(function(t){t.inRange(e.x,e.y)&&n.push(t);})),n}function ne(t,e,n,i){var a=Number.POSITIVE_INFINITY,r=[];return te(t,(function(t){if(!n||t.inRange(e.x,e.y)){var o=t.getCenterPoint(),s=i(e,o);s<a?(r=[t],a=s):s===a&&r.push(t);}})),r}function ie(t){var e=-1!==t.indexOf("x"),n=-1!==t.indexOf("y");return function(t,i){var a=e?Math.abs(t.x-i.x):0,r=n?Math.abs(t.y-i.y):0;return Math.sqrt(Math.pow(a,2)+Math.pow(r,2))}}function ae(t,e,n){var i=Qt(e,t);n.axis=n.axis||"x";var a=ie(n.axis),r=n.intersect?ee(t,i):ne(t,i,!1,a),o=[];return r.length?(t._getSortedVisibleDatasetMetas().forEach((function(t){var e=t.data[r[0]._index];e&&!e._view.skip&&o.push(e);})),o):[]}var re={modes:{single:function(t,e){var n=Qt(e,t),i=[];return te(t,(function(t){if(t.inRange(n.x,n.y))return i.push(t),i})),i.slice(0,1)},label:ae,index:ae,dataset:function(t,e,n){var i=Qt(e,t);n.axis=n.axis||"xy";var a=ie(n.axis),r=n.intersect?ee(t,i):ne(t,i,!1,a);return r.length>0&&(r=t.getDatasetMeta(r[0]._datasetIndex).data),r},"x-axis":function(t,e){return ae(t,e,{intersect:!1})},point:function(t,e){return ee(t,Qt(e,t))},nearest:function(t,e,n){var i=Qt(e,t);n.axis=n.axis||"xy";var a=ie(n.axis);return ne(t,i,n.intersect,a)},x:function(t,e,n){var i=Qt(e,t),a=[],r=!1;return te(t,(function(t){t.inXRange(i.x)&&a.push(t),t.inRange(i.x,i.y)&&(r=!0);})),n.intersect&&!r&&(a=[]),a},y:function(t,e,n){var i=Qt(e,t),a=[],r=!1;return te(t,(function(t){t.inYRange(i.y)&&a.push(t),t.inRange(i.x,i.y)&&(r=!0);})),n.intersect&&!r&&(a=[]),a}}},oe=H.extend;function se(t,e){return H.where(t,(function(t){return t.pos===e}))}function le(t,e){return t.sort((function(t,n){var i=e?n:t,a=e?t:n;return i.weight===a.weight?i.index-a.index:i.weight-a.weight}))}function ue(t,e,n,i){return Math.max(t[n],e[n])+Math.max(t[i],e[i])}function de(t,e,n){var i,a,r=n.box,o=t.maxPadding;if(n.size&&(t[n.pos]-=n.size),n.size=n.horizontal?r.height:r.width,t[n.pos]+=n.size,r.getPadding){var s=r.getPadding();o.top=Math.max(o.top,s.top),o.left=Math.max(o.left,s.left),o.bottom=Math.max(o.bottom,s.bottom),o.right=Math.max(o.right,s.right);}if(i=e.outerWidth-ue(o,t,"left","right"),a=e.outerHeight-ue(o,t,"top","bottom"),i!==t.w||a!==t.h){t.w=i,t.h=a;var l=n.horizontal?[i,t.w]:[a,t.h];return !(l[0]===l[1]||isNaN(l[0])&&isNaN(l[1]))}}function he(t,e){var n=e.maxPadding;function i(t){var i={left:0,top:0,right:0,bottom:0};return t.forEach((function(t){i[t]=Math.max(e[t],n[t]);})),i}return i(t?["left","right"]:["top","bottom"])}function ce(t,e,n){var i,a,r,o,s,l,u=[];for(i=0,a=t.length;i<a;++i)(o=(r=t[i]).box).update(r.width||e.w,r.height||e.h,he(r.horizontal,e)),de(e,n,r)&&(l=!0,u.length&&(s=!0)),o.fullWidth||u.push(r);return s&&ce(u,e,n)||l}function fe(t,e,n){var i,a,r,o,s=n.padding,l=e.x,u=e.y;for(i=0,a=t.length;i<a;++i)o=(r=t[i]).box,r.horizontal?(o.left=o.fullWidth?s.left:e.left,o.right=o.fullWidth?n.outerWidth-s.right:e.left+e.w,o.top=u,o.bottom=u+o.height,o.width=o.right-o.left,u=o.bottom):(o.left=l,o.right=l+o.width,o.top=e.top,o.bottom=e.top+e.h,o.height=o.bottom-o.top,l=o.right);e.x=l,e.y=u;}N._set("global",{layout:{padding:{top:0,right:0,bottom:0,left:0}}});var ge,pe={defaults:{},addBox:function(t,e){t.boxes||(t.boxes=[]),e.fullWidth=e.fullWidth||!1,e.position=e.position||"top",e.weight=e.weight||0,e._layers=e._layers||function(){return [{z:0,draw:function(){e.draw.apply(e,arguments);}}]},t.boxes.push(e);},removeBox:function(t,e){var n=t.boxes?t.boxes.indexOf(e):-1;-1!==n&&t.boxes.splice(n,1);},configure:function(t,e,n){for(var i,a=["fullWidth","position","weight"],r=a.length,o=0;o<r;++o)i=a[o],n.hasOwnProperty(i)&&(e[i]=n[i]);},update:function(t,e,n){if(t){var i=t.options.layout||{},a=H.options.toPadding(i.padding),r=e-a.width,o=n-a.height,s=function(t){var e=function(t){var e,n,i,a=[];for(e=0,n=(t||[]).length;e<n;++e)i=t[e],a.push({index:e,box:i,pos:i.position,horizontal:i.isHorizontal(),weight:i.weight});return a}(t),n=le(se(e,"left"),!0),i=le(se(e,"right")),a=le(se(e,"top"),!0),r=le(se(e,"bottom"));return {leftAndTop:n.concat(a),rightAndBottom:i.concat(r),chartArea:se(e,"chartArea"),vertical:n.concat(i),horizontal:a.concat(r)}}(t.boxes),l=s.vertical,u=s.horizontal,d=Object.freeze({outerWidth:e,outerHeight:n,padding:a,availableWidth:r,vBoxMaxWidth:r/2/l.length,hBoxMaxHeight:o/2}),h=oe({maxPadding:oe({},a),w:r,h:o,x:a.left,y:a.top},a);!function(t,e){var n,i,a;for(n=0,i=t.length;n<i;++n)(a=t[n]).width=a.horizontal?a.box.fullWidth&&e.availableWidth:e.vBoxMaxWidth,a.height=a.horizontal&&e.hBoxMaxHeight;}(l.concat(u),d),ce(l,h,d),ce(u,h,d)&&ce(l,h,d),function(t){var e=t.maxPadding;function n(n){var i=Math.max(e[n]-t[n],0);return t[n]+=i,i}t.y+=n("top"),t.x+=n("left"),n("right"),n("bottom");}(h),fe(s.leftAndTop,h,d),h.x+=h.w,h.y+=h.h,fe(s.rightAndBottom,h,d),t.chartArea={left:h.left,top:h.top,right:h.left+h.w,bottom:h.top+h.h},H.each(s.chartArea,(function(e){var n=e.box;oe(n,t.chartArea),n.update(h.w,h.h);}));}}},me=(ge=Object.freeze({__proto__:null,default:"@keyframes chartjs-render-animation{from{opacity:.99}to{opacity:1}}.chartjs-render-monitor{animation:chartjs-render-animation 1ms}.chartjs-size-monitor,.chartjs-size-monitor-expand,.chartjs-size-monitor-shrink{position:absolute;direction:ltr;left:0;top:0;right:0;bottom:0;overflow:hidden;pointer-events:none;visibility:hidden;z-index:-1}.chartjs-size-monitor-expand>div{position:absolute;width:1000000px;height:1000000px;left:0;top:0}.chartjs-size-monitor-shrink>div{position:absolute;width:200%;height:200%;left:0;top:0}"}))&&ge.default||ge,ve="$chartjs",be="chartjs-size-monitor",xe="chartjs-render-monitor",ye="chartjs-render-animation",_e=["animationstart","webkitAnimationStart"],ke={touchstart:"mousedown",touchmove:"mousemove",touchend:"mouseup",pointerenter:"mouseenter",pointerdown:"mousedown",pointermove:"mousemove",pointerup:"mouseup",pointerleave:"mouseout",pointerout:"mouseout"};function we(t,e){var n=H.getStyle(t,e),i=n&&n.match(/^(\d+)(\.\d+)?px$/);return i?Number(i[1]):void 0}var Me=!!function(){var t=!1;try{var e=Object.defineProperty({},"passive",{get:function(){t=!0;}});window.addEventListener("e",null,e);}catch(t){}return t}()&&{passive:!0};function Se(t,e,n){t.addEventListener(e,n,Me);}function Ce(t,e,n){t.removeEventListener(e,n,Me);}function Pe(t,e,n,i,a){return {type:t,chart:e,native:a||null,x:void 0!==n?n:null,y:void 0!==i?i:null}}function Ae(t){var e=document.createElement("div");return e.className=t||"",e}function De(t,e,n){var i,a,r,o,s=t[ve]||(t[ve]={}),l=s.resizer=function(t){var e=Ae(be),n=Ae(be+"-expand"),i=Ae(be+"-shrink");n.appendChild(Ae()),i.appendChild(Ae()),e.appendChild(n),e.appendChild(i),e._reset=function(){n.scrollLeft=1e6,n.scrollTop=1e6,i.scrollLeft=1e6,i.scrollTop=1e6;};var a=function(){e._reset(),t();};return Se(n,"scroll",a.bind(n,"expand")),Se(i,"scroll",a.bind(i,"shrink")),e}((i=function(){if(s.resizer){var i=n.options.maintainAspectRatio&&t.parentNode,a=i?i.clientWidth:0;e(Pe("resize",n)),i&&i.clientWidth<a&&n.canvas&&e(Pe("resize",n));}},r=!1,o=[],function(){o=Array.prototype.slice.call(arguments),a=a||this,r||(r=!0,H.requestAnimFrame.call(window,(function(){r=!1,i.apply(a,o);})));}));!function(t,e){var n=t[ve]||(t[ve]={}),i=n.renderProxy=function(t){t.animationName===ye&&e();};H.each(_e,(function(e){Se(t,e,i);})),n.reflow=!!t.offsetParent,t.classList.add(xe);}(t,(function(){if(s.resizer){var e=t.parentNode;e&&e!==l.parentNode&&e.insertBefore(l,e.firstChild),l._reset();}}));}function Te(t){var e=t[ve]||{},n=e.resizer;delete e.resizer,function(t){var e=t[ve]||{},n=e.renderProxy;n&&(H.each(_e,(function(e){Ce(t,e,n);})),delete e.renderProxy),t.classList.remove(xe);}(t),n&&n.parentNode&&n.parentNode.removeChild(n);}var Ie={disableCSSInjection:!1,_enabled:"undefined"!=typeof window&&"undefined"!=typeof document,_ensureLoaded:function(t){if(!this.disableCSSInjection){var e=t.getRootNode?t.getRootNode():document;!function(t,e){var n=t[ve]||(t[ve]={});if(!n.containsStyles){n.containsStyles=!0,e="/* Chart.js */\n"+e;var i=document.createElement("style");i.setAttribute("type","text/css"),i.appendChild(document.createTextNode(e)),t.appendChild(i);}}(e.host?e:document.head,me);}},acquireContext:function(t,e){"string"==typeof t?t=document.getElementById(t):t.length&&(t=t[0]),t&&t.canvas&&(t=t.canvas);var n=t&&t.getContext&&t.getContext("2d");return n&&n.canvas===t?(this._ensureLoaded(t),function(t,e){var n=t.style,i=t.getAttribute("height"),a=t.getAttribute("width");if(t[ve]={initial:{height:i,width:a,style:{display:n.display,height:n.height,width:n.width}}},n.display=n.display||"block",null===a||""===a){var r=we(t,"width");void 0!==r&&(t.width=r);}if(null===i||""===i)if(""===t.style.height)t.height=t.width/(e.options.aspectRatio||2);else {var o=we(t,"height");void 0!==r&&(t.height=o);}}(t,e),n):null},releaseContext:function(t){var e=t.canvas;if(e[ve]){var n=e[ve].initial;["height","width"].forEach((function(t){var i=n[t];H.isNullOrUndef(i)?e.removeAttribute(t):e.setAttribute(t,i);})),H.each(n.style||{},(function(t,n){e.style[n]=t;})),e.width=e.width,delete e[ve];}},addEventListener:function(t,e,n){var i=t.canvas;if("resize"!==e){var a=n[ve]||(n[ve]={});Se(i,e,(a.proxies||(a.proxies={}))[t.id+"_"+e]=function(e){n(function(t,e){var n=ke[t.type]||t.type,i=H.getRelativePosition(t,e);return Pe(n,e,i.x,i.y,t)}(e,t));});}else De(i,n,t);},removeEventListener:function(t,e,n){var i=t.canvas;if("resize"!==e){var a=((n[ve]||{}).proxies||{})[t.id+"_"+e];a&&Ce(i,e,a);}else Te(i);}};H.addEvent=Se,H.removeEvent=Ce;var Fe=Ie._enabled?Ie:{acquireContext:function(t){return t&&t.canvas&&(t=t.canvas),t&&t.getContext("2d")||null}},Oe=H.extend({initialize:function(){},acquireContext:function(){},releaseContext:function(){},addEventListener:function(){},removeEventListener:function(){}},Fe);N._set("global",{plugins:{}});var Le={_plugins:[],_cacheId:0,register:function(t){var e=this._plugins;[].concat(t).forEach((function(t){-1===e.indexOf(t)&&e.push(t);})),this._cacheId++;},unregister:function(t){var e=this._plugins;[].concat(t).forEach((function(t){var n=e.indexOf(t);-1!==n&&e.splice(n,1);})),this._cacheId++;},clear:function(){this._plugins=[],this._cacheId++;},count:function(){return this._plugins.length},getAll:function(){return this._plugins},notify:function(t,e,n){var i,a,r,o,s,l=this.descriptors(t),u=l.length;for(i=0;i<u;++i)if("function"==typeof(s=(r=(a=l[i]).plugin)[e])&&((o=[t].concat(n||[])).push(a.options),!1===s.apply(r,o)))return !1;return !0},descriptors:function(t){var e=t.$plugins||(t.$plugins={});if(e.id===this._cacheId)return e.descriptors;var n=[],i=[],a=t&&t.config||{},r=a.options&&a.options.plugins||{};return this._plugins.concat(a.plugins||[]).forEach((function(t){if(-1===n.indexOf(t)){var e=t.id,a=r[e];!1!==a&&(!0===a&&(a=H.clone(N.global.plugins[e])),n.push(t),i.push({plugin:t,options:a||{}}));}})),e.descriptors=i,e.id=this._cacheId,i},_invalidate:function(t){delete t.$plugins;}},Re={constructors:{},defaults:{},registerScaleType:function(t,e,n){this.constructors[t]=e,this.defaults[t]=H.clone(n);},getScaleConstructor:function(t){return this.constructors.hasOwnProperty(t)?this.constructors[t]:void 0},getScaleDefaults:function(t){return this.defaults.hasOwnProperty(t)?H.merge(Object.create(null),[N.scale,this.defaults[t]]):{}},updateScaleDefaults:function(t,e){this.defaults.hasOwnProperty(t)&&(this.defaults[t]=H.extend(this.defaults[t],e));},addScalesToLayout:function(t){H.each(t.scales,(function(e){e.fullWidth=e.options.fullWidth,e.position=e.options.position,e.weight=e.options.weight,pe.addBox(t,e);}));}},ze=H.valueOrDefault,Ne=H.rtl.getRtlAdapter;N._set("global",{tooltips:{enabled:!0,custom:null,mode:"nearest",position:"average",intersect:!0,backgroundColor:"rgba(0,0,0,0.8)",titleFontStyle:"bold",titleSpacing:2,titleMarginBottom:6,titleFontColor:"#fff",titleAlign:"left",bodySpacing:2,bodyFontColor:"#fff",bodyAlign:"left",footerFontStyle:"bold",footerSpacing:2,footerMarginTop:6,footerFontColor:"#fff",footerAlign:"left",yPadding:6,xPadding:6,caretPadding:2,caretSize:5,cornerRadius:6,multiKeyBackground:"#fff",displayColors:!0,borderColor:"rgba(0,0,0,0)",borderWidth:0,callbacks:{beforeTitle:H.noop,title:function(t,e){var n="",i=e.labels,a=i?i.length:0;if(t.length>0){var r=t[0];r.label?n=r.label:r.xLabel?n=r.xLabel:a>0&&r.index<a&&(n=i[r.index]);}return n},afterTitle:H.noop,beforeBody:H.noop,beforeLabel:H.noop,label:function(t,e){var n=e.datasets[t.datasetIndex].label||"";return n&&(n+=": "),H.isNullOrUndef(t.value)?n+=t.yLabel:n+=t.value,n},labelColor:function(t,e){var n=e.getDatasetMeta(t.datasetIndex).data[t.index]._view;return {borderColor:n.borderColor,backgroundColor:n.backgroundColor}},labelTextColor:function(){return this._options.bodyFontColor},afterLabel:H.noop,afterBody:H.noop,beforeFooter:H.noop,footer:H.noop,afterFooter:H.noop}}});var Be={average:function(t){if(!t.length)return !1;var e,n,i=0,a=0,r=0;for(e=0,n=t.length;e<n;++e){var o=t[e];if(o&&o.hasValue()){var s=o.tooltipPosition();i+=s.x,a+=s.y,++r;}}return {x:i/r,y:a/r}},nearest:function(t,e){var n,i,a,r=e.x,o=e.y,s=Number.POSITIVE_INFINITY;for(n=0,i=t.length;n<i;++n){var l=t[n];if(l&&l.hasValue()){var u=l.getCenterPoint(),d=H.distanceBetweenPoints(e,u);d<s&&(s=d,a=l);}}if(a){var h=a.tooltipPosition();r=h.x,o=h.y;}return {x:r,y:o}}};function Ee(t,e){return e&&(H.isArray(e)?Array.prototype.push.apply(t,e):t.push(e)),t}function We(t){return ("string"==typeof t||t instanceof String)&&t.indexOf("\n")>-1?t.split("\n"):t}function Ve(t){var e=N.global;return {xPadding:t.xPadding,yPadding:t.yPadding,xAlign:t.xAlign,yAlign:t.yAlign,rtl:t.rtl,textDirection:t.textDirection,bodyFontColor:t.bodyFontColor,_bodyFontFamily:ze(t.bodyFontFamily,e.defaultFontFamily),_bodyFontStyle:ze(t.bodyFontStyle,e.defaultFontStyle),_bodyAlign:t.bodyAlign,bodyFontSize:ze(t.bodyFontSize,e.defaultFontSize),bodySpacing:t.bodySpacing,titleFontColor:t.titleFontColor,_titleFontFamily:ze(t.titleFontFamily,e.defaultFontFamily),_titleFontStyle:ze(t.titleFontStyle,e.defaultFontStyle),titleFontSize:ze(t.titleFontSize,e.defaultFontSize),_titleAlign:t.titleAlign,titleSpacing:t.titleSpacing,titleMarginBottom:t.titleMarginBottom,footerFontColor:t.footerFontColor,_footerFontFamily:ze(t.footerFontFamily,e.defaultFontFamily),_footerFontStyle:ze(t.footerFontStyle,e.defaultFontStyle),footerFontSize:ze(t.footerFontSize,e.defaultFontSize),_footerAlign:t.footerAlign,footerSpacing:t.footerSpacing,footerMarginTop:t.footerMarginTop,caretSize:t.caretSize,cornerRadius:t.cornerRadius,backgroundColor:t.backgroundColor,opacity:0,legendColorBackground:t.multiKeyBackground,displayColors:t.displayColors,borderColor:t.borderColor,borderWidth:t.borderWidth}}function He(t,e){return "center"===e?t.x+t.width/2:"right"===e?t.x+t.width-t.xPadding:t.x+t.xPadding}function je(t){return Ee([],We(t))}var qe=K.extend({initialize:function(){this._model=Ve(this._options),this._lastActive=[];},getTitle:function(){var t=this,e=t._options,n=e.callbacks,i=n.beforeTitle.apply(t,arguments),a=n.title.apply(t,arguments),r=n.afterTitle.apply(t,arguments),o=[];return o=Ee(o,We(i)),o=Ee(o,We(a)),o=Ee(o,We(r))},getBeforeBody:function(){return je(this._options.callbacks.beforeBody.apply(this,arguments))},getBody:function(t,e){var n=this,i=n._options.callbacks,a=[];return H.each(t,(function(t){var r={before:[],lines:[],after:[]};Ee(r.before,We(i.beforeLabel.call(n,t,e))),Ee(r.lines,i.label.call(n,t,e)),Ee(r.after,We(i.afterLabel.call(n,t,e))),a.push(r);})),a},getAfterBody:function(){return je(this._options.callbacks.afterBody.apply(this,arguments))},getFooter:function(){var t=this,e=t._options.callbacks,n=e.beforeFooter.apply(t,arguments),i=e.footer.apply(t,arguments),a=e.afterFooter.apply(t,arguments),r=[];return r=Ee(r,We(n)),r=Ee(r,We(i)),r=Ee(r,We(a))},update:function(t){var e,n,i,a,r,o,s,l,u,d,h=this,c=h._options,f=h._model,g=h._model=Ve(c),p=h._active,m=h._data,v={xAlign:f.xAlign,yAlign:f.yAlign},b={x:f.x,y:f.y},x={width:f.width,height:f.height},y={x:f.caretX,y:f.caretY};if(p.length){g.opacity=1;var _=[],k=[];y=Be[c.position].call(h,p,h._eventPosition);var w=[];for(e=0,n=p.length;e<n;++e)w.push((i=p[e],a=void 0,r=void 0,o=void 0,s=void 0,l=void 0,u=void 0,d=void 0,a=i._xScale,r=i._yScale||i._scale,o=i._index,s=i._datasetIndex,l=i._chart.getDatasetMeta(s).controller,u=l._getIndexScale(),d=l._getValueScale(),{xLabel:a?a.getLabelForIndex(o,s):"",yLabel:r?r.getLabelForIndex(o,s):"",label:u?""+u.getLabelForIndex(o,s):"",value:d?""+d.getLabelForIndex(o,s):"",index:o,datasetIndex:s,x:i._model.x,y:i._model.y}));c.filter&&(w=w.filter((function(t){return c.filter(t,m)}))),c.itemSort&&(w=w.sort((function(t,e){return c.itemSort(t,e,m)}))),H.each(w,(function(t){_.push(c.callbacks.labelColor.call(h,t,h._chart)),k.push(c.callbacks.labelTextColor.call(h,t,h._chart));})),g.title=h.getTitle(w,m),g.beforeBody=h.getBeforeBody(w,m),g.body=h.getBody(w,m),g.afterBody=h.getAfterBody(w,m),g.footer=h.getFooter(w,m),g.x=y.x,g.y=y.y,g.caretPadding=c.caretPadding,g.labelColors=_,g.labelTextColors=k,g.dataPoints=w,x=function(t,e){var n=t._chart.ctx,i=2*e.yPadding,a=0,r=e.body,o=r.reduce((function(t,e){return t+e.before.length+e.lines.length+e.after.length}),0);o+=e.beforeBody.length+e.afterBody.length;var s=e.title.length,l=e.footer.length,u=e.titleFontSize,d=e.bodyFontSize,h=e.footerFontSize;i+=s*u,i+=s?(s-1)*e.titleSpacing:0,i+=s?e.titleMarginBottom:0,i+=o*d,i+=o?(o-1)*e.bodySpacing:0,i+=l?e.footerMarginTop:0,i+=l*h,i+=l?(l-1)*e.footerSpacing:0;var c=0,f=function(t){a=Math.max(a,n.measureText(t).width+c);};return n.font=H.fontString(u,e._titleFontStyle,e._titleFontFamily),H.each(e.title,f),n.font=H.fontString(d,e._bodyFontStyle,e._bodyFontFamily),H.each(e.beforeBody.concat(e.afterBody),f),c=e.displayColors?d+2:0,H.each(r,(function(t){H.each(t.before,f),H.each(t.lines,f),H.each(t.after,f);})),c=0,n.font=H.fontString(h,e._footerFontStyle,e._footerFontFamily),H.each(e.footer,f),{width:a+=2*e.xPadding,height:i}}(this,g),b=function(t,e,n,i){var a=t.x,r=t.y,o=t.caretSize,s=t.caretPadding,l=t.cornerRadius,u=n.xAlign,d=n.yAlign,h=o+s,c=l+s;return "right"===u?a-=e.width:"center"===u&&((a-=e.width/2)+e.width>i.width&&(a=i.width-e.width),a<0&&(a=0)),"top"===d?r+=h:r-="bottom"===d?e.height+h:e.height/2,"center"===d?"left"===u?a+=h:"right"===u&&(a-=h):"left"===u?a-=c:"right"===u&&(a+=c),{x:a,y:r}}(g,x,v=function(t,e){var n,i,a,r,o,s=t._model,l=t._chart,u=t._chart.chartArea,d="center",h="center";s.y<e.height?h="top":s.y>l.height-e.height&&(h="bottom");var c=(u.left+u.right)/2,f=(u.top+u.bottom)/2;"center"===h?(n=function(t){return t<=c},i=function(t){return t>c}):(n=function(t){return t<=e.width/2},i=function(t){return t>=l.width-e.width/2}),a=function(t){return t+e.width+s.caretSize+s.caretPadding>l.width},r=function(t){return t-e.width-s.caretSize-s.caretPadding<0},o=function(t){return t<=f?"top":"bottom"},n(s.x)?(d="left",a(s.x)&&(d="center",h=o(s.y))):i(s.x)&&(d="right",r(s.x)&&(d="center",h=o(s.y)));var g=t._options;return {xAlign:g.xAlign?g.xAlign:d,yAlign:g.yAlign?g.yAlign:h}}(this,x),h._chart);}else g.opacity=0;return g.xAlign=v.xAlign,g.yAlign=v.yAlign,g.x=b.x,g.y=b.y,g.width=x.width,g.height=x.height,g.caretX=y.x,g.caretY=y.y,h._model=g,t&&c.custom&&c.custom.call(h,g),h},drawCaret:function(t,e){var n=this._chart.ctx,i=this._view,a=this.getCaretPosition(t,e,i);n.lineTo(a.x1,a.y1),n.lineTo(a.x2,a.y2),n.lineTo(a.x3,a.y3);},getCaretPosition:function(t,e,n){var i,a,r,o,s,l,u=n.caretSize,d=n.cornerRadius,h=n.xAlign,c=n.yAlign,f=t.x,g=t.y,p=e.width,m=e.height;if("center"===c)s=g+m/2,"left"===h?(a=(i=f)-u,r=i,o=s+u,l=s-u):(a=(i=f+p)+u,r=i,o=s-u,l=s+u);else if("left"===h?(i=(a=f+d+u)-u,r=a+u):"right"===h?(i=(a=f+p-d-u)-u,r=a+u):(i=(a=n.caretX)-u,r=a+u),"top"===c)s=(o=g)-u,l=o;else {s=(o=g+m)+u,l=o;var v=r;r=i,i=v;}return {x1:i,x2:a,x3:r,y1:o,y2:s,y3:l}},drawTitle:function(t,e,n){var i,a,r,o=e.title,s=o.length;if(s){var l=Ne(e.rtl,e.x,e.width);for(t.x=He(e,e._titleAlign),n.textAlign=l.textAlign(e._titleAlign),n.textBaseline="middle",i=e.titleFontSize,a=e.titleSpacing,n.fillStyle=e.titleFontColor,n.font=H.fontString(i,e._titleFontStyle,e._titleFontFamily),r=0;r<s;++r)n.fillText(o[r],l.x(t.x),t.y+i/2),t.y+=i+a,r+1===s&&(t.y+=e.titleMarginBottom-a);}},drawBody:function(t,e,n){var i,a,r,o,s,l,u,d,h=e.bodyFontSize,c=e.bodySpacing,f=e._bodyAlign,g=e.body,p=e.displayColors,m=0,v=p?He(e,"left"):0,b=Ne(e.rtl,e.x,e.width),x=function(e){n.fillText(e,b.x(t.x+m),t.y+h/2),t.y+=h+c;},y=b.textAlign(f);for(n.textAlign=f,n.textBaseline="middle",n.font=H.fontString(h,e._bodyFontStyle,e._bodyFontFamily),t.x=He(e,y),n.fillStyle=e.bodyFontColor,H.each(e.beforeBody,x),m=p&&"right"!==y?"center"===f?h/2+1:h+2:0,s=0,u=g.length;s<u;++s){for(i=g[s],a=e.labelTextColors[s],r=e.labelColors[s],n.fillStyle=a,H.each(i.before,x),l=0,d=(o=i.lines).length;l<d;++l){if(p){var _=b.x(v);n.fillStyle=e.legendColorBackground,n.fillRect(b.leftForLtr(_,h),t.y,h,h),n.lineWidth=1,n.strokeStyle=r.borderColor,n.strokeRect(b.leftForLtr(_,h),t.y,h,h),n.fillStyle=r.backgroundColor,n.fillRect(b.leftForLtr(b.xPlus(_,1),h-2),t.y+1,h-2,h-2),n.fillStyle=a;}x(o[l]);}H.each(i.after,x);}m=0,H.each(e.afterBody,x),t.y-=c;},drawFooter:function(t,e,n){var i,a,r=e.footer,o=r.length;if(o){var s=Ne(e.rtl,e.x,e.width);for(t.x=He(e,e._footerAlign),t.y+=e.footerMarginTop,n.textAlign=s.textAlign(e._footerAlign),n.textBaseline="middle",i=e.footerFontSize,n.fillStyle=e.footerFontColor,n.font=H.fontString(i,e._footerFontStyle,e._footerFontFamily),a=0;a<o;++a)n.fillText(r[a],s.x(t.x),t.y+i/2),t.y+=i+e.footerSpacing;}},drawBackground:function(t,e,n,i){n.fillStyle=e.backgroundColor,n.strokeStyle=e.borderColor,n.lineWidth=e.borderWidth;var a=e.xAlign,r=e.yAlign,o=t.x,s=t.y,l=i.width,u=i.height,d=e.cornerRadius;n.beginPath(),n.moveTo(o+d,s),"top"===r&&this.drawCaret(t,i),n.lineTo(o+l-d,s),n.quadraticCurveTo(o+l,s,o+l,s+d),"center"===r&&"right"===a&&this.drawCaret(t,i),n.lineTo(o+l,s+u-d),n.quadraticCurveTo(o+l,s+u,o+l-d,s+u),"bottom"===r&&this.drawCaret(t,i),n.lineTo(o+d,s+u),n.quadraticCurveTo(o,s+u,o,s+u-d),"center"===r&&"left"===a&&this.drawCaret(t,i),n.lineTo(o,s+d),n.quadraticCurveTo(o,s,o+d,s),n.closePath(),n.fill(),e.borderWidth>0&&n.stroke();},draw:function(){var t=this._chart.ctx,e=this._view;if(0!==e.opacity){var n={width:e.width,height:e.height},i={x:e.x,y:e.y},a=Math.abs(e.opacity<.001)?0:e.opacity,r=e.title.length||e.beforeBody.length||e.body.length||e.afterBody.length||e.footer.length;this._options.enabled&&r&&(t.save(),t.globalAlpha=a,this.drawBackground(i,e,t,n),i.y+=e.yPadding,H.rtl.overrideTextDirection(t,e.textDirection),this.drawTitle(i,e,t),this.drawBody(i,e,t),this.drawFooter(i,e,t),H.rtl.restoreTextDirection(t,e.textDirection),t.restore());}},handleEvent:function(t){var e,n=this,i=n._options;return n._lastActive=n._lastActive||[],"mouseout"===t.type?n._active=[]:(n._active=n._chart.getElementsAtEventForMode(t,i.mode,i),i.reverse&&n._active.reverse()),(e=!H.arrayEquals(n._active,n._lastActive))&&(n._lastActive=n._active,(i.enabled||i.custom)&&(n._eventPosition={x:t.x,y:t.y},n.update(!0),n.pivot())),e}}),Ue=Be,Ye=qe;Ye.positioners=Ue;var Ge=H.valueOrDefault;function Xe(){return H.merge(Object.create(null),[].slice.call(arguments),{merger:function(t,e,n,i){if("xAxes"===t||"yAxes"===t){var a,r,o,s=n[t].length;for(e[t]||(e[t]=[]),a=0;a<s;++a)o=n[t][a],r=Ge(o.type,"xAxes"===t?"category":"linear"),a>=e[t].length&&e[t].push({}),!e[t][a].type||o.type&&o.type!==e[t][a].type?H.merge(e[t][a],[Re.getScaleDefaults(r),o]):H.merge(e[t][a],o);}else H._merger(t,e,n,i);}})}function Ke(){return H.merge(Object.create(null),[].slice.call(arguments),{merger:function(t,e,n,i){var a=e[t]||Object.create(null),r=n[t];"scales"===t?e[t]=Xe(a,r):"scale"===t?e[t]=H.merge(a,[Re.getScaleDefaults(r.type),r]):H._merger(t,e,n,i);}})}function Ze(t){var e=t.options;H.each(t.scales,(function(e){pe.removeBox(t,e);})),e=Ke(N.global,N[t.config.type],e),t.options=t.config.options=e,t.ensureScalesHaveIDs(),t.buildOrUpdateScales(),t.tooltip._options=e.tooltips,t.tooltip.initialize();}function $e(t,e,n){var i,a=function(t){return t.id===i};do{i=e+n++;}while(H.findIndex(t,a)>=0);return i}function Je(t){return "top"===t||"bottom"===t}function Qe(t,e){return function(n,i){return n[t]===i[t]?n[e]-i[e]:n[t]-i[t]}}N._set("global",{elements:{},events:["mousemove","mouseout","click","touchstart","touchmove"],hover:{onHover:null,mode:"nearest",intersect:!0,animationDuration:400},onClick:null,maintainAspectRatio:!0,responsive:!0,responsiveAnimationDuration:0});var tn=function(t,e){return this.construct(t,e),this};H.extend(tn.prototype,{construct:function(t,e){var n=this;e=function(t){var e=(t=t||Object.create(null)).data=t.data||{};return e.datasets=e.datasets||[],e.labels=e.labels||[],t.options=Ke(N.global,N[t.type],t.options||{}),t}(e);var i=Oe.acquireContext(t,e),a=i&&i.canvas,r=a&&a.height,o=a&&a.width;n.id=H.uid(),n.ctx=i,n.canvas=a,n.config=e,n.width=o,n.height=r,n.aspectRatio=r?o/r:null,n.options=e.options,n._bufferedRender=!1,n._layers=[],n.chart=n,n.controller=n,tn.instances[n.id]=n,Object.defineProperty(n,"data",{get:function(){return n.config.data},set:function(t){n.config.data=t;}}),i&&a?(n.initialize(),n.update()):console.error("Failed to create chart: can't acquire context from the given item");},initialize:function(){var t=this;return Le.notify(t,"beforeInit"),H.retinaScale(t,t.options.devicePixelRatio),t.bindEvents(),t.options.responsive&&t.resize(!0),t.initToolTip(),Le.notify(t,"afterInit"),t},clear:function(){return H.canvas.clear(this),this},stop:function(){return J.cancelAnimation(this),this},resize:function(t){var e=this,n=e.options,i=e.canvas,a=n.maintainAspectRatio&&e.aspectRatio||null,r=Math.max(0,Math.floor(H.getMaximumWidth(i))),o=Math.max(0,Math.floor(a?r/a:H.getMaximumHeight(i)));if((e.width!==r||e.height!==o)&&(i.width=e.width=r,i.height=e.height=o,i.style.width=r+"px",i.style.height=o+"px",H.retinaScale(e,n.devicePixelRatio),!t)){var s={width:r,height:o};Le.notify(e,"resize",[s]),n.onResize&&n.onResize(e,s),e.stop(),e.update({duration:n.responsiveAnimationDuration});}},ensureScalesHaveIDs:function(){var t=this.options,e=t.scales||{},n=t.scale;H.each(e.xAxes,(function(t,n){t.id||(t.id=$e(e.xAxes,"x-axis-",n));})),H.each(e.yAxes,(function(t,n){t.id||(t.id=$e(e.yAxes,"y-axis-",n));})),n&&(n.id=n.id||"scale");},buildOrUpdateScales:function(){var t=this,e=t.options,n=t.scales||{},i=[],a=Object.keys(n).reduce((function(t,e){return t[e]=!1,t}),{});e.scales&&(i=i.concat((e.scales.xAxes||[]).map((function(t){return {options:t,dtype:"category",dposition:"bottom"}})),(e.scales.yAxes||[]).map((function(t){return {options:t,dtype:"linear",dposition:"left"}})))),e.scale&&i.push({options:e.scale,dtype:"radialLinear",isDefault:!0,dposition:"chartArea"}),H.each(i,(function(e){var i=e.options,r=i.id,o=Ge(i.type,e.dtype);Je(i.position)!==Je(e.dposition)&&(i.position=e.dposition),a[r]=!0;var s=null;if(r in n&&n[r].type===o)(s=n[r]).options=i,s.ctx=t.ctx,s.chart=t;else {var l=Re.getScaleConstructor(o);if(!l)return;s=new l({id:r,type:o,options:i,ctx:t.ctx,chart:t}),n[s.id]=s;}s.mergeTicksOptions(),e.isDefault&&(t.scale=s);})),H.each(a,(function(t,e){t||delete n[e];})),t.scales=n,Re.addScalesToLayout(this);},buildOrUpdateControllers:function(){var t,e,n=this,i=[],a=n.data.datasets;for(t=0,e=a.length;t<e;t++){var r=a[t],o=n.getDatasetMeta(t),s=r.type||n.config.type;if(o.type&&o.type!==s&&(n.destroyDatasetMeta(t),o=n.getDatasetMeta(t)),o.type=s,o.order=r.order||0,o.index=t,o.controller)o.controller.updateIndex(t),o.controller.linkScales();else {var l=Jt[o.type];if(void 0===l)throw new Error('"'+o.type+'" is not a chart type.');o.controller=new l(n,t),i.push(o.controller);}}return i},resetElements:function(){var t=this;H.each(t.data.datasets,(function(e,n){t.getDatasetMeta(n).controller.reset();}),t);},reset:function(){this.resetElements(),this.tooltip.initialize();},update:function(t){var e,n,i=this;if(t&&"object"==typeof t||(t={duration:t,lazy:arguments[1]}),Ze(i),Le._invalidate(i),!1!==Le.notify(i,"beforeUpdate")){i.tooltip._data=i.data;var a=i.buildOrUpdateControllers();for(e=0,n=i.data.datasets.length;e<n;e++)i.getDatasetMeta(e).controller.buildOrUpdateElements();i.updateLayout(),i.options.animation&&i.options.animation.duration&&H.each(a,(function(t){t.reset();})),i.updateDatasets(),i.tooltip.initialize(),i.lastActive=[],Le.notify(i,"afterUpdate"),i._layers.sort(Qe("z","_idx")),i._bufferedRender?i._bufferedRequest={duration:t.duration,easing:t.easing,lazy:t.lazy}:i.render(t);}},updateLayout:function(){var t=this;!1!==Le.notify(t,"beforeLayout")&&(pe.update(this,this.width,this.height),t._layers=[],H.each(t.boxes,(function(e){e._configure&&e._configure(),t._layers.push.apply(t._layers,e._layers());}),t),t._layers.forEach((function(t,e){t._idx=e;})),Le.notify(t,"afterScaleUpdate"),Le.notify(t,"afterLayout"));},updateDatasets:function(){if(!1!==Le.notify(this,"beforeDatasetsUpdate")){for(var t=0,e=this.data.datasets.length;t<e;++t)this.updateDataset(t);Le.notify(this,"afterDatasetsUpdate");}},updateDataset:function(t){var e=this.getDatasetMeta(t),n={meta:e,index:t};!1!==Le.notify(this,"beforeDatasetUpdate",[n])&&(e.controller._update(),Le.notify(this,"afterDatasetUpdate",[n]));},render:function(t){var e=this;t&&"object"==typeof t||(t={duration:t,lazy:arguments[1]});var n=e.options.animation,i=Ge(t.duration,n&&n.duration),a=t.lazy;if(!1!==Le.notify(e,"beforeRender")){var r=function(t){Le.notify(e,"afterRender"),H.callback(n&&n.onComplete,[t],e);};if(n&&i){var o=new $({numSteps:i/16.66,easing:t.easing||n.easing,render:function(t,e){var n=H.easing.effects[e.easing],i=e.currentStep,a=i/e.numSteps;t.draw(n(a),a,i);},onAnimationProgress:n.onProgress,onAnimationComplete:r});J.addAnimation(e,o,i,a);}else e.draw(),r(new $({numSteps:0,chart:e}));return e}},draw:function(t){var e,n,i=this;if(i.clear(),H.isNullOrUndef(t)&&(t=1),i.transition(t),!(i.width<=0||i.height<=0)&&!1!==Le.notify(i,"beforeDraw",[t])){for(n=i._layers,e=0;e<n.length&&n[e].z<=0;++e)n[e].draw(i.chartArea);for(i.drawDatasets(t);e<n.length;++e)n[e].draw(i.chartArea);i._drawTooltip(t),Le.notify(i,"afterDraw",[t]);}},transition:function(t){for(var e=0,n=(this.data.datasets||[]).length;e<n;++e)this.isDatasetVisible(e)&&this.getDatasetMeta(e).controller.transition(t);this.tooltip.transition(t);},_getSortedDatasetMetas:function(t){var e,n,i=[];for(e=0,n=(this.data.datasets||[]).length;e<n;++e)t&&!this.isDatasetVisible(e)||i.push(this.getDatasetMeta(e));return i.sort(Qe("order","index")),i},_getSortedVisibleDatasetMetas:function(){return this._getSortedDatasetMetas(!0)},drawDatasets:function(t){var e,n;if(!1!==Le.notify(this,"beforeDatasetsDraw",[t])){for(n=(e=this._getSortedVisibleDatasetMetas()).length-1;n>=0;--n)this.drawDataset(e[n],t);Le.notify(this,"afterDatasetsDraw",[t]);}},drawDataset:function(t,e){var n={meta:t,index:t.index,easingValue:e};!1!==Le.notify(this,"beforeDatasetDraw",[n])&&(t.controller.draw(e),Le.notify(this,"afterDatasetDraw",[n]));},_drawTooltip:function(t){var e=this.tooltip,n={tooltip:e,easingValue:t};!1!==Le.notify(this,"beforeTooltipDraw",[n])&&(e.draw(),Le.notify(this,"afterTooltipDraw",[n]));},getElementAtEvent:function(t){return re.modes.single(this,t)},getElementsAtEvent:function(t){return re.modes.label(this,t,{intersect:!0})},getElementsAtXAxis:function(t){return re.modes["x-axis"](this,t,{intersect:!0})},getElementsAtEventForMode:function(t,e,n){var i=re.modes[e];return "function"==typeof i?i(this,t,n):[]},getDatasetAtEvent:function(t){return re.modes.dataset(this,t,{intersect:!0})},getDatasetMeta:function(t){var e=this.data.datasets[t];e._meta||(e._meta={});var n=e._meta[this.id];return n||(n=e._meta[this.id]={type:null,data:[],dataset:null,controller:null,hidden:null,xAxisID:null,yAxisID:null,order:e.order||0,index:t}),n},getVisibleDatasetCount:function(){for(var t=0,e=0,n=this.data.datasets.length;e<n;++e)this.isDatasetVisible(e)&&t++;return t},isDatasetVisible:function(t){var e=this.getDatasetMeta(t);return "boolean"==typeof e.hidden?!e.hidden:!this.data.datasets[t].hidden},generateLegend:function(){return this.options.legendCallback(this)},destroyDatasetMeta:function(t){var e=this.id,n=this.data.datasets[t],i=n._meta&&n._meta[e];i&&(i.controller.destroy(),delete n._meta[e]);},destroy:function(){var t,e,n=this,i=n.canvas;for(n.stop(),t=0,e=n.data.datasets.length;t<e;++t)n.destroyDatasetMeta(t);i&&(n.unbindEvents(),H.canvas.clear(n),Oe.releaseContext(n.ctx),n.canvas=null,n.ctx=null),Le.notify(n,"destroy"),delete tn.instances[n.id];},toBase64Image:function(){return this.canvas.toDataURL.apply(this.canvas,arguments)},initToolTip:function(){var t=this;t.tooltip=new Ye({_chart:t,_chartInstance:t,_data:t.data,_options:t.options.tooltips},t);},bindEvents:function(){var t=this,e=t._listeners={},n=function(){t.eventHandler.apply(t,arguments);};H.each(t.options.events,(function(i){Oe.addEventListener(t,i,n),e[i]=n;})),t.options.responsive&&(n=function(){t.resize();},Oe.addEventListener(t,"resize",n),e.resize=n);},unbindEvents:function(){var t=this,e=t._listeners;e&&(delete t._listeners,H.each(e,(function(e,n){Oe.removeEventListener(t,n,e);})));},updateHoverStyle:function(t,e,n){var i,a,r,o=n?"set":"remove";for(a=0,r=t.length;a<r;++a)(i=t[a])&&this.getDatasetMeta(i._datasetIndex).controller[o+"HoverStyle"](i);"dataset"===e&&this.getDatasetMeta(t[0]._datasetIndex).controller["_"+o+"DatasetHoverStyle"]();},eventHandler:function(t){var e=this,n=e.tooltip;if(!1!==Le.notify(e,"beforeEvent",[t])){e._bufferedRender=!0,e._bufferedRequest=null;var i=e.handleEvent(t);n&&(i=n._start?n.handleEvent(t):i|n.handleEvent(t)),Le.notify(e,"afterEvent",[t]);var a=e._bufferedRequest;return a?e.render(a):i&&!e.animating&&(e.stop(),e.render({duration:e.options.hover.animationDuration,lazy:!0})),e._bufferedRender=!1,e._bufferedRequest=null,e}},handleEvent:function(t){var e,n=this,i=n.options||{},a=i.hover;return n.lastActive=n.lastActive||[],"mouseout"===t.type?n.active=[]:n.active=n.getElementsAtEventForMode(t,a.mode,a),H.callback(i.onHover||i.hover.onHover,[t.native,n.active],n),"mouseup"!==t.type&&"click"!==t.type||i.onClick&&i.onClick.call(n,t.native,n.active),n.lastActive.length&&n.updateHoverStyle(n.lastActive,a.mode,!1),n.active.length&&a.mode&&n.updateHoverStyle(n.active,a.mode,!0),e=!H.arrayEquals(n.active,n.lastActive),n.lastActive=n.active,e}}),tn.instances={};var en=tn;tn.Controller=tn,tn.types={},H.configMerge=Ke,H.scaleMerge=Xe;function nn(){throw new Error("This method is not implemented: either no adapter can be found or an incomplete integration was provided.")}function an(t){this.options=t||{};}H.extend(an.prototype,{formats:nn,parse:nn,format:nn,add:nn,diff:nn,startOf:nn,endOf:nn,_create:function(t){return t}}),an.override=function(t){H.extend(an.prototype,t);};var rn={_date:an},on={formatters:{values:function(t){return H.isArray(t)?t:""+t},linear:function(t,e,n){var i=n.length>3?n[2]-n[1]:n[1]-n[0];Math.abs(i)>1&&t!==Math.floor(t)&&(i=t-Math.floor(t));var a=H.log10(Math.abs(i)),r="";if(0!==t)if(Math.max(Math.abs(n[0]),Math.abs(n[n.length-1]))<1e-4){var o=H.log10(Math.abs(t)),s=Math.floor(o)-Math.floor(a);s=Math.max(Math.min(s,20),0),r=t.toExponential(s);}else {var l=-1*Math.floor(a);l=Math.max(Math.min(l,20),0),r=t.toFixed(l);}else r="0";return r},logarithmic:function(t,e,n){var i=t/Math.pow(10,Math.floor(H.log10(t)));return 0===t?"0":1===i||2===i||5===i||0===e||e===n.length-1?t.toExponential():""}}},sn=H.isArray,ln=H.isNullOrUndef,un=H.valueOrDefault,dn=H.valueAtIndexOrDefault;function hn(t,e,n){var i,a=t.getTicks().length,r=Math.min(e,a-1),o=t.getPixelForTick(r),s=t._startPixel,l=t._endPixel;if(!(n&&(i=1===a?Math.max(o-s,l-o):0===e?(t.getPixelForTick(1)-o)/2:(o-t.getPixelForTick(r-1))/2,(o+=r<e?i:-i)<s-1e-6||o>l+1e-6)))return o}function cn(t,e,n,i){var a,r,o,s,l,u,d,h,c,f,g,p,m,v=n.length,b=[],x=[],y=[],_=0,k=0;for(a=0;a<v;++a){if(s=n[a].label,l=n[a].major?e.major:e.minor,t.font=u=l.string,d=i[u]=i[u]||{data:{},gc:[]},h=l.lineHeight,c=f=0,ln(s)||sn(s)){if(sn(s))for(r=0,o=s.length;r<o;++r)g=s[r],ln(g)||sn(g)||(c=H.measureText(t,d.data,d.gc,c,g),f+=h);}else c=H.measureText(t,d.data,d.gc,c,s),f=h;b.push(c),x.push(f),y.push(h/2),_=Math.max(c,_),k=Math.max(f,k);}function w(t){return {width:b[t]||0,height:x[t]||0,offset:y[t]||0}}return function(t,e){H.each(t,(function(t){var n,i=t.gc,a=i.length/2;if(a>e){for(n=0;n<a;++n)delete t.data[i[n]];i.splice(0,a);}}));}(i,v),p=b.indexOf(_),m=x.indexOf(k),{first:w(0),last:w(v-1),widest:w(p),highest:w(m)}}function fn(t){return t.drawTicks?t.tickMarkLength:0}function gn(t){var e,n;return t.display?(e=H.options._parseFont(t),n=H.options.toPadding(t.padding),e.lineHeight+n.height):0}function pn(t,e){return H.extend(H.options._parseFont({fontFamily:un(e.fontFamily,t.fontFamily),fontSize:un(e.fontSize,t.fontSize),fontStyle:un(e.fontStyle,t.fontStyle),lineHeight:un(e.lineHeight,t.lineHeight)}),{color:H.options.resolve([e.fontColor,t.fontColor,N.global.defaultFontColor])})}function mn(t){var e=pn(t,t.minor);return {minor:e,major:t.major.enabled?pn(t,t.major):e}}function vn(t){var e,n,i,a=[];for(n=0,i=t.length;n<i;++n)void 0!==(e=t[n])._index&&a.push(e);return a}function bn(t,e,n,i){var a,r,o,s,l=un(n,0),u=Math.min(un(i,t.length),t.length),d=0;for(e=Math.ceil(e),i&&(e=(a=i-n)/Math.floor(a/e)),s=l;s<0;)d++,s=Math.round(l+d*e);for(r=Math.max(l,0);r<u;r++)o=t[r],r===s?(o._index=r,d++,s=Math.round(l+d*e)):delete o.label;}N._set("scale",{display:!0,position:"left",offset:!1,gridLines:{display:!0,color:"rgba(0,0,0,0.1)",lineWidth:1,drawBorder:!0,drawOnChartArea:!0,drawTicks:!0,tickMarkLength:10,zeroLineWidth:1,zeroLineColor:"rgba(0,0,0,0.25)",zeroLineBorderDash:[],zeroLineBorderDashOffset:0,offsetGridLines:!1,borderDash:[],borderDashOffset:0},scaleLabel:{display:!1,labelString:"",padding:{top:4,bottom:4}},ticks:{beginAtZero:!1,minRotation:0,maxRotation:50,mirror:!1,padding:0,reverse:!1,display:!0,autoSkip:!0,autoSkipPadding:0,labelOffset:0,callback:on.formatters.values,minor:{},major:{}}});var xn=K.extend({zeroLineIndex:0,getPadding:function(){return {left:this.paddingLeft||0,top:this.paddingTop||0,right:this.paddingRight||0,bottom:this.paddingBottom||0}},getTicks:function(){return this._ticks},_getLabels:function(){var t=this.chart.data;return this.options.labels||(this.isHorizontal()?t.xLabels:t.yLabels)||t.labels||[]},mergeTicksOptions:function(){},beforeUpdate:function(){H.callback(this.options.beforeUpdate,[this]);},update:function(t,e,n){var i,a,r,o,s,l=this,u=l.options.ticks,d=u.sampleSize;if(l.beforeUpdate(),l.maxWidth=t,l.maxHeight=e,l.margins=H.extend({left:0,right:0,top:0,bottom:0},n),l._ticks=null,l.ticks=null,l._labelSizes=null,l._maxLabelLines=0,l.longestLabelWidth=0,l.longestTextCache=l.longestTextCache||{},l._gridLineItems=null,l._labelItems=null,l.beforeSetDimensions(),l.setDimensions(),l.afterSetDimensions(),l.beforeDataLimits(),l.determineDataLimits(),l.afterDataLimits(),l.beforeBuildTicks(),o=l.buildTicks()||[],(!(o=l.afterBuildTicks(o)||o)||!o.length)&&l.ticks)for(o=[],i=0,a=l.ticks.length;i<a;++i)o.push({value:l.ticks[i],major:!1});return l._ticks=o,s=d<o.length,r=l._convertTicksToLabels(s?function(t,e){for(var n=[],i=t.length/e,a=0,r=t.length;a<r;a+=i)n.push(t[Math.floor(a)]);return n}(o,d):o),l._configure(),l.beforeCalculateTickRotation(),l.calculateTickRotation(),l.afterCalculateTickRotation(),l.beforeFit(),l.fit(),l.afterFit(),l._ticksToDraw=u.display&&(u.autoSkip||"auto"===u.source)?l._autoSkip(o):o,s&&(r=l._convertTicksToLabels(l._ticksToDraw)),l.ticks=r,l.afterUpdate(),l.minSize},_configure:function(){var t,e,n=this,i=n.options.ticks.reverse;n.isHorizontal()?(t=n.left,e=n.right):(t=n.top,e=n.bottom,i=!i),n._startPixel=t,n._endPixel=e,n._reversePixels=i,n._length=e-t;},afterUpdate:function(){H.callback(this.options.afterUpdate,[this]);},beforeSetDimensions:function(){H.callback(this.options.beforeSetDimensions,[this]);},setDimensions:function(){var t=this;t.isHorizontal()?(t.width=t.maxWidth,t.left=0,t.right=t.width):(t.height=t.maxHeight,t.top=0,t.bottom=t.height),t.paddingLeft=0,t.paddingTop=0,t.paddingRight=0,t.paddingBottom=0;},afterSetDimensions:function(){H.callback(this.options.afterSetDimensions,[this]);},beforeDataLimits:function(){H.callback(this.options.beforeDataLimits,[this]);},determineDataLimits:H.noop,afterDataLimits:function(){H.callback(this.options.afterDataLimits,[this]);},beforeBuildTicks:function(){H.callback(this.options.beforeBuildTicks,[this]);},buildTicks:H.noop,afterBuildTicks:function(t){var e=this;return sn(t)&&t.length?H.callback(e.options.afterBuildTicks,[e,t]):(e.ticks=H.callback(e.options.afterBuildTicks,[e,e.ticks])||e.ticks,t)},beforeTickToLabelConversion:function(){H.callback(this.options.beforeTickToLabelConversion,[this]);},convertTicksToLabels:function(){var t=this.options.ticks;this.ticks=this.ticks.map(t.userCallback||t.callback,this);},afterTickToLabelConversion:function(){H.callback(this.options.afterTickToLabelConversion,[this]);},beforeCalculateTickRotation:function(){H.callback(this.options.beforeCalculateTickRotation,[this]);},calculateTickRotation:function(){var t,e,n,i,a,r,o,s=this,l=s.options,u=l.ticks,d=s.getTicks().length,h=u.minRotation||0,c=u.maxRotation,f=h;!s._isVisible()||!u.display||h>=c||d<=1||!s.isHorizontal()?s.labelRotation=h:(e=(t=s._getLabelSizes()).widest.width,n=t.highest.height-t.highest.offset,i=Math.min(s.maxWidth,s.chart.width-e),e+6>(a=l.offset?s.maxWidth/d:i/(d-1))&&(a=i/(d-(l.offset?.5:1)),r=s.maxHeight-fn(l.gridLines)-u.padding-gn(l.scaleLabel),o=Math.sqrt(e*e+n*n),f=H.toDegrees(Math.min(Math.asin(Math.min((t.highest.height+6)/a,1)),Math.asin(Math.min(r/o,1))-Math.asin(n/o))),f=Math.max(h,Math.min(c,f))),s.labelRotation=f);},afterCalculateTickRotation:function(){H.callback(this.options.afterCalculateTickRotation,[this]);},beforeFit:function(){H.callback(this.options.beforeFit,[this]);},fit:function(){var t=this,e=t.minSize={width:0,height:0},n=t.chart,i=t.options,a=i.ticks,r=i.scaleLabel,o=i.gridLines,s=t._isVisible(),l="bottom"===i.position,u=t.isHorizontal();if(u?e.width=t.maxWidth:s&&(e.width=fn(o)+gn(r)),u?s&&(e.height=fn(o)+gn(r)):e.height=t.maxHeight,a.display&&s){var d=mn(a),h=t._getLabelSizes(),c=h.first,f=h.last,g=h.widest,p=h.highest,m=.4*d.minor.lineHeight,v=a.padding;if(u){var b=0!==t.labelRotation,x=H.toRadians(t.labelRotation),y=Math.cos(x),_=Math.sin(x),k=_*g.width+y*(p.height-(b?p.offset:0))+(b?0:m);e.height=Math.min(t.maxHeight,e.height+k+v);var w,M,S=t.getPixelForTick(0)-t.left,C=t.right-t.getPixelForTick(t.getTicks().length-1);b?(w=l?y*c.width+_*c.offset:_*(c.height-c.offset),M=l?_*(f.height-f.offset):y*f.width+_*f.offset):(w=c.width/2,M=f.width/2),t.paddingLeft=Math.max((w-S)*t.width/(t.width-S),0)+3,t.paddingRight=Math.max((M-C)*t.width/(t.width-C),0)+3;}else {var P=a.mirror?0:g.width+v+m;e.width=Math.min(t.maxWidth,e.width+P),t.paddingTop=c.height/2,t.paddingBottom=f.height/2;}}t.handleMargins(),u?(t.width=t._length=n.width-t.margins.left-t.margins.right,t.height=e.height):(t.width=e.width,t.height=t._length=n.height-t.margins.top-t.margins.bottom);},handleMargins:function(){var t=this;t.margins&&(t.margins.left=Math.max(t.paddingLeft,t.margins.left),t.margins.top=Math.max(t.paddingTop,t.margins.top),t.margins.right=Math.max(t.paddingRight,t.margins.right),t.margins.bottom=Math.max(t.paddingBottom,t.margins.bottom));},afterFit:function(){H.callback(this.options.afterFit,[this]);},isHorizontal:function(){var t=this.options.position;return "top"===t||"bottom"===t},isFullWidth:function(){return this.options.fullWidth},getRightValue:function(t){if(ln(t))return NaN;if(("number"==typeof t||t instanceof Number)&&!isFinite(t))return NaN;if(t)if(this.isHorizontal()){if(void 0!==t.x)return this.getRightValue(t.x)}else if(void 0!==t.y)return this.getRightValue(t.y);return t},_convertTicksToLabels:function(t){var e,n,i,a=this;for(a.ticks=t.map((function(t){return t.value})),a.beforeTickToLabelConversion(),e=a.convertTicksToLabels(t)||a.ticks,a.afterTickToLabelConversion(),n=0,i=t.length;n<i;++n)t[n].label=e[n];return e},_getLabelSizes:function(){var t=this,e=t._labelSizes;return e||(t._labelSizes=e=cn(t.ctx,mn(t.options.ticks),t.getTicks(),t.longestTextCache),t.longestLabelWidth=e.widest.width),e},_parseValue:function(t){var e,n,i,a;return sn(t)?(e=+this.getRightValue(t[0]),n=+this.getRightValue(t[1]),i=Math.min(e,n),a=Math.max(e,n)):(e=void 0,n=t=+this.getRightValue(t),i=t,a=t),{min:i,max:a,start:e,end:n}},_getScaleLabel:function(t){var e=this._parseValue(t);return void 0!==e.start?"["+e.start+", "+e.end+"]":+this.getRightValue(t)},getLabelForIndex:H.noop,getPixelForValue:H.noop,getValueForPixel:H.noop,getPixelForTick:function(t){var e=this.options.offset,n=this._ticks.length,i=1/Math.max(n-(e?0:1),1);return t<0||t>n-1?null:this.getPixelForDecimal(t*i+(e?i/2:0))},getPixelForDecimal:function(t){return this._reversePixels&&(t=1-t),this._startPixel+t*this._length},getDecimalForPixel:function(t){var e=(t-this._startPixel)/this._length;return this._reversePixels?1-e:e},getBasePixel:function(){return this.getPixelForValue(this.getBaseValue())},getBaseValue:function(){var t=this.min,e=this.max;return this.beginAtZero?0:t<0&&e<0?e:t>0&&e>0?t:0},_autoSkip:function(t){var e,n,i,a,r=this.options.ticks,o=this._length,s=r.maxTicksLimit||o/this._tickSize()+1,l=r.major.enabled?function(t){var e,n,i=[];for(e=0,n=t.length;e<n;e++)t[e].major&&i.push(e);return i}(t):[],u=l.length,d=l[0],h=l[u-1];if(u>s)return function(t,e,n){var i,a,r=0,o=e[0];for(n=Math.ceil(n),i=0;i<t.length;i++)a=t[i],i===o?(a._index=i,o=e[++r*n]):delete a.label;}(t,l,u/s),vn(t);if(i=function(t,e,n,i){var a,r,o,s,l=function(t){var e,n,i=t.length;if(i<2)return !1;for(n=t[0],e=1;e<i;++e)if(t[e]-t[e-1]!==n)return !1;return n}(t),u=(e.length-1)/i;if(!l)return Math.max(u,1);for(o=0,s=(a=H.math._factorize(l)).length-1;o<s;o++)if((r=a[o])>u)return r;return Math.max(u,1)}(l,t,0,s),u>0){for(e=0,n=u-1;e<n;e++)bn(t,i,l[e],l[e+1]);return a=u>1?(h-d)/(u-1):null,bn(t,i,H.isNullOrUndef(a)?0:d-a,d),bn(t,i,h,H.isNullOrUndef(a)?t.length:h+a),vn(t)}return bn(t,i),vn(t)},_tickSize:function(){var t=this.options.ticks,e=H.toRadians(this.labelRotation),n=Math.abs(Math.cos(e)),i=Math.abs(Math.sin(e)),a=this._getLabelSizes(),r=t.autoSkipPadding||0,o=a?a.widest.width+r:0,s=a?a.highest.height+r:0;return this.isHorizontal()?s*n>o*i?o/n:s/i:s*i<o*n?s/n:o/i},_isVisible:function(){var t,e,n,i=this.chart,a=this.options.display;if("auto"!==a)return !!a;for(t=0,e=i.data.datasets.length;t<e;++t)if(i.isDatasetVisible(t)&&((n=i.getDatasetMeta(t)).xAxisID===this.id||n.yAxisID===this.id))return !0;return !1},_computeGridLineItems:function(t){var e,n,i,a,r,o,s,l,u,d,h,c,f,g,p,m,v,b=this,x=b.chart,y=b.options,_=y.gridLines,k=y.position,w=_.offsetGridLines,M=b.isHorizontal(),S=b._ticksToDraw,C=S.length+(w?1:0),P=fn(_),A=[],D=_.drawBorder?dn(_.lineWidth,0,0):0,T=D/2,I=H._alignPixel,F=function(t){return I(x,t,D)};for("top"===k?(e=F(b.bottom),s=b.bottom-P,u=e-T,h=F(t.top)+T,f=t.bottom):"bottom"===k?(e=F(b.top),h=t.top,f=F(t.bottom)-T,s=e+T,u=b.top+P):"left"===k?(e=F(b.right),o=b.right-P,l=e-T,d=F(t.left)+T,c=t.right):(e=F(b.left),d=t.left,c=F(t.right)-T,o=e+T,l=b.left+P),n=0;n<C;++n)i=S[n]||{},ln(i.label)&&n<S.length||(n===b.zeroLineIndex&&y.offset===w?(g=_.zeroLineWidth,p=_.zeroLineColor,m=_.zeroLineBorderDash||[],v=_.zeroLineBorderDashOffset||0):(g=dn(_.lineWidth,n,1),p=dn(_.color,n,"rgba(0,0,0,0.1)"),m=_.borderDash||[],v=_.borderDashOffset||0),void 0!==(a=hn(b,i._index||n,w))&&(r=I(x,a,g),M?o=l=d=c=r:s=u=h=f=r,A.push({tx1:o,ty1:s,tx2:l,ty2:u,x1:d,y1:h,x2:c,y2:f,width:g,color:p,borderDash:m,borderDashOffset:v})));return A.ticksLength=C,A.borderValue=e,A},_computeLabelItems:function(){var t,e,n,i,a,r,o,s,l,u,d,h,c=this,f=c.options,g=f.ticks,p=f.position,m=g.mirror,v=c.isHorizontal(),b=c._ticksToDraw,x=mn(g),y=g.padding,_=fn(f.gridLines),k=-H.toRadians(c.labelRotation),w=[];for("top"===p?(r=c.bottom-_-y,o=k?"left":"center"):"bottom"===p?(r=c.top+_+y,o=k?"right":"center"):"left"===p?(a=c.right-(m?0:_)-y,o=m?"left":"right"):(a=c.left+(m?0:_)+y,o=m?"right":"left"),t=0,e=b.length;t<e;++t)i=(n=b[t]).label,ln(i)||(s=c.getPixelForTick(n._index||t)+g.labelOffset,u=(l=n.major?x.major:x.minor).lineHeight,d=sn(i)?i.length:1,v?(a=s,h="top"===p?((k?1:.5)-d)*u:(k?0:.5)*u):(r=s,h=(1-d)*u/2),w.push({x:a,y:r,rotation:k,label:i,font:l,textOffset:h,textAlign:o}));return w},_drawGrid:function(t){var e=this,n=e.options.gridLines;if(n.display){var i,a,r,o,s,l=e.ctx,u=e.chart,d=H._alignPixel,h=n.drawBorder?dn(n.lineWidth,0,0):0,c=e._gridLineItems||(e._gridLineItems=e._computeGridLineItems(t));for(r=0,o=c.length;r<o;++r)i=(s=c[r]).width,a=s.color,i&&a&&(l.save(),l.lineWidth=i,l.strokeStyle=a,l.setLineDash&&(l.setLineDash(s.borderDash),l.lineDashOffset=s.borderDashOffset),l.beginPath(),n.drawTicks&&(l.moveTo(s.tx1,s.ty1),l.lineTo(s.tx2,s.ty2)),n.drawOnChartArea&&(l.moveTo(s.x1,s.y1),l.lineTo(s.x2,s.y2)),l.stroke(),l.restore());if(h){var f,g,p,m,v=h,b=dn(n.lineWidth,c.ticksLength-1,1),x=c.borderValue;e.isHorizontal()?(f=d(u,e.left,v)-v/2,g=d(u,e.right,b)+b/2,p=m=x):(p=d(u,e.top,v)-v/2,m=d(u,e.bottom,b)+b/2,f=g=x),l.lineWidth=h,l.strokeStyle=dn(n.color,0),l.beginPath(),l.moveTo(f,p),l.lineTo(g,m),l.stroke();}}},_drawLabels:function(){var t=this;if(t.options.ticks.display){var e,n,i,a,r,o,s,l,u=t.ctx,d=t._labelItems||(t._labelItems=t._computeLabelItems());for(e=0,i=d.length;e<i;++e){if(o=(r=d[e]).font,u.save(),u.translate(r.x,r.y),u.rotate(r.rotation),u.font=o.string,u.fillStyle=o.color,u.textBaseline="middle",u.textAlign=r.textAlign,s=r.label,l=r.textOffset,sn(s))for(n=0,a=s.length;n<a;++n)u.fillText(""+s[n],0,l),l+=o.lineHeight;else u.fillText(s,0,l);u.restore();}}},_drawTitle:function(){var t=this,e=t.ctx,n=t.options,i=n.scaleLabel;if(i.display){var a,r,o=un(i.fontColor,N.global.defaultFontColor),s=H.options._parseFont(i),l=H.options.toPadding(i.padding),u=s.lineHeight/2,d=n.position,h=0;if(t.isHorizontal())a=t.left+t.width/2,r="bottom"===d?t.bottom-u-l.bottom:t.top+u+l.top;else {var c="left"===d;a=c?t.left+u+l.top:t.right-u-l.top,r=t.top+t.height/2,h=c?-.5*Math.PI:.5*Math.PI;}e.save(),e.translate(a,r),e.rotate(h),e.textAlign="center",e.textBaseline="middle",e.fillStyle=o,e.font=s.string,e.fillText(i.labelString,0,0),e.restore();}},draw:function(t){this._isVisible()&&(this._drawGrid(t),this._drawTitle(),this._drawLabels());},_layers:function(){var t=this,e=t.options,n=e.ticks&&e.ticks.z||0,i=e.gridLines&&e.gridLines.z||0;return t._isVisible()&&n!==i&&t.draw===t._draw?[{z:i,draw:function(){t._drawGrid.apply(t,arguments),t._drawTitle.apply(t,arguments);}},{z:n,draw:function(){t._drawLabels.apply(t,arguments);}}]:[{z:n,draw:function(){t.draw.apply(t,arguments);}}]},_getMatchingVisibleMetas:function(t){var e=this,n=e.isHorizontal();return e.chart._getSortedVisibleDatasetMetas().filter((function(i){return (!t||i.type===t)&&(n?i.xAxisID===e.id:i.yAxisID===e.id)}))}});xn.prototype._draw=xn.prototype.draw;var yn=xn,_n=H.isNullOrUndef,kn=yn.extend({determineDataLimits:function(){var t,e=this,n=e._getLabels(),i=e.options.ticks,a=i.min,r=i.max,o=0,s=n.length-1;void 0!==a&&(t=n.indexOf(a))>=0&&(o=t),void 0!==r&&(t=n.indexOf(r))>=0&&(s=t),e.minIndex=o,e.maxIndex=s,e.min=n[o],e.max=n[s];},buildTicks:function(){var t=this._getLabels(),e=this.minIndex,n=this.maxIndex;this.ticks=0===e&&n===t.length-1?t:t.slice(e,n+1);},getLabelForIndex:function(t,e){var n=this.chart;return n.getDatasetMeta(e).controller._getValueScaleId()===this.id?this.getRightValue(n.data.datasets[e].data[t]):this._getLabels()[t]},_configure:function(){var t=this,e=t.options.offset,n=t.ticks;yn.prototype._configure.call(t),t.isHorizontal()||(t._reversePixels=!t._reversePixels),n&&(t._startValue=t.minIndex-(e?.5:0),t._valueRange=Math.max(n.length-(e?0:1),1));},getPixelForValue:function(t,e,n){var i,a,r,o=this;return _n(e)||_n(n)||(t=o.chart.data.datasets[n].data[e]),_n(t)||(i=o.isHorizontal()?t.x:t.y),(void 0!==i||void 0!==t&&isNaN(e))&&(a=o._getLabels(),t=H.valueOrDefault(i,t),e=-1!==(r=a.indexOf(t))?r:e,isNaN(e)&&(e=t)),o.getPixelForDecimal((e-o._startValue)/o._valueRange)},getPixelForTick:function(t){var e=this.ticks;return t<0||t>e.length-1?null:this.getPixelForValue(e[t],t+this.minIndex)},getValueForPixel:function(t){var e=Math.round(this._startValue+this.getDecimalForPixel(t)*this._valueRange);return Math.min(Math.max(e,0),this.ticks.length-1)},getBasePixel:function(){return this.bottom}}),wn={position:"bottom"};kn._defaults=wn;var Mn=H.noop,Sn=H.isNullOrUndef;var Cn=yn.extend({getRightValue:function(t){return "string"==typeof t?+t:yn.prototype.getRightValue.call(this,t)},handleTickRangeOptions:function(){var t=this,e=t.options.ticks;if(e.beginAtZero){var n=H.sign(t.min),i=H.sign(t.max);n<0&&i<0?t.max=0:n>0&&i>0&&(t.min=0);}var a=void 0!==e.min||void 0!==e.suggestedMin,r=void 0!==e.max||void 0!==e.suggestedMax;void 0!==e.min?t.min=e.min:void 0!==e.suggestedMin&&(null===t.min?t.min=e.suggestedMin:t.min=Math.min(t.min,e.suggestedMin)),void 0!==e.max?t.max=e.max:void 0!==e.suggestedMax&&(null===t.max?t.max=e.suggestedMax:t.max=Math.max(t.max,e.suggestedMax)),a!==r&&t.min>=t.max&&(a?t.max=t.min+1:t.min=t.max-1),t.min===t.max&&(t.max++,e.beginAtZero||t.min--);},getTickLimit:function(){var t,e=this.options.ticks,n=e.stepSize,i=e.maxTicksLimit;return n?t=Math.ceil(this.max/n)-Math.floor(this.min/n)+1:(t=this._computeTickLimit(),i=i||11),i&&(t=Math.min(i,t)),t},_computeTickLimit:function(){return Number.POSITIVE_INFINITY},handleDirectionalChanges:Mn,buildTicks:function(){var t=this,e=t.options.ticks,n=t.getTickLimit(),i={maxTicks:n=Math.max(2,n),min:e.min,max:e.max,precision:e.precision,stepSize:H.valueOrDefault(e.fixedStepSize,e.stepSize)},a=t.ticks=function(t,e){var n,i,a,r,o=[],s=t.stepSize,l=s||1,u=t.maxTicks-1,d=t.min,h=t.max,c=t.precision,f=e.min,g=e.max,p=H.niceNum((g-f)/u/l)*l;if(p<1e-14&&Sn(d)&&Sn(h))return [f,g];(r=Math.ceil(g/p)-Math.floor(f/p))>u&&(p=H.niceNum(r*p/u/l)*l),s||Sn(c)?n=Math.pow(10,H._decimalPlaces(p)):(n=Math.pow(10,c),p=Math.ceil(p*n)/n),i=Math.floor(f/p)*p,a=Math.ceil(g/p)*p,s&&(!Sn(d)&&H.almostWhole(d/p,p/1e3)&&(i=d),!Sn(h)&&H.almostWhole(h/p,p/1e3)&&(a=h)),r=(a-i)/p,r=H.almostEquals(r,Math.round(r),p/1e3)?Math.round(r):Math.ceil(r),i=Math.round(i*n)/n,a=Math.round(a*n)/n,o.push(Sn(d)?i:d);for(var m=1;m<r;++m)o.push(Math.round((i+m*p)*n)/n);return o.push(Sn(h)?a:h),o}(i,t);t.handleDirectionalChanges(),t.max=H.max(a),t.min=H.min(a),e.reverse?(a.reverse(),t.start=t.max,t.end=t.min):(t.start=t.min,t.end=t.max);},convertTicksToLabels:function(){var t=this;t.ticksAsNumbers=t.ticks.slice(),t.zeroLineIndex=t.ticks.indexOf(0),yn.prototype.convertTicksToLabels.call(t);},_configure:function(){var t,e=this,n=e.getTicks(),i=e.min,a=e.max;yn.prototype._configure.call(e),e.options.offset&&n.length&&(i-=t=(a-i)/Math.max(n.length-1,1)/2,a+=t),e._startValue=i,e._endValue=a,e._valueRange=a-i;}}),Pn={position:"left",ticks:{callback:on.formatters.linear}};function An(t,e,n,i){var a,r,o=t.options,s=function(t,e,n){var i=[n.type,void 0===e&&void 0===n.stack?n.index:"",n.stack].join(".");return void 0===t[i]&&(t[i]={pos:[],neg:[]}),t[i]}(e,o.stacked,n),l=s.pos,u=s.neg,d=i.length;for(a=0;a<d;++a)r=t._parseValue(i[a]),isNaN(r.min)||isNaN(r.max)||n.data[a].hidden||(l[a]=l[a]||0,u[a]=u[a]||0,o.relativePoints?l[a]=100:r.min<0||r.max<0?u[a]+=r.min:l[a]+=r.max);}function Dn(t,e,n){var i,a,r=n.length;for(i=0;i<r;++i)a=t._parseValue(n[i]),isNaN(a.min)||isNaN(a.max)||e.data[i].hidden||(t.min=Math.min(t.min,a.min),t.max=Math.max(t.max,a.max));}var Tn=Cn.extend({determineDataLimits:function(){var t,e,n,i,a=this,r=a.options,o=a.chart.data.datasets,s=a._getMatchingVisibleMetas(),l=r.stacked,u={},d=s.length;if(a.min=Number.POSITIVE_INFINITY,a.max=Number.NEGATIVE_INFINITY,void 0===l)for(t=0;!l&&t<d;++t)l=void 0!==(e=s[t]).stack;for(t=0;t<d;++t)n=o[(e=s[t]).index].data,l?An(a,u,e,n):Dn(a,e,n);H.each(u,(function(t){i=t.pos.concat(t.neg),a.min=Math.min(a.min,H.min(i)),a.max=Math.max(a.max,H.max(i));})),a.min=H.isFinite(a.min)&&!isNaN(a.min)?a.min:0,a.max=H.isFinite(a.max)&&!isNaN(a.max)?a.max:1,a.handleTickRangeOptions();},_computeTickLimit:function(){var t;return this.isHorizontal()?Math.ceil(this.width/40):(t=H.options._parseFont(this.options.ticks),Math.ceil(this.height/t.lineHeight))},handleDirectionalChanges:function(){this.isHorizontal()||this.ticks.reverse();},getLabelForIndex:function(t,e){return this._getScaleLabel(this.chart.data.datasets[e].data[t])},getPixelForValue:function(t){return this.getPixelForDecimal((+this.getRightValue(t)-this._startValue)/this._valueRange)},getValueForPixel:function(t){return this._startValue+this.getDecimalForPixel(t)*this._valueRange},getPixelForTick:function(t){var e=this.ticksAsNumbers;return t<0||t>e.length-1?null:this.getPixelForValue(e[t])}}),In=Pn;Tn._defaults=In;var Fn=H.valueOrDefault,On=H.math.log10;var Ln={position:"left",ticks:{callback:on.formatters.logarithmic}};function Rn(t,e){return H.isFinite(t)&&t>=0?t:e}var zn=yn.extend({determineDataLimits:function(){var t,e,n,i,a,r,o=this,s=o.options,l=o.chart,u=l.data.datasets,d=o.isHorizontal();function h(t){return d?t.xAxisID===o.id:t.yAxisID===o.id}o.min=Number.POSITIVE_INFINITY,o.max=Number.NEGATIVE_INFINITY,o.minNotZero=Number.POSITIVE_INFINITY;var c=s.stacked;if(void 0===c)for(t=0;t<u.length;t++)if(e=l.getDatasetMeta(t),l.isDatasetVisible(t)&&h(e)&&void 0!==e.stack){c=!0;break}if(s.stacked||c){var f={};for(t=0;t<u.length;t++){var g=[(e=l.getDatasetMeta(t)).type,void 0===s.stacked&&void 0===e.stack?t:"",e.stack].join(".");if(l.isDatasetVisible(t)&&h(e))for(void 0===f[g]&&(f[g]=[]),a=0,r=(i=u[t].data).length;a<r;a++){var p=f[g];n=o._parseValue(i[a]),isNaN(n.min)||isNaN(n.max)||e.data[a].hidden||n.min<0||n.max<0||(p[a]=p[a]||0,p[a]+=n.max);}}H.each(f,(function(t){if(t.length>0){var e=H.min(t),n=H.max(t);o.min=Math.min(o.min,e),o.max=Math.max(o.max,n);}}));}else for(t=0;t<u.length;t++)if(e=l.getDatasetMeta(t),l.isDatasetVisible(t)&&h(e))for(a=0,r=(i=u[t].data).length;a<r;a++)n=o._parseValue(i[a]),isNaN(n.min)||isNaN(n.max)||e.data[a].hidden||n.min<0||n.max<0||(o.min=Math.min(n.min,o.min),o.max=Math.max(n.max,o.max),0!==n.min&&(o.minNotZero=Math.min(n.min,o.minNotZero)));o.min=H.isFinite(o.min)?o.min:null,o.max=H.isFinite(o.max)?o.max:null,o.minNotZero=H.isFinite(o.minNotZero)?o.minNotZero:null,this.handleTickRangeOptions();},handleTickRangeOptions:function(){var t=this,e=t.options.ticks;t.min=Rn(e.min,t.min),t.max=Rn(e.max,t.max),t.min===t.max&&(0!==t.min&&null!==t.min?(t.min=Math.pow(10,Math.floor(On(t.min))-1),t.max=Math.pow(10,Math.floor(On(t.max))+1)):(t.min=1,t.max=10)),null===t.min&&(t.min=Math.pow(10,Math.floor(On(t.max))-1)),null===t.max&&(t.max=0!==t.min?Math.pow(10,Math.floor(On(t.min))+1):10),null===t.minNotZero&&(t.min>0?t.minNotZero=t.min:t.max<1?t.minNotZero=Math.pow(10,Math.floor(On(t.max))):t.minNotZero=1);},buildTicks:function(){var t=this,e=t.options.ticks,n=!t.isHorizontal(),i={min:Rn(e.min),max:Rn(e.max)},a=t.ticks=function(t,e){var n,i,a=[],r=Fn(t.min,Math.pow(10,Math.floor(On(e.min)))),o=Math.floor(On(e.max)),s=Math.ceil(e.max/Math.pow(10,o));0===r?(n=Math.floor(On(e.minNotZero)),i=Math.floor(e.minNotZero/Math.pow(10,n)),a.push(r),r=i*Math.pow(10,n)):(n=Math.floor(On(r)),i=Math.floor(r/Math.pow(10,n)));var l=n<0?Math.pow(10,Math.abs(n)):1;do{a.push(r),10===++i&&(i=1,l=++n>=0?1:l),r=Math.round(i*Math.pow(10,n)*l)/l;}while(n<o||n===o&&i<s);var u=Fn(t.max,r);return a.push(u),a}(i,t);t.max=H.max(a),t.min=H.min(a),e.reverse?(n=!n,t.start=t.max,t.end=t.min):(t.start=t.min,t.end=t.max),n&&a.reverse();},convertTicksToLabels:function(){this.tickValues=this.ticks.slice(),yn.prototype.convertTicksToLabels.call(this);},getLabelForIndex:function(t,e){return this._getScaleLabel(this.chart.data.datasets[e].data[t])},getPixelForTick:function(t){var e=this.tickValues;return t<0||t>e.length-1?null:this.getPixelForValue(e[t])},_getFirstTickValue:function(t){var e=Math.floor(On(t));return Math.floor(t/Math.pow(10,e))*Math.pow(10,e)},_configure:function(){var t=this,e=t.min,n=0;yn.prototype._configure.call(t),0===e&&(e=t._getFirstTickValue(t.minNotZero),n=Fn(t.options.ticks.fontSize,N.global.defaultFontSize)/t._length),t._startValue=On(e),t._valueOffset=n,t._valueRange=(On(t.max)-On(e))/(1-n);},getPixelForValue:function(t){var e=this,n=0;return (t=+e.getRightValue(t))>e.min&&t>0&&(n=(On(t)-e._startValue)/e._valueRange+e._valueOffset),e.getPixelForDecimal(n)},getValueForPixel:function(t){var e=this,n=e.getDecimalForPixel(t);return 0===n&&0===e.min?0:Math.pow(10,e._startValue+(n-e._valueOffset)*e._valueRange)}}),Nn=Ln;zn._defaults=Nn;var Bn=H.valueOrDefault,En=H.valueAtIndexOrDefault,Wn=H.options.resolve,Vn={display:!0,animate:!0,position:"chartArea",angleLines:{display:!0,color:"rgba(0,0,0,0.1)",lineWidth:1,borderDash:[],borderDashOffset:0},gridLines:{circular:!1},ticks:{showLabelBackdrop:!0,backdropColor:"rgba(255,255,255,0.75)",backdropPaddingY:2,backdropPaddingX:2,callback:on.formatters.linear},pointLabels:{display:!0,fontSize:10,callback:function(t){return t}}};function Hn(t){var e=t.ticks;return e.display&&t.display?Bn(e.fontSize,N.global.defaultFontSize)+2*e.backdropPaddingY:0}function jn(t,e,n,i,a){return t===i||t===a?{start:e-n/2,end:e+n/2}:t<i||t>a?{start:e-n,end:e}:{start:e,end:e+n}}function qn(t){return 0===t||180===t?"center":t<180?"left":"right"}function Un(t,e,n,i){var a,r,o=n.y+i/2;if(H.isArray(e))for(a=0,r=e.length;a<r;++a)t.fillText(e[a],n.x,o),o+=i;else t.fillText(e,n.x,o);}function Yn(t,e,n){90===t||270===t?n.y-=e.h/2:(t>270||t<90)&&(n.y-=e.h);}function Gn(t){return H.isNumber(t)?t:0}var Xn=Cn.extend({setDimensions:function(){var t=this;t.width=t.maxWidth,t.height=t.maxHeight,t.paddingTop=Hn(t.options)/2,t.xCenter=Math.floor(t.width/2),t.yCenter=Math.floor((t.height-t.paddingTop)/2),t.drawingArea=Math.min(t.height-t.paddingTop,t.width)/2;},determineDataLimits:function(){var t=this,e=t.chart,n=Number.POSITIVE_INFINITY,i=Number.NEGATIVE_INFINITY;H.each(e.data.datasets,(function(a,r){if(e.isDatasetVisible(r)){var o=e.getDatasetMeta(r);H.each(a.data,(function(e,a){var r=+t.getRightValue(e);isNaN(r)||o.data[a].hidden||(n=Math.min(r,n),i=Math.max(r,i));}));}})),t.min=n===Number.POSITIVE_INFINITY?0:n,t.max=i===Number.NEGATIVE_INFINITY?0:i,t.handleTickRangeOptions();},_computeTickLimit:function(){return Math.ceil(this.drawingArea/Hn(this.options))},convertTicksToLabels:function(){var t=this;Cn.prototype.convertTicksToLabels.call(t),t.pointLabels=t.chart.data.labels.map((function(){var e=H.callback(t.options.pointLabels.callback,arguments,t);return e||0===e?e:""}));},getLabelForIndex:function(t,e){return +this.getRightValue(this.chart.data.datasets[e].data[t])},fit:function(){var t=this.options;t.display&&t.pointLabels.display?function(t){var e,n,i,a=H.options._parseFont(t.options.pointLabels),r={l:0,r:t.width,t:0,b:t.height-t.paddingTop},o={};t.ctx.font=a.string,t._pointLabelSizes=[];var s,l,u,d=t.chart.data.labels.length;for(e=0;e<d;e++){i=t.getPointPosition(e,t.drawingArea+5),s=t.ctx,l=a.lineHeight,u=t.pointLabels[e],n=H.isArray(u)?{w:H.longestText(s,s.font,u),h:u.length*l}:{w:s.measureText(u).width,h:l},t._pointLabelSizes[e]=n;var h=t.getIndexAngle(e),c=H.toDegrees(h)%360,f=jn(c,i.x,n.w,0,180),g=jn(c,i.y,n.h,90,270);f.start<r.l&&(r.l=f.start,o.l=h),f.end>r.r&&(r.r=f.end,o.r=h),g.start<r.t&&(r.t=g.start,o.t=h),g.end>r.b&&(r.b=g.end,o.b=h);}t.setReductions(t.drawingArea,r,o);}(this):this.setCenterPoint(0,0,0,0);},setReductions:function(t,e,n){var i=this,a=e.l/Math.sin(n.l),r=Math.max(e.r-i.width,0)/Math.sin(n.r),o=-e.t/Math.cos(n.t),s=-Math.max(e.b-(i.height-i.paddingTop),0)/Math.cos(n.b);a=Gn(a),r=Gn(r),o=Gn(o),s=Gn(s),i.drawingArea=Math.min(Math.floor(t-(a+r)/2),Math.floor(t-(o+s)/2)),i.setCenterPoint(a,r,o,s);},setCenterPoint:function(t,e,n,i){var a=this,r=a.width-e-a.drawingArea,o=t+a.drawingArea,s=n+a.drawingArea,l=a.height-a.paddingTop-i-a.drawingArea;a.xCenter=Math.floor((o+r)/2+a.left),a.yCenter=Math.floor((s+l)/2+a.top+a.paddingTop);},getIndexAngle:function(t){var e=this.chart,n=(t*(360/e.data.labels.length)+((e.options||{}).startAngle||0))%360;return (n<0?n+360:n)*Math.PI*2/360},getDistanceFromCenterForValue:function(t){var e=this;if(H.isNullOrUndef(t))return NaN;var n=e.drawingArea/(e.max-e.min);return e.options.ticks.reverse?(e.max-t)*n:(t-e.min)*n},getPointPosition:function(t,e){var n=this.getIndexAngle(t)-Math.PI/2;return {x:Math.cos(n)*e+this.xCenter,y:Math.sin(n)*e+this.yCenter}},getPointPositionForValue:function(t,e){return this.getPointPosition(t,this.getDistanceFromCenterForValue(e))},getBasePosition:function(t){var e=this.min,n=this.max;return this.getPointPositionForValue(t||0,this.beginAtZero?0:e<0&&n<0?n:e>0&&n>0?e:0)},_drawGrid:function(){var t,e,n,i=this,a=i.ctx,r=i.options,o=r.gridLines,s=r.angleLines,l=Bn(s.lineWidth,o.lineWidth),u=Bn(s.color,o.color);if(r.pointLabels.display&&function(t){var e=t.ctx,n=t.options,i=n.pointLabels,a=Hn(n),r=t.getDistanceFromCenterForValue(n.ticks.reverse?t.min:t.max),o=H.options._parseFont(i);e.save(),e.font=o.string,e.textBaseline="middle";for(var s=t.chart.data.labels.length-1;s>=0;s--){var l=0===s?a/2:0,u=t.getPointPosition(s,r+l+5),d=En(i.fontColor,s,N.global.defaultFontColor);e.fillStyle=d;var h=t.getIndexAngle(s),c=H.toDegrees(h);e.textAlign=qn(c),Yn(c,t._pointLabelSizes[s],u),Un(e,t.pointLabels[s],u,o.lineHeight);}e.restore();}(i),o.display&&H.each(i.ticks,(function(t,n){0!==n&&(e=i.getDistanceFromCenterForValue(i.ticksAsNumbers[n]),function(t,e,n,i){var a,r=t.ctx,o=e.circular,s=t.chart.data.labels.length,l=En(e.color,i-1),u=En(e.lineWidth,i-1);if((o||s)&&l&&u){if(r.save(),r.strokeStyle=l,r.lineWidth=u,r.setLineDash&&(r.setLineDash(e.borderDash||[]),r.lineDashOffset=e.borderDashOffset||0),r.beginPath(),o)r.arc(t.xCenter,t.yCenter,n,0,2*Math.PI);else {a=t.getPointPosition(0,n),r.moveTo(a.x,a.y);for(var d=1;d<s;d++)a=t.getPointPosition(d,n),r.lineTo(a.x,a.y);}r.closePath(),r.stroke(),r.restore();}}(i,o,e,n));})),s.display&&l&&u){for(a.save(),a.lineWidth=l,a.strokeStyle=u,a.setLineDash&&(a.setLineDash(Wn([s.borderDash,o.borderDash,[]])),a.lineDashOffset=Wn([s.borderDashOffset,o.borderDashOffset,0])),t=i.chart.data.labels.length-1;t>=0;t--)e=i.getDistanceFromCenterForValue(r.ticks.reverse?i.min:i.max),n=i.getPointPosition(t,e),a.beginPath(),a.moveTo(i.xCenter,i.yCenter),a.lineTo(n.x,n.y),a.stroke();a.restore();}},_drawLabels:function(){var t=this,e=t.ctx,n=t.options.ticks;if(n.display){var i,a,r=t.getIndexAngle(0),o=H.options._parseFont(n),s=Bn(n.fontColor,N.global.defaultFontColor);e.save(),e.font=o.string,e.translate(t.xCenter,t.yCenter),e.rotate(r),e.textAlign="center",e.textBaseline="middle",H.each(t.ticks,(function(r,l){(0!==l||n.reverse)&&(i=t.getDistanceFromCenterForValue(t.ticksAsNumbers[l]),n.showLabelBackdrop&&(a=e.measureText(r).width,e.fillStyle=n.backdropColor,e.fillRect(-a/2-n.backdropPaddingX,-i-o.size/2-n.backdropPaddingY,a+2*n.backdropPaddingX,o.size+2*n.backdropPaddingY)),e.fillStyle=s,e.fillText(r,0,-i));})),e.restore();}},_drawTitle:H.noop}),Kn=Vn;Xn._defaults=Kn;var Zn=H._deprecated,$n=H.options.resolve,Jn=H.valueOrDefault,Qn=Number.MIN_SAFE_INTEGER||-9007199254740991,ti=Number.MAX_SAFE_INTEGER||9007199254740991,ei={millisecond:{common:!0,size:1,steps:1e3},second:{common:!0,size:1e3,steps:60},minute:{common:!0,size:6e4,steps:60},hour:{common:!0,size:36e5,steps:24},day:{common:!0,size:864e5,steps:30},week:{common:!1,size:6048e5,steps:4},month:{common:!0,size:2628e6,steps:12},quarter:{common:!1,size:7884e6,steps:4},year:{common:!0,size:3154e7}},ni=Object.keys(ei);function ii(t,e){return t-e}function ai(t){return H.valueOrDefault(t.time.min,t.ticks.min)}function ri(t){return H.valueOrDefault(t.time.max,t.ticks.max)}function oi(t,e,n,i){var a=function(t,e,n){for(var i,a,r,o=0,s=t.length-1;o>=0&&o<=s;){if(a=t[(i=o+s>>1)-1]||null,r=t[i],!a)return {lo:null,hi:r};if(r[e]<n)o=i+1;else {if(!(a[e]>n))return {lo:a,hi:r};s=i-1;}}return {lo:r,hi:null}}(t,e,n),r=a.lo?a.hi?a.lo:t[t.length-2]:t[0],o=a.lo?a.hi?a.hi:t[t.length-1]:t[1],s=o[e]-r[e],l=s?(n-r[e])/s:0,u=(o[i]-r[i])*l;return r[i]+u}function si(t,e){var n=t._adapter,i=t.options.time,a=i.parser,r=a||i.format,o=e;return "function"==typeof a&&(o=a(o)),H.isFinite(o)||(o="string"==typeof r?n.parse(o,r):n.parse(o)),null!==o?+o:(a||"function"!=typeof r||(o=r(e),H.isFinite(o)||(o=n.parse(o))),o)}function li(t,e){if(H.isNullOrUndef(e))return null;var n=t.options.time,i=si(t,t.getRightValue(e));return null===i?i:(n.round&&(i=+t._adapter.startOf(i,n.round)),i)}function ui(t,e,n,i){var a,r,o,s=ni.length;for(a=ni.indexOf(t);a<s-1;++a)if(o=(r=ei[ni[a]]).steps?r.steps:ti,r.common&&Math.ceil((n-e)/(o*r.size))<=i)return ni[a];return ni[s-1]}function di(t,e,n){var i,a,r=[],o={},s=e.length;for(i=0;i<s;++i)o[a=e[i]]=i,r.push({value:a,major:!1});return 0!==s&&n?function(t,e,n,i){var a,r,o=t._adapter,s=+o.startOf(e[0].value,i),l=e[e.length-1].value;for(a=s;a<=l;a=+o.add(a,1,i))(r=n[a])>=0&&(e[r].major=!0);return e}(t,r,o,n):r}var hi=yn.extend({initialize:function(){this.mergeTicksOptions(),yn.prototype.initialize.call(this);},update:function(){var t=this,e=t.options,n=e.time||(e.time={}),i=t._adapter=new rn._date(e.adapters.date);return Zn("time scale",n.format,"time.format","time.parser"),Zn("time scale",n.min,"time.min","ticks.min"),Zn("time scale",n.max,"time.max","ticks.max"),H.mergeIf(n.displayFormats,i.formats()),yn.prototype.update.apply(t,arguments)},getRightValue:function(t){return t&&void 0!==t.t&&(t=t.t),yn.prototype.getRightValue.call(this,t)},determineDataLimits:function(){var t,e,n,i,a,r,o,s=this,l=s.chart,u=s._adapter,d=s.options,h=d.time.unit||"day",c=ti,f=Qn,g=[],p=[],m=[],v=s._getLabels();for(t=0,n=v.length;t<n;++t)m.push(li(s,v[t]));for(t=0,n=(l.data.datasets||[]).length;t<n;++t)if(l.isDatasetVisible(t))if(a=l.data.datasets[t].data,H.isObject(a[0]))for(p[t]=[],e=0,i=a.length;e<i;++e)r=li(s,a[e]),g.push(r),p[t][e]=r;else p[t]=m.slice(0),o||(g=g.concat(m),o=!0);else p[t]=[];m.length&&(c=Math.min(c,m[0]),f=Math.max(f,m[m.length-1])),g.length&&(g=n>1?function(t){var e,n,i,a={},r=[];for(e=0,n=t.length;e<n;++e)a[i=t[e]]||(a[i]=!0,r.push(i));return r}(g).sort(ii):g.sort(ii),c=Math.min(c,g[0]),f=Math.max(f,g[g.length-1])),c=li(s,ai(d))||c,f=li(s,ri(d))||f,c=c===ti?+u.startOf(Date.now(),h):c,f=f===Qn?+u.endOf(Date.now(),h)+1:f,s.min=Math.min(c,f),s.max=Math.max(c+1,f),s._table=[],s._timestamps={data:g,datasets:p,labels:m};},buildTicks:function(){var t,e,n,i=this,a=i.min,r=i.max,o=i.options,s=o.ticks,l=o.time,u=i._timestamps,d=[],h=i.getLabelCapacity(a),c=s.source,f=o.distribution;for(u="data"===c||"auto"===c&&"series"===f?u.data:"labels"===c?u.labels:function(t,e,n,i){var a,r=t._adapter,o=t.options,s=o.time,l=s.unit||ui(s.minUnit,e,n,i),u=$n([s.stepSize,s.unitStepSize,1]),d="week"===l&&s.isoWeekday,h=e,c=[];if(d&&(h=+r.startOf(h,"isoWeek",d)),h=+r.startOf(h,d?"day":l),r.diff(n,e,l)>1e5*u)throw e+" and "+n+" are too far apart with stepSize of "+u+" "+l;for(a=h;a<n;a=+r.add(a,u,l))c.push(a);return a!==n&&"ticks"!==o.bounds||c.push(a),c}(i,a,r,h),"ticks"===o.bounds&&u.length&&(a=u[0],r=u[u.length-1]),a=li(i,ai(o))||a,r=li(i,ri(o))||r,t=0,e=u.length;t<e;++t)(n=u[t])>=a&&n<=r&&d.push(n);return i.min=a,i.max=r,i._unit=l.unit||(s.autoSkip?ui(l.minUnit,i.min,i.max,h):function(t,e,n,i,a){var r,o;for(r=ni.length-1;r>=ni.indexOf(n);r--)if(o=ni[r],ei[o].common&&t._adapter.diff(a,i,o)>=e-1)return o;return ni[n?ni.indexOf(n):0]}(i,d.length,l.minUnit,i.min,i.max)),i._majorUnit=s.major.enabled&&"year"!==i._unit?function(t){for(var e=ni.indexOf(t)+1,n=ni.length;e<n;++e)if(ei[ni[e]].common)return ni[e]}(i._unit):void 0,i._table=function(t,e,n,i){if("linear"===i||!t.length)return [{time:e,pos:0},{time:n,pos:1}];var a,r,o,s,l,u=[],d=[e];for(a=0,r=t.length;a<r;++a)(s=t[a])>e&&s<n&&d.push(s);for(d.push(n),a=0,r=d.length;a<r;++a)l=d[a+1],o=d[a-1],s=d[a],void 0!==o&&void 0!==l&&Math.round((l+o)/2)===s||u.push({time:s,pos:a/(r-1)});return u}(i._timestamps.data,a,r,f),i._offsets=function(t,e,n,i,a){var r,o,s=0,l=0;return a.offset&&e.length&&(r=oi(t,"time",e[0],"pos"),s=1===e.length?1-r:(oi(t,"time",e[1],"pos")-r)/2,o=oi(t,"time",e[e.length-1],"pos"),l=1===e.length?o:(o-oi(t,"time",e[e.length-2],"pos"))/2),{start:s,end:l,factor:1/(s+1+l)}}(i._table,d,0,0,o),s.reverse&&d.reverse(),di(i,d,i._majorUnit)},getLabelForIndex:function(t,e){var n=this,i=n._adapter,a=n.chart.data,r=n.options.time,o=a.labels&&t<a.labels.length?a.labels[t]:"",s=a.datasets[e].data[t];return H.isObject(s)&&(o=n.getRightValue(s)),r.tooltipFormat?i.format(si(n,o),r.tooltipFormat):"string"==typeof o?o:i.format(si(n,o),r.displayFormats.datetime)},tickFormatFunction:function(t,e,n,i){var a=this._adapter,r=this.options,o=r.time.displayFormats,s=o[this._unit],l=this._majorUnit,u=o[l],d=n[e],h=r.ticks,c=l&&u&&d&&d.major,f=a.format(t,i||(c?u:s)),g=c?h.major:h.minor,p=$n([g.callback,g.userCallback,h.callback,h.userCallback]);return p?p(f,e,n):f},convertTicksToLabels:function(t){var e,n,i=[];for(e=0,n=t.length;e<n;++e)i.push(this.tickFormatFunction(t[e].value,e,t));return i},getPixelForOffset:function(t){var e=this._offsets,n=oi(this._table,"time",t,"pos");return this.getPixelForDecimal((e.start+n)*e.factor)},getPixelForValue:function(t,e,n){var i=null;if(void 0!==e&&void 0!==n&&(i=this._timestamps.datasets[n][e]),null===i&&(i=li(this,t)),null!==i)return this.getPixelForOffset(i)},getPixelForTick:function(t){var e=this.getTicks();return t>=0&&t<e.length?this.getPixelForOffset(e[t].value):null},getValueForPixel:function(t){var e=this._offsets,n=this.getDecimalForPixel(t)/e.factor-e.end,i=oi(this._table,"pos",n,"time");return this._adapter._create(i)},_getLabelSize:function(t){var e=this.options.ticks,n=this.ctx.measureText(t).width,i=H.toRadians(this.isHorizontal()?e.maxRotation:e.minRotation),a=Math.cos(i),r=Math.sin(i),o=Jn(e.fontSize,N.global.defaultFontSize);return {w:n*a+o*r,h:n*r+o*a}},getLabelWidth:function(t){return this._getLabelSize(t).w},getLabelCapacity:function(t){var e=this,n=e.options.time,i=n.displayFormats,a=i[n.unit]||i.millisecond,r=e.tickFormatFunction(t,0,di(e,[t],e._majorUnit),a),o=e._getLabelSize(r),s=Math.floor(e.isHorizontal()?e.width/o.w:e.height/o.h);return e.options.offset&&s--,s>0?s:1}}),ci={position:"bottom",distribution:"linear",bounds:"data",adapters:{},time:{parser:!1,unit:!1,round:!1,displayFormat:!1,isoWeekday:!1,minUnit:"millisecond",displayFormats:{}},ticks:{autoSkip:!1,source:"auto",major:{enabled:!1}}};hi._defaults=ci;var fi={category:kn,linear:Tn,logarithmic:zn,radialLinear:Xn,time:hi},gi={datetime:"MMM D, YYYY, h:mm:ss a",millisecond:"h:mm:ss.SSS a",second:"h:mm:ss a",minute:"h:mm a",hour:"hA",day:"MMM D",week:"ll",month:"MMM YYYY",quarter:"[Q]Q - YYYY",year:"YYYY"};rn._date.override("function"==typeof t?{_id:"moment",formats:function(){return gi},parse:function(e,n){return "string"==typeof e&&"string"==typeof n?e=t(e,n):e instanceof t||(e=t(e)),e.isValid()?e.valueOf():null},format:function(e,n){return t(e).format(n)},add:function(e,n,i){return t(e).add(n,i).valueOf()},diff:function(e,n,i){return t(e).diff(t(n),i)},startOf:function(e,n,i){return e=t(e),"isoWeek"===n?e.isoWeekday(i).valueOf():e.startOf(n).valueOf()},endOf:function(e,n){return t(e).endOf(n).valueOf()},_create:function(e){return t(e)}}:{}),N._set("global",{plugins:{filler:{propagate:!0}}});var pi={dataset:function(t){var e=t.fill,n=t.chart,i=n.getDatasetMeta(e),a=i&&n.isDatasetVisible(e)&&i.dataset._children||[],r=a.length||0;return r?function(t,e){return e<r&&a[e]._view||null}:null},boundary:function(t){var e=t.boundary,n=e?e.x:null,i=e?e.y:null;return H.isArray(e)?function(t,n){return e[n]}:function(t){return {x:null===n?t.x:n,y:null===i?t.y:i}}}};function mi(t,e,n){var i,a=t._model||{},r=a.fill;if(void 0===r&&(r=!!a.backgroundColor),!1===r||null===r)return !1;if(!0===r)return "origin";if(i=parseFloat(r,10),isFinite(i)&&Math.floor(i)===i)return "-"!==r[0]&&"+"!==r[0]||(i=e+i),!(i===e||i<0||i>=n)&&i;switch(r){case"bottom":return "start";case"top":return "end";case"zero":return "origin";case"origin":case"start":case"end":return r;default:return !1}}function vi(t){return (t.el._scale||{}).getPointPositionForValue?function(t){var e,n,i,a,r,o=t.el._scale,s=o.options,l=o.chart.data.labels.length,u=t.fill,d=[];if(!l)return null;for(e=s.ticks.reverse?o.max:o.min,n=s.ticks.reverse?o.min:o.max,i=o.getPointPositionForValue(0,e),a=0;a<l;++a)r="start"===u||"end"===u?o.getPointPositionForValue(a,"start"===u?e:n):o.getBasePosition(a),s.gridLines.circular&&(r.cx=i.x,r.cy=i.y,r.angle=o.getIndexAngle(a)-Math.PI/2),d.push(r);return d}(t):function(t){var e,n=t.el._model||{},i=t.el._scale||{},a=t.fill,r=null;if(isFinite(a))return null;if("start"===a?r=void 0===n.scaleBottom?i.bottom:n.scaleBottom:"end"===a?r=void 0===n.scaleTop?i.top:n.scaleTop:void 0!==n.scaleZero?r=n.scaleZero:i.getBasePixel&&(r=i.getBasePixel()),null!=r){if(void 0!==r.x&&void 0!==r.y)return r;if(H.isFinite(r))return {x:(e=i.isHorizontal())?r:null,y:e?null:r}}return null}(t)}function bi(t,e,n){var i,a=t[e].fill,r=[e];if(!n)return a;for(;!1!==a&&-1===r.indexOf(a);){if(!isFinite(a))return a;if(!(i=t[a]))return !1;if(i.visible)return a;r.push(a),a=i.fill;}return !1}function xi(t){var e=t.fill,n="dataset";return !1===e?null:(isFinite(e)||(n="boundary"),pi[n](t))}function yi(t){return t&&!t.skip}function _i(t,e,n,i,a){var r,o,s,l;if(i&&a){for(t.moveTo(e[0].x,e[0].y),r=1;r<i;++r)H.canvas.lineTo(t,e[r-1],e[r]);if(void 0===n[0].angle)for(t.lineTo(n[a-1].x,n[a-1].y),r=a-1;r>0;--r)H.canvas.lineTo(t,n[r],n[r-1],!0);else for(o=n[0].cx,s=n[0].cy,l=Math.sqrt(Math.pow(n[0].x-o,2)+Math.pow(n[0].y-s,2)),r=a-1;r>0;--r)t.arc(o,s,l,n[r].angle,n[r-1].angle,!0);}}function ki(t,e,n,i,a,r){var o,s,l,u,d,h,c,f,g=e.length,p=i.spanGaps,m=[],v=[],b=0,x=0;for(t.beginPath(),o=0,s=g;o<s;++o)d=n(u=e[l=o%g]._view,l,i),h=yi(u),c=yi(d),r&&void 0===f&&h&&(s=g+(f=o+1)),h&&c?(b=m.push(u),x=v.push(d)):b&&x&&(p?(h&&m.push(u),c&&v.push(d)):(_i(t,m,v,b,x),b=x=0,m=[],v=[]));_i(t,m,v,b,x),t.closePath(),t.fillStyle=a,t.fill();}var wi={id:"filler",afterDatasetsUpdate:function(t,e){var n,i,a,r,o=(t.data.datasets||[]).length,s=e.propagate,l=[];for(i=0;i<o;++i)r=null,(a=(n=t.getDatasetMeta(i)).dataset)&&a._model&&a instanceof kt.Line&&(r={visible:t.isDatasetVisible(i),fill:mi(a,i,o),chart:t,el:a}),n.$filler=r,l.push(r);for(i=0;i<o;++i)(r=l[i])&&(r.fill=bi(l,i,s),r.boundary=vi(r),r.mapper=xi(r));},beforeDatasetsDraw:function(t){var e,n,i,a,r,o,s,l=t._getSortedVisibleDatasetMetas(),u=t.ctx;for(n=l.length-1;n>=0;--n)(e=l[n].$filler)&&e.visible&&(a=(i=e.el)._view,r=i._children||[],o=e.mapper,s=a.backgroundColor||N.global.defaultColor,o&&s&&r.length&&(H.canvas.clipArea(u,t.chartArea),ki(u,r,o,a,s,i._loop),H.canvas.unclipArea(u)));}},Mi=H.rtl.getRtlAdapter,Si=H.noop,Ci=H.valueOrDefault;function Pi(t,e){return t.usePointStyle&&t.boxWidth>e?e:t.boxWidth}N._set("global",{legend:{display:!0,position:"top",align:"center",fullWidth:!0,reverse:!1,weight:1e3,onClick:function(t,e){var n=e.datasetIndex,i=this.chart,a=i.getDatasetMeta(n);a.hidden=null===a.hidden?!i.data.datasets[n].hidden:null,i.update();},onHover:null,onLeave:null,labels:{boxWidth:40,padding:10,generateLabels:function(t){var e=t.data.datasets,n=t.options.legend||{},i=n.labels&&n.labels.usePointStyle;return t._getSortedDatasetMetas().map((function(n){var a=n.controller.getStyle(i?0:void 0);return {text:e[n.index].label,fillStyle:a.backgroundColor,hidden:!t.isDatasetVisible(n.index),lineCap:a.borderCapStyle,lineDash:a.borderDash,lineDashOffset:a.borderDashOffset,lineJoin:a.borderJoinStyle,lineWidth:a.borderWidth,strokeStyle:a.borderColor,pointStyle:a.pointStyle,rotation:a.rotation,datasetIndex:n.index}}),this)}}},legendCallback:function(t){var e,n,i,a=document.createElement("ul"),r=t.data.datasets;for(a.setAttribute("class",t.id+"-legend"),e=0,n=r.length;e<n;e++)(i=a.appendChild(document.createElement("li"))).appendChild(document.createElement("span")).style.backgroundColor=r[e].backgroundColor,r[e].label&&i.appendChild(document.createTextNode(r[e].label));return a.outerHTML}});var Ai=K.extend({initialize:function(t){H.extend(this,t),this.legendHitBoxes=[],this._hoveredItem=null,this.doughnutMode=!1;},beforeUpdate:Si,update:function(t,e,n){var i=this;return i.beforeUpdate(),i.maxWidth=t,i.maxHeight=e,i.margins=n,i.beforeSetDimensions(),i.setDimensions(),i.afterSetDimensions(),i.beforeBuildLabels(),i.buildLabels(),i.afterBuildLabels(),i.beforeFit(),i.fit(),i.afterFit(),i.afterUpdate(),i.minSize},afterUpdate:Si,beforeSetDimensions:Si,setDimensions:function(){var t=this;t.isHorizontal()?(t.width=t.maxWidth,t.left=0,t.right=t.width):(t.height=t.maxHeight,t.top=0,t.bottom=t.height),t.paddingLeft=0,t.paddingTop=0,t.paddingRight=0,t.paddingBottom=0,t.minSize={width:0,height:0};},afterSetDimensions:Si,beforeBuildLabels:Si,buildLabels:function(){var t=this,e=t.options.labels||{},n=H.callback(e.generateLabels,[t.chart],t)||[];e.filter&&(n=n.filter((function(n){return e.filter(n,t.chart.data)}))),t.options.reverse&&n.reverse(),t.legendItems=n;},afterBuildLabels:Si,beforeFit:Si,fit:function(){var t=this,e=t.options,n=e.labels,i=e.display,a=t.ctx,r=H.options._parseFont(n),o=r.size,s=t.legendHitBoxes=[],l=t.minSize,u=t.isHorizontal();if(u?(l.width=t.maxWidth,l.height=i?10:0):(l.width=i?10:0,l.height=t.maxHeight),i){if(a.font=r.string,u){var d=t.lineWidths=[0],h=0;a.textAlign="left",a.textBaseline="middle",H.each(t.legendItems,(function(t,e){var i=Pi(n,o)+o/2+a.measureText(t.text).width;(0===e||d[d.length-1]+i+2*n.padding>l.width)&&(h+=o+n.padding,d[d.length-(e>0?0:1)]=0),s[e]={left:0,top:0,width:i,height:o},d[d.length-1]+=i+n.padding;})),l.height+=h;}else {var c=n.padding,f=t.columnWidths=[],g=t.columnHeights=[],p=n.padding,m=0,v=0;H.each(t.legendItems,(function(t,e){var i=Pi(n,o)+o/2+a.measureText(t.text).width;e>0&&v+o+2*c>l.height&&(p+=m+n.padding,f.push(m),g.push(v),m=0,v=0),m=Math.max(m,i),v+=o+c,s[e]={left:0,top:0,width:i,height:o};})),p+=m,f.push(m),g.push(v),l.width+=p;}t.width=l.width,t.height=l.height;}else t.width=l.width=t.height=l.height=0;},afterFit:Si,isHorizontal:function(){return "top"===this.options.position||"bottom"===this.options.position},draw:function(){var t=this,e=t.options,n=e.labels,i=N.global,a=i.defaultColor,r=i.elements.line,o=t.height,s=t.columnHeights,l=t.width,u=t.lineWidths;if(e.display){var d,h=Mi(e.rtl,t.left,t.minSize.width),c=t.ctx,f=Ci(n.fontColor,i.defaultFontColor),g=H.options._parseFont(n),p=g.size;c.textAlign=h.textAlign("left"),c.textBaseline="middle",c.lineWidth=.5,c.strokeStyle=f,c.fillStyle=f,c.font=g.string;var m=Pi(n,p),v=t.legendHitBoxes,b=function(t,i){switch(e.align){case"start":return n.padding;case"end":return t-i;default:return (t-i+n.padding)/2}},x=t.isHorizontal();d=x?{x:t.left+b(l,u[0]),y:t.top+n.padding,line:0}:{x:t.left+n.padding,y:t.top+b(o,s[0]),line:0},H.rtl.overrideTextDirection(t.ctx,e.textDirection);var y=p+n.padding;H.each(t.legendItems,(function(e,i){var f=c.measureText(e.text).width,g=m+p/2+f,_=d.x,k=d.y;h.setWidth(t.minSize.width),x?i>0&&_+g+n.padding>t.left+t.minSize.width&&(k=d.y+=y,d.line++,_=d.x=t.left+b(l,u[d.line])):i>0&&k+y>t.top+t.minSize.height&&(_=d.x=_+t.columnWidths[d.line]+n.padding,d.line++,k=d.y=t.top+b(o,s[d.line]));var w=h.x(_);!function(t,e,i){if(!(isNaN(m)||m<=0)){c.save();var o=Ci(i.lineWidth,r.borderWidth);if(c.fillStyle=Ci(i.fillStyle,a),c.lineCap=Ci(i.lineCap,r.borderCapStyle),c.lineDashOffset=Ci(i.lineDashOffset,r.borderDashOffset),c.lineJoin=Ci(i.lineJoin,r.borderJoinStyle),c.lineWidth=o,c.strokeStyle=Ci(i.strokeStyle,a),c.setLineDash&&c.setLineDash(Ci(i.lineDash,r.borderDash)),n&&n.usePointStyle){var s=m*Math.SQRT2/2,l=h.xPlus(t,m/2),u=e+p/2;H.canvas.drawPoint(c,i.pointStyle,s,l,u,i.rotation);}else c.fillRect(h.leftForLtr(t,m),e,m,p),0!==o&&c.strokeRect(h.leftForLtr(t,m),e,m,p);c.restore();}}(w,k,e),v[i].left=h.leftForLtr(w,v[i].width),v[i].top=k,function(t,e,n,i){var a=p/2,r=h.xPlus(t,m+a),o=e+a;c.fillText(n.text,r,o),n.hidden&&(c.beginPath(),c.lineWidth=2,c.moveTo(r,o),c.lineTo(h.xPlus(r,i),o),c.stroke());}(w,k,e,f),x?d.x+=g+n.padding:d.y+=y;})),H.rtl.restoreTextDirection(t.ctx,e.textDirection);}},_getLegendItemAt:function(t,e){var n,i,a,r=this;if(t>=r.left&&t<=r.right&&e>=r.top&&e<=r.bottom)for(a=r.legendHitBoxes,n=0;n<a.length;++n)if(t>=(i=a[n]).left&&t<=i.left+i.width&&e>=i.top&&e<=i.top+i.height)return r.legendItems[n];return null},handleEvent:function(t){var e,n=this,i=n.options,a="mouseup"===t.type?"click":t.type;if("mousemove"===a){if(!i.onHover&&!i.onLeave)return}else {if("click"!==a)return;if(!i.onClick)return}e=n._getLegendItemAt(t.x,t.y),"click"===a?e&&i.onClick&&i.onClick.call(n,t.native,e):(i.onLeave&&e!==n._hoveredItem&&(n._hoveredItem&&i.onLeave.call(n,t.native,n._hoveredItem),n._hoveredItem=e),i.onHover&&e&&i.onHover.call(n,t.native,e));}});function Di(t,e){var n=new Ai({ctx:t.ctx,options:e,chart:t});pe.configure(t,n,e),pe.addBox(t,n),t.legend=n;}var Ti={id:"legend",_element:Ai,beforeInit:function(t){var e=t.options.legend;e&&Di(t,e);},beforeUpdate:function(t){var e=t.options.legend,n=t.legend;e?(H.mergeIf(e,N.global.legend),n?(pe.configure(t,n,e),n.options=e):Di(t,e)):n&&(pe.removeBox(t,n),delete t.legend);},afterEvent:function(t,e){var n=t.legend;n&&n.handleEvent(e);}},Ii=H.noop;N._set("global",{title:{display:!1,fontStyle:"bold",fullWidth:!0,padding:10,position:"top",text:"",weight:2e3}});var Fi=K.extend({initialize:function(t){H.extend(this,t),this.legendHitBoxes=[];},beforeUpdate:Ii,update:function(t,e,n){var i=this;return i.beforeUpdate(),i.maxWidth=t,i.maxHeight=e,i.margins=n,i.beforeSetDimensions(),i.setDimensions(),i.afterSetDimensions(),i.beforeBuildLabels(),i.buildLabels(),i.afterBuildLabels(),i.beforeFit(),i.fit(),i.afterFit(),i.afterUpdate(),i.minSize},afterUpdate:Ii,beforeSetDimensions:Ii,setDimensions:function(){var t=this;t.isHorizontal()?(t.width=t.maxWidth,t.left=0,t.right=t.width):(t.height=t.maxHeight,t.top=0,t.bottom=t.height),t.paddingLeft=0,t.paddingTop=0,t.paddingRight=0,t.paddingBottom=0,t.minSize={width:0,height:0};},afterSetDimensions:Ii,beforeBuildLabels:Ii,buildLabels:Ii,afterBuildLabels:Ii,beforeFit:Ii,fit:function(){var t,e=this,n=e.options,i=e.minSize={},a=e.isHorizontal();n.display?(t=(H.isArray(n.text)?n.text.length:1)*H.options._parseFont(n).lineHeight+2*n.padding,e.width=i.width=a?e.maxWidth:t,e.height=i.height=a?t:e.maxHeight):e.width=i.width=e.height=i.height=0;},afterFit:Ii,isHorizontal:function(){var t=this.options.position;return "top"===t||"bottom"===t},draw:function(){var t=this,e=t.ctx,n=t.options;if(n.display){var i,a,r,o=H.options._parseFont(n),s=o.lineHeight,l=s/2+n.padding,u=0,d=t.top,h=t.left,c=t.bottom,f=t.right;e.fillStyle=H.valueOrDefault(n.fontColor,N.global.defaultFontColor),e.font=o.string,t.isHorizontal()?(a=h+(f-h)/2,r=d+l,i=f-h):(a="left"===n.position?h+l:f-l,r=d+(c-d)/2,i=c-d,u=Math.PI*("left"===n.position?-.5:.5)),e.save(),e.translate(a,r),e.rotate(u),e.textAlign="center",e.textBaseline="middle";var g=n.text;if(H.isArray(g))for(var p=0,m=0;m<g.length;++m)e.fillText(g[m],0,p,i),p+=s;else e.fillText(g,0,0,i);e.restore();}}});function Oi(t,e){var n=new Fi({ctx:t.ctx,options:e,chart:t});pe.configure(t,n,e),pe.addBox(t,n),t.titleBlock=n;}var Li={},Ri=wi,zi=Ti,Ni={id:"title",_element:Fi,beforeInit:function(t){var e=t.options.title;e&&Oi(t,e);},beforeUpdate:function(t){var e=t.options.title,n=t.titleBlock;e?(H.mergeIf(e,N.global.title),n?(pe.configure(t,n,e),n.options=e):Oi(t,e)):n&&(pe.removeBox(t,n),delete t.titleBlock);}};for(var Bi in Li.filler=Ri,Li.legend=zi,Li.title=Ni,en.helpers=H,function(){function t(t,e,n){var i;return "string"==typeof t?(i=parseInt(t,10),-1!==t.indexOf("%")&&(i=i/100*e.parentNode[n])):i=t,i}function e(t){return null!=t&&"none"!==t}function n(n,i,a){var r=document.defaultView,o=H._getParentNode(n),s=r.getComputedStyle(n)[i],l=r.getComputedStyle(o)[i],u=e(s),d=e(l),h=Number.POSITIVE_INFINITY;return u||d?Math.min(u?t(s,n,a):h,d?t(l,o,a):h):"none"}H.where=function(t,e){if(H.isArray(t)&&Array.prototype.filter)return t.filter(e);var n=[];return H.each(t,(function(t){e(t)&&n.push(t);})),n},H.findIndex=Array.prototype.findIndex?function(t,e,n){return t.findIndex(e,n)}:function(t,e,n){n=void 0===n?t:n;for(var i=0,a=t.length;i<a;++i)if(e.call(n,t[i],i,t))return i;return -1},H.findNextWhere=function(t,e,n){H.isNullOrUndef(n)&&(n=-1);for(var i=n+1;i<t.length;i++){var a=t[i];if(e(a))return a}},H.findPreviousWhere=function(t,e,n){H.isNullOrUndef(n)&&(n=t.length);for(var i=n-1;i>=0;i--){var a=t[i];if(e(a))return a}},H.isNumber=function(t){return !isNaN(parseFloat(t))&&isFinite(t)},H.almostEquals=function(t,e,n){return Math.abs(t-e)<n},H.almostWhole=function(t,e){var n=Math.round(t);return n-e<=t&&n+e>=t},H.max=function(t){return t.reduce((function(t,e){return isNaN(e)?t:Math.max(t,e)}),Number.NEGATIVE_INFINITY)},H.min=function(t){return t.reduce((function(t,e){return isNaN(e)?t:Math.min(t,e)}),Number.POSITIVE_INFINITY)},H.sign=Math.sign?function(t){return Math.sign(t)}:function(t){return 0===(t=+t)||isNaN(t)?t:t>0?1:-1},H.toRadians=function(t){return t*(Math.PI/180)},H.toDegrees=function(t){return t*(180/Math.PI)},H._decimalPlaces=function(t){if(H.isFinite(t)){for(var e=1,n=0;Math.round(t*e)/e!==t;)e*=10,n++;return n}},H.getAngleFromPoint=function(t,e){var n=e.x-t.x,i=e.y-t.y,a=Math.sqrt(n*n+i*i),r=Math.atan2(i,n);return r<-.5*Math.PI&&(r+=2*Math.PI),{angle:r,distance:a}},H.distanceBetweenPoints=function(t,e){return Math.sqrt(Math.pow(e.x-t.x,2)+Math.pow(e.y-t.y,2))},H.aliasPixel=function(t){return t%2==0?0:.5},H._alignPixel=function(t,e,n){var i=t.currentDevicePixelRatio,a=n/2;return Math.round((e-a)*i)/i+a},H.splineCurve=function(t,e,n,i){var a=t.skip?e:t,r=e,o=n.skip?e:n,s=Math.sqrt(Math.pow(r.x-a.x,2)+Math.pow(r.y-a.y,2)),l=Math.sqrt(Math.pow(o.x-r.x,2)+Math.pow(o.y-r.y,2)),u=s/(s+l),d=l/(s+l),h=i*(u=isNaN(u)?0:u),c=i*(d=isNaN(d)?0:d);return {previous:{x:r.x-h*(o.x-a.x),y:r.y-h*(o.y-a.y)},next:{x:r.x+c*(o.x-a.x),y:r.y+c*(o.y-a.y)}}},H.EPSILON=Number.EPSILON||1e-14,H.splineCurveMonotone=function(t){var e,n,i,a,r,o,s,l,u,d=(t||[]).map((function(t){return {model:t._model,deltaK:0,mK:0}})),h=d.length;for(e=0;e<h;++e)if(!(i=d[e]).model.skip){if(n=e>0?d[e-1]:null,(a=e<h-1?d[e+1]:null)&&!a.model.skip){var c=a.model.x-i.model.x;i.deltaK=0!==c?(a.model.y-i.model.y)/c:0;}!n||n.model.skip?i.mK=i.deltaK:!a||a.model.skip?i.mK=n.deltaK:this.sign(n.deltaK)!==this.sign(i.deltaK)?i.mK=0:i.mK=(n.deltaK+i.deltaK)/2;}for(e=0;e<h-1;++e)i=d[e],a=d[e+1],i.model.skip||a.model.skip||(H.almostEquals(i.deltaK,0,this.EPSILON)?i.mK=a.mK=0:(r=i.mK/i.deltaK,o=a.mK/i.deltaK,(l=Math.pow(r,2)+Math.pow(o,2))<=9||(s=3/Math.sqrt(l),i.mK=r*s*i.deltaK,a.mK=o*s*i.deltaK)));for(e=0;e<h;++e)(i=d[e]).model.skip||(n=e>0?d[e-1]:null,a=e<h-1?d[e+1]:null,n&&!n.model.skip&&(u=(i.model.x-n.model.x)/3,i.model.controlPointPreviousX=i.model.x-u,i.model.controlPointPreviousY=i.model.y-u*i.mK),a&&!a.model.skip&&(u=(a.model.x-i.model.x)/3,i.model.controlPointNextX=i.model.x+u,i.model.controlPointNextY=i.model.y+u*i.mK));},H.nextItem=function(t,e,n){return n?e>=t.length-1?t[0]:t[e+1]:e>=t.length-1?t[t.length-1]:t[e+1]},H.previousItem=function(t,e,n){return n?e<=0?t[t.length-1]:t[e-1]:e<=0?t[0]:t[e-1]},H.niceNum=function(t,e){var n=Math.floor(H.log10(t)),i=t/Math.pow(10,n);return (e?i<1.5?1:i<3?2:i<7?5:10:i<=1?1:i<=2?2:i<=5?5:10)*Math.pow(10,n)},H.requestAnimFrame="undefined"==typeof window?function(t){t();}:window.requestAnimationFrame||window.webkitRequestAnimationFrame||window.mozRequestAnimationFrame||window.oRequestAnimationFrame||window.msRequestAnimationFrame||function(t){return window.setTimeout(t,1e3/60)},H.getRelativePosition=function(t,e){var n,i,a=t.originalEvent||t,r=t.target||t.srcElement,o=r.getBoundingClientRect(),s=a.touches;s&&s.length>0?(n=s[0].clientX,i=s[0].clientY):(n=a.clientX,i=a.clientY);var l=parseFloat(H.getStyle(r,"padding-left")),u=parseFloat(H.getStyle(r,"padding-top")),d=parseFloat(H.getStyle(r,"padding-right")),h=parseFloat(H.getStyle(r,"padding-bottom")),c=o.right-o.left-l-d,f=o.bottom-o.top-u-h;return {x:n=Math.round((n-o.left-l)/c*r.width/e.currentDevicePixelRatio),y:i=Math.round((i-o.top-u)/f*r.height/e.currentDevicePixelRatio)}},H.getConstraintWidth=function(t){return n(t,"max-width","clientWidth")},H.getConstraintHeight=function(t){return n(t,"max-height","clientHeight")},H._calculatePadding=function(t,e,n){return (e=H.getStyle(t,e)).indexOf("%")>-1?n*parseInt(e,10)/100:parseInt(e,10)},H._getParentNode=function(t){var e=t.parentNode;return e&&"[object ShadowRoot]"===e.toString()&&(e=e.host),e},H.getMaximumWidth=function(t){var e=H._getParentNode(t);if(!e)return t.clientWidth;var n=e.clientWidth,i=n-H._calculatePadding(e,"padding-left",n)-H._calculatePadding(e,"padding-right",n),a=H.getConstraintWidth(t);return isNaN(a)?i:Math.min(i,a)},H.getMaximumHeight=function(t){var e=H._getParentNode(t);if(!e)return t.clientHeight;var n=e.clientHeight,i=n-H._calculatePadding(e,"padding-top",n)-H._calculatePadding(e,"padding-bottom",n),a=H.getConstraintHeight(t);return isNaN(a)?i:Math.min(i,a)},H.getStyle=function(t,e){return t.currentStyle?t.currentStyle[e]:document.defaultView.getComputedStyle(t,null).getPropertyValue(e)},H.retinaScale=function(t,e){var n=t.currentDevicePixelRatio=e||"undefined"!=typeof window&&window.devicePixelRatio||1;if(1!==n){var i=t.canvas,a=t.height,r=t.width;i.height=a*n,i.width=r*n,t.ctx.scale(n,n),i.style.height||i.style.width||(i.style.height=a+"px",i.style.width=r+"px");}},H.fontString=function(t,e,n){return e+" "+t+"px "+n},H.longestText=function(t,e,n,i){var a=(i=i||{}).data=i.data||{},r=i.garbageCollect=i.garbageCollect||[];i.font!==e&&(a=i.data={},r=i.garbageCollect=[],i.font=e),t.font=e;var o,s,l,u,d,h=0,c=n.length;for(o=0;o<c;o++)if(null!=(u=n[o])&&!0!==H.isArray(u))h=H.measureText(t,a,r,h,u);else if(H.isArray(u))for(s=0,l=u.length;s<l;s++)null==(d=u[s])||H.isArray(d)||(h=H.measureText(t,a,r,h,d));var f=r.length/2;if(f>n.length){for(o=0;o<f;o++)delete a[r[o]];r.splice(0,f);}return h},H.measureText=function(t,e,n,i,a){var r=e[a];return r||(r=e[a]=t.measureText(a).width,n.push(a)),r>i&&(i=r),i},H.numberOfLabelLines=function(t){var e=1;return H.each(t,(function(t){H.isArray(t)&&t.length>e&&(e=t.length);})),e},H.color=_?function(t){return t instanceof CanvasGradient&&(t=N.global.defaultColor),_(t)}:function(t){return console.error("Color.js not found!"),t},H.getHoverColor=function(t){return t instanceof CanvasPattern||t instanceof CanvasGradient?t:H.color(t).saturate(.5).darken(.1).rgbString()};}(),en._adapters=rn,en.Animation=$,en.animationService=J,en.controllers=Jt,en.DatasetController=it,en.defaults=N,en.Element=K,en.elements=kt,en.Interaction=re,en.layouts=pe,en.platform=Oe,en.plugins=Le,en.Scale=yn,en.scaleService=Re,en.Ticks=on,en.Tooltip=Ye,en.helpers.each(fi,(function(t,e){en.scaleService.registerScaleType(e,t,t._defaults);})),Li)Li.hasOwnProperty(Bi)&&en.plugins.register(Li[Bi]);en.platform.initialize();var Ei=en;return "undefined"!=typeof window&&(window.Chart=en),en.Chart=en,en.Legend=Li.legend._element,en.Title=Li.title._element,en.pluginService=en.plugins,en.PluginBase=en.Element.extend({}),en.canvasHelpers=en.helpers.canvas,en.layoutService=en.layouts,en.LinearScaleBase=Cn,en.helpers.each(["Bar","Bubble","Doughnut","Line","PolarArea","Radar","Scatter"],(function(t){en[t]=function(e,n){return new en(e,en.helpers.merge(n||{},{type:t.charAt(0).toLowerCase()+t.slice(1)}))};})),Ei}));

window.commonJsFunctions = {
  setTitle: function (title) {
    document.title = title + " - Blogifier";
  },
  setPageTitle: function () {

  },
  showPrompt: function (message) {
    return prompt(message, 'Type anything here');
  },
  hideLoader: function (id) {
    var el = document.getElementById(id);
    el.style.display = 'none';
  },
  getFieldValue: function (field) {
    if (field.type === 'checkbox') {
      return document.getElementById(field.id).checked;
    }
    else {
      return document.getElementById(field.id).value;
    }
  },
  getTxtValue: function (txt) {
    return document.getElementById(txt).value;
  },
  getSrcValue: function (src) {
    return document.getElementById(src).src;
  },
  focusElement: function (id) {
    setTimeout(function () {
      const element = document.getElementById(id);
      element.focus();
    }, 500);
  },
  writeCookie: function (name, value, days) {

    var expires;
    if (days) {
      var date = new Date();
      date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
      expires = "; expires=" + date.toGMTString();
    }
    else {
      expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
  },
  setTooltip: function (args) {
    setTimeout(function () {
      let options = { "trigger": "hover", fallbackPlacements: ['bottom'] };
      let tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
      tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl, options)
      });
    }, 1000);
  },
  showModal: function (id) {
    document.getElementById(id).showModal();
  },
  startClock: function () {
    var clock = document.getElementById('clock');
    var clockDay = document.getElementById('clock-day');
    var clockMonth = document.getElementById('clock-month');

    function time() {
      var date = new Date();
      var hours = date.getHours();
      var minutes = date.getMinutes();
      hours = hours % 12;
      hours = hours ? hours : 12;
      minutes = minutes < 10 ? '0' + minutes : minutes;
      clock.textContent = hours + ':' + minutes;

      var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
      var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

      clockDay.textContent = days[date.getDay()];
      clockMonth.textContent = months[date.getMonth()] + ' ' + date.getDate();
    }
    time();
    setInterval(time, 60 * 1000);
  }
};

window.DataService = function () {
  let upload = function (url, obj, success, fail) {
    fetch(url, {
      method: 'POST',
      body: obj
    })
      .then(response => response.json())
      .then(data => success(data))
      .catch(error => fail(error));
  };
  return {
    upload: upload
  };
}();

window.fileManager = function (dataService) {
  let inputFile = document.getElementById('frmUploadFile');
  let frmUpload = document.getElementById('frmUpload');
  let callBack;
  let uplType;
  let postId;

  function uploadClick(uploadType, id) {
    uplType = uploadType;

    if (uploadType === 'AppCover') { callBack = appCoverCallback; }
    if (uploadType === 'PostImage') { callBack = insertImgCallback; }
    if (uploadType === 'PostCover') { callBack = postCoverCallback; postId = id; }
    if (uploadType === 'Avatar') { callBack = userAvatarCallback; }

    inputFile.click();
    return false;
  }

  function uploadSubmit() {
    let data = new FormData(frmUpload);
    let url = postId > 0 ? `api/storage/upload/${uplType}?postId=${postId}` : `api/storage/upload/${uplType}`;
    dataService.upload(url, data, callBack, fail);
  }

  function clipBoardUpload(ClipboardEvent) {
    let file = getPasteFile(ClipboardEvent);

    if (file === null)
      return

    uplType = "PostImage";
    let url = `api/storage/upload/${uplType}`;
    let data = new FormData();
    data.append('file', file, `${Date.now()}.png`);
    dataService.upload(url, data, (arg) => { insertImgCallback(arg); }, fail);
  }

  function getPasteFile(ClipboardEvent) {
    const data = ClipboardEvent.clipboardData;
    if (data == null) {
      return null;
    }

    if (data.items.length === 0) {
      return null;
    }

    const item = data.items[0];

    if (item.kind === 'string') {
      return null;
    }

    const file = item.getAsFile();

    if (file.type.indexOf("image") === -1)
      return null

    return file;
  }

  function appCoverCallback(data) {
    let defaultCover = document.getElementById('defaultCover');
    defaultCover.value = data;
  }

  function postCoverCallback(data) {
    let postCover = document.getElementById("postCover");
    postCover.src = data;
  }

  function userAvatarCallback(data) {
    let profilePicture = document.querySelectorAll('.profilePicture');
    for (i = 0; i < profilePicture.length; i++) {
      profilePicture[i].src = data;
    }
  }

  function insertImgCallback(data) {
    let cm = _editor.codemirror;
    let url = data;
    let output = url + '](' + url + ')';

    if (url.toLowerCase().match(/.(mp4|ogv|webm)$/i)) {
      let extv = 'mp4';
      if (url.toLowerCase().match(/.(ogv)$/i)) { extv = 'ogg'; }
      if (url.toLowerCase().match(/.(webm)$/i)) { extv = 'webm'; }
      output = `<video width="700" height="350" controls>\r\n\t\t<source src="${url}" type="video/${extv}">\r\n\t\tYour browser does not support the video tag.\r\n</video>`;
    }
    else if (url.toLowerCase().match(/.(mp3|ogg|wav)$/i)) {
      let exta = 'mp3';
      if (url.toLowerCase().match(/.(ogg)$/i)) { exta = 'ogg'; }
      if (url.toLowerCase().match(/.(wav)$/i)) { exta = 'wav'; }
      output = `<audio controls>\r\n\t\t<source src="${url}" type="audio/${exta}">\r\n\t\tYour browser does not support the audio tag.\r\n</audio>`;
    }
    else if (url.toLowerCase().match(/.(jpg|jpeg|png|gif)$/i)) {
      output = '\r\n![' + output;
    }
    else {
      output = '\r\n[' + output;
    }
    cm.getSelection();
    cm.replaceSelection(output);
  }

  function fail(data) {
    console.log(data.url);
  }

  return {
    uploadClick: uploadClick,
    uploadSubmit: uploadSubmit,
    clipBoardUpload: clipBoardUpload,
  };
}(DataService);
