(function () {
  'use strict';

  /*!
    * Bootstrap v5.1.3 (https://getbootstrap.com/)
    * Copyright 2011-2021 The Bootstrap Authors (https://github.com/twbs/bootstrap/graphs/contributors)
    * Licensed under MIT (https://github.com/twbs/bootstrap/blob/main/LICENSE)
    */
  !function(t,e){"object"==typeof exports&&"undefined"!=typeof module?module.exports=e():"function"==typeof define&&define.amd?define(e):(t="undefined"!=typeof globalThis?globalThis:t||self).bootstrap=e();}(undefined,(function(){const t="transitionend",e=t=>{let e=t.getAttribute("data-bs-target");if(!e||"#"===e){let i=t.getAttribute("href");if(!i||!i.includes("#")&&!i.startsWith("."))return null;i.includes("#")&&!i.startsWith("#")&&(i=`#${i.split("#")[1]}`),e=i&&"#"!==i?i.trim():null;}return e},i=t=>{const i=e(t);return i&&document.querySelector(i)?i:null},n=t=>{const i=e(t);return i?document.querySelector(i):null},s=e=>{e.dispatchEvent(new Event(t));},o=t=>!(!t||"object"!=typeof t)&&(void 0!==t.jquery&&(t=t[0]),void 0!==t.nodeType),r=t=>o(t)?t.jquery?t[0]:t:"string"==typeof t&&t.length>0?document.querySelector(t):null,a=(t,e,i)=>{Object.keys(i).forEach((n=>{const s=i[n],r=e[n],a=r&&o(r)?"element":null==(l=r)?`${l}`:{}.toString.call(l).match(/\s([a-z]+)/i)[1].toLowerCase();var l;if(!new RegExp(s).test(a))throw new TypeError(`${t.toUpperCase()}: Option "${n}" provided type "${a}" but expected type "${s}".`)}));},l=t=>!(!o(t)||0===t.getClientRects().length)&&"visible"===getComputedStyle(t).getPropertyValue("visibility"),c=t=>!t||t.nodeType!==Node.ELEMENT_NODE||!!t.classList.contains("disabled")||(void 0!==t.disabled?t.disabled:t.hasAttribute("disabled")&&"false"!==t.getAttribute("disabled")),h=t=>{if(!document.documentElement.attachShadow)return null;if("function"==typeof t.getRootNode){const e=t.getRootNode();return e instanceof ShadowRoot?e:null}return t instanceof ShadowRoot?t:t.parentNode?h(t.parentNode):null},d=()=>{},u=t=>{t.offsetHeight;},f=()=>{const{jQuery:t}=window;return t&&!document.body.hasAttribute("data-bs-no-jquery")?t:null},p=[],m=()=>"rtl"===document.documentElement.dir,g=t=>{var e;e=()=>{const e=f();if(e){const i=t.NAME,n=e.fn[i];e.fn[i]=t.jQueryInterface,e.fn[i].Constructor=t,e.fn[i].noConflict=()=>(e.fn[i]=n,t.jQueryInterface);}},"loading"===document.readyState?(p.length||document.addEventListener("DOMContentLoaded",(()=>{p.forEach((t=>t()));})),p.push(e)):e();},_=t=>{"function"==typeof t&&t();},b=(e,i,n=!0)=>{if(!n)return void _(e);const o=(t=>{if(!t)return 0;let{transitionDuration:e,transitionDelay:i}=window.getComputedStyle(t);const n=Number.parseFloat(e),s=Number.parseFloat(i);return n||s?(e=e.split(",")[0],i=i.split(",")[0],1e3*(Number.parseFloat(e)+Number.parseFloat(i))):0})(i)+5;let r=!1;const a=({target:n})=>{n===i&&(r=!0,i.removeEventListener(t,a),_(e));};i.addEventListener(t,a),setTimeout((()=>{r||s(i);}),o);},v=(t,e,i,n)=>{let s=t.indexOf(e);if(-1===s)return t[!i&&n?t.length-1:0];const o=t.length;return s+=i?1:-1,n&&(s=(s+o)%o),t[Math.max(0,Math.min(s,o-1))]},y=/[^.]*(?=\..*)\.|.*/,w=/\..*/,E=/::\d+$/,A={};let T=1;const O={mouseenter:"mouseover",mouseleave:"mouseout"},C=/^(mouseenter|mouseleave)/i,k=new Set(["click","dblclick","mouseup","mousedown","contextmenu","mousewheel","DOMMouseScroll","mouseover","mouseout","mousemove","selectstart","selectend","keydown","keypress","keyup","orientationchange","touchstart","touchmove","touchend","touchcancel","pointerdown","pointermove","pointerup","pointerleave","pointercancel","gesturestart","gesturechange","gestureend","focus","blur","change","reset","select","submit","focusin","focusout","load","unload","beforeunload","resize","move","DOMContentLoaded","readystatechange","error","abort","scroll"]);function L(t,e){return e&&`${e}::${T++}`||t.uidEvent||T++}function x(t){const e=L(t);return t.uidEvent=e,A[e]=A[e]||{},A[e]}function D(t,e,i=null){const n=Object.keys(t);for(let s=0,o=n.length;s<o;s++){const o=t[n[s]];if(o.originalHandler===e&&o.delegationSelector===i)return o}return null}function S(t,e,i){const n="string"==typeof e,s=n?i:e;let o=P(t);return k.has(o)||(o=t),[n,s,o]}function N(t,e,i,n,s){if("string"!=typeof e||!t)return;if(i||(i=n,n=null),C.test(e)){const t=t=>function(e){if(!e.relatedTarget||e.relatedTarget!==e.delegateTarget&&!e.delegateTarget.contains(e.relatedTarget))return t.call(this,e)};n?n=t(n):i=t(i);}const[o,r,a]=S(e,i,n),l=x(t),c=l[a]||(l[a]={}),h=D(c,r,o?i:null);if(h)return void(h.oneOff=h.oneOff&&s);const d=L(r,e.replace(y,"")),u=o?function(t,e,i){return function n(s){const o=t.querySelectorAll(e);for(let{target:r}=s;r&&r!==this;r=r.parentNode)for(let a=o.length;a--;)if(o[a]===r)return s.delegateTarget=r,n.oneOff&&j.off(t,s.type,e,i),i.apply(r,[s]);return null}}(t,i,n):function(t,e){return function i(n){return n.delegateTarget=t,i.oneOff&&j.off(t,n.type,e),e.apply(t,[n])}}(t,i);u.delegationSelector=o?i:null,u.originalHandler=r,u.oneOff=s,u.uidEvent=d,c[d]=u,t.addEventListener(a,u,o);}function I(t,e,i,n,s){const o=D(e[i],n,s);o&&(t.removeEventListener(i,o,Boolean(s)),delete e[i][o.uidEvent]);}function P(t){return t=t.replace(w,""),O[t]||t}const j={on(t,e,i,n){N(t,e,i,n,!1);},one(t,e,i,n){N(t,e,i,n,!0);},off(t,e,i,n){if("string"!=typeof e||!t)return;const[s,o,r]=S(e,i,n),a=r!==e,l=x(t),c=e.startsWith(".");if(void 0!==o){if(!l||!l[r])return;return void I(t,l,r,o,s?i:null)}c&&Object.keys(l).forEach((i=>{!function(t,e,i,n){const s=e[i]||{};Object.keys(s).forEach((o=>{if(o.includes(n)){const n=s[o];I(t,e,i,n.originalHandler,n.delegationSelector);}}));}(t,l,i,e.slice(1));}));const h=l[r]||{};Object.keys(h).forEach((i=>{const n=i.replace(E,"");if(!a||e.includes(n)){const e=h[i];I(t,l,r,e.originalHandler,e.delegationSelector);}}));},trigger(t,e,i){if("string"!=typeof e||!t)return null;const n=f(),s=P(e),o=e!==s,r=k.has(s);let a,l=!0,c=!0,h=!1,d=null;return o&&n&&(a=n.Event(e,i),n(t).trigger(a),l=!a.isPropagationStopped(),c=!a.isImmediatePropagationStopped(),h=a.isDefaultPrevented()),r?(d=document.createEvent("HTMLEvents"),d.initEvent(s,l,!0)):d=new CustomEvent(e,{bubbles:l,cancelable:!0}),void 0!==i&&Object.keys(i).forEach((t=>{Object.defineProperty(d,t,{get:()=>i[t]});})),h&&d.preventDefault(),c&&t.dispatchEvent(d),d.defaultPrevented&&void 0!==a&&a.preventDefault(),d}},M=new Map,H={set(t,e,i){M.has(t)||M.set(t,new Map);const n=M.get(t);n.has(e)||0===n.size?n.set(e,i):console.error(`Bootstrap doesn't allow more than one instance per element. Bound instance: ${Array.from(n.keys())[0]}.`);},get:(t,e)=>M.has(t)&&M.get(t).get(e)||null,remove(t,e){if(!M.has(t))return;const i=M.get(t);i.delete(e),0===i.size&&M.delete(t);}};class B{constructor(t){(t=r(t))&&(this._element=t,H.set(this._element,this.constructor.DATA_KEY,this));}dispose(){H.remove(this._element,this.constructor.DATA_KEY),j.off(this._element,this.constructor.EVENT_KEY),Object.getOwnPropertyNames(this).forEach((t=>{this[t]=null;}));}_queueCallback(t,e,i=!0){b(t,e,i);}static getInstance(t){return H.get(r(t),this.DATA_KEY)}static getOrCreateInstance(t,e={}){return this.getInstance(t)||new this(t,"object"==typeof e?e:null)}static get VERSION(){return "5.1.3"}static get NAME(){throw new Error('You have to implement the static method "NAME", for each component!')}static get DATA_KEY(){return `bs.${this.NAME}`}static get EVENT_KEY(){return `.${this.DATA_KEY}`}}const R=(t,e="hide")=>{const i=`click.dismiss${t.EVENT_KEY}`,s=t.NAME;j.on(document,i,`[data-bs-dismiss="${s}"]`,(function(i){if(["A","AREA"].includes(this.tagName)&&i.preventDefault(),c(this))return;const o=n(this)||this.closest(`.${s}`);t.getOrCreateInstance(o)[e]();}));};class W extends B{static get NAME(){return "alert"}close(){if(j.trigger(this._element,"close.bs.alert").defaultPrevented)return;this._element.classList.remove("show");const t=this._element.classList.contains("fade");this._queueCallback((()=>this._destroyElement()),this._element,t);}_destroyElement(){this._element.remove(),j.trigger(this._element,"closed.bs.alert"),this.dispose();}static jQueryInterface(t){return this.each((function(){const e=W.getOrCreateInstance(this);if("string"==typeof t){if(void 0===e[t]||t.startsWith("_")||"constructor"===t)throw new TypeError(`No method named "${t}"`);e[t](this);}}))}}R(W,"close"),g(W);const $='[data-bs-toggle="button"]';class z extends B{static get NAME(){return "button"}toggle(){this._element.setAttribute("aria-pressed",this._element.classList.toggle("active"));}static jQueryInterface(t){return this.each((function(){const e=z.getOrCreateInstance(this);"toggle"===t&&e[t]();}))}}function q(t){return "true"===t||"false"!==t&&(t===Number(t).toString()?Number(t):""===t||"null"===t?null:t)}function F(t){return t.replace(/[A-Z]/g,(t=>`-${t.toLowerCase()}`))}j.on(document,"click.bs.button.data-api",$,(t=>{t.preventDefault();const e=t.target.closest($);z.getOrCreateInstance(e).toggle();})),g(z);const U={setDataAttribute(t,e,i){t.setAttribute(`data-bs-${F(e)}`,i);},removeDataAttribute(t,e){t.removeAttribute(`data-bs-${F(e)}`);},getDataAttributes(t){if(!t)return {};const e={};return Object.keys(t.dataset).filter((t=>t.startsWith("bs"))).forEach((i=>{let n=i.replace(/^bs/,"");n=n.charAt(0).toLowerCase()+n.slice(1,n.length),e[n]=q(t.dataset[i]);})),e},getDataAttribute:(t,e)=>q(t.getAttribute(`data-bs-${F(e)}`)),offset(t){const e=t.getBoundingClientRect();return {top:e.top+window.pageYOffset,left:e.left+window.pageXOffset}},position:t=>({top:t.offsetTop,left:t.offsetLeft})},V={find:(t,e=document.documentElement)=>[].concat(...Element.prototype.querySelectorAll.call(e,t)),findOne:(t,e=document.documentElement)=>Element.prototype.querySelector.call(e,t),children:(t,e)=>[].concat(...t.children).filter((t=>t.matches(e))),parents(t,e){const i=[];let n=t.parentNode;for(;n&&n.nodeType===Node.ELEMENT_NODE&&3!==n.nodeType;)n.matches(e)&&i.push(n),n=n.parentNode;return i},prev(t,e){let i=t.previousElementSibling;for(;i;){if(i.matches(e))return [i];i=i.previousElementSibling;}return []},next(t,e){let i=t.nextElementSibling;for(;i;){if(i.matches(e))return [i];i=i.nextElementSibling;}return []},focusableChildren(t){const e=["a","button","input","textarea","select","details","[tabindex]",'[contenteditable="true"]'].map((t=>`${t}:not([tabindex^="-"])`)).join(", ");return this.find(e,t).filter((t=>!c(t)&&l(t)))}},K="carousel",X={interval:5e3,keyboard:!0,slide:!1,pause:"hover",wrap:!0,touch:!0},Y={interval:"(number|boolean)",keyboard:"boolean",slide:"(boolean|string)",pause:"(string|boolean)",wrap:"boolean",touch:"boolean"},Q="next",G="prev",Z="left",J="right",tt={ArrowLeft:J,ArrowRight:Z},et="slid.bs.carousel",it="active",nt=".active.carousel-item";class st extends B{constructor(t,e){super(t),this._items=null,this._interval=null,this._activeElement=null,this._isPaused=!1,this._isSliding=!1,this.touchTimeout=null,this.touchStartX=0,this.touchDeltaX=0,this._config=this._getConfig(e),this._indicatorsElement=V.findOne(".carousel-indicators",this._element),this._touchSupported="ontouchstart"in document.documentElement||navigator.maxTouchPoints>0,this._pointerEvent=Boolean(window.PointerEvent),this._addEventListeners();}static get Default(){return X}static get NAME(){return K}next(){this._slide(Q);}nextWhenVisible(){!document.hidden&&l(this._element)&&this.next();}prev(){this._slide(G);}pause(t){t||(this._isPaused=!0),V.findOne(".carousel-item-next, .carousel-item-prev",this._element)&&(s(this._element),this.cycle(!0)),clearInterval(this._interval),this._interval=null;}cycle(t){t||(this._isPaused=!1),this._interval&&(clearInterval(this._interval),this._interval=null),this._config&&this._config.interval&&!this._isPaused&&(this._updateInterval(),this._interval=setInterval((document.visibilityState?this.nextWhenVisible:this.next).bind(this),this._config.interval));}to(t){this._activeElement=V.findOne(nt,this._element);const e=this._getItemIndex(this._activeElement);if(t>this._items.length-1||t<0)return;if(this._isSliding)return void j.one(this._element,et,(()=>this.to(t)));if(e===t)return this.pause(),void this.cycle();const i=t>e?Q:G;this._slide(i,this._items[t]);}_getConfig(t){return t={...X,...U.getDataAttributes(this._element),..."object"==typeof t?t:{}},a(K,t,Y),t}_handleSwipe(){const t=Math.abs(this.touchDeltaX);if(t<=40)return;const e=t/this.touchDeltaX;this.touchDeltaX=0,e&&this._slide(e>0?J:Z);}_addEventListeners(){this._config.keyboard&&j.on(this._element,"keydown.bs.carousel",(t=>this._keydown(t))),"hover"===this._config.pause&&(j.on(this._element,"mouseenter.bs.carousel",(t=>this.pause(t))),j.on(this._element,"mouseleave.bs.carousel",(t=>this.cycle(t)))),this._config.touch&&this._touchSupported&&this._addTouchEventListeners();}_addTouchEventListeners(){const t=t=>this._pointerEvent&&("pen"===t.pointerType||"touch"===t.pointerType),e=e=>{t(e)?this.touchStartX=e.clientX:this._pointerEvent||(this.touchStartX=e.touches[0].clientX);},i=t=>{this.touchDeltaX=t.touches&&t.touches.length>1?0:t.touches[0].clientX-this.touchStartX;},n=e=>{t(e)&&(this.touchDeltaX=e.clientX-this.touchStartX),this._handleSwipe(),"hover"===this._config.pause&&(this.pause(),this.touchTimeout&&clearTimeout(this.touchTimeout),this.touchTimeout=setTimeout((t=>this.cycle(t)),500+this._config.interval));};V.find(".carousel-item img",this._element).forEach((t=>{j.on(t,"dragstart.bs.carousel",(t=>t.preventDefault()));})),this._pointerEvent?(j.on(this._element,"pointerdown.bs.carousel",(t=>e(t))),j.on(this._element,"pointerup.bs.carousel",(t=>n(t))),this._element.classList.add("pointer-event")):(j.on(this._element,"touchstart.bs.carousel",(t=>e(t))),j.on(this._element,"touchmove.bs.carousel",(t=>i(t))),j.on(this._element,"touchend.bs.carousel",(t=>n(t))));}_keydown(t){if(/input|textarea/i.test(t.target.tagName))return;const e=tt[t.key];e&&(t.preventDefault(),this._slide(e));}_getItemIndex(t){return this._items=t&&t.parentNode?V.find(".carousel-item",t.parentNode):[],this._items.indexOf(t)}_getItemByOrder(t,e){const i=t===Q;return v(this._items,e,i,this._config.wrap)}_triggerSlideEvent(t,e){const i=this._getItemIndex(t),n=this._getItemIndex(V.findOne(nt,this._element));return j.trigger(this._element,"slide.bs.carousel",{relatedTarget:t,direction:e,from:n,to:i})}_setActiveIndicatorElement(t){if(this._indicatorsElement){const e=V.findOne(".active",this._indicatorsElement);e.classList.remove(it),e.removeAttribute("aria-current");const i=V.find("[data-bs-target]",this._indicatorsElement);for(let e=0;e<i.length;e++)if(Number.parseInt(i[e].getAttribute("data-bs-slide-to"),10)===this._getItemIndex(t)){i[e].classList.add(it),i[e].setAttribute("aria-current","true");break}}}_updateInterval(){const t=this._activeElement||V.findOne(nt,this._element);if(!t)return;const e=Number.parseInt(t.getAttribute("data-bs-interval"),10);e?(this._config.defaultInterval=this._config.defaultInterval||this._config.interval,this._config.interval=e):this._config.interval=this._config.defaultInterval||this._config.interval;}_slide(t,e){const i=this._directionToOrder(t),n=V.findOne(nt,this._element),s=this._getItemIndex(n),o=e||this._getItemByOrder(i,n),r=this._getItemIndex(o),a=Boolean(this._interval),l=i===Q,c=l?"carousel-item-start":"carousel-item-end",h=l?"carousel-item-next":"carousel-item-prev",d=this._orderToDirection(i);if(o&&o.classList.contains(it))return void(this._isSliding=!1);if(this._isSliding)return;if(this._triggerSlideEvent(o,d).defaultPrevented)return;if(!n||!o)return;this._isSliding=!0,a&&this.pause(),this._setActiveIndicatorElement(o),this._activeElement=o;const f=()=>{j.trigger(this._element,et,{relatedTarget:o,direction:d,from:s,to:r});};if(this._element.classList.contains("slide")){o.classList.add(h),u(o),n.classList.add(c),o.classList.add(c);const t=()=>{o.classList.remove(c,h),o.classList.add(it),n.classList.remove(it,h,c),this._isSliding=!1,setTimeout(f,0);};this._queueCallback(t,n,!0);}else n.classList.remove(it),o.classList.add(it),this._isSliding=!1,f();a&&this.cycle();}_directionToOrder(t){return [J,Z].includes(t)?m()?t===Z?G:Q:t===Z?Q:G:t}_orderToDirection(t){return [Q,G].includes(t)?m()?t===G?Z:J:t===G?J:Z:t}static carouselInterface(t,e){const i=st.getOrCreateInstance(t,e);let{_config:n}=i;"object"==typeof e&&(n={...n,...e});const s="string"==typeof e?e:n.slide;if("number"==typeof e)i.to(e);else if("string"==typeof s){if(void 0===i[s])throw new TypeError(`No method named "${s}"`);i[s]();}else n.interval&&n.ride&&(i.pause(),i.cycle());}static jQueryInterface(t){return this.each((function(){st.carouselInterface(this,t);}))}static dataApiClickHandler(t){const e=n(this);if(!e||!e.classList.contains("carousel"))return;const i={...U.getDataAttributes(e),...U.getDataAttributes(this)},s=this.getAttribute("data-bs-slide-to");s&&(i.interval=!1),st.carouselInterface(e,i),s&&st.getInstance(e).to(s),t.preventDefault();}}j.on(document,"click.bs.carousel.data-api","[data-bs-slide], [data-bs-slide-to]",st.dataApiClickHandler),j.on(window,"load.bs.carousel.data-api",(()=>{const t=V.find('[data-bs-ride="carousel"]');for(let e=0,i=t.length;e<i;e++)st.carouselInterface(t[e],st.getInstance(t[e]));})),g(st);const ot="collapse",rt={toggle:!0,parent:null},at={toggle:"boolean",parent:"(null|element)"},lt="show",ct="collapse",ht="collapsing",dt="collapsed",ut=":scope .collapse .collapse",ft='[data-bs-toggle="collapse"]';class pt extends B{constructor(t,e){super(t),this._isTransitioning=!1,this._config=this._getConfig(e),this._triggerArray=[];const n=V.find(ft);for(let t=0,e=n.length;t<e;t++){const e=n[t],s=i(e),o=V.find(s).filter((t=>t===this._element));null!==s&&o.length&&(this._selector=s,this._triggerArray.push(e));}this._initializeChildren(),this._config.parent||this._addAriaAndCollapsedClass(this._triggerArray,this._isShown()),this._config.toggle&&this.toggle();}static get Default(){return rt}static get NAME(){return ot}toggle(){this._isShown()?this.hide():this.show();}show(){if(this._isTransitioning||this._isShown())return;let t,e=[];if(this._config.parent){const t=V.find(ut,this._config.parent);e=V.find(".collapse.show, .collapse.collapsing",this._config.parent).filter((e=>!t.includes(e)));}const i=V.findOne(this._selector);if(e.length){const n=e.find((t=>i!==t));if(t=n?pt.getInstance(n):null,t&&t._isTransitioning)return}if(j.trigger(this._element,"show.bs.collapse").defaultPrevented)return;e.forEach((e=>{i!==e&&pt.getOrCreateInstance(e,{toggle:!1}).hide(),t||H.set(e,"bs.collapse",null);}));const n=this._getDimension();this._element.classList.remove(ct),this._element.classList.add(ht),this._element.style[n]=0,this._addAriaAndCollapsedClass(this._triggerArray,!0),this._isTransitioning=!0;const s=`scroll${n[0].toUpperCase()+n.slice(1)}`;this._queueCallback((()=>{this._isTransitioning=!1,this._element.classList.remove(ht),this._element.classList.add(ct,lt),this._element.style[n]="",j.trigger(this._element,"shown.bs.collapse");}),this._element,!0),this._element.style[n]=`${this._element[s]}px`;}hide(){if(this._isTransitioning||!this._isShown())return;if(j.trigger(this._element,"hide.bs.collapse").defaultPrevented)return;const t=this._getDimension();this._element.style[t]=`${this._element.getBoundingClientRect()[t]}px`,u(this._element),this._element.classList.add(ht),this._element.classList.remove(ct,lt);const e=this._triggerArray.length;for(let t=0;t<e;t++){const e=this._triggerArray[t],i=n(e);i&&!this._isShown(i)&&this._addAriaAndCollapsedClass([e],!1);}this._isTransitioning=!0,this._element.style[t]="",this._queueCallback((()=>{this._isTransitioning=!1,this._element.classList.remove(ht),this._element.classList.add(ct),j.trigger(this._element,"hidden.bs.collapse");}),this._element,!0);}_isShown(t=this._element){return t.classList.contains(lt)}_getConfig(t){return (t={...rt,...U.getDataAttributes(this._element),...t}).toggle=Boolean(t.toggle),t.parent=r(t.parent),a(ot,t,at),t}_getDimension(){return this._element.classList.contains("collapse-horizontal")?"width":"height"}_initializeChildren(){if(!this._config.parent)return;const t=V.find(ut,this._config.parent);V.find(ft,this._config.parent).filter((e=>!t.includes(e))).forEach((t=>{const e=n(t);e&&this._addAriaAndCollapsedClass([t],this._isShown(e));}));}_addAriaAndCollapsedClass(t,e){t.length&&t.forEach((t=>{e?t.classList.remove(dt):t.classList.add(dt),t.setAttribute("aria-expanded",e);}));}static jQueryInterface(t){return this.each((function(){const e={};"string"==typeof t&&/show|hide/.test(t)&&(e.toggle=!1);const i=pt.getOrCreateInstance(this,e);if("string"==typeof t){if(void 0===i[t])throw new TypeError(`No method named "${t}"`);i[t]();}}))}}j.on(document,"click.bs.collapse.data-api",ft,(function(t){("A"===t.target.tagName||t.delegateTarget&&"A"===t.delegateTarget.tagName)&&t.preventDefault();const e=i(this);V.find(e).forEach((t=>{pt.getOrCreateInstance(t,{toggle:!1}).toggle();}));})),g(pt);var mt="top",gt="bottom",_t="right",bt="left",vt="auto",yt=[mt,gt,_t,bt],wt="start",Et="end",At="clippingParents",Tt="viewport",Ot="popper",Ct="reference",kt=yt.reduce((function(t,e){return t.concat([e+"-"+wt,e+"-"+Et])}),[]),Lt=[].concat(yt,[vt]).reduce((function(t,e){return t.concat([e,e+"-"+wt,e+"-"+Et])}),[]),xt="beforeRead",Dt="read",St="afterRead",Nt="beforeMain",It="main",Pt="afterMain",jt="beforeWrite",Mt="write",Ht="afterWrite",Bt=[xt,Dt,St,Nt,It,Pt,jt,Mt,Ht];function Rt(t){return t?(t.nodeName||"").toLowerCase():null}function Wt(t){if(null==t)return window;if("[object Window]"!==t.toString()){var e=t.ownerDocument;return e&&e.defaultView||window}return t}function $t(t){return t instanceof Wt(t).Element||t instanceof Element}function zt(t){return t instanceof Wt(t).HTMLElement||t instanceof HTMLElement}function qt(t){return "undefined"!=typeof ShadowRoot&&(t instanceof Wt(t).ShadowRoot||t instanceof ShadowRoot)}const Ft={name:"applyStyles",enabled:!0,phase:"write",fn:function(t){var e=t.state;Object.keys(e.elements).forEach((function(t){var i=e.styles[t]||{},n=e.attributes[t]||{},s=e.elements[t];zt(s)&&Rt(s)&&(Object.assign(s.style,i),Object.keys(n).forEach((function(t){var e=n[t];!1===e?s.removeAttribute(t):s.setAttribute(t,!0===e?"":e);})));}));},effect:function(t){var e=t.state,i={popper:{position:e.options.strategy,left:"0",top:"0",margin:"0"},arrow:{position:"absolute"},reference:{}};return Object.assign(e.elements.popper.style,i.popper),e.styles=i,e.elements.arrow&&Object.assign(e.elements.arrow.style,i.arrow),function(){Object.keys(e.elements).forEach((function(t){var n=e.elements[t],s=e.attributes[t]||{},o=Object.keys(e.styles.hasOwnProperty(t)?e.styles[t]:i[t]).reduce((function(t,e){return t[e]="",t}),{});zt(n)&&Rt(n)&&(Object.assign(n.style,o),Object.keys(s).forEach((function(t){n.removeAttribute(t);})));}));}},requires:["computeStyles"]};function Ut(t){return t.split("-")[0]}function Vt(t,e){var i=t.getBoundingClientRect();return {width:i.width/1,height:i.height/1,top:i.top/1,right:i.right/1,bottom:i.bottom/1,left:i.left/1,x:i.left/1,y:i.top/1}}function Kt(t){var e=Vt(t),i=t.offsetWidth,n=t.offsetHeight;return Math.abs(e.width-i)<=1&&(i=e.width),Math.abs(e.height-n)<=1&&(n=e.height),{x:t.offsetLeft,y:t.offsetTop,width:i,height:n}}function Xt(t,e){var i=e.getRootNode&&e.getRootNode();if(t.contains(e))return !0;if(i&&qt(i)){var n=e;do{if(n&&t.isSameNode(n))return !0;n=n.parentNode||n.host;}while(n)}return !1}function Yt(t){return Wt(t).getComputedStyle(t)}function Qt(t){return ["table","td","th"].indexOf(Rt(t))>=0}function Gt(t){return (($t(t)?t.ownerDocument:t.document)||window.document).documentElement}function Zt(t){return "html"===Rt(t)?t:t.assignedSlot||t.parentNode||(qt(t)?t.host:null)||Gt(t)}function Jt(t){return zt(t)&&"fixed"!==Yt(t).position?t.offsetParent:null}function te(t){for(var e=Wt(t),i=Jt(t);i&&Qt(i)&&"static"===Yt(i).position;)i=Jt(i);return i&&("html"===Rt(i)||"body"===Rt(i)&&"static"===Yt(i).position)?e:i||function(t){var e=-1!==navigator.userAgent.toLowerCase().indexOf("firefox");if(-1!==navigator.userAgent.indexOf("Trident")&&zt(t)&&"fixed"===Yt(t).position)return null;for(var i=Zt(t);zt(i)&&["html","body"].indexOf(Rt(i))<0;){var n=Yt(i);if("none"!==n.transform||"none"!==n.perspective||"paint"===n.contain||-1!==["transform","perspective"].indexOf(n.willChange)||e&&"filter"===n.willChange||e&&n.filter&&"none"!==n.filter)return i;i=i.parentNode;}return null}(t)||e}function ee(t){return ["top","bottom"].indexOf(t)>=0?"x":"y"}var ie=Math.max,ne=Math.min,se=Math.round;function oe(t,e,i){return ie(t,ne(e,i))}function re(t){return Object.assign({},{top:0,right:0,bottom:0,left:0},t)}function ae(t,e){return e.reduce((function(e,i){return e[i]=t,e}),{})}const le={name:"arrow",enabled:!0,phase:"main",fn:function(t){var e,i=t.state,n=t.name,s=t.options,o=i.elements.arrow,r=i.modifiersData.popperOffsets,a=Ut(i.placement),l=ee(a),c=[bt,_t].indexOf(a)>=0?"height":"width";if(o&&r){var h=function(t,e){return re("number"!=typeof(t="function"==typeof t?t(Object.assign({},e.rects,{placement:e.placement})):t)?t:ae(t,yt))}(s.padding,i),d=Kt(o),u="y"===l?mt:bt,f="y"===l?gt:_t,p=i.rects.reference[c]+i.rects.reference[l]-r[l]-i.rects.popper[c],m=r[l]-i.rects.reference[l],g=te(o),_=g?"y"===l?g.clientHeight||0:g.clientWidth||0:0,b=p/2-m/2,v=h[u],y=_-d[c]-h[f],w=_/2-d[c]/2+b,E=oe(v,w,y),A=l;i.modifiersData[n]=((e={})[A]=E,e.centerOffset=E-w,e);}},effect:function(t){var e=t.state,i=t.options.element,n=void 0===i?"[data-popper-arrow]":i;null!=n&&("string"!=typeof n||(n=e.elements.popper.querySelector(n)))&&Xt(e.elements.popper,n)&&(e.elements.arrow=n);},requires:["popperOffsets"],requiresIfExists:["preventOverflow"]};function ce(t){return t.split("-")[1]}var he={top:"auto",right:"auto",bottom:"auto",left:"auto"};function de(t){var e,i=t.popper,n=t.popperRect,s=t.placement,o=t.variation,r=t.offsets,a=t.position,l=t.gpuAcceleration,c=t.adaptive,h=t.roundOffsets,d=!0===h?function(t){var e=t.x,i=t.y,n=window.devicePixelRatio||1;return {x:se(se(e*n)/n)||0,y:se(se(i*n)/n)||0}}(r):"function"==typeof h?h(r):r,u=d.x,f=void 0===u?0:u,p=d.y,m=void 0===p?0:p,g=r.hasOwnProperty("x"),_=r.hasOwnProperty("y"),b=bt,v=mt,y=window;if(c){var w=te(i),E="clientHeight",A="clientWidth";w===Wt(i)&&"static"!==Yt(w=Gt(i)).position&&"absolute"===a&&(E="scrollHeight",A="scrollWidth"),w=w,s!==mt&&(s!==bt&&s!==_t||o!==Et)||(v=gt,m-=w[E]-n.height,m*=l?1:-1),s!==bt&&(s!==mt&&s!==gt||o!==Et)||(b=_t,f-=w[A]-n.width,f*=l?1:-1);}var T,O=Object.assign({position:a},c&&he);return l?Object.assign({},O,((T={})[v]=_?"0":"",T[b]=g?"0":"",T.transform=(y.devicePixelRatio||1)<=1?"translate("+f+"px, "+m+"px)":"translate3d("+f+"px, "+m+"px, 0)",T)):Object.assign({},O,((e={})[v]=_?m+"px":"",e[b]=g?f+"px":"",e.transform="",e))}const ue={name:"computeStyles",enabled:!0,phase:"beforeWrite",fn:function(t){var e=t.state,i=t.options,n=i.gpuAcceleration,s=void 0===n||n,o=i.adaptive,r=void 0===o||o,a=i.roundOffsets,l=void 0===a||a,c={placement:Ut(e.placement),variation:ce(e.placement),popper:e.elements.popper,popperRect:e.rects.popper,gpuAcceleration:s};null!=e.modifiersData.popperOffsets&&(e.styles.popper=Object.assign({},e.styles.popper,de(Object.assign({},c,{offsets:e.modifiersData.popperOffsets,position:e.options.strategy,adaptive:r,roundOffsets:l})))),null!=e.modifiersData.arrow&&(e.styles.arrow=Object.assign({},e.styles.arrow,de(Object.assign({},c,{offsets:e.modifiersData.arrow,position:"absolute",adaptive:!1,roundOffsets:l})))),e.attributes.popper=Object.assign({},e.attributes.popper,{"data-popper-placement":e.placement});},data:{}};var fe={passive:!0};const pe={name:"eventListeners",enabled:!0,phase:"write",fn:function(){},effect:function(t){var e=t.state,i=t.instance,n=t.options,s=n.scroll,o=void 0===s||s,r=n.resize,a=void 0===r||r,l=Wt(e.elements.popper),c=[].concat(e.scrollParents.reference,e.scrollParents.popper);return o&&c.forEach((function(t){t.addEventListener("scroll",i.update,fe);})),a&&l.addEventListener("resize",i.update,fe),function(){o&&c.forEach((function(t){t.removeEventListener("scroll",i.update,fe);})),a&&l.removeEventListener("resize",i.update,fe);}},data:{}};var me={left:"right",right:"left",bottom:"top",top:"bottom"};function ge(t){return t.replace(/left|right|bottom|top/g,(function(t){return me[t]}))}var _e={start:"end",end:"start"};function be(t){return t.replace(/start|end/g,(function(t){return _e[t]}))}function ve(t){var e=Wt(t);return {scrollLeft:e.pageXOffset,scrollTop:e.pageYOffset}}function ye(t){return Vt(Gt(t)).left+ve(t).scrollLeft}function we(t){var e=Yt(t),i=e.overflow,n=e.overflowX,s=e.overflowY;return /auto|scroll|overlay|hidden/.test(i+s+n)}function Ee(t){return ["html","body","#document"].indexOf(Rt(t))>=0?t.ownerDocument.body:zt(t)&&we(t)?t:Ee(Zt(t))}function Ae(t,e){var i;void 0===e&&(e=[]);var n=Ee(t),s=n===(null==(i=t.ownerDocument)?void 0:i.body),o=Wt(n),r=s?[o].concat(o.visualViewport||[],we(n)?n:[]):n,a=e.concat(r);return s?a:a.concat(Ae(Zt(r)))}function Te(t){return Object.assign({},t,{left:t.x,top:t.y,right:t.x+t.width,bottom:t.y+t.height})}function Oe(t,e){return e===Tt?Te(function(t){var e=Wt(t),i=Gt(t),n=e.visualViewport,s=i.clientWidth,o=i.clientHeight,r=0,a=0;return n&&(s=n.width,o=n.height,/^((?!chrome|android).)*safari/i.test(navigator.userAgent)||(r=n.offsetLeft,a=n.offsetTop)),{width:s,height:o,x:r+ye(t),y:a}}(t)):zt(e)?function(t){var e=Vt(t);return e.top=e.top+t.clientTop,e.left=e.left+t.clientLeft,e.bottom=e.top+t.clientHeight,e.right=e.left+t.clientWidth,e.width=t.clientWidth,e.height=t.clientHeight,e.x=e.left,e.y=e.top,e}(e):Te(function(t){var e,i=Gt(t),n=ve(t),s=null==(e=t.ownerDocument)?void 0:e.body,o=ie(i.scrollWidth,i.clientWidth,s?s.scrollWidth:0,s?s.clientWidth:0),r=ie(i.scrollHeight,i.clientHeight,s?s.scrollHeight:0,s?s.clientHeight:0),a=-n.scrollLeft+ye(t),l=-n.scrollTop;return "rtl"===Yt(s||i).direction&&(a+=ie(i.clientWidth,s?s.clientWidth:0)-o),{width:o,height:r,x:a,y:l}}(Gt(t)))}function Ce(t){var e,i=t.reference,n=t.element,s=t.placement,o=s?Ut(s):null,r=s?ce(s):null,a=i.x+i.width/2-n.width/2,l=i.y+i.height/2-n.height/2;switch(o){case mt:e={x:a,y:i.y-n.height};break;case gt:e={x:a,y:i.y+i.height};break;case _t:e={x:i.x+i.width,y:l};break;case bt:e={x:i.x-n.width,y:l};break;default:e={x:i.x,y:i.y};}var c=o?ee(o):null;if(null!=c){var h="y"===c?"height":"width";switch(r){case wt:e[c]=e[c]-(i[h]/2-n[h]/2);break;case Et:e[c]=e[c]+(i[h]/2-n[h]/2);}}return e}function ke(t,e){void 0===e&&(e={});var i=e,n=i.placement,s=void 0===n?t.placement:n,o=i.boundary,r=void 0===o?At:o,a=i.rootBoundary,l=void 0===a?Tt:a,c=i.elementContext,h=void 0===c?Ot:c,d=i.altBoundary,u=void 0!==d&&d,f=i.padding,p=void 0===f?0:f,m=re("number"!=typeof p?p:ae(p,yt)),g=h===Ot?Ct:Ot,_=t.rects.popper,b=t.elements[u?g:h],v=function(t,e,i){var n="clippingParents"===e?function(t){var e=Ae(Zt(t)),i=["absolute","fixed"].indexOf(Yt(t).position)>=0&&zt(t)?te(t):t;return $t(i)?e.filter((function(t){return $t(t)&&Xt(t,i)&&"body"!==Rt(t)})):[]}(t):[].concat(e),s=[].concat(n,[i]),o=s[0],r=s.reduce((function(e,i){var n=Oe(t,i);return e.top=ie(n.top,e.top),e.right=ne(n.right,e.right),e.bottom=ne(n.bottom,e.bottom),e.left=ie(n.left,e.left),e}),Oe(t,o));return r.width=r.right-r.left,r.height=r.bottom-r.top,r.x=r.left,r.y=r.top,r}($t(b)?b:b.contextElement||Gt(t.elements.popper),r,l),y=Vt(t.elements.reference),w=Ce({reference:y,element:_,strategy:"absolute",placement:s}),E=Te(Object.assign({},_,w)),A=h===Ot?E:y,T={top:v.top-A.top+m.top,bottom:A.bottom-v.bottom+m.bottom,left:v.left-A.left+m.left,right:A.right-v.right+m.right},O=t.modifiersData.offset;if(h===Ot&&O){var C=O[s];Object.keys(T).forEach((function(t){var e=[_t,gt].indexOf(t)>=0?1:-1,i=[mt,gt].indexOf(t)>=0?"y":"x";T[t]+=C[i]*e;}));}return T}function Le(t,e){void 0===e&&(e={});var i=e,n=i.placement,s=i.boundary,o=i.rootBoundary,r=i.padding,a=i.flipVariations,l=i.allowedAutoPlacements,c=void 0===l?Lt:l,h=ce(n),d=h?a?kt:kt.filter((function(t){return ce(t)===h})):yt,u=d.filter((function(t){return c.indexOf(t)>=0}));0===u.length&&(u=d);var f=u.reduce((function(e,i){return e[i]=ke(t,{placement:i,boundary:s,rootBoundary:o,padding:r})[Ut(i)],e}),{});return Object.keys(f).sort((function(t,e){return f[t]-f[e]}))}const xe={name:"flip",enabled:!0,phase:"main",fn:function(t){var e=t.state,i=t.options,n=t.name;if(!e.modifiersData[n]._skip){for(var s=i.mainAxis,o=void 0===s||s,r=i.altAxis,a=void 0===r||r,l=i.fallbackPlacements,c=i.padding,h=i.boundary,d=i.rootBoundary,u=i.altBoundary,f=i.flipVariations,p=void 0===f||f,m=i.allowedAutoPlacements,g=e.options.placement,_=Ut(g),b=l||(_!==g&&p?function(t){if(Ut(t)===vt)return [];var e=ge(t);return [be(t),e,be(e)]}(g):[ge(g)]),v=[g].concat(b).reduce((function(t,i){return t.concat(Ut(i)===vt?Le(e,{placement:i,boundary:h,rootBoundary:d,padding:c,flipVariations:p,allowedAutoPlacements:m}):i)}),[]),y=e.rects.reference,w=e.rects.popper,E=new Map,A=!0,T=v[0],O=0;O<v.length;O++){var C=v[O],k=Ut(C),L=ce(C)===wt,x=[mt,gt].indexOf(k)>=0,D=x?"width":"height",S=ke(e,{placement:C,boundary:h,rootBoundary:d,altBoundary:u,padding:c}),N=x?L?_t:bt:L?gt:mt;y[D]>w[D]&&(N=ge(N));var I=ge(N),P=[];if(o&&P.push(S[k]<=0),a&&P.push(S[N]<=0,S[I]<=0),P.every((function(t){return t}))){T=C,A=!1;break}E.set(C,P);}if(A)for(var j=function(t){var e=v.find((function(e){var i=E.get(e);if(i)return i.slice(0,t).every((function(t){return t}))}));if(e)return T=e,"break"},M=p?3:1;M>0&&"break"!==j(M);M--);e.placement!==T&&(e.modifiersData[n]._skip=!0,e.placement=T,e.reset=!0);}},requiresIfExists:["offset"],data:{_skip:!1}};function De(t,e,i){return void 0===i&&(i={x:0,y:0}),{top:t.top-e.height-i.y,right:t.right-e.width+i.x,bottom:t.bottom-e.height+i.y,left:t.left-e.width-i.x}}function Se(t){return [mt,_t,gt,bt].some((function(e){return t[e]>=0}))}const Ne={name:"hide",enabled:!0,phase:"main",requiresIfExists:["preventOverflow"],fn:function(t){var e=t.state,i=t.name,n=e.rects.reference,s=e.rects.popper,o=e.modifiersData.preventOverflow,r=ke(e,{elementContext:"reference"}),a=ke(e,{altBoundary:!0}),l=De(r,n),c=De(a,s,o),h=Se(l),d=Se(c);e.modifiersData[i]={referenceClippingOffsets:l,popperEscapeOffsets:c,isReferenceHidden:h,hasPopperEscaped:d},e.attributes.popper=Object.assign({},e.attributes.popper,{"data-popper-reference-hidden":h,"data-popper-escaped":d});}},Ie={name:"offset",enabled:!0,phase:"main",requires:["popperOffsets"],fn:function(t){var e=t.state,i=t.options,n=t.name,s=i.offset,o=void 0===s?[0,0]:s,r=Lt.reduce((function(t,i){return t[i]=function(t,e,i){var n=Ut(t),s=[bt,mt].indexOf(n)>=0?-1:1,o="function"==typeof i?i(Object.assign({},e,{placement:t})):i,r=o[0],a=o[1];return r=r||0,a=(a||0)*s,[bt,_t].indexOf(n)>=0?{x:a,y:r}:{x:r,y:a}}(i,e.rects,o),t}),{}),a=r[e.placement],l=a.x,c=a.y;null!=e.modifiersData.popperOffsets&&(e.modifiersData.popperOffsets.x+=l,e.modifiersData.popperOffsets.y+=c),e.modifiersData[n]=r;}},Pe={name:"popperOffsets",enabled:!0,phase:"read",fn:function(t){var e=t.state,i=t.name;e.modifiersData[i]=Ce({reference:e.rects.reference,element:e.rects.popper,strategy:"absolute",placement:e.placement});},data:{}},je={name:"preventOverflow",enabled:!0,phase:"main",fn:function(t){var e=t.state,i=t.options,n=t.name,s=i.mainAxis,o=void 0===s||s,r=i.altAxis,a=void 0!==r&&r,l=i.boundary,c=i.rootBoundary,h=i.altBoundary,d=i.padding,u=i.tether,f=void 0===u||u,p=i.tetherOffset,m=void 0===p?0:p,g=ke(e,{boundary:l,rootBoundary:c,padding:d,altBoundary:h}),_=Ut(e.placement),b=ce(e.placement),v=!b,y=ee(_),w="x"===y?"y":"x",E=e.modifiersData.popperOffsets,A=e.rects.reference,T=e.rects.popper,O="function"==typeof m?m(Object.assign({},e.rects,{placement:e.placement})):m,C={x:0,y:0};if(E){if(o||a){var k="y"===y?mt:bt,L="y"===y?gt:_t,x="y"===y?"height":"width",D=E[y],S=E[y]+g[k],N=E[y]-g[L],I=f?-T[x]/2:0,P=b===wt?A[x]:T[x],j=b===wt?-T[x]:-A[x],M=e.elements.arrow,H=f&&M?Kt(M):{width:0,height:0},B=e.modifiersData["arrow#persistent"]?e.modifiersData["arrow#persistent"].padding:{top:0,right:0,bottom:0,left:0},R=B[k],W=B[L],$=oe(0,A[x],H[x]),z=v?A[x]/2-I-$-R-O:P-$-R-O,q=v?-A[x]/2+I+$+W+O:j+$+W+O,F=e.elements.arrow&&te(e.elements.arrow),U=F?"y"===y?F.clientTop||0:F.clientLeft||0:0,V=e.modifiersData.offset?e.modifiersData.offset[e.placement][y]:0,K=E[y]+z-V-U,X=E[y]+q-V;if(o){var Y=oe(f?ne(S,K):S,D,f?ie(N,X):N);E[y]=Y,C[y]=Y-D;}if(a){var Q="x"===y?mt:bt,G="x"===y?gt:_t,Z=E[w],J=Z+g[Q],tt=Z-g[G],et=oe(f?ne(J,K):J,Z,f?ie(tt,X):tt);E[w]=et,C[w]=et-Z;}}e.modifiersData[n]=C;}},requiresIfExists:["offset"]};function Me(t,e,i){void 0===i&&(i=!1);var n=zt(e);zt(e)&&function(t){var e=t.getBoundingClientRect();e.width,t.offsetWidth,e.height,t.offsetHeight;}(e);var s,o,r=Gt(e),a=Vt(t),l={scrollLeft:0,scrollTop:0},c={x:0,y:0};return (n||!n&&!i)&&(("body"!==Rt(e)||we(r))&&(l=(s=e)!==Wt(s)&&zt(s)?{scrollLeft:(o=s).scrollLeft,scrollTop:o.scrollTop}:ve(s)),zt(e)?((c=Vt(e)).x+=e.clientLeft,c.y+=e.clientTop):r&&(c.x=ye(r))),{x:a.left+l.scrollLeft-c.x,y:a.top+l.scrollTop-c.y,width:a.width,height:a.height}}function He(t){var e=new Map,i=new Set,n=[];function s(t){i.add(t.name),[].concat(t.requires||[],t.requiresIfExists||[]).forEach((function(t){if(!i.has(t)){var n=e.get(t);n&&s(n);}})),n.push(t);}return t.forEach((function(t){e.set(t.name,t);})),t.forEach((function(t){i.has(t.name)||s(t);})),n}var Be={placement:"bottom",modifiers:[],strategy:"absolute"};function Re(){for(var t=arguments.length,e=new Array(t),i=0;i<t;i++)e[i]=arguments[i];return !e.some((function(t){return !(t&&"function"==typeof t.getBoundingClientRect)}))}function We(t){void 0===t&&(t={});var e=t,i=e.defaultModifiers,n=void 0===i?[]:i,s=e.defaultOptions,o=void 0===s?Be:s;return function(t,e,i){void 0===i&&(i=o);var s,r,a={placement:"bottom",orderedModifiers:[],options:Object.assign({},Be,o),modifiersData:{},elements:{reference:t,popper:e},attributes:{},styles:{}},l=[],c=!1,h={state:a,setOptions:function(i){var s="function"==typeof i?i(a.options):i;d(),a.options=Object.assign({},o,a.options,s),a.scrollParents={reference:$t(t)?Ae(t):t.contextElement?Ae(t.contextElement):[],popper:Ae(e)};var r,c,u=function(t){var e=He(t);return Bt.reduce((function(t,i){return t.concat(e.filter((function(t){return t.phase===i})))}),[])}((r=[].concat(n,a.options.modifiers),c=r.reduce((function(t,e){var i=t[e.name];return t[e.name]=i?Object.assign({},i,e,{options:Object.assign({},i.options,e.options),data:Object.assign({},i.data,e.data)}):e,t}),{}),Object.keys(c).map((function(t){return c[t]}))));return a.orderedModifiers=u.filter((function(t){return t.enabled})),a.orderedModifiers.forEach((function(t){var e=t.name,i=t.options,n=void 0===i?{}:i,s=t.effect;if("function"==typeof s){var o=s({state:a,name:e,instance:h,options:n});l.push(o||function(){});}})),h.update()},forceUpdate:function(){if(!c){var t=a.elements,e=t.reference,i=t.popper;if(Re(e,i)){a.rects={reference:Me(e,te(i),"fixed"===a.options.strategy),popper:Kt(i)},a.reset=!1,a.placement=a.options.placement,a.orderedModifiers.forEach((function(t){return a.modifiersData[t.name]=Object.assign({},t.data)}));for(var n=0;n<a.orderedModifiers.length;n++)if(!0!==a.reset){var s=a.orderedModifiers[n],o=s.fn,r=s.options,l=void 0===r?{}:r,d=s.name;"function"==typeof o&&(a=o({state:a,options:l,name:d,instance:h})||a);}else a.reset=!1,n=-1;}}},update:(s=function(){return new Promise((function(t){h.forceUpdate(),t(a);}))},function(){return r||(r=new Promise((function(t){Promise.resolve().then((function(){r=void 0,t(s());}));}))),r}),destroy:function(){d(),c=!0;}};if(!Re(t,e))return h;function d(){l.forEach((function(t){return t()})),l=[];}return h.setOptions(i).then((function(t){!c&&i.onFirstUpdate&&i.onFirstUpdate(t);})),h}}var $e=We(),ze=We({defaultModifiers:[pe,Pe,ue,Ft]}),qe=We({defaultModifiers:[pe,Pe,ue,Ft,Ie,xe,je,le,Ne]});const Fe=Object.freeze({__proto__:null,popperGenerator:We,detectOverflow:ke,createPopperBase:$e,createPopper:qe,createPopperLite:ze,top:mt,bottom:gt,right:_t,left:bt,auto:vt,basePlacements:yt,start:wt,end:Et,clippingParents:At,viewport:Tt,popper:Ot,reference:Ct,variationPlacements:kt,placements:Lt,beforeRead:xt,read:Dt,afterRead:St,beforeMain:Nt,main:It,afterMain:Pt,beforeWrite:jt,write:Mt,afterWrite:Ht,modifierPhases:Bt,applyStyles:Ft,arrow:le,computeStyles:ue,eventListeners:pe,flip:xe,hide:Ne,offset:Ie,popperOffsets:Pe,preventOverflow:je}),Ue="dropdown",Ve="Escape",Ke="Space",Xe="ArrowUp",Ye="ArrowDown",Qe=new RegExp("ArrowUp|ArrowDown|Escape"),Ge="click.bs.dropdown.data-api",Ze="keydown.bs.dropdown.data-api",Je="show",ti='[data-bs-toggle="dropdown"]',ei=".dropdown-menu",ii=m()?"top-end":"top-start",ni=m()?"top-start":"top-end",si=m()?"bottom-end":"bottom-start",oi=m()?"bottom-start":"bottom-end",ri=m()?"left-start":"right-start",ai=m()?"right-start":"left-start",li={offset:[0,2],boundary:"clippingParents",reference:"toggle",display:"dynamic",popperConfig:null,autoClose:!0},ci={offset:"(array|string|function)",boundary:"(string|element)",reference:"(string|element|object)",display:"string",popperConfig:"(null|object|function)",autoClose:"(boolean|string)"};class hi extends B{constructor(t,e){super(t),this._popper=null,this._config=this._getConfig(e),this._menu=this._getMenuElement(),this._inNavbar=this._detectNavbar();}static get Default(){return li}static get DefaultType(){return ci}static get NAME(){return Ue}toggle(){return this._isShown()?this.hide():this.show()}show(){if(c(this._element)||this._isShown(this._menu))return;const t={relatedTarget:this._element};if(j.trigger(this._element,"show.bs.dropdown",t).defaultPrevented)return;const e=hi.getParentFromElement(this._element);this._inNavbar?U.setDataAttribute(this._menu,"popper","none"):this._createPopper(e),"ontouchstart"in document.documentElement&&!e.closest(".navbar-nav")&&[].concat(...document.body.children).forEach((t=>j.on(t,"mouseover",d))),this._element.focus(),this._element.setAttribute("aria-expanded",!0),this._menu.classList.add(Je),this._element.classList.add(Je),j.trigger(this._element,"shown.bs.dropdown",t);}hide(){if(c(this._element)||!this._isShown(this._menu))return;const t={relatedTarget:this._element};this._completeHide(t);}dispose(){this._popper&&this._popper.destroy(),super.dispose();}update(){this._inNavbar=this._detectNavbar(),this._popper&&this._popper.update();}_completeHide(t){j.trigger(this._element,"hide.bs.dropdown",t).defaultPrevented||("ontouchstart"in document.documentElement&&[].concat(...document.body.children).forEach((t=>j.off(t,"mouseover",d))),this._popper&&this._popper.destroy(),this._menu.classList.remove(Je),this._element.classList.remove(Je),this._element.setAttribute("aria-expanded","false"),U.removeDataAttribute(this._menu,"popper"),j.trigger(this._element,"hidden.bs.dropdown",t));}_getConfig(t){if(t={...this.constructor.Default,...U.getDataAttributes(this._element),...t},a(Ue,t,this.constructor.DefaultType),"object"==typeof t.reference&&!o(t.reference)&&"function"!=typeof t.reference.getBoundingClientRect)throw new TypeError(`${Ue.toUpperCase()}: Option "reference" provided type "object" without a required "getBoundingClientRect" method.`);return t}_createPopper(t){if(void 0===Fe)throw new TypeError("Bootstrap's dropdowns require Popper (https://popper.js.org)");let e=this._element;"parent"===this._config.reference?e=t:o(this._config.reference)?e=r(this._config.reference):"object"==typeof this._config.reference&&(e=this._config.reference);const i=this._getPopperConfig(),n=i.modifiers.find((t=>"applyStyles"===t.name&&!1===t.enabled));this._popper=qe(e,this._menu,i),n&&U.setDataAttribute(this._menu,"popper","static");}_isShown(t=this._element){return t.classList.contains(Je)}_getMenuElement(){return V.next(this._element,ei)[0]}_getPlacement(){const t=this._element.parentNode;if(t.classList.contains("dropend"))return ri;if(t.classList.contains("dropstart"))return ai;const e="end"===getComputedStyle(this._menu).getPropertyValue("--bs-position").trim();return t.classList.contains("dropup")?e?ni:ii:e?oi:si}_detectNavbar(){return null!==this._element.closest(".navbar")}_getOffset(){const{offset:t}=this._config;return "string"==typeof t?t.split(",").map((t=>Number.parseInt(t,10))):"function"==typeof t?e=>t(e,this._element):t}_getPopperConfig(){const t={placement:this._getPlacement(),modifiers:[{name:"preventOverflow",options:{boundary:this._config.boundary}},{name:"offset",options:{offset:this._getOffset()}}]};return "static"===this._config.display&&(t.modifiers=[{name:"applyStyles",enabled:!1}]),{...t,..."function"==typeof this._config.popperConfig?this._config.popperConfig(t):this._config.popperConfig}}_selectMenuItem({key:t,target:e}){const i=V.find(".dropdown-menu .dropdown-item:not(.disabled):not(:disabled)",this._menu).filter(l);i.length&&v(i,e,t===Ye,!i.includes(e)).focus();}static jQueryInterface(t){return this.each((function(){const e=hi.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===e[t])throw new TypeError(`No method named "${t}"`);e[t]();}}))}static clearMenus(t){if(t&&(2===t.button||"keyup"===t.type&&"Tab"!==t.key))return;const e=V.find(ti);for(let i=0,n=e.length;i<n;i++){const n=hi.getInstance(e[i]);if(!n||!1===n._config.autoClose)continue;if(!n._isShown())continue;const s={relatedTarget:n._element};if(t){const e=t.composedPath(),i=e.includes(n._menu);if(e.includes(n._element)||"inside"===n._config.autoClose&&!i||"outside"===n._config.autoClose&&i)continue;if(n._menu.contains(t.target)&&("keyup"===t.type&&"Tab"===t.key||/input|select|option|textarea|form/i.test(t.target.tagName)))continue;"click"===t.type&&(s.clickEvent=t);}n._completeHide(s);}}static getParentFromElement(t){return n(t)||t.parentNode}static dataApiKeydownHandler(t){if(/input|textarea/i.test(t.target.tagName)?t.key===Ke||t.key!==Ve&&(t.key!==Ye&&t.key!==Xe||t.target.closest(ei)):!Qe.test(t.key))return;const e=this.classList.contains(Je);if(!e&&t.key===Ve)return;if(t.preventDefault(),t.stopPropagation(),c(this))return;const i=this.matches(ti)?this:V.prev(this,ti)[0],n=hi.getOrCreateInstance(i);if(t.key!==Ve)return t.key===Xe||t.key===Ye?(e||n.show(),void n._selectMenuItem(t)):void(e&&t.key!==Ke||hi.clearMenus());n.hide();}}j.on(document,Ze,ti,hi.dataApiKeydownHandler),j.on(document,Ze,ei,hi.dataApiKeydownHandler),j.on(document,Ge,hi.clearMenus),j.on(document,"keyup.bs.dropdown.data-api",hi.clearMenus),j.on(document,Ge,ti,(function(t){t.preventDefault(),hi.getOrCreateInstance(this).toggle();})),g(hi);const di=".fixed-top, .fixed-bottom, .is-fixed, .sticky-top",ui=".sticky-top";class fi{constructor(){this._element=document.body;}getWidth(){const t=document.documentElement.clientWidth;return Math.abs(window.innerWidth-t)}hide(){const t=this.getWidth();this._disableOverFlow(),this._setElementAttributes(this._element,"paddingRight",(e=>e+t)),this._setElementAttributes(di,"paddingRight",(e=>e+t)),this._setElementAttributes(ui,"marginRight",(e=>e-t));}_disableOverFlow(){this._saveInitialAttribute(this._element,"overflow"),this._element.style.overflow="hidden";}_setElementAttributes(t,e,i){const n=this.getWidth();this._applyManipulationCallback(t,(t=>{if(t!==this._element&&window.innerWidth>t.clientWidth+n)return;this._saveInitialAttribute(t,e);const s=window.getComputedStyle(t)[e];t.style[e]=`${i(Number.parseFloat(s))}px`;}));}reset(){this._resetElementAttributes(this._element,"overflow"),this._resetElementAttributes(this._element,"paddingRight"),this._resetElementAttributes(di,"paddingRight"),this._resetElementAttributes(ui,"marginRight");}_saveInitialAttribute(t,e){const i=t.style[e];i&&U.setDataAttribute(t,e,i);}_resetElementAttributes(t,e){this._applyManipulationCallback(t,(t=>{const i=U.getDataAttribute(t,e);void 0===i?t.style.removeProperty(e):(U.removeDataAttribute(t,e),t.style[e]=i);}));}_applyManipulationCallback(t,e){o(t)?e(t):V.find(t,this._element).forEach(e);}isOverflowing(){return this.getWidth()>0}}const pi={className:"modal-backdrop",isVisible:!0,isAnimated:!1,rootElement:"body",clickCallback:null},mi={className:"string",isVisible:"boolean",isAnimated:"boolean",rootElement:"(element|string)",clickCallback:"(function|null)"},gi="show",_i="mousedown.bs.backdrop";class bi{constructor(t){this._config=this._getConfig(t),this._isAppended=!1,this._element=null;}show(t){this._config.isVisible?(this._append(),this._config.isAnimated&&u(this._getElement()),this._getElement().classList.add(gi),this._emulateAnimation((()=>{_(t);}))):_(t);}hide(t){this._config.isVisible?(this._getElement().classList.remove(gi),this._emulateAnimation((()=>{this.dispose(),_(t);}))):_(t);}_getElement(){if(!this._element){const t=document.createElement("div");t.className=this._config.className,this._config.isAnimated&&t.classList.add("fade"),this._element=t;}return this._element}_getConfig(t){return (t={...pi,..."object"==typeof t?t:{}}).rootElement=r(t.rootElement),a("backdrop",t,mi),t}_append(){this._isAppended||(this._config.rootElement.append(this._getElement()),j.on(this._getElement(),_i,(()=>{_(this._config.clickCallback);})),this._isAppended=!0);}dispose(){this._isAppended&&(j.off(this._element,_i),this._element.remove(),this._isAppended=!1);}_emulateAnimation(t){b(t,this._getElement(),this._config.isAnimated);}}const vi={trapElement:null,autofocus:!0},yi={trapElement:"element",autofocus:"boolean"},wi=".bs.focustrap",Ei="backward";class Ai{constructor(t){this._config=this._getConfig(t),this._isActive=!1,this._lastTabNavDirection=null;}activate(){const{trapElement:t,autofocus:e}=this._config;this._isActive||(e&&t.focus(),j.off(document,wi),j.on(document,"focusin.bs.focustrap",(t=>this._handleFocusin(t))),j.on(document,"keydown.tab.bs.focustrap",(t=>this._handleKeydown(t))),this._isActive=!0);}deactivate(){this._isActive&&(this._isActive=!1,j.off(document,wi));}_handleFocusin(t){const{target:e}=t,{trapElement:i}=this._config;if(e===document||e===i||i.contains(e))return;const n=V.focusableChildren(i);0===n.length?i.focus():this._lastTabNavDirection===Ei?n[n.length-1].focus():n[0].focus();}_handleKeydown(t){"Tab"===t.key&&(this._lastTabNavDirection=t.shiftKey?Ei:"forward");}_getConfig(t){return t={...vi,..."object"==typeof t?t:{}},a("focustrap",t,yi),t}}const Ti="modal",Oi="Escape",Ci={backdrop:!0,keyboard:!0,focus:!0},ki={backdrop:"(boolean|string)",keyboard:"boolean",focus:"boolean"},Li="hidden.bs.modal",xi="show.bs.modal",Di="resize.bs.modal",Si="click.dismiss.bs.modal",Ni="keydown.dismiss.bs.modal",Ii="mousedown.dismiss.bs.modal",Pi="modal-open",ji="show",Mi="modal-static";class Hi extends B{constructor(t,e){super(t),this._config=this._getConfig(e),this._dialog=V.findOne(".modal-dialog",this._element),this._backdrop=this._initializeBackDrop(),this._focustrap=this._initializeFocusTrap(),this._isShown=!1,this._ignoreBackdropClick=!1,this._isTransitioning=!1,this._scrollBar=new fi;}static get Default(){return Ci}static get NAME(){return Ti}toggle(t){return this._isShown?this.hide():this.show(t)}show(t){this._isShown||this._isTransitioning||j.trigger(this._element,xi,{relatedTarget:t}).defaultPrevented||(this._isShown=!0,this._isAnimated()&&(this._isTransitioning=!0),this._scrollBar.hide(),document.body.classList.add(Pi),this._adjustDialog(),this._setEscapeEvent(),this._setResizeEvent(),j.on(this._dialog,Ii,(()=>{j.one(this._element,"mouseup.dismiss.bs.modal",(t=>{t.target===this._element&&(this._ignoreBackdropClick=!0);}));})),this._showBackdrop((()=>this._showElement(t))));}hide(){if(!this._isShown||this._isTransitioning)return;if(j.trigger(this._element,"hide.bs.modal").defaultPrevented)return;this._isShown=!1;const t=this._isAnimated();t&&(this._isTransitioning=!0),this._setEscapeEvent(),this._setResizeEvent(),this._focustrap.deactivate(),this._element.classList.remove(ji),j.off(this._element,Si),j.off(this._dialog,Ii),this._queueCallback((()=>this._hideModal()),this._element,t);}dispose(){[window,this._dialog].forEach((t=>j.off(t,".bs.modal"))),this._backdrop.dispose(),this._focustrap.deactivate(),super.dispose();}handleUpdate(){this._adjustDialog();}_initializeBackDrop(){return new bi({isVisible:Boolean(this._config.backdrop),isAnimated:this._isAnimated()})}_initializeFocusTrap(){return new Ai({trapElement:this._element})}_getConfig(t){return t={...Ci,...U.getDataAttributes(this._element),..."object"==typeof t?t:{}},a(Ti,t,ki),t}_showElement(t){const e=this._isAnimated(),i=V.findOne(".modal-body",this._dialog);this._element.parentNode&&this._element.parentNode.nodeType===Node.ELEMENT_NODE||document.body.append(this._element),this._element.style.display="block",this._element.removeAttribute("aria-hidden"),this._element.setAttribute("aria-modal",!0),this._element.setAttribute("role","dialog"),this._element.scrollTop=0,i&&(i.scrollTop=0),e&&u(this._element),this._element.classList.add(ji),this._queueCallback((()=>{this._config.focus&&this._focustrap.activate(),this._isTransitioning=!1,j.trigger(this._element,"shown.bs.modal",{relatedTarget:t});}),this._dialog,e);}_setEscapeEvent(){this._isShown?j.on(this._element,Ni,(t=>{this._config.keyboard&&t.key===Oi?(t.preventDefault(),this.hide()):this._config.keyboard||t.key!==Oi||this._triggerBackdropTransition();})):j.off(this._element,Ni);}_setResizeEvent(){this._isShown?j.on(window,Di,(()=>this._adjustDialog())):j.off(window,Di);}_hideModal(){this._element.style.display="none",this._element.setAttribute("aria-hidden",!0),this._element.removeAttribute("aria-modal"),this._element.removeAttribute("role"),this._isTransitioning=!1,this._backdrop.hide((()=>{document.body.classList.remove(Pi),this._resetAdjustments(),this._scrollBar.reset(),j.trigger(this._element,Li);}));}_showBackdrop(t){j.on(this._element,Si,(t=>{this._ignoreBackdropClick?this._ignoreBackdropClick=!1:t.target===t.currentTarget&&(!0===this._config.backdrop?this.hide():"static"===this._config.backdrop&&this._triggerBackdropTransition());})),this._backdrop.show(t);}_isAnimated(){return this._element.classList.contains("fade")}_triggerBackdropTransition(){if(j.trigger(this._element,"hidePrevented.bs.modal").defaultPrevented)return;const{classList:t,scrollHeight:e,style:i}=this._element,n=e>document.documentElement.clientHeight;!n&&"hidden"===i.overflowY||t.contains(Mi)||(n||(i.overflowY="hidden"),t.add(Mi),this._queueCallback((()=>{t.remove(Mi),n||this._queueCallback((()=>{i.overflowY="";}),this._dialog);}),this._dialog),this._element.focus());}_adjustDialog(){const t=this._element.scrollHeight>document.documentElement.clientHeight,e=this._scrollBar.getWidth(),i=e>0;(!i&&t&&!m()||i&&!t&&m())&&(this._element.style.paddingLeft=`${e}px`),(i&&!t&&!m()||!i&&t&&m())&&(this._element.style.paddingRight=`${e}px`);}_resetAdjustments(){this._element.style.paddingLeft="",this._element.style.paddingRight="";}static jQueryInterface(t,e){return this.each((function(){const i=Hi.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===i[t])throw new TypeError(`No method named "${t}"`);i[t](e);}}))}}j.on(document,"click.bs.modal.data-api",'[data-bs-toggle="modal"]',(function(t){const e=n(this);["A","AREA"].includes(this.tagName)&&t.preventDefault(),j.one(e,xi,(t=>{t.defaultPrevented||j.one(e,Li,(()=>{l(this)&&this.focus();}));}));const i=V.findOne(".modal.show");i&&Hi.getInstance(i).hide(),Hi.getOrCreateInstance(e).toggle(this);})),R(Hi),g(Hi);const Bi="offcanvas",Ri={backdrop:!0,keyboard:!0,scroll:!1},Wi={backdrop:"boolean",keyboard:"boolean",scroll:"boolean"},$i="show",zi=".offcanvas.show",qi="hidden.bs.offcanvas";class Fi extends B{constructor(t,e){super(t),this._config=this._getConfig(e),this._isShown=!1,this._backdrop=this._initializeBackDrop(),this._focustrap=this._initializeFocusTrap(),this._addEventListeners();}static get NAME(){return Bi}static get Default(){return Ri}toggle(t){return this._isShown?this.hide():this.show(t)}show(t){this._isShown||j.trigger(this._element,"show.bs.offcanvas",{relatedTarget:t}).defaultPrevented||(this._isShown=!0,this._element.style.visibility="visible",this._backdrop.show(),this._config.scroll||(new fi).hide(),this._element.removeAttribute("aria-hidden"),this._element.setAttribute("aria-modal",!0),this._element.setAttribute("role","dialog"),this._element.classList.add($i),this._queueCallback((()=>{this._config.scroll||this._focustrap.activate(),j.trigger(this._element,"shown.bs.offcanvas",{relatedTarget:t});}),this._element,!0));}hide(){this._isShown&&(j.trigger(this._element,"hide.bs.offcanvas").defaultPrevented||(this._focustrap.deactivate(),this._element.blur(),this._isShown=!1,this._element.classList.remove($i),this._backdrop.hide(),this._queueCallback((()=>{this._element.setAttribute("aria-hidden",!0),this._element.removeAttribute("aria-modal"),this._element.removeAttribute("role"),this._element.style.visibility="hidden",this._config.scroll||(new fi).reset(),j.trigger(this._element,qi);}),this._element,!0)));}dispose(){this._backdrop.dispose(),this._focustrap.deactivate(),super.dispose();}_getConfig(t){return t={...Ri,...U.getDataAttributes(this._element),..."object"==typeof t?t:{}},a(Bi,t,Wi),t}_initializeBackDrop(){return new bi({className:"offcanvas-backdrop",isVisible:this._config.backdrop,isAnimated:!0,rootElement:this._element.parentNode,clickCallback:()=>this.hide()})}_initializeFocusTrap(){return new Ai({trapElement:this._element})}_addEventListeners(){j.on(this._element,"keydown.dismiss.bs.offcanvas",(t=>{this._config.keyboard&&"Escape"===t.key&&this.hide();}));}static jQueryInterface(t){return this.each((function(){const e=Fi.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===e[t]||t.startsWith("_")||"constructor"===t)throw new TypeError(`No method named "${t}"`);e[t](this);}}))}}j.on(document,"click.bs.offcanvas.data-api",'[data-bs-toggle="offcanvas"]',(function(t){const e=n(this);if(["A","AREA"].includes(this.tagName)&&t.preventDefault(),c(this))return;j.one(e,qi,(()=>{l(this)&&this.focus();}));const i=V.findOne(zi);i&&i!==e&&Fi.getInstance(i).hide(),Fi.getOrCreateInstance(e).toggle(this);})),j.on(window,"load.bs.offcanvas.data-api",(()=>V.find(zi).forEach((t=>Fi.getOrCreateInstance(t).show())))),R(Fi),g(Fi);const Ui=new Set(["background","cite","href","itemtype","longdesc","poster","src","xlink:href"]),Vi=/^(?:(?:https?|mailto|ftp|tel|file|sms):|[^#&/:?]*(?:[#/?]|$))/i,Ki=/^data:(?:image\/(?:bmp|gif|jpeg|jpg|png|tiff|webp)|video\/(?:mpeg|mp4|ogg|webm)|audio\/(?:mp3|oga|ogg|opus));base64,[\d+/a-z]+=*$/i,Xi=(t,e)=>{const i=t.nodeName.toLowerCase();if(e.includes(i))return !Ui.has(i)||Boolean(Vi.test(t.nodeValue)||Ki.test(t.nodeValue));const n=e.filter((t=>t instanceof RegExp));for(let t=0,e=n.length;t<e;t++)if(n[t].test(i))return !0;return !1};function Yi(t,e,i){if(!t.length)return t;if(i&&"function"==typeof i)return i(t);const n=(new window.DOMParser).parseFromString(t,"text/html"),s=[].concat(...n.body.querySelectorAll("*"));for(let t=0,i=s.length;t<i;t++){const i=s[t],n=i.nodeName.toLowerCase();if(!Object.keys(e).includes(n)){i.remove();continue}const o=[].concat(...i.attributes),r=[].concat(e["*"]||[],e[n]||[]);o.forEach((t=>{Xi(t,r)||i.removeAttribute(t.nodeName);}));}return n.body.innerHTML}const Qi="tooltip",Gi=new Set(["sanitize","allowList","sanitizeFn"]),Zi={animation:"boolean",template:"string",title:"(string|element|function)",trigger:"string",delay:"(number|object)",html:"boolean",selector:"(string|boolean)",placement:"(string|function)",offset:"(array|string|function)",container:"(string|element|boolean)",fallbackPlacements:"array",boundary:"(string|element)",customClass:"(string|function)",sanitize:"boolean",sanitizeFn:"(null|function)",allowList:"object",popperConfig:"(null|object|function)"},Ji={AUTO:"auto",TOP:"top",RIGHT:m()?"left":"right",BOTTOM:"bottom",LEFT:m()?"right":"left"},tn={animation:!0,template:'<div class="tooltip" role="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div>',trigger:"hover focus",title:"",delay:0,html:!1,selector:!1,placement:"top",offset:[0,0],container:!1,fallbackPlacements:["top","right","bottom","left"],boundary:"clippingParents",customClass:"",sanitize:!0,sanitizeFn:null,allowList:{"*":["class","dir","id","lang","role",/^aria-[\w-]*$/i],a:["target","href","title","rel"],area:[],b:[],br:[],col:[],code:[],div:[],em:[],hr:[],h1:[],h2:[],h3:[],h4:[],h5:[],h6:[],i:[],img:["src","srcset","alt","title","width","height"],li:[],ol:[],p:[],pre:[],s:[],small:[],span:[],sub:[],sup:[],strong:[],u:[],ul:[]},popperConfig:null},en={HIDE:"hide.bs.tooltip",HIDDEN:"hidden.bs.tooltip",SHOW:"show.bs.tooltip",SHOWN:"shown.bs.tooltip",INSERTED:"inserted.bs.tooltip",CLICK:"click.bs.tooltip",FOCUSIN:"focusin.bs.tooltip",FOCUSOUT:"focusout.bs.tooltip",MOUSEENTER:"mouseenter.bs.tooltip",MOUSELEAVE:"mouseleave.bs.tooltip"},nn="fade",sn="show",on="show",rn="out",an=".tooltip-inner",ln=".modal",cn="hide.bs.modal",hn="hover",dn="focus";class un extends B{constructor(t,e){if(void 0===Fe)throw new TypeError("Bootstrap's tooltips require Popper (https://popper.js.org)");super(t),this._isEnabled=!0,this._timeout=0,this._hoverState="",this._activeTrigger={},this._popper=null,this._config=this._getConfig(e),this.tip=null,this._setListeners();}static get Default(){return tn}static get NAME(){return Qi}static get Event(){return en}static get DefaultType(){return Zi}enable(){this._isEnabled=!0;}disable(){this._isEnabled=!1;}toggleEnabled(){this._isEnabled=!this._isEnabled;}toggle(t){if(this._isEnabled)if(t){const e=this._initializeOnDelegatedTarget(t);e._activeTrigger.click=!e._activeTrigger.click,e._isWithActiveTrigger()?e._enter(null,e):e._leave(null,e);}else {if(this.getTipElement().classList.contains(sn))return void this._leave(null,this);this._enter(null,this);}}dispose(){clearTimeout(this._timeout),j.off(this._element.closest(ln),cn,this._hideModalHandler),this.tip&&this.tip.remove(),this._disposePopper(),super.dispose();}show(){if("none"===this._element.style.display)throw new Error("Please use show on visible elements");if(!this.isWithContent()||!this._isEnabled)return;const t=j.trigger(this._element,this.constructor.Event.SHOW),e=h(this._element),i=null===e?this._element.ownerDocument.documentElement.contains(this._element):e.contains(this._element);if(t.defaultPrevented||!i)return;"tooltip"===this.constructor.NAME&&this.tip&&this.getTitle()!==this.tip.querySelector(an).innerHTML&&(this._disposePopper(),this.tip.remove(),this.tip=null);const n=this.getTipElement(),s=(t=>{do{t+=Math.floor(1e6*Math.random());}while(document.getElementById(t));return t})(this.constructor.NAME);n.setAttribute("id",s),this._element.setAttribute("aria-describedby",s),this._config.animation&&n.classList.add(nn);const o="function"==typeof this._config.placement?this._config.placement.call(this,n,this._element):this._config.placement,r=this._getAttachment(o);this._addAttachmentClass(r);const{container:a}=this._config;H.set(n,this.constructor.DATA_KEY,this),this._element.ownerDocument.documentElement.contains(this.tip)||(a.append(n),j.trigger(this._element,this.constructor.Event.INSERTED)),this._popper?this._popper.update():this._popper=qe(this._element,n,this._getPopperConfig(r)),n.classList.add(sn);const l=this._resolvePossibleFunction(this._config.customClass);l&&n.classList.add(...l.split(" ")),"ontouchstart"in document.documentElement&&[].concat(...document.body.children).forEach((t=>{j.on(t,"mouseover",d);}));const c=this.tip.classList.contains(nn);this._queueCallback((()=>{const t=this._hoverState;this._hoverState=null,j.trigger(this._element,this.constructor.Event.SHOWN),t===rn&&this._leave(null,this);}),this.tip,c);}hide(){if(!this._popper)return;const t=this.getTipElement();if(j.trigger(this._element,this.constructor.Event.HIDE).defaultPrevented)return;t.classList.remove(sn),"ontouchstart"in document.documentElement&&[].concat(...document.body.children).forEach((t=>j.off(t,"mouseover",d))),this._activeTrigger.click=!1,this._activeTrigger.focus=!1,this._activeTrigger.hover=!1;const e=this.tip.classList.contains(nn);this._queueCallback((()=>{this._isWithActiveTrigger()||(this._hoverState!==on&&t.remove(),this._cleanTipClass(),this._element.removeAttribute("aria-describedby"),j.trigger(this._element,this.constructor.Event.HIDDEN),this._disposePopper());}),this.tip,e),this._hoverState="";}update(){null!==this._popper&&this._popper.update();}isWithContent(){return Boolean(this.getTitle())}getTipElement(){if(this.tip)return this.tip;const t=document.createElement("div");t.innerHTML=this._config.template;const e=t.children[0];return this.setContent(e),e.classList.remove(nn,sn),this.tip=e,this.tip}setContent(t){this._sanitizeAndSetContent(t,this.getTitle(),an);}_sanitizeAndSetContent(t,e,i){const n=V.findOne(i,t);e||!n?this.setElementContent(n,e):n.remove();}setElementContent(t,e){if(null!==t)return o(e)?(e=r(e),void(this._config.html?e.parentNode!==t&&(t.innerHTML="",t.append(e)):t.textContent=e.textContent)):void(this._config.html?(this._config.sanitize&&(e=Yi(e,this._config.allowList,this._config.sanitizeFn)),t.innerHTML=e):t.textContent=e)}getTitle(){const t=this._element.getAttribute("data-bs-original-title")||this._config.title;return this._resolvePossibleFunction(t)}updateAttachment(t){return "right"===t?"end":"left"===t?"start":t}_initializeOnDelegatedTarget(t,e){return e||this.constructor.getOrCreateInstance(t.delegateTarget,this._getDelegateConfig())}_getOffset(){const{offset:t}=this._config;return "string"==typeof t?t.split(",").map((t=>Number.parseInt(t,10))):"function"==typeof t?e=>t(e,this._element):t}_resolvePossibleFunction(t){return "function"==typeof t?t.call(this._element):t}_getPopperConfig(t){const e={placement:t,modifiers:[{name:"flip",options:{fallbackPlacements:this._config.fallbackPlacements}},{name:"offset",options:{offset:this._getOffset()}},{name:"preventOverflow",options:{boundary:this._config.boundary}},{name:"arrow",options:{element:`.${this.constructor.NAME}-arrow`}},{name:"onChange",enabled:!0,phase:"afterWrite",fn:t=>this._handlePopperPlacementChange(t)}],onFirstUpdate:t=>{t.options.placement!==t.placement&&this._handlePopperPlacementChange(t);}};return {...e,..."function"==typeof this._config.popperConfig?this._config.popperConfig(e):this._config.popperConfig}}_addAttachmentClass(t){this.getTipElement().classList.add(`${this._getBasicClassPrefix()}-${this.updateAttachment(t)}`);}_getAttachment(t){return Ji[t.toUpperCase()]}_setListeners(){this._config.trigger.split(" ").forEach((t=>{if("click"===t)j.on(this._element,this.constructor.Event.CLICK,this._config.selector,(t=>this.toggle(t)));else if("manual"!==t){const e=t===hn?this.constructor.Event.MOUSEENTER:this.constructor.Event.FOCUSIN,i=t===hn?this.constructor.Event.MOUSELEAVE:this.constructor.Event.FOCUSOUT;j.on(this._element,e,this._config.selector,(t=>this._enter(t))),j.on(this._element,i,this._config.selector,(t=>this._leave(t)));}})),this._hideModalHandler=()=>{this._element&&this.hide();},j.on(this._element.closest(ln),cn,this._hideModalHandler),this._config.selector?this._config={...this._config,trigger:"manual",selector:""}:this._fixTitle();}_fixTitle(){const t=this._element.getAttribute("title"),e=typeof this._element.getAttribute("data-bs-original-title");(t||"string"!==e)&&(this._element.setAttribute("data-bs-original-title",t||""),!t||this._element.getAttribute("aria-label")||this._element.textContent||this._element.setAttribute("aria-label",t),this._element.setAttribute("title",""));}_enter(t,e){e=this._initializeOnDelegatedTarget(t,e),t&&(e._activeTrigger["focusin"===t.type?dn:hn]=!0),e.getTipElement().classList.contains(sn)||e._hoverState===on?e._hoverState=on:(clearTimeout(e._timeout),e._hoverState=on,e._config.delay&&e._config.delay.show?e._timeout=setTimeout((()=>{e._hoverState===on&&e.show();}),e._config.delay.show):e.show());}_leave(t,e){e=this._initializeOnDelegatedTarget(t,e),t&&(e._activeTrigger["focusout"===t.type?dn:hn]=e._element.contains(t.relatedTarget)),e._isWithActiveTrigger()||(clearTimeout(e._timeout),e._hoverState=rn,e._config.delay&&e._config.delay.hide?e._timeout=setTimeout((()=>{e._hoverState===rn&&e.hide();}),e._config.delay.hide):e.hide());}_isWithActiveTrigger(){for(const t in this._activeTrigger)if(this._activeTrigger[t])return !0;return !1}_getConfig(t){const e=U.getDataAttributes(this._element);return Object.keys(e).forEach((t=>{Gi.has(t)&&delete e[t];})),(t={...this.constructor.Default,...e,..."object"==typeof t&&t?t:{}}).container=!1===t.container?document.body:r(t.container),"number"==typeof t.delay&&(t.delay={show:t.delay,hide:t.delay}),"number"==typeof t.title&&(t.title=t.title.toString()),"number"==typeof t.content&&(t.content=t.content.toString()),a(Qi,t,this.constructor.DefaultType),t.sanitize&&(t.template=Yi(t.template,t.allowList,t.sanitizeFn)),t}_getDelegateConfig(){const t={};for(const e in this._config)this.constructor.Default[e]!==this._config[e]&&(t[e]=this._config[e]);return t}_cleanTipClass(){const t=this.getTipElement(),e=new RegExp(`(^|\\s)${this._getBasicClassPrefix()}\\S+`,"g"),i=t.getAttribute("class").match(e);null!==i&&i.length>0&&i.map((t=>t.trim())).forEach((e=>t.classList.remove(e)));}_getBasicClassPrefix(){return "bs-tooltip"}_handlePopperPlacementChange(t){const{state:e}=t;e&&(this.tip=e.elements.popper,this._cleanTipClass(),this._addAttachmentClass(this._getAttachment(e.placement)));}_disposePopper(){this._popper&&(this._popper.destroy(),this._popper=null);}static jQueryInterface(t){return this.each((function(){const e=un.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===e[t])throw new TypeError(`No method named "${t}"`);e[t]();}}))}}g(un);const fn={...un.Default,placement:"right",offset:[0,8],trigger:"click",content:"",template:'<div class="popover" role="tooltip"><div class="popover-arrow"></div><h3 class="popover-header"></h3><div class="popover-body"></div></div>'},pn={...un.DefaultType,content:"(string|element|function)"},mn={HIDE:"hide.bs.popover",HIDDEN:"hidden.bs.popover",SHOW:"show.bs.popover",SHOWN:"shown.bs.popover",INSERTED:"inserted.bs.popover",CLICK:"click.bs.popover",FOCUSIN:"focusin.bs.popover",FOCUSOUT:"focusout.bs.popover",MOUSEENTER:"mouseenter.bs.popover",MOUSELEAVE:"mouseleave.bs.popover"};class gn extends un{static get Default(){return fn}static get NAME(){return "popover"}static get Event(){return mn}static get DefaultType(){return pn}isWithContent(){return this.getTitle()||this._getContent()}setContent(t){this._sanitizeAndSetContent(t,this.getTitle(),".popover-header"),this._sanitizeAndSetContent(t,this._getContent(),".popover-body");}_getContent(){return this._resolvePossibleFunction(this._config.content)}_getBasicClassPrefix(){return "bs-popover"}static jQueryInterface(t){return this.each((function(){const e=gn.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===e[t])throw new TypeError(`No method named "${t}"`);e[t]();}}))}}g(gn);const _n="scrollspy",bn={offset:10,method:"auto",target:""},vn={offset:"number",method:"string",target:"(string|element)"},yn="active",wn=".nav-link, .list-group-item, .dropdown-item",En="position";class An extends B{constructor(t,e){super(t),this._scrollElement="BODY"===this._element.tagName?window:this._element,this._config=this._getConfig(e),this._offsets=[],this._targets=[],this._activeTarget=null,this._scrollHeight=0,j.on(this._scrollElement,"scroll.bs.scrollspy",(()=>this._process())),this.refresh(),this._process();}static get Default(){return bn}static get NAME(){return _n}refresh(){const t=this._scrollElement===this._scrollElement.window?"offset":En,e="auto"===this._config.method?t:this._config.method,n=e===En?this._getScrollTop():0;this._offsets=[],this._targets=[],this._scrollHeight=this._getScrollHeight(),V.find(wn,this._config.target).map((t=>{const s=i(t),o=s?V.findOne(s):null;if(o){const t=o.getBoundingClientRect();if(t.width||t.height)return [U[e](o).top+n,s]}return null})).filter((t=>t)).sort(((t,e)=>t[0]-e[0])).forEach((t=>{this._offsets.push(t[0]),this._targets.push(t[1]);}));}dispose(){j.off(this._scrollElement,".bs.scrollspy"),super.dispose();}_getConfig(t){return (t={...bn,...U.getDataAttributes(this._element),..."object"==typeof t&&t?t:{}}).target=r(t.target)||document.documentElement,a(_n,t,vn),t}_getScrollTop(){return this._scrollElement===window?this._scrollElement.pageYOffset:this._scrollElement.scrollTop}_getScrollHeight(){return this._scrollElement.scrollHeight||Math.max(document.body.scrollHeight,document.documentElement.scrollHeight)}_getOffsetHeight(){return this._scrollElement===window?window.innerHeight:this._scrollElement.getBoundingClientRect().height}_process(){const t=this._getScrollTop()+this._config.offset,e=this._getScrollHeight(),i=this._config.offset+e-this._getOffsetHeight();if(this._scrollHeight!==e&&this.refresh(),t>=i){const t=this._targets[this._targets.length-1];this._activeTarget!==t&&this._activate(t);}else {if(this._activeTarget&&t<this._offsets[0]&&this._offsets[0]>0)return this._activeTarget=null,void this._clear();for(let e=this._offsets.length;e--;)this._activeTarget!==this._targets[e]&&t>=this._offsets[e]&&(void 0===this._offsets[e+1]||t<this._offsets[e+1])&&this._activate(this._targets[e]);}}_activate(t){this._activeTarget=t,this._clear();const e=wn.split(",").map((e=>`${e}[data-bs-target="${t}"],${e}[href="${t}"]`)),i=V.findOne(e.join(","),this._config.target);i.classList.add(yn),i.classList.contains("dropdown-item")?V.findOne(".dropdown-toggle",i.closest(".dropdown")).classList.add(yn):V.parents(i,".nav, .list-group").forEach((t=>{V.prev(t,".nav-link, .list-group-item").forEach((t=>t.classList.add(yn))),V.prev(t,".nav-item").forEach((t=>{V.children(t,".nav-link").forEach((t=>t.classList.add(yn)));}));})),j.trigger(this._scrollElement,"activate.bs.scrollspy",{relatedTarget:t});}_clear(){V.find(wn,this._config.target).filter((t=>t.classList.contains(yn))).forEach((t=>t.classList.remove(yn)));}static jQueryInterface(t){return this.each((function(){const e=An.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===e[t])throw new TypeError(`No method named "${t}"`);e[t]();}}))}}j.on(window,"load.bs.scrollspy.data-api",(()=>{V.find('[data-bs-spy="scroll"]').forEach((t=>new An(t)));})),g(An);const Tn="active",On="fade",Cn="show",kn=".active",Ln=":scope > li > .active";class xn extends B{static get NAME(){return "tab"}show(){if(this._element.parentNode&&this._element.parentNode.nodeType===Node.ELEMENT_NODE&&this._element.classList.contains(Tn))return;let t;const e=n(this._element),i=this._element.closest(".nav, .list-group");if(i){const e="UL"===i.nodeName||"OL"===i.nodeName?Ln:kn;t=V.find(e,i),t=t[t.length-1];}const s=t?j.trigger(t,"hide.bs.tab",{relatedTarget:this._element}):null;if(j.trigger(this._element,"show.bs.tab",{relatedTarget:t}).defaultPrevented||null!==s&&s.defaultPrevented)return;this._activate(this._element,i);const o=()=>{j.trigger(t,"hidden.bs.tab",{relatedTarget:this._element}),j.trigger(this._element,"shown.bs.tab",{relatedTarget:t});};e?this._activate(e,e.parentNode,o):o();}_activate(t,e,i){const n=(!e||"UL"!==e.nodeName&&"OL"!==e.nodeName?V.children(e,kn):V.find(Ln,e))[0],s=i&&n&&n.classList.contains(On),o=()=>this._transitionComplete(t,n,i);n&&s?(n.classList.remove(Cn),this._queueCallback(o,t,!0)):o();}_transitionComplete(t,e,i){if(e){e.classList.remove(Tn);const t=V.findOne(":scope > .dropdown-menu .active",e.parentNode);t&&t.classList.remove(Tn),"tab"===e.getAttribute("role")&&e.setAttribute("aria-selected",!1);}t.classList.add(Tn),"tab"===t.getAttribute("role")&&t.setAttribute("aria-selected",!0),u(t),t.classList.contains(On)&&t.classList.add(Cn);let n=t.parentNode;if(n&&"LI"===n.nodeName&&(n=n.parentNode),n&&n.classList.contains("dropdown-menu")){const e=t.closest(".dropdown");e&&V.find(".dropdown-toggle",e).forEach((t=>t.classList.add(Tn))),t.setAttribute("aria-expanded",!0);}i&&i();}static jQueryInterface(t){return this.each((function(){const e=xn.getOrCreateInstance(this);if("string"==typeof t){if(void 0===e[t])throw new TypeError(`No method named "${t}"`);e[t]();}}))}}j.on(document,"click.bs.tab.data-api",'[data-bs-toggle="tab"], [data-bs-toggle="pill"], [data-bs-toggle="list"]',(function(t){["A","AREA"].includes(this.tagName)&&t.preventDefault(),c(this)||xn.getOrCreateInstance(this).show();})),g(xn);const Dn="toast",Sn="hide",Nn="show",In="showing",Pn={animation:"boolean",autohide:"boolean",delay:"number"},jn={animation:!0,autohide:!0,delay:5e3};class Mn extends B{constructor(t,e){super(t),this._config=this._getConfig(e),this._timeout=null,this._hasMouseInteraction=!1,this._hasKeyboardInteraction=!1,this._setListeners();}static get DefaultType(){return Pn}static get Default(){return jn}static get NAME(){return Dn}show(){j.trigger(this._element,"show.bs.toast").defaultPrevented||(this._clearTimeout(),this._config.animation&&this._element.classList.add("fade"),this._element.classList.remove(Sn),u(this._element),this._element.classList.add(Nn),this._element.classList.add(In),this._queueCallback((()=>{this._element.classList.remove(In),j.trigger(this._element,"shown.bs.toast"),this._maybeScheduleHide();}),this._element,this._config.animation));}hide(){this._element.classList.contains(Nn)&&(j.trigger(this._element,"hide.bs.toast").defaultPrevented||(this._element.classList.add(In),this._queueCallback((()=>{this._element.classList.add(Sn),this._element.classList.remove(In),this._element.classList.remove(Nn),j.trigger(this._element,"hidden.bs.toast");}),this._element,this._config.animation)));}dispose(){this._clearTimeout(),this._element.classList.contains(Nn)&&this._element.classList.remove(Nn),super.dispose();}_getConfig(t){return t={...jn,...U.getDataAttributes(this._element),..."object"==typeof t&&t?t:{}},a(Dn,t,this.constructor.DefaultType),t}_maybeScheduleHide(){this._config.autohide&&(this._hasMouseInteraction||this._hasKeyboardInteraction||(this._timeout=setTimeout((()=>{this.hide();}),this._config.delay)));}_onInteraction(t,e){switch(t.type){case"mouseover":case"mouseout":this._hasMouseInteraction=e;break;case"focusin":case"focusout":this._hasKeyboardInteraction=e;}if(e)return void this._clearTimeout();const i=t.relatedTarget;this._element===i||this._element.contains(i)||this._maybeScheduleHide();}_setListeners(){j.on(this._element,"mouseover.bs.toast",(t=>this._onInteraction(t,!0))),j.on(this._element,"mouseout.bs.toast",(t=>this._onInteraction(t,!1))),j.on(this._element,"focusin.bs.toast",(t=>this._onInteraction(t,!0))),j.on(this._element,"focusout.bs.toast",(t=>this._onInteraction(t,!1)));}_clearTimeout(){clearTimeout(this._timeout),this._timeout=null;}static jQueryInterface(t){return this.each((function(){const e=Mn.getOrCreateInstance(this,t);if("string"==typeof t){if(void 0===e[t])throw new TypeError(`No method named "${t}"`);e[t](this);}}))}}return R(Mn),g(Mn),{Alert:W,Button:z,Carousel:st,Collapse:pt,Dropdown:hi,Modal:Hi,Offcanvas:Fi,Popover:gn,ScrollSpy:An,Tab:xn,Toast:Mn,Tooltip:un}}));

  /*
    Highlight.js 10.5.0 (af20048d)
    License: BSD-3-Clause
    Copyright (c) 2006-2020, Ivan Sagalaev
  */
  var hljs = function () {
   function e(t) {
      return t instanceof Map ? t.clear = t.delete = t.set = () => {
        throw Error("map is read-only")
      } : t instanceof Set && (t.add = t.clear = t.delete = () => {
        throw Error("set is read-only")
      }), Object.freeze(t), Object.getOwnPropertyNames(t).forEach((n => {
        var s = t[n]
          ; "object" != typeof s || Object.isFrozen(s) || e(s);
      })), t
    } var t = e, n = e; t.default = n
      ; class s {
      constructor(e) { void 0 === e.data && (e.data = {}), this.data = e.data; }
      ignoreMatch() { this.ignore = !0; }
    } function r(e) {
      return e.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;").replace(/'/g, "&#x27;")
    } function a(e, ...t) {
      const n = Object.create(null); for (const t in e) n[t] = e[t]
        ; return t.forEach((e => { for (const t in e) n[t] = e[t]; })), n
    } const i = e => !!e.kind
      ; class o {
      constructor(e, t) {
        this.buffer = "", this.classPrefix = t.classPrefix, e.walk(this);
      } addText(e) {
        this.buffer += r(e);
      } openNode(e) {
        if (!i(e)) return; let t = e.kind
          ; e.sublanguage || (t = `${this.classPrefix}${t}`), this.span(t);
      } closeNode(e) {
        i(e) && (this.buffer += "</span>");
      } value() { return this.buffer } span(e) {
        this.buffer += `<span class="${e}">`;
      }
    } class l {
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
          l._collapse(e);
        })));
      }
    } class c extends l {
      constructor(e) { super(), this.options = e; }
      addKeyword(e, t) { "" !== e && (this.openNode(t), this.addText(e), this.closeNode()); }
      addText(e) { "" !== e && this.add(e); } addSublanguage(e, t) {
        const n = e.root
          ; n.kind = t, n.sublanguage = !0, this.add(n);
      } toHTML() {
        return new o(this, this.options).value()
      } finalize() { return !0 }
    } function u(e) {
      return e ? "string" == typeof e ? e : e.source : null
    }
    const g = "[a-zA-Z]\\w*", d = "[a-zA-Z_]\\w*", h = "\\b\\d+(\\.\\d+)?", f = "(-?)(\\b0[xX][a-fA-F0-9]+|(\\b\\d+(\\.\\d*)?|\\.\\d+)([eE][-+]?\\d+)?)", p = "\\b(0b[01]+)", m = {
      begin: "\\\\[\\s\\S]", relevance: 0
    }, b = {
      className: "string", begin: "'", end: "'",
      illegal: "\\n", contains: [m]
    }, x = {
      className: "string", begin: '"', end: '"',
      illegal: "\\n", contains: [m]
    }, E = {
      begin: /\b(a|an|the|are|I'm|isn't|don't|doesn't|won't|but|just|should|pretty|simply|enough|gonna|going|wtf|so|such|will|you|your|they|like|more)\b/
    }, v = (e, t, n = {}) => {
      const s = a({ className: "comment", begin: e, end: t, contains: [] }, n)
        ; return s.contains.push(E), s.contains.push({
          className: "doctag",
          begin: "(?:TODO|FIXME|NOTE|BUG|OPTIMIZE|HACK|XXX):", relevance: 0
        }), s
    }, N = v("//", "$"), w = v("/\\*", "\\*/"), R = v("#", "$"); var y = Object.freeze({
      __proto__: null, IDENT_RE: g, UNDERSCORE_IDENT_RE: d, NUMBER_RE: h, C_NUMBER_RE: f,
      BINARY_NUMBER_RE: p,
      RE_STARTERS_RE: "!|!=|!==|%|%=|&|&&|&=|\\*|\\*=|\\+|\\+=|,|-|-=|/=|/|:|;|<<|<<=|<=|<|===|==|=|>>>=|>>=|>=|>>>|>>|>|\\?|\\[|\\{|\\(|\\^|\\^=|\\||\\|=|\\|\\||~",
      SHEBANG: (e = {}) => {
        const t = /^#![ ]*\//
          ; return e.binary && (e.begin = ((...e) => e.map((e => u(e))).join(""))(t, /.*\b/, e.binary, /\b.*/)),
            a({
              className: "meta", begin: t, end: /$/, relevance: 0, "on:begin": (e, t) => {
                0 !== e.index && t.ignoreMatch();
              }
            }, e)
      }, BACKSLASH_ESCAPE: m, APOS_STRING_MODE: b,
      QUOTE_STRING_MODE: x, PHRASAL_WORDS_MODE: E, COMMENT: v, C_LINE_COMMENT_MODE: N,
      C_BLOCK_COMMENT_MODE: w, HASH_COMMENT_MODE: R, NUMBER_MODE: {
        className: "number",
        begin: h, relevance: 0
      }, C_NUMBER_MODE: { className: "number", begin: f, relevance: 0 },
      BINARY_NUMBER_MODE: { className: "number", begin: p, relevance: 0 }, CSS_NUMBER_MODE: {
        className: "number",
        begin: h + "(%|em|ex|ch|rem|vw|vh|vmin|vmax|cm|mm|in|pt|pc|px|deg|grad|rad|turn|s|ms|Hz|kHz|dpi|dpcm|dppx)?",
        relevance: 0
      }, REGEXP_MODE: {
        begin: /(?=\/[^/\n]*\/)/, contains: [{
          className: "regexp",
          begin: /\//, end: /\/[gimuy]*/, illegal: /\n/, contains: [m, {
            begin: /\[/, end: /\]/,
            relevance: 0, contains: [m]
          }]
        }]
      }, TITLE_MODE: {
        className: "title", begin: g, relevance: 0
      }, UNDERSCORE_TITLE_MODE: { className: "title", begin: d, relevance: 0 }, METHOD_GUARD: {
        begin: "\\.\\s*[a-zA-Z_]\\w*", relevance: 0
      }, END_SAME_AS_BEGIN: e => Object.assign(e, {
        "on:begin": (e, t) => { t.data._beginMatch = e[1]; }, "on:end": (e, t) => {
          t.data._beginMatch !== e[1] && t.ignoreMatch();
        }
      })
    }); function _(e, t) {
      "." === e.input[e.index - 1] && t.ignoreMatch();
    } function k(e, t) {
      t && e.beginKeywords && (e.begin = "\\b(" + e.beginKeywords.split(" ").join("|") + ")(?!\\.)(?=\\b|\\s)",
        e.__beforeBegin = _, e.keywords = e.keywords || e.beginKeywords, delete e.beginKeywords);
    } function M(e, t) {
      Array.isArray(e.illegal) && (e.illegal = ((...e) => "(" + e.map((e => u(e))).join("|") + ")")(...e.illegal));
    } function O(e, t) {
      if (e.match) {
        if (e.begin || e.end) throw Error("begin & end are not supported with match")
          ; e.begin = e.match, delete e.match;
      }
    } function A(e, t) {
      void 0 === e.relevance && (e.relevance = 1);
    }
    const L = ["of", "and", "for", "in", "not", "or", "if", "then", "parent", "list", "value"]
      ; function B(e, t) { return t ? Number(t) : (e => L.includes(e.toLowerCase()))(e) ? 0 : 1 }
    function I(e, { plugins: t }) {
      function n(t, n) {
        return RegExp(u(t), "m" + (e.case_insensitive ? "i" : "") + (n ? "g" : ""))
      } class s {
        constructor() {
          this.matchIndexes = {}, this.regexes = [], this.matchAt = 1, this.position = 0;
        }
        addRule(e, t) {
          t.position = this.position++, this.matchIndexes[this.matchAt] = t, this.regexes.push([t, e]),
            this.matchAt += (e => RegExp(e.toString() + "|").exec("").length - 1)(e) + 1;
        } compile() {
          0 === this.regexes.length && (this.exec = () => null)
            ; const e = this.regexes.map((e => e[1])); this.matcherRe = n(((e, t = "|") => {
              const n = /\[(?:[^\\\]]|\\.)*\]|\(\??|\\([1-9][0-9]*)|\\./; let s = 0, r = ""
                ; for (let a = 0; a < e.length; a++) {
                  s += 1; const i = s; let o = u(e[a])
                    ; for (a > 0 && (r += t), r += "("; o.length > 0;) {
                      const e = n.exec(o); if (null == e) { r += o; break }
                      r += o.substring(0, e.index),
                        o = o.substring(e.index + e[0].length), "\\" === e[0][0] && e[1] ? r += "\\" + (Number(e[1]) + i) : (r += e[0],
                          "(" === e[0] && s++);
                    } r += ")";
                } return r
            })(e), !0), this.lastIndex = 0;
        } exec(e) {
          this.matcherRe.lastIndex = this.lastIndex; const t = this.matcherRe.exec(e)
            ; if (!t) return null
              ; const n = t.findIndex(((e, t) => t > 0 && void 0 !== e)), s = this.matchIndexes[n]
            ; return t.splice(0, n), Object.assign(t, s)
        }
      } class r {
        constructor() {
          this.rules = [], this.multiRegexes = [],
            this.count = 0, this.lastIndex = 0, this.regexIndex = 0;
        } getMatcher(e) {
          if (this.multiRegexes[e]) return this.multiRegexes[e]; const t = new s
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
        ; return e.classNameAliases = a(e.classNameAliases || {}), function t(s, i) {
          const o = s
            ; if (s.compiled) return o
              ;[O].forEach((e => e(s, i))), e.compilerExtensions.forEach((e => e(s, i))),
                s.__beforeBegin = null, [k, M, A].forEach((e => e(s, i))), s.compiled = !0; let l = null
            ; if ("object" == typeof s.keywords && (l = s.keywords.$pattern,
              delete s.keywords.$pattern), s.keywords && (s.keywords = ((e, t) => {
                const n = {}
                  ; return "string" == typeof e ? s("keyword", e) : Object.keys(e).forEach((t => {
                    s(t, e[t]);
                  })), n; function s(e, s) {
                    t && (s = s.toLowerCase()), s.split(" ").forEach((t => {
                      const s = t.split("|"); n[s[0]] = [e, B(s[0], s[1])];
                    }));
                  }
              })(s.keywords, e.case_insensitive)),
              s.lexemes && l) throw Error("ERR: Prefer `keywords.$pattern` to `mode.lexemes`, BOTH are not allowed. (see mode reference) ")
            ; return l = l || s.lexemes || /\w+/,
              o.keywordPatternRe = n(l, !0), i && (s.begin || (s.begin = /\B|\b/),
                o.beginRe = n(s.begin), s.endSameAsBegin && (s.end = s.begin),
                s.end || s.endsWithParent || (s.end = /\B|\b/),
                s.end && (o.endRe = n(s.end)), o.terminatorEnd = u(s.end) || "",
                s.endsWithParent && i.terminatorEnd && (o.terminatorEnd += (s.end ? "|" : "") + i.terminatorEnd)),
              s.illegal && (o.illegalRe = n(s.illegal)),
              s.contains || (s.contains = []), s.contains = [].concat(...s.contains.map((e => (e => (e.variants && !e.cachedVariants && (e.cachedVariants = e.variants.map((t => a(e, {
                variants: null
              }, t)))), e.cachedVariants ? e.cachedVariants : T(e) ? a(e, {
                starts: e.starts ? a(e.starts) : null
              }) : Object.isFrozen(e) ? a(e) : e))("self" === e ? s : e)))), s.contains.forEach((e => {
                t(e, o);
              })), s.starts && t(s.starts, i), o.matcher = (e => {
                const t = new r
                  ; return e.contains.forEach((e => t.addRule(e.begin, {
                    rule: e, type: "begin"
                  }))), e.terminatorEnd && t.addRule(e.terminatorEnd, {
                    type: "end"
                  }), e.illegal && t.addRule(e.illegal, { type: "illegal" }), t
              })(o), o
        }(e)
    } function T(e) {
      return !!e && (e.endsWithParent || T(e.starts))
    } function j(e) {
      const t = {
        props: ["language", "code", "autodetect"], data: () => ({
          detectedLanguage: "",
          unknownLanguage: !1
        }), computed: {
          className() {
            return this.unknownLanguage ? "" : "hljs " + this.detectedLanguage
          }, highlighted() {
            if (!this.autoDetect && !e.getLanguage(this.language)) return console.warn(`The language "${this.language}" you specified could not be found.`),
              this.unknownLanguage = !0, r(this.code); let t = {}
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
    } const S = {
      "after:highlightBlock": ({ block: e, result: t, text: n }) => {
        const s = D(e)
          ; if (!s.length) return; const a = document.createElement("div")
          ; a.innerHTML = t.value, t.value = ((e, t, n) => {
            let s = 0, a = ""; const i = []; function o() {
              return e.length && t.length ? e[0].offset !== t[0].offset ? e[0].offset < t[0].offset ? e : t : "start" === t[0].event ? e : t : e.length ? e : t
            } function l(e) {
              a += "<" + P(e) + [].map.call(e.attributes, (function (e) {
                return " " + e.nodeName + '="' + r(e.value) + '"'
              })).join("") + ">";
            } function c(e) {
              a += "</" + P(e) + ">";
            } function u(e) { ("start" === e.event ? l : c)(e.node); }
            for (; e.length || t.length;) {
              let t = o()
                ; if (a += r(n.substring(s, t[0].offset)), s = t[0].offset, t === e) {
                  i.reverse().forEach(c)
                    ; do { u(t.splice(0, 1)[0]), t = o(); } while (t === e && t.length && t[0].offset === s)
                    ; i.reverse().forEach(l);
                } else "start" === t[0].event ? i.push(t[0].node) : i.pop(), u(t.splice(0, 1)[0]);
            }
            return a + r(n.substr(s))
          })(s, D(a), n);
      }
    }; function P(e) {
      return e.nodeName.toLowerCase()
    } function D(e) {
      const t = []; return function e(n, s) {
        for (let r = n.firstChild; r; r = r.nextSibling)3 === r.nodeType ? s += r.nodeValue.length : 1 === r.nodeType && (t.push({
          event: "start", offset: s, node: r
        }), s = e(r, s), P(r).match(/br|hr|img|input/) || t.push({
          event: "stop", offset: s, node: r
        })); return s
      }(e, 0), t
    } const C = e => {
      console.error(e);
    }, H = (e, ...t) => { console.log("WARN: " + e, ...t); }, $ = (e, t) => {
      console.log(`Deprecated as of ${e}. ${t}`);
    }, U = r, z = a, K = Symbol("nomatch")
      ; return (e => {
        const n = Object.create(null), r = Object.create(null), a = []; let i = !0
          ; const o = /(^(<[^>]+>|\t|)+|\n)/gm, l = "Could not find the language '{}', did you forget to load/include a language module?", u = {
            disableAutodetect: !0, name: "Plain text", contains: []
          }; let g = {
            noHighlightRe: /^(no-?highlight)$/i,
            languageDetectRe: /\blang(?:uage)?-([\w-]+)\b/i, classPrefix: "hljs-",
            tabReplace: null, useBR: !1, languages: null, __emitter: c
          }; function d(e) {
            return g.noHighlightRe.test(e)
          } function h(e, t, n, s) {
            const r = { code: t, language: e }
              ; _("before:highlight", r); const a = r.result ? r.result : f(r.language, r.code, n, s)
              ; return a.code = r.code, _("after:highlight", a), a
          } function f(e, t, r, o) {
            const c = t
              ; function u(e, t) {
                const n = w.case_insensitive ? t[0].toLowerCase() : t[0]
                  ; return Object.prototype.hasOwnProperty.call(e.keywords, n) && e.keywords[n]
              }
            function d() {
              null != _.subLanguage ? (() => {
                if ("" === O) return; let e = null
                  ; if ("string" == typeof _.subLanguage) {
                    if (!n[_.subLanguage]) return void M.addText(O)
                      ; e = f(_.subLanguage, O, !0, k[_.subLanguage]), k[_.subLanguage] = e.top;
                  } else e = p(O, _.subLanguage.length ? _.subLanguage : null)
                  ; _.relevance > 0 && (A += e.relevance), M.addSublanguage(e.emitter, e.language);
              })() : (() => {
                if (!_.keywords) return void M.addText(O); let e = 0
                  ; _.keywordPatternRe.lastIndex = 0; let t = _.keywordPatternRe.exec(O), n = ""; for (; t;) {
                    n += O.substring(e, t.index); const s = u(_, t); if (s) {
                      const [e, r] = s
                        ; M.addText(n), n = "", A += r; const a = w.classNameAliases[e] || e; M.addKeyword(t[0], a);
                    } else n += t[0]; e = _.keywordPatternRe.lastIndex, t = _.keywordPatternRe.exec(O);
                  }
                n += O.substr(e), M.addText(n);
              })(), O = "";
            } function h(e) {
              return e.className && M.openNode(w.classNameAliases[e.className] || e.className),
                _ = Object.create(e, { parent: { value: _ } }), _
            } function m(e, t, n) {
              let r = ((e, t) => {
                const n = e && e.exec(t); return n && 0 === n.index
              })(e.endRe, n); if (r) {
                if (e["on:end"]) {
                  const n = new s(e); e["on:end"](t, n), n.ignore && (r = !1);
                } if (r) {
                  for (; e.endsParent && e.parent;)e = e.parent; return e
                }
              }
              if (e.endsWithParent) return m(e.parent, t, n)
            } function b(e) {
              return 0 === _.matcher.regexIndex ? (O += e[0], 1) : (T = !0, 0)
            } function x(e) {
              const t = e[0], n = c.substr(e.index), s = m(_, e, n); if (!s) return K; const r = _
                ; r.skip ? O += t : (r.returnEnd || r.excludeEnd || (O += t), d(), r.excludeEnd && (O = t)); do {
                  _.className && M.closeNode(), _.skip || _.subLanguage || (A += _.relevance), _ = _.parent;
                } while (_ !== s.parent)
                ; return s.starts && (s.endSameAsBegin && (s.starts.endRe = s.endRe),
                  h(s.starts)), r.returnEnd ? 0 : t.length
            } let E = {}; function v(t, n) {
              const a = n && n[0]
                ; if (O += t, null == a) return d(), 0
                  ; if ("begin" === E.type && "end" === n.type && E.index === n.index && "" === a) {
                    if (O += c.slice(n.index, n.index + 1), !i) {
                      const t = Error("0 width match regex")
                        ; throw t.languageName = e, t.badRule = E.rule, t
                    } return 1
                  }
              if (E = n, "begin" === n.type) return function (e) {
                const t = e[0], n = e.rule, r = new s(n), a = [n.__beforeBegin, n["on:begin"]]
                  ; for (const n of a) if (n && (n(e, r), r.ignore)) return b(t)
                    ; return n && n.endSameAsBegin && (n.endRe = RegExp(t.replace(/[-/\\^$*+?.()|[\]{}]/g, "\\$&"), "m")),
                      n.skip ? O += t : (n.excludeBegin && (O += t),
                        d(), n.returnBegin || n.excludeBegin || (O = t)), h(n), n.returnBegin ? 0 : t.length
              }(n)
                ; if ("illegal" === n.type && !r) {
                  const e = Error('Illegal lexeme "' + a + '" for mode "' + (_.className || "<unnamed>") + '"')
                    ; throw e.mode = _, e
                } if ("end" === n.type) { const e = x(n); if (e !== K) return e }
              if ("illegal" === n.type && "" === a) return 1
                ; if (B > 1e5 && B > 3 * n.index) throw Error("potential infinite loop, way more iterations than matches")
                  ; return O += a, a.length
            } const w = N(e)
              ; if (!w) throw C(l.replace("{}", e)), Error('Unknown language: "' + e + '"')
                ; const R = I(w, { plugins: a }); let y = "", _ = o || R; const k = {}, M = new g.__emitter(g); (() => {
                  const e = []; for (let t = _; t !== w; t = t.parent)t.className && e.unshift(t.className)
                    ; e.forEach((e => M.openNode(e)));
                })(); let O = "", A = 0, L = 0, B = 0, T = !1; try {
                  for (_.matcher.considerAll(); ;) {
                    B++, T ? T = !1 : _.matcher.considerAll(), _.matcher.lastIndex = L
                      ; const e = _.matcher.exec(c); if (!e) break; const t = v(c.substring(L, e.index), e)
                      ; L = e.index + t;
                  } return v(c.substr(L)), M.closeAllNodes(), M.finalize(), y = M.toHTML(), {
                    relevance: A, value: y, language: e, illegal: !1, emitter: M, top: _
                  }
                } catch (t) {
                  if (t.message && t.message.includes("Illegal")) return {
                    illegal: !0, illegalBy: {
                      msg: t.message, context: c.slice(L - 100, L + 100), mode: t.mode
                    }, sofar: y, relevance: 0,
                    value: U(c), emitter: M
                  }; if (i) return {
                    illegal: !1, relevance: 0, value: U(c), emitter: M,
                    language: e, top: _, errorRaised: t
                  }; throw t
                }
          } function p(e, t) {
            t = t || g.languages || Object.keys(n); const s = (e => {
              const t = {
                relevance: 0,
                emitter: new g.__emitter(g), value: U(e), illegal: !1, top: u
              }
                ; return t.emitter.addText(e), t
            })(e), r = t.filter(N).filter(R).map((t => f(t, e, !1)))
              ; r.unshift(s); const a = r.sort(((e, t) => {
                if (e.relevance !== t.relevance) return t.relevance - e.relevance
                  ; if (e.language && t.language) {
                    if (N(e.language).supersetOf === t.language) return 1
                      ; if (N(t.language).supersetOf === e.language) return -1
                  } return 0
              })), [i, o] = a, l = i
              ; return l.second_best = o, l
          } const m = {
            "before:highlightBlock": ({ block: e }) => {
              g.useBR && (e.innerHTML = e.innerHTML.replace(/\n/g, "").replace(/<br[ /]*>/g, "\n"));
            }, "after:highlightBlock": ({ result: e }) => {
              g.useBR && (e.value = e.value.replace(/\n/g, "<br>"));
            }
          }, b = /^(<[^>]+>|\t)+/gm, x = {
            "after:highlightBlock": ({ result: e }) => {
              g.tabReplace && (e.value = e.value.replace(b, (e => e.replace(/\t/g, g.tabReplace))));
            }
          }
          ; function E(e) {
            let t = null; const n = (e => {
              let t = e.className + " "
                ; t += e.parentNode ? e.parentNode.className : ""; const n = g.languageDetectRe.exec(t)
                ; if (n) {
                  const t = N(n[1])
                    ; return t || (H(l.replace("{}", n[1])), H("Falling back to no-highlight mode for this block.", e)),
                      t ? n[1] : "no-highlight"
                } return t.split(/\s+/).find((e => d(e) || N(e)))
            })(e)
              ; if (d(n)) return; _("before:highlightBlock", { block: e, language: n }), t = e
              ; const s = t.textContent, a = n ? h(n, s, !0) : p(s); _("after:highlightBlock", {
                block: e,
                result: a, text: s
              }), e.innerHTML = a.value, ((e, t, n) => {
                const s = t ? r[t] : n
                  ; e.classList.add("hljs"), s && e.classList.add(s);
              })(e, n, a.language), e.result = {
                language: a.language, re: a.relevance, relavance: a.relevance
              }, a.second_best && (e.second_best = {
                language: a.second_best.language,
                re: a.second_best.relevance, relavance: a.second_best.relevance
              });
          } const v = () => {
            v.called || (v.called = !0, document.querySelectorAll("pre code").forEach(E));
          }
          ; function N(e) { return e = (e || "").toLowerCase(), n[e] || n[r[e]] }
        function w(e, { languageName: t }) {
          "string" == typeof e && (e = [e]), e.forEach((e => {
            r[e] = t;
          }));
        } function R(e) { const t = N(e); return t && !t.disableAutodetect } function _(e, t) {
          const n = e; a.forEach((e => { e[n] && e[n](t); }));
        } Object.assign(e, {
          highlight: h,
          highlightAuto: p, fixMarkup: e => {
            return $("10.2.0", "fixMarkup will be removed entirely in v11.0"),
              $("10.2.0", "Please see https://github.com/highlightjs/highlight.js/issues/2534"),
              t = e,
              g.tabReplace || g.useBR ? t.replace(o, (e => "\n" === e ? g.useBR ? "<br>" : e : g.tabReplace ? e.replace(/\t/g, g.tabReplace) : e)) : t
              ; var t;
          }, highlightBlock: E, configure: e => {
            e.useBR && ($("10.3.0", "'useBR' will be removed entirely in v11.0"),
              $("10.3.0", "Please see https://github.com/highlightjs/highlight.js/issues/2559")),
              g = z(g, e);
          }, initHighlighting: v, initHighlightingOnLoad: () => {
            window.addEventListener("DOMContentLoaded", v, !1);
          }, registerLanguage: (t, s) => {
            let r = null; try { r = s(e); } catch (e) {
              if (C("Language definition for '{}' could not be registered.".replace("{}", t)),
                !i) throw e; C(e), r = u;
            }
            r.name || (r.name = t), n[t] = r, r.rawDefinition = s.bind(null, e), r.aliases && w(r.aliases, {
              languageName: t
            });
          }, listLanguages: () => Object.keys(n), getLanguage: N,
          registerAliases: w, requireLanguage: e => {
            $("10.4.0", "requireLanguage will be removed entirely in v11."),
              $("10.4.0", "Please see https://github.com/highlightjs/highlight.js/pull/2844")
              ; const t = N(e); if (t) return t
                ; throw Error("The '{}' language is required, but not loaded.".replace("{}", e))
          },
          autoDetection: R, inherit: z, addPlugin: e => { a.push(e); }, vuePlugin: j(e).VuePlugin
        }), e.debugMode = () => { i = !1; }, e.safeMode = () => { i = !0; }, e.versionString = "10.5.0"
          ; for (const e in y) "object" == typeof y[e] && t(y[e])
            ; return Object.assign(e, y), e.addPlugin(m), e.addPlugin(S), e.addPlugin(x), e
      })({})
  }(); "object" == typeof exports && "undefined" != typeof module && (module.exports = hljs); hljs.registerLanguage("apache", (() => {
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
  })()); hljs.registerLanguage("properties", (() => {
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
  })()); hljs.registerLanguage("diff", (() => {
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
  })()); hljs.registerLanguage("cpp", (() => {
   function e(e) {
      return ((...e) => e.map((e => (e => e ? "string" == typeof e ? e : e.source : null)(e))).join(""))("(", e, ")?")
    } return t => {
      const n = (t => {
        const n = t.COMMENT("//", "$", {
          contains: [{ begin: /\\\n/ }]
        }), r = "[a-zA-Z_]\\w*::", a = "(decltype\\(auto\\)|" + e(r) + "[a-zA-Z_]\\w*" + e("<[^<>]+>") + ")", s = {
          className: "keyword", begin: "\\b[a-z\\d_]*_t\\b"
        }, i = {
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
        }, c = {
          className: "number", variants: [{ begin: "\\b(0b[01']+)" }, {
            begin: "(-?)\\b([\\d']+(\\.[\\d']*)?|\\.[\\d']+)((ll|LL|l|L)(u|U)?|(u|U)(ll|LL|l|L)?|f|F|b|B)"
          }, {
            begin: "(-?)(\\b0[xX][a-fA-F0-9']+|(\\b[\\d']+(\\.[\\d']*)?|\\.[\\d']+)([eE][-+]?[\\d']+)?)"
          }], relevance: 0
        }, o = {
          className: "meta", begin: /#\s*[a-z]+\b/, end: /$/, keywords: {
            "meta-keyword": "if else elif endif define undef warning error line pragma _Pragma ifdef ifndef include"
          }, contains: [{ begin: /\\\n/, relevance: 0 }, t.inherit(i, { className: "meta-string" }), {
            className: "meta-string", begin: /<.*?>/, end: /$/, illegal: "\\n"
          }, n, t.C_BLOCK_COMMENT_MODE]
        }, l = {
          className: "title", begin: e(r) + t.IDENT_RE,
          relevance: 0
        }, d = e(r) + t.IDENT_RE + "\\s*\\(", u = {
          keyword: "int float while private char char8_t char16_t char32_t catch import module export virtual operator sizeof dynamic_cast|10 typedef const_cast|10 const for static_cast|10 union namespace unsigned long volatile static protected bool template mutable if public friend do goto auto void enum else break extern using asm case typeid wchar_t short reinterpret_cast|10 default double register explicit signed typename try this switch continue inline delete alignas alignof constexpr consteval constinit decltype concept co_await co_return co_yield requires noexcept static_assert thread_local restrict final override atomic_bool atomic_char atomic_schar atomic_uchar atomic_short atomic_ushort atomic_int atomic_uint atomic_long atomic_ulong atomic_llong atomic_ullong new throw return and and_eq bitand bitor compl not not_eq or or_eq xor xor_eq",
          built_in: "std string wstring cin cout cerr clog stdin stdout stderr stringstream istringstream ostringstream auto_ptr deque list queue stack vector map set pair bitset multiset multimap unordered_set unordered_map unordered_multiset unordered_multimap priority_queue make_pair array shared_ptr abort terminate abs acos asin atan2 atan calloc ceil cosh cos exit exp fabs floor fmod fprintf fputs free frexp fscanf future isalnum isalpha iscntrl isdigit isgraph islower isprint ispunct isspace isupper isxdigit tolower toupper labs ldexp log10 log malloc realloc memchr memcmp memcpy memset modf pow printf putchar puts scanf sinh sin snprintf sprintf sqrt sscanf strcat strchr strcmp strcpy strcspn strlen strncat strncmp strncpy strpbrk strrchr strspn strstr tanh tan vfprintf vprintf vsprintf endl initializer_list unique_ptr _Bool complex _Complex imaginary _Imaginary",
          literal: "true false nullptr NULL"
        }, p = [o, s, n, t.C_BLOCK_COMMENT_MODE, c, i], m = {
          variants: [{ begin: /=/, end: /;/ }, { begin: /\(/, end: /\)/ }, {
            beginKeywords: "new throw return else", end: /;/
          }], keywords: u, contains: p.concat([{
            begin: /\(/, end: /\)/, keywords: u, contains: p.concat(["self"]), relevance: 0
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
            end: /\)/, keywords: u, relevance: 0, contains: [n, t.C_BLOCK_COMMENT_MODE, i, c, s, {
              begin: /\(/, end: /\)/, keywords: u, relevance: 0,
              contains: ["self", n, t.C_BLOCK_COMMENT_MODE, i, c, s]
            }]
          }, s, n, t.C_BLOCK_COMMENT_MODE, o]
        }; return {
          aliases: ["c", "cc", "h", "c++", "h++", "hpp", "hh", "hxx", "cxx"], keywords: u,
          disableAutodetect: !0, illegal: "</", contains: [].concat(m, _, p, [o, {
            begin: "\\b(deque|list|queue|priority_queue|pair|stack|vector|map|set|bitset|multiset|multimap|unordered_map|unordered_set|unordered_multiset|unordered_multimap|array)\\s*<",
            end: ">", keywords: u, contains: ["self", s]
          }, { begin: t.IDENT_RE + "::", keywords: u }, {
              className: "class", beginKeywords: "enum class struct union", end: /[{;:<>=]/,
              contains: [{ beginKeywords: "final class struct" }, t.TITLE_MODE]
            }]), exports: {
              preprocessor: o, strings: i, keywords: u
            }
        }
      })(t)
        ; return n.disableAutodetect = !1, n.name = "C++",
          n.aliases = ["cc", "c++", "h++", "hpp", "hh", "hxx", "cxx"], n
    }
  })()); hljs.registerLanguage("kotlin", (() => {
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
          name: "Kotlin", aliases: ["kt"], keywords: n, contains: [e.COMMENT("/\\*\\*", "\\*/", {
            relevance: 0, contains: [{ className: "doctag", begin: "@[A-Za-z]+" }]
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
  })()); hljs.registerLanguage("ruby", (() => {
   function e(...e) {
      return e.map((e => {
        return (n = e) ? "string" == typeof n ? n : n.source : null; var n;
      })).join("")
    } return n => {
      var a, i = "([a-zA-Z_]\\w*[!?=]?|[-+~]@|<<|>>|=~|===?|<=>|[<>]=?|\\*\\*|[-/+%^&*~`|]|\\[\\]=?)", s = {
        keyword: "and then defined module in return redo if BEGIN retry end for self when next until do begin unless END rescue else break undef not super class case require yield alias while ensure elsif or include attr_reader attr_writer attr_accessor __FILE__",
        built_in: "proc lambda", literal: "true false nil"
      }, r = {
        className: "doctag",
        begin: "@[A-Za-z]+"
      }, b = { begin: "#<", end: ">" }, t = [n.COMMENT("#", "$", {
        contains: [r]
      }), n.COMMENT("^=begin", "^=end", {
        contains: [r], relevance: 10
      }), n.COMMENT("^__END__", "\\n$")], c = {
        className: "subst", begin: /#\{/, end: /\}/,
        keywords: s
      }, d = {
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
          begin: /\B\?(\\\d{1,3}|\\x[A-Fa-f0-9]{1,2}|\\u[A-Fa-f0-9]{4}|\\?\S)\b/
        }, {
          begin: /<<[-~]?'?(\w+)\n(?:[^\n]*\n)*?\s*\1\b/, returnBegin: !0, contains: [{
            begin: /<<[-~]?'?/
          }, n.END_SAME_AS_BEGIN({
            begin: /(\w+)/, end: /(\w+)/,
            contains: [n.BACKSLASH_ESCAPE, c]
          })]
        }]
      }, g = "[0-9](_?[0-9])*", l = {
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
      }, o = {
        className: "params", begin: "\\(", end: "\\)",
        endsParent: !0, keywords: s
      }, _ = [d, {
        className: "class", beginKeywords: "class module",
        end: "$|;", illegal: /=/, contains: [n.inherit(n.TITLE_MODE, {
          begin: "[A-Za-z_]\\w*(::\\w+)*(\\?|!)?"
        }), {
          begin: "<\\s*", contains: [{
            begin: "(" + n.IDENT_RE + "::)?" + n.IDENT_RE
          }]
        }].concat(t)
      }, {
          className: "function",
          begin: e(/def\s*/, (a = i + "\\s*(\\(|;|$)", e("(?=", a, ")"))), keywords: "def", end: "$|;",
          contains: [n.inherit(n.TITLE_MODE, { begin: i }), o].concat(t)
        }, {
          begin: n.IDENT_RE + "::"
        }, { className: "symbol", begin: n.UNDERSCORE_IDENT_RE + "(!|\\?)?:", relevance: 0 }, {
          className: "symbol", begin: ":(?!\\s)", contains: [d, { begin: i }], relevance: 0
        }, l, {
          className: "variable",
          begin: "(\\$\\W)|((\\$|@@?)(\\w+))(?=[^@$?])(?![A-Za-z])(?![@$?'])"
        }, {
          className: "params", begin: /\|/, end: /\|/, relevance: 0, keywords: s
        }, {
          begin: "(" + n.RE_STARTERS_RE + "|unless)\\s*", keywords: "unless", contains: [{
            className: "regexp", contains: [n.BACKSLASH_ESCAPE, c], illegal: /\n/, variants: [{
              begin: "/", end: "/[a-z]*"
            }, { begin: /%r\{/, end: /\}[a-z]*/ }, {
              begin: "%r\\(",
              end: "\\)[a-z]*"
            }, { begin: "%r!", end: "![a-z]*" }, { begin: "%r\\[", end: "\\][a-z]*" }]
          }].concat(b, t), relevance: 0
        }].concat(b, t); c.contains = _, o.contains = _; var E = [{
          begin: /^\s*=>/, starts: { end: "$", contains: _ }
        }, {
          className: "meta",
          begin: "^([>?]>|[\\w#]+\\(\\w+\\):\\d+:\\d+>|(\\w+-)?\\d+\\.\\d+\\.\\d+(p\\d+)?[^\\d][^>]+>)(?=[ ])",
          starts: { end: "$", contains: _ }
        }]; return t.unshift(b), {
          name: "Ruby",
          aliases: ["rb", "gemspec", "podspec", "thor", "irb"], keywords: s, illegal: /\/\*/,
          contains: [n.SHEBANG({ binary: "ruby" })].concat(E).concat(t).concat(_)
        }
    }
  })()); hljs.registerLanguage("swift", (() => {
   function e(e) {
      return e ? "string" == typeof e ? e : e.source : null
    } function n(e) { return i("(?=", e, ")") }
    function i(...n) { return n.map((n => e(n))).join("") } function a(...n) {
      return "(" + n.map((n => e(n))).join("|") + ")"
    }
    const t = e => i(/\b/, e, /\w$/.test(e) ? /\b/ : /\B/), u = ["Protocol", "Type"].map(t), s = ["init", "self"].map(t), r = ["Any", "Self"], o = ["associatedtype", /as\?/, /as!/, "as", "break", "case", "catch", "class", "continue", "convenience", "default", "defer", "deinit", "didSet", "do", "dynamic", "else", "enum", "extension", "fallthrough", "fileprivate(set)", "fileprivate", "final", "for", "func", "get", "guard", "if", "import", "indirect", "infix", /init\?/, /init!/, "inout", "internal(set)", "internal", "in", "is", "lazy", "let", "mutating", "nonmutating", "open(set)", "open", "operator", "optional", "override", "postfix", "precedencegroup", "prefix", "private(set)", "private", "protocol", "public(set)", "public", "repeat", "required", "rethrows", "return", "set", "some", "static", "struct", "subscript", "super", "switch", "throws", "throw", /try\?/, /try!/, "try", "typealias", "unowned(safe)", "unowned(unsafe)", "unowned", "var", "weak", "where", "while", "willSet"], l = ["false", "nil", "true"], c = ["#colorLiteral", "#column", "#dsohandle", "#else", "#elseif", "#endif", "#error", "#file", "#fileID", "#fileLiteral", "#filePath", "#function", "#if", "#imageLiteral", "#keyPath", "#line", "#selector", "#sourceLocation", "#warn_unqualified_access", "#warning"], b = ["abs", "all", "any", "assert", "assertionFailure", "debugPrint", "dump", "fatalError", "getVaList", "isKnownUniquelyReferenced", "max", "min", "numericCast", "pointwiseMax", "pointwiseMin", "precondition", "preconditionFailure", "print", "readLine", "repeatElement", "sequence", "stride", "swap", "swift_unboxFromSwiftValueWithType", "transcode", "type", "unsafeBitCast", "unsafeDowncast", "withExtendedLifetime", "withUnsafeMutablePointer", "withUnsafePointer", "withVaList", "withoutActuallyEscaping", "zip"], p = a(/[/=\-+!*%<>&|^~?]/, /[\u00A1-\u00A7]/, /[\u00A9\u00AB]/, /[\u00AC\u00AE]/, /[\u00B0\u00B1]/, /[\u00B6\u00BB\u00BF\u00D7\u00F7]/, /[\u2016-\u2017]/, /[\u2020-\u2027]/, /[\u2030-\u203E]/, /[\u2041-\u2053]/, /[\u2055-\u205E]/, /[\u2190-\u23FF]/, /[\u2500-\u2775]/, /[\u2794-\u2BFF]/, /[\u2E00-\u2E7F]/, /[\u3001-\u3003]/, /[\u3008-\u3020]/, /[\u3030]/), F = a(p, /[\u0300-\u036F]/, /[\u1DC0-\u1DFF]/, /[\u20D0-\u20FF]/, /[\uFE00-\uFE0F]/, /[\uFE20-\uFE2F]/), d = i(p, F, "*"), g = a(/[a-zA-Z_]/, /[\u00A8\u00AA\u00AD\u00AF\u00B2-\u00B5\u00B7-\u00BA]/, /[\u00BC-\u00BE\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u00FF]/, /[\u0100-\u02FF\u0370-\u167F\u1681-\u180D\u180F-\u1DBF]/, /[\u1E00-\u1FFF]/, /[\u200B-\u200D\u202A-\u202E\u203F-\u2040\u2054\u2060-\u206F]/, /[\u2070-\u20CF\u2100-\u218F\u2460-\u24FF\u2776-\u2793]/, /[\u2C00-\u2DFF\u2E80-\u2FFF]/, /[\u3004-\u3007\u3021-\u302F\u3031-\u303F\u3040-\uD7FF]/, /[\uF900-\uFD3D\uFD40-\uFDCF\uFDF0-\uFE1F\uFE30-\uFE44]/, /[\uFE47-\uFFFD]/), f = a(g, /\d/, /[\u0300-\u036F\u1DC0-\u1DFF\u20D0-\u20FF\uFE20-\uFE2F]/), m = i(g, f, "*"), w = i(/[A-Z]/, f, "*"), E = ["autoclosure", i(/convention\(/, a("swift", "block", "c"), /\)/), "discardableResult", "dynamicCallable", "dynamicMemberLookup", "escaping", "frozen", "GKInspectable", "IBAction", "IBDesignable", "IBInspectable", "IBOutlet", "IBSegueAction", "inlinable", "main", "nonobjc", "NSApplicationMain", "NSCopying", "NSManaged", i(/objc\(/, m, /\)/), "objc", "objcMembers", "propertyWrapper", "requires_stored_property_inits", "testable", "UIApplicationMain", "unknown", "usableFromInline"], y = ["iOS", "iOSApplicationExtension", "macOS", "macOSApplicationExtension", "macCatalyst", "macCatalystApplicationExtension", "watchOS", "watchOSApplicationExtension", "tvOS", "tvOSApplicationExtension", "swift"]
      ; return e => {
        const p = e.COMMENT("/\\*", "\\*/", { contains: ["self"] }), g = {
          className: "keyword", begin: i(/\./, n(a(...u, ...s))), end: a(...u, ...s),
          excludeBegin: !0
        }, A = {
          begin: i(/\./, a(...o)), relevance: 0
        }, C = o.filter((e => "string" == typeof e)).concat(["_|0"]), v = {
          variants: [{
            className: "keyword",
            begin: a(...o.filter((e => "string" != typeof e)).concat(r).map(t), ...s)
          }]
        }, _ = {
          $pattern: a(/\b\w+(\(\w+\))?/, /#\w+/), keyword: C.concat(c).join(" "),
          literal: l.join(" ")
        }, N = [g, A, v], D = [{ begin: i(/\./, a(...b)), relevance: 0 }, {
          className: "built_in", begin: i(/\b/, a(...b), /(?=\()/)
        }], B = {
          begin: /->/, relevance: 0
        }, M = [B, {
          className: "operator", relevance: 0, variants: [{ begin: d }, {
            begin: `\\.(\\.|${F})+`
          }]
        }], h = "([0-9a-fA-F]_*)+", S = {
          className: "number",
          relevance: 0, variants: [{
            begin: "\\b(([0-9]_*)+)(\\.(([0-9]_*)+))?([eE][+-]?(([0-9]_*)+))?\\b"
          }, {
            begin: `\\b0x(${h})(\\.(${h}))?([pP][+-]?(([0-9]_*)+))?\\b`
          }, {
            begin: /\b0o([0-7]_*)+\b/
          }, { begin: /\b0b([01]_*)+\b/ }]
        }, O = (e = "") => ({
          className: "subst", variants: [{ begin: i(/\\/, e, /[0\\tnr"']/) }, {
            begin: i(/\\/, e, /u\{[0-9a-fA-F]{1,8}\}/)
          }]
        }), x = (e = "") => ({
          className: "subst",
          begin: i(/\\/, e, /[\t ]*(?:[\r\n]|\r\n)/)
        }), k = (e = "") => ({
          className: "subst",
          label: "interpol", begin: i(/\\/, e, /\(/), end: /\)/
        }), L = (e = "") => ({
          begin: i(e, /"""/),
          end: i(/"""/, e), contains: [O(e), x(e), k(e)]
        }), I = (e = "") => ({
          begin: i(e, /"/),
          end: i(/"/, e), contains: [O(e), k(e)]
        }), $ = {
          className: "string",
          variants: [L(), L("#"), L("##"), L("###"), I(), I("#"), I("##"), I("###")]
        }, T = [{
          begin: i(/`/, m, /`/)
        }, { className: "variable", begin: /\$\d+/ }, {
          className: "variable",
          begin: `\\$${f}+`
        }], j = [{
          begin: /(@|#)available\(/, end: /\)/, keywords: {
            $pattern: /[@#]?\w+/, keyword: y.concat(["@available", "#available"]).join(" ")
          },
          contains: [...M, S, $]
        }, { className: "keyword", begin: i(/@/, a(...E)) }, {
          className: "meta", begin: i(/@/, m)
        }], K = {
          begin: n(/\b[A-Z]/), relevance: 0, contains: [{
            className: "type",
            begin: i(/(AV|CA|CF|CG|CI|CL|CM|CN|CT|MK|MP|MTK|MTL|NS|SCN|SK|UI|WK|XC)/, f, "+")
          }, { className: "type", begin: w, relevance: 0 }, { begin: /[?!]+/, relevance: 0 }, {
            begin: /\.\.\./, relevance: 0
          }, { begin: i(/\s+&\s+/, n(w)), relevance: 0 }]
        }, P = {
          begin: /</, end: />/, keywords: _, contains: [...N, ...j, B, K]
        }; K.contains.push(P)
          ; for (const e of $.variants) {
            const n = e.contains.find((e => "interpol" === e.label))
              ; n.keywords = _; const i = [...N, ...D, ...M, S, $, ...T]; n.contains = [...i, {
                begin: /\(/,
                end: /\)/, contains: ["self", ...i]
              }];
          } return {
            name: "Swift", keywords: _,
            contains: [e.C_LINE_COMMENT_MODE, p, {
              className: "function", beginKeywords: "func",
              end: /\{/, excludeEnd: !0, contains: [e.inherit(e.TITLE_MODE, {
                begin: /[A-Za-z$_][0-9A-Za-z$_]*/
              }), { begin: /</, end: />/ }, {
                className: "params",
                begin: /\(/, end: /\)/, endsParent: !0, keywords: _,
                contains: ["self", ...N, S, $, e.C_BLOCK_COMMENT_MODE, { begin: ":" }], illegal: /["']/
              }],
              illegal: /\[|%/
            }, {
              className: "class",
              beginKeywords: "struct protocol class extension enum", end: "\\{", excludeEnd: !0,
              keywords: _, contains: [e.inherit(e.TITLE_MODE, {
                begin: /[A-Za-z$_][\u00C0-\u02B80-9A-Za-z$_]*/
              }), ...N]
            }, {
              beginKeywords: "import",
              end: /$/, contains: [e.C_LINE_COMMENT_MODE, p], relevance: 0
            }, ...N, ...D, ...M, S, $, ...T, ...j, K]
          }
      }
  })()); hljs.registerLanguage("http", (() => {
   function e(...e) {
      return e.map((e => {
        return (n = e) ? "string" == typeof n ? n : n.source : null; var n;
      })).join("")
    } return n => {
      const a = "HTTP/(2|1\\.[01])", s = [{
        className: "attribute",
        begin: e("^", /[A-Za-z][A-Za-z0-9-]*/, "(?=\\:\\s)"), starts: {
          contains: [{
            className: "punctuation", begin: /: /, relevance: 0, starts: { end: "$", relevance: 0 }
          }]
        }
      }, { begin: "\\n\\n", starts: { subLanguage: [], endsWithParent: !0 } }]; return {
        name: "HTTP", aliases: ["https"], illegal: /\S/, contains: [{
          begin: "^(?=" + a + " \\d{3})",
          end: /$/, contains: [{ className: "meta", begin: a }, {
            className: "number",
            begin: "\\b\\d{3}\\b"
          }], starts: { end: /\b\B/, illegal: /\S/, contains: s }
        }, {
          begin: "(?=^[A-Z]+ (.*?) " + a + "$)", end: /$/, contains: [{
            className: "string",
            begin: " ", end: " ", excludeBegin: !0, excludeEnd: !0
          }, { className: "meta", begin: a }, {
            className: "keyword", begin: "[A-Z]+"
          }], starts: { end: /\b\B/, illegal: /\S/, contains: s }
        }]
      }
    }
  })()); hljs.registerLanguage("python", (() => {
   return e => {
      const n = {
        keyword: "and as assert async await break class continue def del elif else except finally for  from global if import in is lambda nonlocal|10 not or pass raise return try while with yield",
        built_in: "__import__ abs all any ascii bin bool breakpoint bytearray bytes callable chr classmethod compile complex delattr dict dir divmod enumerate eval exec filter float format frozenset getattr globals hasattr hash help hex id input int isinstance issubclass iter len list locals map max memoryview min next object oct open ord pow print property range repr reversed round set setattr slice sorted staticmethod str sum super tuple type vars zip",
        literal: "__debug__ Ellipsis False None NotImplemented True"
      }, a = {
        className: "meta", begin: /^(>>>|\.\.\.) /
      }, s = {
        className: "subst", begin: /\{/,
        end: /\}/, keywords: n, illegal: /#/
      }, i = { begin: /\{\{/, relevance: 0 }, r = {
        className: "string", contains: [e.BACKSLASH_ESCAPE], variants: [{
          begin: /([uU]|[bB]|[rR]|[bB][rR]|[rR][bB])?'''/, end: /'''/,
          contains: [e.BACKSLASH_ESCAPE, a], relevance: 10
        }, {
          begin: /([uU]|[bB]|[rR]|[bB][rR]|[rR][bB])?"""/, end: /"""/,
          contains: [e.BACKSLASH_ESCAPE, a], relevance: 10
        }, {
          begin: /([fF][rR]|[rR][fF]|[fF])'''/, end: /'''/,
          contains: [e.BACKSLASH_ESCAPE, a, i, s]
        }, {
          begin: /([fF][rR]|[rR][fF]|[fF])"""/,
          end: /"""/, contains: [e.BACKSLASH_ESCAPE, a, i, s]
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
          contains: [e.BACKSLASH_ESCAPE, i, s]
        }, {
          begin: /([fF][rR]|[rR][fF]|[fF])"/, end: /"/,
          contains: [e.BACKSLASH_ESCAPE, i, s]
        }, e.APOS_STRING_MODE, e.QUOTE_STRING_MODE]
      }, t = "[0-9](_?[0-9])*", l = `(\\b(${t}))?\\.(${t})|\\b(${t})\\.`, b = {
        className: "number", relevance: 0, variants: [{
          begin: `(\\b(${t})|(${l}))[eE][+-]?(${t})[jJ]?\\b`
        }, { begin: `(${l})[jJ]?` }, {
          begin: "\\b([1-9](_?[0-9])*|0+(_?0)*)[lLjJ]?\\b"
        }, {
          begin: "\\b0[bB](_?[01])+[lL]?\\b"
        }, { begin: "\\b0[oO](_?[0-7])+[lL]?\\b" }, {
          begin: "\\b0[xX](_?[0-9a-fA-F])+[lL]?\\b"
        }, { begin: `\\b(${t})[jJ]\\b` }]
      }, o = {
        className: "params", variants: [{ begin: /\(\s*\)/, skip: !0, className: null }, {
          begin: /\(/, end: /\)/, excludeBegin: !0, excludeEnd: !0, keywords: n,
          contains: ["self", a, b, r, e.HASH_COMMENT_MODE]
        }]
      }; return s.contains = [r, b, a], {
        name: "Python", aliases: ["py", "gyp", "ipython"], keywords: n,
        illegal: /(<\/|->|\?)|=>/, contains: [a, b, { begin: /\bself\b/ }, {
          beginKeywords: "if",
          relevance: 0
        }, r, e.HASH_COMMENT_MODE, {
            variants: [{
              className: "function",
              beginKeywords: "def"
            }, { className: "class", beginKeywords: "class" }], end: /:/,
            illegal: /[${=;\n,]/, contains: [e.UNDERSCORE_TITLE_MODE, o, {
              begin: /->/,
              endsWithParent: !0, keywords: "None"
            }]
          }, {
            className: "meta", begin: /^[\t ]*@/,
            end: /(?=#)|$/, contains: [b, o, r]
          }, { begin: /\b(print|exec)\(/ }]
      }
    }
  })()); hljs.registerLanguage("python-repl", (() => {
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
  })()); hljs.registerLanguage("java", (() => {
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
            excludeEnd: !0, keywords: "class interface enum", illegal: /[:"\[\]]/, contains: [{
              beginKeywords: "extends implements"
            }, e.UNDERSCORE_TITLE_MODE]
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
  })()); hljs.registerLanguage("nginx", (() => {
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
  })()); hljs.registerLanguage("xml", (() => {
   function e(e) {
      return e ? "string" == typeof e ? e : e.source : null
    } function n(e) { return a("(?=", e, ")") }
    function a(...n) { return n.map((n => e(n))).join("") } function s(...n) {
      return "(" + n.map((n => e(n))).join("|") + ")"
    } return e => {
      const t = a(/[A-Z_]/, a("(", /[A-Z0-9_.-]+:/, ")?"), /[A-Z0-9_.-]*/), i = {
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
          contains: [{ className: "name", begin: t, relevance: 0 }, { begin: />/, relevance: 0 }]
        }]
      }
    }
  })()); hljs.registerLanguage("markdown", (() => {
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
  })()); hljs.registerLanguage("yaml", (() => {
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
        end: ",", endsWithParent: !0, excludeEnd: !0, contains: [], keywords: n, relevance: 0
      }, t = {
        begin: /\{/, end: /\}/, contains: [l], illegal: "\\n", relevance: 0
      }, g = {
        begin: "\\[",
        end: "\\]", contains: [l], illegal: "\\n", relevance: 0
      }, b = [{
        className: "attr",
        variants: [{ begin: "\\w[\\w :\\/.-]*:(?=[ \t]|$)" }, {
          begin: '"\\w[\\w :\\/.-]*":(?=[ \t]|$)'
        }, {
          begin: "'\\w[\\w :\\/.-]*':(?=[ \t]|$)"
        }]
      }, { className: "meta", begin: "^---\\s*$", relevance: 10 }, {
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
          aliases: ["yml", "YAML"], contains: b
        }
    }
  })()); hljs.registerLanguage("bash", (() => {
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
  })()); hljs.registerLanguage("go", (() => {
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
  })()); hljs.registerLanguage("coffeescript", (() => {
   const e = ["as", "in", "of", "if", "for", "while", "finally", "var", "new", "function", "do", "return", "void", "else", "break", "catch", "instanceof", "with", "throw", "case", "default", "try", "switch", "continue", "typeof", "delete", "let", "yield", "const", "class", "debugger", "async", "await", "static", "import", "from", "export", "extends"], n = ["true", "false", "null", "undefined", "NaN", "Infinity"], a = [].concat(["setInterval", "setTimeout", "clearInterval", "clearTimeout", "require", "exports", "eval", "isFinite", "isNaN", "parseFloat", "parseInt", "decodeURI", "decodeURIComponent", "encodeURI", "encodeURIComponent", "escape", "unescape"], ["arguments", "this", "super", "console", "window", "document", "localStorage", "module", "global"], ["Intl", "DataView", "Number", "Math", "Date", "String", "RegExp", "Object", "Function", "Boolean", "Error", "Symbol", "Set", "Map", "WeakSet", "WeakMap", "Proxy", "Reflect", "JSON", "Promise", "Float64Array", "Int16Array", "Int32Array", "Int8Array", "Uint16Array", "Uint32Array", "Float32Array", "Array", "Uint8Array", "Uint8ClampedArray", "ArrayBuffer"], ["EvalError", "InternalError", "RangeError", "ReferenceError", "SyntaxError", "TypeError", "URIError"])
      ; return r => {
        const t = {
          keyword: e.concat(["then", "unless", "until", "loop", "by", "when", "and", "or", "is", "isnt", "not"]).filter((i = ["var", "const", "let", "function", "static"],
            e => !i.includes(e))).join(" "),
          literal: n.concat(["yes", "no", "on", "off"]).join(" "),
          built_in: a.concat(["npm", "print"]).join(" ")
        }; var i
          ; const s = "[A-Za-z$_][0-9A-Za-z$_]*", o = {
            className: "subst", begin: /#\{/, end: /\}/,
            keywords: t
          }, c = [r.BINARY_NUMBER_MODE, r.inherit(r.C_NUMBER_MODE, {
            starts: {
              end: "(\\s*/)?", relevance: 0
            }
          }), {
            className: "string", variants: [{
              begin: /'''/,
              end: /'''/, contains: [r.BACKSLASH_ESCAPE]
            }, {
              begin: /'/, end: /'/,
              contains: [r.BACKSLASH_ESCAPE]
            }, {
              begin: /"""/, end: /"""/,
              contains: [r.BACKSLASH_ESCAPE, o]
            }, {
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
  })()); hljs.registerLanguage("csharp", (() => {
   return e => {
      var n = {
        keyword: ["abstract", "as", "base", "break", "case", "class", "const", "continue", "do", "else", "event", "explicit", "extern", "finally", "fixed", "for", "foreach", "goto", "if", "implicit", "in", "interface", "internal", "is", "lock", "namespace", "new", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "record", "ref", "return", "sealed", "sizeof", "stackalloc", "static", "struct", "switch", "this", "throw", "try", "typeof", "unchecked", "unsafe", "using", "virtual", "void", "volatile", "while"].concat(["add", "alias", "and", "ascending", "async", "await", "by", "descending", "equals", "from", "get", "global", "group", "init", "into", "join", "let", "nameof", "not", "notnull", "on", "or", "orderby", "partial", "remove", "select", "set", "unmanaged", "value|0", "var", "when", "where", "with", "yield"]).join(" "),
        built_in: "bool byte char decimal delegate double dynamic enum float int long nint nuint object sbyte short string ulong unit ushort",
        literal: "default false null true"
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
          })]; var g = {
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
  })()); hljs.registerLanguage("scss", (() => {
   return e => {
      var t = "@[a-z-]+", i = {
        className: "variable", begin: "(\\$[a-zA-Z-][a-zA-Z0-9_-]*)\\b"
      }, r = {
        className: "number", begin: "#[0-9A-Fa-f]+"
      }
        ; return e.CSS_NUMBER_MODE, e.QUOTE_STRING_MODE,
          e.APOS_STRING_MODE, e.C_BLOCK_COMMENT_MODE, {
          name: "SCSS", case_insensitive: !0,
          illegal: "[=/|']", contains: [e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE, {
            className: "selector-id", begin: "#[A-Za-z0-9_-]+", relevance: 0
          }, {
            className: "selector-class", begin: "\\.[A-Za-z0-9_-]+", relevance: 0
          }, {
            className: "selector-attr", begin: "\\[", end: "\\]", illegal: "$"
          }, {
            className: "selector-tag",
            begin: "\\b(a|abbr|acronym|address|area|article|aside|audio|b|base|big|blockquote|body|br|button|canvas|caption|cite|code|col|colgroup|command|datalist|dd|del|details|dfn|div|dl|dt|em|embed|fieldset|figcaption|figure|footer|form|frame|frameset|(h[1-6])|head|header|hgroup|hr|html|i|iframe|img|input|ins|kbd|keygen|label|legend|li|link|map|mark|meta|meter|nav|noframes|noscript|object|ol|optgroup|option|output|p|param|pre|progress|q|rp|rt|ruby|samp|script|section|select|small|span|strike|strong|style|sub|sup|table|tbody|td|textarea|tfoot|th|thead|time|title|tr|tt|ul|var|video)\\b",
            relevance: 0
          }, {
            className: "selector-pseudo",
            begin: ":(visited|valid|root|right|required|read-write|read-only|out-range|optional|only-of-type|only-child|nth-of-type|nth-last-of-type|nth-last-child|nth-child|not|link|left|last-of-type|last-child|lang|invalid|indeterminate|in-range|hover|focus|first-of-type|first-line|first-letter|first-child|first|enabled|empty|disabled|default|checked|before|after|active)"
          }, {
            className: "selector-pseudo",
            begin: "::(after|before|choices|first-letter|first-line|repeat-index|repeat-item|selection|value)"
          }, i, {
            className: "attribute",
            begin: "\\b(src|z-index|word-wrap|word-spacing|word-break|width|widows|white-space|visibility|vertical-align|unicode-bidi|transition-timing-function|transition-property|transition-duration|transition-delay|transition|transform-style|transform-origin|transform|top|text-underline-position|text-transform|text-shadow|text-rendering|text-overflow|text-indent|text-decoration-style|text-decoration-line|text-decoration-color|text-decoration|text-align-last|text-align|tab-size|table-layout|right|resize|quotes|position|pointer-events|perspective-origin|perspective|page-break-inside|page-break-before|page-break-after|padding-top|padding-right|padding-left|padding-bottom|padding|overflow-y|overflow-x|overflow-wrap|overflow|outline-width|outline-style|outline-offset|outline-color|outline|orphans|order|opacity|object-position|object-fit|normal|none|nav-up|nav-right|nav-left|nav-index|nav-down|min-width|min-height|max-width|max-height|mask|marks|margin-top|margin-right|margin-left|margin-bottom|margin|list-style-type|list-style-position|list-style-image|list-style|line-height|letter-spacing|left|justify-content|initial|inherit|ime-mode|image-orientation|image-resolution|image-rendering|icon|hyphens|height|font-weight|font-variant-ligatures|font-variant|font-style|font-stretch|font-size-adjust|font-size|font-language-override|font-kerning|font-feature-settings|font-family|font|float|flex-wrap|flex-shrink|flex-grow|flex-flow|flex-direction|flex-basis|flex|filter|empty-cells|display|direction|cursor|counter-reset|counter-increment|content|column-width|column-span|column-rule-width|column-rule-style|column-rule-color|column-rule|column-gap|column-fill|column-count|columns|color|clip-path|clip|clear|caption-side|break-inside|break-before|break-after|box-sizing|box-shadow|box-decoration-break|bottom|border-width|border-top-width|border-top-style|border-top-right-radius|border-top-left-radius|border-top-color|border-top|border-style|border-spacing|border-right-width|border-right-style|border-right-color|border-right|border-radius|border-left-width|border-left-style|border-left-color|border-left|border-image-width|border-image-source|border-image-slice|border-image-repeat|border-image-outset|border-image|border-color|border-collapse|border-bottom-width|border-bottom-style|border-bottom-right-radius|border-bottom-left-radius|border-bottom-color|border-bottom|border|background-size|background-repeat|background-position|background-origin|background-image|background-color|background-clip|background-attachment|background-blend-mode|background|backface-visibility|auto|animation-timing-function|animation-play-state|animation-name|animation-iteration-count|animation-fill-mode|animation-duration|animation-direction|animation-delay|animation|align-self|align-items|align-content)\\b",
            illegal: "[^\\s]"
          }, {
            begin: "\\b(whitespace|wait|w-resize|visible|vertical-text|vertical-ideographic|uppercase|upper-roman|upper-alpha|underline|transparent|top|thin|thick|text|text-top|text-bottom|tb-rl|table-header-group|table-footer-group|sw-resize|super|strict|static|square|solid|small-caps|separate|se-resize|scroll|s-resize|rtl|row-resize|ridge|right|repeat|repeat-y|repeat-x|relative|progress|pointer|overline|outside|outset|oblique|nowrap|not-allowed|normal|none|nw-resize|no-repeat|no-drop|newspaper|ne-resize|n-resize|move|middle|medium|ltr|lr-tb|lowercase|lower-roman|lower-alpha|loose|list-item|line|line-through|line-edge|lighter|left|keep-all|justify|italic|inter-word|inter-ideograph|inside|inset|inline|inline-block|inherit|inactive|ideograph-space|ideograph-parenthesis|ideograph-numeric|ideograph-alpha|horizontal|hidden|help|hand|groove|fixed|ellipsis|e-resize|double|dotted|distribute|distribute-space|distribute-letter|distribute-all-lines|disc|disabled|default|decimal|dashed|crosshair|collapse|col-resize|circle|char|center|capitalize|break-word|break-all|bottom|both|bolder|bold|block|bidi-override|below|baseline|auto|always|all-scroll|absolute|table|table-cell)\\b"
          }, {
            begin: ":", end: ";",
            contains: [i, r, e.CSS_NUMBER_MODE, e.QUOTE_STRING_MODE, e.APOS_STRING_MODE, {
              className: "meta", begin: "!important"
            }]
          }, {
            begin: "@(page|font-face)", lexemes: t,
            keywords: "@page @font-face"
          }, {
            begin: "@", end: "[{;]", returnBegin: !0,
            keywords: "and or not only", contains: [{
              begin: t, className: "keyword"
            }, i, e.QUOTE_STRING_MODE, e.APOS_STRING_MODE, r, e.CSS_NUMBER_MODE]
          }]
        }
    }
  })()); hljs.registerLanguage("r", (() => {
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
  })()); hljs.registerLanguage("less", (() => {
   return e => {
      var n = "([\\w-]+|@\\{[\\w-]+\\})", a = [], s = [], t = e => ({
        className: "string",
        begin: "~?" + e + ".*?" + e
      }), r = (e, n, a) => ({ className: e, begin: n, relevance: a }), i = {
        begin: "\\(", end: "\\)", contains: s, relevance: 0
      }
        ; s.push(e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE, t("'"), t('"'), e.CSS_NUMBER_MODE, {
          begin: "(url|data-uri)\\(", starts: {
            className: "string", end: "[\\)\\n]",
            excludeEnd: !0
          }
        }, r("number", "#[0-9A-Fa-f]+\\b"), i, r("variable", "@@?[\\w-]+", 10), r("variable", "@\\{[\\w-]+\\}"), r("built_in", "~?`[^`]*?`"), {
          className: "attribute", begin: "[\\w-]+\\s*:", end: ":", returnBegin: !0, excludeEnd: !0
        }, { className: "meta", begin: "!important" }); var c = s.concat({
          begin: /\{/, end: /\}/,
          contains: a
        }), l = {
          beginKeywords: "when", endsWithParent: !0, contains: [{
            beginKeywords: "and not"
          }].concat(s)
        }, g = {
          begin: n + "\\s*:", returnBegin: !0,
          end: "[;}]", relevance: 0, contains: [{
            className: "attribute", begin: n, end: ":",
            excludeEnd: !0, starts: { endsWithParent: !0, illegal: "[<=$]", relevance: 0, contains: s }
          }]
        }, d = {
          className: "keyword",
          begin: "@(import|media|charset|font-face|(-[a-z]+-)?keyframes|supports|document|namespace|page|viewport|host)\\b",
          starts: { end: "[;{}]", returnEnd: !0, contains: s, relevance: 0 }
        }, o = {
          className: "variable", variants: [{ begin: "@[\\w-]+\\s*:", relevance: 15 }, {
            begin: "@[\\w-]+"
          }], starts: { end: "[;}]", returnEnd: !0, contains: c }
        }, b = {
          variants: [{
            begin: "[\\.#:&\\[>]", end: "[;{}]"
          }, { begin: n, end: /\{/ }], returnBegin: !0,
          returnEnd: !0, illegal: "[<='$\"]", relevance: 0,
          contains: [e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE, l, r("keyword", "all\\b"), r("variable", "@\\{[\\w-]+\\}"), r("selector-tag", n + "%?", 0), r("selector-id", "#" + n), r("selector-class", "\\." + n, 0), r("selector-tag", "&", 0), {
            className: "selector-attr", begin: "\\[", end: "\\]"
          }, {
            className: "selector-pseudo",
            begin: /:(:)?[a-zA-Z0-9_\-+()"'.]+/
          }, { begin: "\\(", end: "\\)", contains: c }, {
            begin: "!important"
          }]
        }
        ; return a.push(e.C_LINE_COMMENT_MODE, e.C_BLOCK_COMMENT_MODE, d, o, g, b), {
          name: "Less", case_insensitive: !0, illegal: "[=>'/<($\"]", contains: a
        }
    }
  })()); hljs.registerLanguage("objectivec", (() => {
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
  })()); hljs.registerLanguage("typescript", (() => {
   const e = "[A-Za-z$_][0-9A-Za-z$_]*", n = ["as", "in", "of", "if", "for", "while", "finally", "var", "new", "function", "do", "return", "void", "else", "break", "catch", "instanceof", "with", "throw", "case", "default", "try", "switch", "continue", "typeof", "delete", "let", "yield", "const", "class", "debugger", "async", "await", "static", "import", "from", "export", "extends"], a = ["true", "false", "null", "undefined", "NaN", "Infinity"], s = [].concat(["setInterval", "setTimeout", "clearInterval", "clearTimeout", "require", "exports", "eval", "isFinite", "isNaN", "parseFloat", "parseInt", "decodeURI", "decodeURIComponent", "encodeURI", "encodeURIComponent", "escape", "unescape"], ["arguments", "this", "super", "console", "window", "document", "localStorage", "module", "global"], ["Intl", "DataView", "Number", "Math", "Date", "String", "RegExp", "Object", "Function", "Boolean", "Error", "Symbol", "Set", "Map", "WeakSet", "WeakMap", "Proxy", "Reflect", "JSON", "Promise", "Float64Array", "Int16Array", "Int32Array", "Int8Array", "Uint16Array", "Uint32Array", "Float32Array", "Array", "Uint8Array", "Uint8ClampedArray", "ArrayBuffer"], ["EvalError", "InternalError", "RangeError", "ReferenceError", "SyntaxError", "TypeError", "URIError"])
      ; function t(e) { return i("(?=", e, ")") } function i(...e) {
        return e.map((e => {
          return (n = e) ? "string" == typeof n ? n : n.source : null; var n;
        })).join("")
      } return r => {
        const c = {
          $pattern: e,
          keyword: n.concat(["type", "namespace", "typedef", "interface", "public", "private", "protected", "implements", "declare", "abstract", "readonly"]).join(" "),
          literal: a.join(" "),
          built_in: s.concat(["any", "void", "number", "boolean", "string", "object", "never", "enum"]).join(" ")
        }, o = { className: "meta", begin: "@[A-Za-z$_][0-9A-Za-z$_]*" }, l = (e, n, a) => {
          const s = e.contains.findIndex((e => e.label === n))
            ; if (-1 === s) throw Error("can not find mode to replace"); e.contains.splice(s, 1, a);
        }, b = (r => {
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
            $pattern: e, keyword: n.join(" "),
            literal: a.join(" "), built_in: s.join(" ")
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
              returnEnd: !1, contains: [r.BACKSLASH_ESCAPE, u], subLanguage: "xml"
            }
          }, m = {
            begin: "css`", end: "", starts: {
              end: "`", returnEnd: !1,
              contains: [r.BACKSLASH_ESCAPE, u], subLanguage: "css"
            }
          }, _ = {
            className: "string",
            begin: "`", end: "`", contains: [r.BACKSLASH_ESCAPE, u]
          }, y = {
            className: "comment",
            variants: [r.COMMENT(/\/\*\*(?!\/)/, "\\*/", {
              relevance: 0, contains: [{
                className: "doctag", begin: "@[A-Za-z]+", contains: [{
                  className: "type", begin: "\\{",
                  end: "\\}", relevance: 0
                }, {
                  className: "variable", begin: c + "(?=\\s*(-)|$)",
                  endsParent: !0, relevance: 0
                }, { begin: /(?=[^\n])\s/, relevance: 0 }]
              }]
            }), r.C_BLOCK_COMMENT_MODE, r.C_LINE_COMMENT_MODE]
          }, p = [r.APOS_STRING_MODE, r.QUOTE_STRING_MODE, E, m, _, g, r.REGEXP_MODE]
            ; u.contains = p.concat({
              begin: /\{/, end: /\}/, keywords: l, contains: ["self"].concat(p)
            }); const N = [].concat(y, u.contains), f = N.concat([{
              begin: /\(/, end: /\)/, keywords: l,
              contains: ["self"].concat(N)
            }]), A = {
              className: "params", begin: /\(/, end: /\)/,
              excludeBegin: !0, excludeEnd: !0, keywords: l, contains: f
            }; return {
              name: "Javascript",
              aliases: ["js", "jsx", "mjs", "cjs"], keywords: l, exports: { PARAMS_CONTAINS: f },
              illegal: /#(?![$_A-z])/, contains: [r.SHEBANG({
                label: "shebang", binary: "node",
                relevance: 5
              }), {
                label: "use_strict", className: "meta", relevance: 10,
                begin: /^\s*['"]use (strict|asm)['"]/
              }, r.APOS_STRING_MODE, r.QUOTE_STRING_MODE, E, m, _, y, g, {
                begin: i(/[{,\n]\s*/, t(i(/(((\/\/.*$)|(\/\*(\*[^/]|[^*])*\*\/))\s*)*/, c + "\\s*:"))),
                relevance: 0, contains: [{ className: "attr", begin: c + t("\\s*:"), relevance: 0 }]
              }, {
                begin: "(" + r.RE_STARTERS_RE + "|\\b(case|return|throw)\\b)\\s*",
                keywords: "return throw case", contains: [y, r.REGEXP_MODE, {
                  className: "function",
                  begin: "(\\([^()]*(\\([^()]*(\\([^()]*\\)[^()]*)*\\)[^()]*)*\\)|" + r.UNDERSCORE_IDENT_RE + ")\\s*=>",
                  returnBegin: !0, end: "\\s*=>", contains: [{
                    className: "params", variants: [{
                      begin: r.UNDERSCORE_IDENT_RE, relevance: 0
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
                contains: ["self", r.inherit(r.TITLE_MODE, { begin: c }), A], illegal: /%/
              }, {
                beginKeywords: "while if switch catch for"
              }, {
                className: "function",
                begin: r.UNDERSCORE_IDENT_RE + "\\([^()]*(\\([^()]*(\\([^()]*\\)[^()]*)*\\)[^()]*)*\\)\\s*\\{",
                returnBegin: !0, contains: [A, r.inherit(r.TITLE_MODE, { begin: c })]
              }, {
                variants: [{
                  begin: "\\." + c
                }, { begin: "\\$" + c }], relevance: 0
              }, {
                className: "class",
                beginKeywords: "class", end: /[{;=]/, excludeEnd: !0, illegal: /[:"[\]]/, contains: [{
                  beginKeywords: "extends"
                }, r.UNDERSCORE_TITLE_MODE]
              }, {
                begin: /\b(?=constructor)/,
                end: /[{;]/, excludeEnd: !0, contains: [r.inherit(r.TITLE_MODE, { begin: c }), "self", A]
              }, {
                begin: "(get|set)\\s+(?=" + c + "\\()", end: /\{/, keywords: "get set",
                contains: [r.inherit(r.TITLE_MODE, { begin: c }), { begin: /\(\)/ }, A]
              }, { begin: /\$[(.]/ }]
            }
        })(r)
          ; return Object.assign(b.keywords, c), b.exports.PARAMS_CONTAINS.push(o), b.contains = b.contains.concat([o, {
            beginKeywords: "namespace", end: /\{/, excludeEnd: !0
          }, {
              beginKeywords: "interface",
              end: /\{/, excludeEnd: !0, keywords: "interface extends"
            }]), l(b, "shebang", r.SHEBANG()), l(b, "use_strict", {
              className: "meta", relevance: 10,
              begin: /^\s*['"]use strict['"]/
            }), b.contains.find((e => "function" === e.className)).relevance = 0, Object.assign(b, {
              name: "TypeScript", aliases: ["ts"]
            }), b
      }
  })()); hljs.registerLanguage("plaintext", (() => {
   return t => ({
      name: "Plain text", aliases: ["text", "txt"], disableAutodetect: !0
    })
  })()); hljs.registerLanguage("json", (() => {
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
  })()); hljs.registerLanguage("perl", (() => {
   function e(...e) {
      return e.map((e => {
        return (n = e) ? "string" == typeof n ? n : n.source : null; var n;
      })).join("")
    } return n => {
      const t = /[dualxmsipn]{0,12}/, s = {
        $pattern: /[\w.]+/,
        keyword: "getpwent getservent quotemeta msgrcv scalar kill dbmclose undef lc ma syswrite tr send umask sysopen shmwrite vec qx utime local oct semctl localtime readpipe do return format read sprintf dbmopen pop getpgrp not getpwnam rewinddir qq fileno qw endprotoent wait sethostent bless s|0 opendir continue each sleep endgrent shutdown dump chomp connect getsockname die socketpair close flock exists index shmget sub for endpwent redo lstat msgctl setpgrp abs exit select print ref gethostbyaddr unshift fcntl syscall goto getnetbyaddr join gmtime symlink semget splice x|0 getpeername recv log setsockopt cos last reverse gethostbyname getgrnam study formline endhostent times chop length gethostent getnetent pack getprotoent getservbyname rand mkdir pos chmod y|0 substr endnetent printf next open msgsnd readdir use unlink getsockopt getpriority rindex wantarray hex system getservbyport endservent int chr untie rmdir prototype tell listen fork shmread ucfirst setprotoent else sysseek link getgrgid shmctl waitpid unpack getnetbyname reset chdir grep split require caller lcfirst until warn while values shift telldir getpwuid my getprotobynumber delete and sort uc defined srand accept package seekdir getprotobyname semop our rename seek if q|0 chroot sysread setpwent no crypt getc chown sqrt write setnetent setpriority foreach tie sin msgget map stat getlogin unless elsif truncate exec keys glob tied closedir ioctl socket readlink eval xor readline binmode setservent eof ord bind alarm pipe atan2 getgrent exp time push setgrent gt lt or ne m|0 break given say state when"
      }, r = { className: "subst", begin: "[$@]\\{", end: "\\}", keywords: s }, i = {
        begin: /->\{/,
        end: /\}/
      }, a = {
        variants: [{ begin: /\$\d/ }, {
          begin: e(/[$%@](\^\w\b|#\w+(::\w+)*|\{\w+\}|\w+(::\w*)*)/, "(?![A-Za-z])(?![@$%])")
        }, { begin: /[$%@][^\s\w{]/, relevance: 0 }]
      }, o = [n.BACKSLASH_ESCAPE, r, a], c = [a, n.HASH_COMMENT_MODE, n.COMMENT(/^=\w/, /=cut/, {
        endsWithParent: !0
      }), i, {
          className: "string", contains: o, variants: [{
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
            contains: [n.BACKSLASH_ESCAPE]
          }, { begin: '"', end: '"' }, {
            begin: "`", end: "`",
            contains: [n.BACKSLASH_ESCAPE]
          }, { begin: /\{\w+\}/, contains: [], relevance: 0 }, {
            begin: "-?\\w+\\s*=>", contains: [], relevance: 0
          }]
        }, {
          className: "number",
          begin: "(\\b0[0-7_]+)|(\\b0x[0-9a-fA-F_]+)|(\\b[1-9][0-9_]*(\\.[0-9_]+)?)|[0_]\\b",
          relevance: 0
        }, {
          begin: "(\\/\\/|" + n.RE_STARTERS_RE + "|\\b(split|return|print|reverse|grep)\\b)\\s*",
          keywords: "split return print reverse grep", relevance: 0,
          contains: [n.HASH_COMMENT_MODE, {
            className: "regexp",
            begin: e(/(s|tr|y)/, /\//, /(\\.|[^\\\/])*/, /\//, /(\\.|[^\\\/])*/, /\//, t),
            relevance: 10
          }, {
            className: "regexp", begin: /(m|qr)?\//, end: e(/\//, t),
            contains: [n.BACKSLASH_ESCAPE], relevance: 0
          }]
        }, {
          className: "function",
          beginKeywords: "sub", end: "(\\s*\\(.*?\\))?[;{]", excludeEnd: !0, relevance: 5,
          contains: [n.TITLE_MODE]
        }, { begin: "-\\w\\b", relevance: 0 }, {
          begin: "^__DATA__$",
          end: "^__END__$", subLanguage: "mojolicious", contains: [{
            begin: "^@@.*", end: "$",
            className: "comment"
          }]
        }]; return r.contains = c, i.contains = c, {
          name: "Perl",
          aliases: ["pl", "pm"], keywords: s, contains: c
        }
    }
  })()); hljs.registerLanguage("shell", (() => {
   return s => ({
      name: "Shell Session", aliases: ["console"], contains: [{
        className: "meta",
        begin: /^\s{0,3}[/~\w\d[\]()@-]*[>%$#]/, starts: {
          end: /[^\\](?=\s*$)/,
          subLanguage: "bash"
        }
      }]
    })
  })()); hljs.registerLanguage("lua", (() => {
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
  })()); hljs.registerLanguage("makefile", (() => {
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
  })()); hljs.registerLanguage("rust", (() => {
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
  })()); hljs.registerLanguage("php", (() => {
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
      }, c = {
        variants: [e.BINARY_NUMBER_MODE, e.C_NUMBER_MODE]
      }, s = {
        keyword: "__CLASS__ __DIR__ __FILE__ __FUNCTION__ __LINE__ __METHOD__ __NAMESPACE__ __TRAIT__ die echo exit include include_once print require require_once array abstract and as binary bool boolean break callable case catch class clone const continue declare default do double else elseif empty enddeclare endfor endforeach endif endswitch endwhile eval extends final finally float for foreach from global goto if implements instanceof insteadof int integer interface isset iterable list match|0 new object or private protected public real return string switch throw trait try unset use var void while xor yield",
        literal: "false null true",
        built_in: "Error|0 AppendIterator ArgumentCountError ArithmeticError ArrayIterator ArrayObject AssertionError BadFunctionCallException BadMethodCallException CachingIterator CallbackFilterIterator CompileError Countable DirectoryIterator DivisionByZeroError DomainException EmptyIterator ErrorException Exception FilesystemIterator FilterIterator GlobIterator InfiniteIterator InvalidArgumentException IteratorIterator LengthException LimitIterator LogicException MultipleIterator NoRewindIterator OutOfBoundsException OutOfRangeException OuterIterator OverflowException ParentIterator ParseError RangeException RecursiveArrayIterator RecursiveCachingIterator RecursiveCallbackFilterIterator RecursiveDirectoryIterator RecursiveFilterIterator RecursiveIterator RecursiveIteratorIterator RecursiveRegexIterator RecursiveTreeIterator RegexIterator RuntimeException SeekableIterator SplDoublyLinkedList SplFileInfo SplFileObject SplFixedArray SplHeap SplMaxHeap SplMinHeap SplObjectStorage SplObserver SplObserver SplPriorityQueue SplQueue SplStack SplSubject SplSubject SplTempFileObject TypeError UnderflowException UnexpectedValueException ArrayAccess Closure Generator Iterator IteratorAggregate Serializable Throwable Traversable WeakReference Directory __PHP_Incomplete_Class parent php_user_filter self static stdClass"
      }; return {
        aliases: ["php", "php3", "php4", "php5", "php6", "php7", "php8"],
        case_insensitive: !0, keywords: s,
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
          illegal: "[$%\\[]", contains: [e.UNDERSCORE_TITLE_MODE, { begin: "=>" }, {
            className: "params", begin: "\\(", end: "\\)", excludeBegin: !0, excludeEnd: !0,
            keywords: s, contains: ["self", r, e.C_BLOCK_COMMENT_MODE, l, c]
          }]
        }, {
          className: "class",
          beginKeywords: "class interface", relevance: 0, end: /\{/, excludeEnd: !0,
          illegal: /[:($"]/, contains: [{
            beginKeywords: "extends implements"
          }, e.UNDERSCORE_TITLE_MODE]
        }, {
          beginKeywords: "namespace", relevance: 0, end: ";",
          illegal: /[.']/, contains: [e.UNDERSCORE_TITLE_MODE]
        }, {
          beginKeywords: "use",
          relevance: 0, end: ";", contains: [e.UNDERSCORE_TITLE_MODE]
        }, l, c]
      }
    }
  })()); hljs.registerLanguage("vbnet", (() => {
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
  })()); hljs.registerLanguage("c", (() => {
   function e(e) {
      return ((...e) => e.map((e => (e => e ? "string" == typeof e ? e : e.source : null)(e))).join(""))("(", e, ")?")
    } return t => {
      const n = (t => {
        const n = t.COMMENT("//", "$", {
          contains: [{ begin: /\\\n/ }]
        }), r = "[a-zA-Z_]\\w*::", a = "(decltype\\(auto\\)|" + e(r) + "[a-zA-Z_]\\w*" + e("<[^<>]+>") + ")", s = {
          className: "keyword", begin: "\\b[a-z\\d_]*_t\\b"
        }, i = {
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
          }, contains: [{ begin: /\\\n/, relevance: 0 }, t.inherit(i, { className: "meta-string" }), {
            className: "meta-string", begin: /<.*?>/, end: /$/, illegal: "\\n"
          }, n, t.C_BLOCK_COMMENT_MODE]
        }, l = {
          className: "title", begin: e(r) + t.IDENT_RE,
          relevance: 0
        }, d = e(r) + t.IDENT_RE + "\\s*\\(", u = {
          keyword: "int float while private char char8_t char16_t char32_t catch import module export virtual operator sizeof dynamic_cast|10 typedef const_cast|10 const for static_cast|10 union namespace unsigned long volatile static protected bool template mutable if public friend do goto auto void enum else break extern using asm case typeid wchar_t short reinterpret_cast|10 default double register explicit signed typename try this switch continue inline delete alignas alignof constexpr consteval constinit decltype concept co_await co_return co_yield requires noexcept static_assert thread_local restrict final override atomic_bool atomic_char atomic_schar atomic_uchar atomic_short atomic_ushort atomic_int atomic_uint atomic_long atomic_ulong atomic_llong atomic_ullong new throw return and and_eq bitand bitor compl not not_eq or or_eq xor xor_eq",
          built_in: "std string wstring cin cout cerr clog stdin stdout stderr stringstream istringstream ostringstream auto_ptr deque list queue stack vector map set pair bitset multiset multimap unordered_set unordered_map unordered_multiset unordered_multimap priority_queue make_pair array shared_ptr abort terminate abs acos asin atan2 atan calloc ceil cosh cos exit exp fabs floor fmod fprintf fputs free frexp fscanf future isalnum isalpha iscntrl isdigit isgraph islower isprint ispunct isspace isupper isxdigit tolower toupper labs ldexp log10 log malloc realloc memchr memcmp memcpy memset modf pow printf putchar puts scanf sinh sin snprintf sprintf sqrt sscanf strcat strchr strcmp strcpy strcspn strlen strncat strncmp strncpy strpbrk strrchr strspn strstr tanh tan vfprintf vprintf vsprintf endl initializer_list unique_ptr _Bool complex _Complex imaginary _Imaginary",
          literal: "true false nullptr NULL"
        }, m = [c, s, n, t.C_BLOCK_COMMENT_MODE, o, i], p = {
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
            end: /\)/, keywords: u, relevance: 0, contains: [n, t.C_BLOCK_COMMENT_MODE, i, o, s, {
              begin: /\(/, end: /\)/, keywords: u, relevance: 0,
              contains: ["self", n, t.C_BLOCK_COMMENT_MODE, i, o, s]
            }]
          }, s, n, t.C_BLOCK_COMMENT_MODE, c]
        }; return {
          aliases: ["c", "cc", "h", "c++", "h++", "hpp", "hh", "hxx", "cxx"], keywords: u,
          disableAutodetect: !0, illegal: "</", contains: [].concat(p, _, m, [c, {
            begin: "\\b(deque|list|queue|priority_queue|pair|stack|vector|map|set|bitset|multiset|multimap|unordered_map|unordered_set|unordered_multiset|unordered_multimap|array)\\s*<",
            end: ">", keywords: u, contains: ["self", s]
          }, { begin: t.IDENT_RE + "::", keywords: u }, {
              className: "class", beginKeywords: "enum class struct union", end: /[{;:<>=]/,
              contains: [{ beginKeywords: "final class struct" }, t.TITLE_MODE]
            }]), exports: {
              preprocessor: c, strings: i, keywords: u
            }
        }
      })(t)
        ; return n.name = "C", n.aliases = ["c", "h"], n
    }
  })()); hljs.registerLanguage("css", (() => {
   return e => {
      var n = "[a-zA-Z-][a-zA-Z0-9_-]*", a = {
        begin: /([*]\s?)?(?:[A-Z_.\-\\]+|--[a-zA-Z0-9_-]+)\s*(\/\*\*\/)?:/,
        returnBegin: !0, end: ";", endsWithParent: !0, contains: [{
          className: "attribute",
          begin: /\S/, end: ":", excludeEnd: !0, starts: {
            endsWithParent: !0, excludeEnd: !0,
            contains: [{
              begin: /[\w-]+\(/, returnBegin: !0, contains: [{
                className: "built_in",
                begin: /[\w-]+/
              }, {
                begin: /\(/, end: /\)/,
                contains: [e.APOS_STRING_MODE, e.QUOTE_STRING_MODE, e.CSS_NUMBER_MODE]
              }]
            }, e.CSS_NUMBER_MODE, e.QUOTE_STRING_MODE, e.APOS_STRING_MODE, e.C_BLOCK_COMMENT_MODE, {
              className: "number", begin: "#[0-9A-Fa-f]+"
            }, { className: "meta", begin: "!important" }]
          }
        }]
      }; return {
        name: "CSS", case_insensitive: !0, illegal: /[=|'\$]/,
        contains: [e.C_BLOCK_COMMENT_MODE, {
          className: "selector-id",
          begin: /#[A-Za-z0-9_-]+/
        }, { className: "selector-class", begin: "\\." + n }, {
          className: "selector-attr", begin: /\[/, end: /\]/, illegal: "$",
          contains: [e.APOS_STRING_MODE, e.QUOTE_STRING_MODE]
        }, {
          className: "selector-pseudo",
          begin: /:(:)?[a-zA-Z0-9_+()"'.-]+/
        }, {
          begin: "@(page|font-face)",
          lexemes: "@[a-z-]+", keywords: "@page @font-face"
        }, {
          begin: "@", end: "[{;]",
          illegal: /:/, returnBegin: !0, contains: [{
            className: "keyword",
            begin: /@-?\w[\w]*(-\w+)*/
          }, {
            begin: /\s/, endsWithParent: !0, excludeEnd: !0,
            relevance: 0, keywords: "and or not only", contains: [{
              begin: /[a-z-]+:/,
              className: "attribute"
            }, e.APOS_STRING_MODE, e.QUOTE_STRING_MODE, e.CSS_NUMBER_MODE]
          }]
        }, { className: "selector-tag", begin: n, relevance: 0 }, {
          begin: /\{/, end: /\}/,
          illegal: /\S/, contains: [e.C_BLOCK_COMMENT_MODE, { begin: /;/ }, a]
        }]
      }
    }
  })()); hljs.registerLanguage("sql", (() => {
   function e(e) {
      return e ? "string" == typeof e ? e : e.source : null
    } function r(...r) {
      return r.map((r => e(r))).join("")
    } function t(...r) {
      return "(" + r.map((r => e(r))).join("|") + ")"
    } return e => {
      const n = e.COMMENT("--", "$"), a = ["true", "false", "unknown"], i = ["bigint", "binary", "blob", "boolean", "char", "character", "clob", "date", "dec", "decfloat", "decimal", "float", "int", "integer", "interval", "nchar", "nclob", "national", "numeric", "real", "row", "smallint", "time", "timestamp", "varchar", "varying", "varbinary"], s = ["abs", "acos", "array_agg", "asin", "atan", "avg", "cast", "ceil", "ceiling", "coalesce", "corr", "cos", "cosh", "count", "covar_pop", "covar_samp", "cume_dist", "dense_rank", "deref", "element", "exp", "extract", "first_value", "floor", "json_array", "json_arrayagg", "json_exists", "json_object", "json_objectagg", "json_query", "json_table", "json_table_primitive", "json_value", "lag", "last_value", "lead", "listagg", "ln", "log", "log10", "lower", "max", "min", "mod", "nth_value", "ntile", "nullif", "percent_rank", "percentile_cont", "percentile_disc", "position", "position_regex", "power", "rank", "regr_avgx", "regr_avgy", "regr_count", "regr_intercept", "regr_r2", "regr_slope", "regr_sxx", "regr_sxy", "regr_syy", "row_number", "sin", "sinh", "sqrt", "stddev_pop", "stddev_samp", "substring", "substring_regex", "sum", "tan", "tanh", "translate", "translate_regex", "treat", "trim", "trim_array", "unnest", "upper", "value_of", "var_pop", "var_samp", "width_bucket"], o = ["create table", "insert into", "primary key", "foreign key", "not null", "alter table", "add constraint", "grouping sets", "on overflow", "character set", "respect nulls", "ignore nulls", "nulls first", "nulls last", "depth first", "breadth first"], c = s, l = ["abs", "acos", "all", "allocate", "alter", "and", "any", "are", "array", "array_agg", "array_max_cardinality", "as", "asensitive", "asin", "asymmetric", "at", "atan", "atomic", "authorization", "avg", "begin", "begin_frame", "begin_partition", "between", "bigint", "binary", "blob", "boolean", "both", "by", "call", "called", "cardinality", "cascaded", "case", "cast", "ceil", "ceiling", "char", "char_length", "character", "character_length", "check", "classifier", "clob", "close", "coalesce", "collate", "collect", "column", "commit", "condition", "connect", "constraint", "contains", "convert", "copy", "corr", "corresponding", "cos", "cosh", "count", "covar_pop", "covar_samp", "create", "cross", "cube", "cume_dist", "current", "current_catalog", "current_date", "current_default_transform_group", "current_path", "current_role", "current_row", "current_schema", "current_time", "current_timestamp", "current_path", "current_role", "current_transform_group_for_type", "current_user", "cursor", "cycle", "date", "day", "deallocate", "dec", "decimal", "decfloat", "declare", "default", "define", "delete", "dense_rank", "deref", "describe", "deterministic", "disconnect", "distinct", "double", "drop", "dynamic", "each", "element", "else", "empty", "end", "end_frame", "end_partition", "end-exec", "equals", "escape", "every", "except", "exec", "execute", "exists", "exp", "external", "extract", "false", "fetch", "filter", "first_value", "float", "floor", "for", "foreign", "frame_row", "free", "from", "full", "function", "fusion", "get", "global", "grant", "group", "grouping", "groups", "having", "hold", "hour", "identity", "in", "indicator", "initial", "inner", "inout", "insensitive", "insert", "int", "integer", "intersect", "intersection", "interval", "into", "is", "join", "json_array", "json_arrayagg", "json_exists", "json_object", "json_objectagg", "json_query", "json_table", "json_table_primitive", "json_value", "lag", "language", "large", "last_value", "lateral", "lead", "leading", "left", "like", "like_regex", "listagg", "ln", "local", "localtime", "localtimestamp", "log", "log10", "lower", "match", "match_number", "match_recognize", "matches", "max", "member", "merge", "method", "min", "minute", "mod", "modifies", "module", "month", "multiset", "national", "natural", "nchar", "nclob", "new", "no", "none", "normalize", "not", "nth_value", "ntile", "null", "nullif", "numeric", "octet_length", "occurrences_regex", "of", "offset", "old", "omit", "on", "one", "only", "open", "or", "order", "out", "outer", "over", "overlaps", "overlay", "parameter", "partition", "pattern", "per", "percent", "percent_rank", "percentile_cont", "percentile_disc", "period", "portion", "position", "position_regex", "power", "precedes", "precision", "prepare", "primary", "procedure", "ptf", "range", "rank", "reads", "real", "recursive", "ref", "references", "referencing", "regr_avgx", "regr_avgy", "regr_count", "regr_intercept", "regr_r2", "regr_slope", "regr_sxx", "regr_sxy", "regr_syy", "release", "result", "return", "returns", "revoke", "right", "rollback", "rollup", "row", "row_number", "rows", "running", "savepoint", "scope", "scroll", "search", "second", "seek", "select", "sensitive", "session_user", "set", "show", "similar", "sin", "sinh", "skip", "smallint", "some", "specific", "specifictype", "sql", "sqlexception", "sqlstate", "sqlwarning", "sqrt", "start", "static", "stddev_pop", "stddev_samp", "submultiset", "subset", "substring", "substring_regex", "succeeds", "sum", "symmetric", "system", "system_time", "system_user", "table", "tablesample", "tan", "tanh", "then", "time", "timestamp", "timezone_hour", "timezone_minute", "to", "trailing", "translate", "translate_regex", "translation", "treat", "trigger", "trim", "trim_array", "true", "truncate", "uescape", "union", "unique", "unknown", "unnest", "update   ", "upper", "user", "using", "value", "values", "value_of", "var_pop", "var_samp", "varbinary", "varchar", "varying", "versioning", "when", "whenever", "where", "width_bucket", "window", "with", "within", "without", "year", "add", "asc", "collation", "desc", "final", "first", "last", "view"].filter((e => !s.includes(e))), u = {
        begin: r(/\b/, t(...c), /\s*\(/), keywords: { built_in: c.join(" ") }
      }; return {
        name: "SQL", case_insensitive: !0, illegal: /[{}]|<\//, keywords: {
          $pattern: /\b[\w\.]+/, keyword: ((e, { exceptions: r, when: t } = {}) => {
            const n = t
              ; return r = r || [], e.map((e => e.match(/\|\d+$/) || r.includes(e) ? e : n(e) ? e + "|0" : e))
          })(l, { when: e => e.length < 3 }).join(" "), literal: a.join(" "), type: i.join(" "),
          built_in: "current_catalog current_date current_default_transform_group current_path current_role current_schema current_transform_group_for_type current_user session_user system_time system_user current_time localtime current_timestamp localtimestamp"
        }, contains: [{
          begin: t(...o), keywords: {
            $pattern: /[\w\.]+/,
            keyword: l.concat(o).join(" "), literal: a.join(" "), type: i.join(" ")
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
  })()); hljs.registerLanguage("ini", (() => {
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
  })()); hljs.registerLanguage("php-template", (() => {
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
  })()); hljs.registerLanguage("javascript", (() => {
   const e = "[A-Za-z$_][0-9A-Za-z$_]*", n = ["as", "in", "of", "if", "for", "while", "finally", "var", "new", "function", "do", "return", "void", "else", "break", "catch", "instanceof", "with", "throw", "case", "default", "try", "switch", "continue", "typeof", "delete", "let", "yield", "const", "class", "debugger", "async", "await", "static", "import", "from", "export", "extends"], a = ["true", "false", "null", "undefined", "NaN", "Infinity"], s = [].concat(["setInterval", "setTimeout", "clearInterval", "clearTimeout", "require", "exports", "eval", "isFinite", "isNaN", "parseFloat", "parseInt", "decodeURI", "decodeURIComponent", "encodeURI", "encodeURIComponent", "escape", "unescape"], ["arguments", "this", "super", "console", "window", "document", "localStorage", "module", "global"], ["Intl", "DataView", "Number", "Math", "Date", "String", "RegExp", "Object", "Function", "Boolean", "Error", "Symbol", "Set", "Map", "WeakSet", "WeakMap", "Proxy", "Reflect", "JSON", "Promise", "Float64Array", "Int16Array", "Int32Array", "Int8Array", "Uint16Array", "Uint32Array", "Float32Array", "Array", "Uint8Array", "Uint8ClampedArray", "ArrayBuffer"], ["EvalError", "InternalError", "RangeError", "ReferenceError", "SyntaxError", "TypeError", "URIError"])
      ; function r(e) { return i("(?=", e, ")") } function i(...e) {
        return e.map((e => {
          return (n = e) ? "string" == typeof n ? n : n.source : null; var n;
        })).join("")
      } return t => {
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
          $pattern: e, keyword: n.join(" "),
          literal: a.join(" "), built_in: s.join(" ")
        }, b = "\\.([0-9](_?[0-9])*)", g = "0|[1-9](_?[0-9])*|0[0-7]*[89][0-9]*", d = {
          className: "number", variants: [{
            begin: `(\\b(${g})((${b})|\\.)?|(${b}))[eE][+-]?([0-9](_?[0-9])*)\\b`
          }, {
            begin: `\\b(${g})\\b((${b})\\b|\\.)?|(${b})\\b`
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
            returnEnd: !1, contains: [t.BACKSLASH_ESCAPE, E], subLanguage: "xml"
          }
        }, _ = {
          begin: "css`", end: "", starts: {
            end: "`", returnEnd: !1,
            contains: [t.BACKSLASH_ESCAPE, E], subLanguage: "css"
          }
        }, m = {
          className: "string",
          begin: "`", end: "`", contains: [t.BACKSLASH_ESCAPE, E]
        }, N = {
          className: "comment",
          variants: [t.COMMENT(/\/\*\*(?!\/)/, "\\*/", {
            relevance: 0, contains: [{
              className: "doctag", begin: "@[A-Za-z]+", contains: [{
                className: "type", begin: "\\{",
                end: "\\}", relevance: 0
              }, {
                className: "variable", begin: c + "(?=\\s*(-)|$)",
                endsParent: !0, relevance: 0
              }, { begin: /(?=[^\n])\s/, relevance: 0 }]
            }]
          }), t.C_BLOCK_COMMENT_MODE, t.C_LINE_COMMENT_MODE]
        }, y = [t.APOS_STRING_MODE, t.QUOTE_STRING_MODE, u, _, m, d, t.REGEXP_MODE]
          ; E.contains = y.concat({
            begin: /\{/, end: /\}/, keywords: l, contains: ["self"].concat(y)
          }); const f = [].concat(N, E.contains), A = f.concat([{
            begin: /\(/, end: /\)/, keywords: l,
            contains: ["self"].concat(f)
          }]), p = {
            className: "params", begin: /\(/, end: /\)/,
            excludeBegin: !0, excludeEnd: !0, keywords: l, contains: A
          }; return {
            name: "Javascript",
            aliases: ["js", "jsx", "mjs", "cjs"], keywords: l, exports: { PARAMS_CONTAINS: A },
            illegal: /#(?![$_A-z])/, contains: [t.SHEBANG({
              label: "shebang", binary: "node",
              relevance: 5
            }), {
              label: "use_strict", className: "meta", relevance: 10,
              begin: /^\s*['"]use (strict|asm)['"]/
            }, t.APOS_STRING_MODE, t.QUOTE_STRING_MODE, u, _, m, N, d, {
              begin: i(/[{,\n]\s*/, r(i(/(((\/\/.*$)|(\/\*(\*[^/]|[^*])*\*\/))\s*)*/, c + "\\s*:"))),
              relevance: 0, contains: [{ className: "attr", begin: c + r("\\s*:"), relevance: 0 }]
            }, {
              begin: "(" + t.RE_STARTERS_RE + "|\\b(case|return|throw)\\b)\\s*",
              keywords: "return throw case", contains: [N, t.REGEXP_MODE, {
                className: "function",
                begin: "(\\([^()]*(\\([^()]*(\\([^()]*\\)[^()]*)*\\)[^()]*)*\\)|" + t.UNDERSCORE_IDENT_RE + ")\\s*=>",
                returnBegin: !0, end: "\\s*=>", contains: [{
                  className: "params", variants: [{
                    begin: t.UNDERSCORE_IDENT_RE, relevance: 0
                  }, {
                    className: null, begin: /\(\s*\)/, skip: !0
                  }, { begin: /\(/, end: /\)/, excludeBegin: !0, excludeEnd: !0, keywords: l, contains: A }]
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
              contains: ["self", t.inherit(t.TITLE_MODE, { begin: c }), p], illegal: /%/
            }, {
              beginKeywords: "while if switch catch for"
            }, {
              className: "function",
              begin: t.UNDERSCORE_IDENT_RE + "\\([^()]*(\\([^()]*(\\([^()]*\\)[^()]*)*\\)[^()]*)*\\)\\s*\\{",
              returnBegin: !0, contains: [p, t.inherit(t.TITLE_MODE, { begin: c })]
            }, {
              variants: [{
                begin: "\\." + c
              }, { begin: "\\$" + c }], relevance: 0
            }, {
              className: "class",
              beginKeywords: "class", end: /[{;=]/, excludeEnd: !0, illegal: /[:"[\]]/, contains: [{
                beginKeywords: "extends"
              }, t.UNDERSCORE_TITLE_MODE]
            }, {
              begin: /\b(?=constructor)/,
              end: /[{;]/, excludeEnd: !0, contains: [t.inherit(t.TITLE_MODE, { begin: c }), "self", p]
            }, {
              begin: "(get|set)\\s+(?=" + c + "\\()", end: /\{/, keywords: "get set",
              contains: [t.inherit(t.TITLE_MODE, { begin: c }), { begin: /\(\)/ }, p]
            }, { begin: /\$[(.]/ }]
          }
      }
  })());


  // enable highlight
  hljs.initHighlightingOnLoad();

  // get the newsletter form elements
  const form = document.getElementById("newsletter");
  const form_email = document.getElementById("newsletter_email");
  const form_status = document.getElementById("newsletter_status");
  const newsletterSucessMsg = form_status.dataset.success, newsletterErrorMsg = form_status.dataset.error;

  // Success, Loading and Error functions
  function successNewsletter() {
    form_status.innerHTML = `<div class="newsletter-msg bg-success"><div class="m-auto"> ${newsletterSucessMsg} </div></div>`;
    setTimeout(resetNewsletter, 2000);
  }
  function loadingNewsletter() {
    form_status.innerHTML = '<div class="newsletter-msg"><div class="m-auto spinner-border" role="status"></div></div>';
  }
  function errorNewsletter(msg) {
    form_status.innerHTML = `<div class="newsletter-msg bg-danger"><div class="m-auto">${newsletterErrorMsg} <br> ${msg}</div></div>`;
  }
  function resetNewsletter() {
    form.reset();
    form_status.innerHTML = "";
  }

  function subscribeNewsletter(url, data) {
    var options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data)
    };
    fetch(url, options)
      .then((response) => {
        if (response.status == 200) {
          return response.json();
        } else {
          throw new Error('The Newsletter is not working!');
        }
      })
      .then(() => {
        successNewsletter();
      })
      .catch((err) => {
        errorNewsletter(err);
      });
  }

  form.addEventListener("submit", function (e) {
    e.preventDefault();
    loadingNewsletter();
    var subscriber_data = {
      Email: form_email.value,
      Ip: "unknown",
      Country: "unknown",
      Region: "unknown"
    };
    fetch('https://ipapi.co/json/')
      .then((response) => {
        if (response.status == 200) {
          return response.json();
        } else {
          throw new Error('Not sure where you are!');
        }
      })
      .then((loc) => {
        subscriber_data.Ip = loc.ip;
        subscriber_data.Country = loc.country_name;
        subscriber_data.Region = loc.region;
        subscribeNewsletter(form.action, subscriber_data);
      })
      .catch((err) => {
        subscribeNewsletter(form.action, subscriber_data);
      });
  });


  // search modal auto focus
  var myModal = document.getElementById('searchModal');
  var myInput = document.getElementById('searchFormInput');
  if (myModal) {
    myModal.addEventListener('shown.bs.modal', function () {
      myInput.focus();
    });
  }

})();
